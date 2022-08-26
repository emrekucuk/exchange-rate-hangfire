using Domain.Entites;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hangfire.RecurringJobs
{
    public class CurrencyExchangeJob
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        public CurrencyExchangeJob(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
        }

        public class CurrencyValue
        {
            public float Value { get; set; }
        }
        public class ApiModel
        {
            public string Result { get; set; }
            public Dictionary<string, float> Data { get; set; }
        }

        public async Task UpdateCurrencyExchange()
        {
            var currencies = await _applicationDbContext.Currencies.ToListAsync();
            var currencyExchanges = await _applicationDbContext.CurrencyExchanges.Where(c => c.Date.Day == DateTime.Today.Day).ToListAsync();

            foreach (var currency in currencies)
            {
                // var currencyValue = await GetFromFreeCurrencyApi(currency.Code);
                var currencyValue = await GetFromExchangeRateApi(currency.Code);

                var matchedCurrency = currencyExchanges.FirstOrDefault(c => c.CurrencyId == currency.Id);
                if (matchedCurrency == null)
                {
                    matchedCurrency = new CurrencyExchange()
                    {
                        CurrencyId = currency.Id,
                        Date = DateTime.UtcNow,
                        Value = currencyValue.Value,
                    };
                    _applicationDbContext.CurrencyExchanges.Add(matchedCurrency);
                }
                else
                {
                    matchedCurrency.Date = DateTime.UtcNow;
                    matchedCurrency.Value = currencyValue.Value;
                    _applicationDbContext.CurrencyExchanges.Update(matchedCurrency);
                }

                await _applicationDbContext.SaveChangesAsync();
            }
        }

        static async Task<CurrencyValue> GetFromExchangeRateApi(string currencyCode)
        {
            var currencyValue = new CurrencyValue();
            using (var httpClient = new HttpClient())
            {
                currencyValue.Value = ConvertStringTo4DigitFloat(JsonConvert.DeserializeObject<ApiModel>(
                    await httpClient.GetStringAsync($"https://api.exchangerate.host/convert?from={currencyCode.ToLower()}&to=TRY"))?.Result);
            }

            System.Console.WriteLine($"{currencyCode} Value: {currencyValue.Value}");
            System.Console.WriteLine("-------------------------------------------------");
            return currencyValue;

        }

        public async Task<CurrencyValue> GetFromFreeCurrencyApi(string currencyCode)
        {
            var currencyValue = new CurrencyValue();
            var apiKey = _configuration.GetValue<string>("FreeCurrencyApiKey");
            using (var httpClient = new HttpClient())
            {
                var getCurrency = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel>(
                    await httpClient.GetStringAsync($"https://freecurrencyapi.net/api/v2/latest?apikey={apiKey}&base_currency=TRY")).Data;
                float value;

                if (getCurrency.TryGetValue(currencyCode, out value))
                    currencyValue.Value = (float)Math.Round(1 / value, 4);
            }

            System.Console.WriteLine($"{currencyCode} Value : {currencyValue.Value}");
            System.Console.WriteLine("-------------------------------------------------");
            return currencyValue;
        }
        static float ConvertStringTo4DigitFloat(string value)
        {
            double newValue = Double.Parse(value);
            return float.Parse($"{newValue:0.0000}");
        }
    }
}