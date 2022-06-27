using Newtonsoft.Json;

namespace Hangfire.RecurringJobs
{
    public class CurrencyExchangeJob
    {
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
            List<string> currencies = new List<string>();
            currencies.Add("USD");
            currencies.Add("EUR");
            currencies.Add("INR");
            currencies.Add("CAD");
            currencies.Add("AUD");

            foreach (var currency in currencies)
            {
                await GetFromExchangeRateApi(currency);
                // await GetFromFreeCurrencyApi(currency);
            }

        }

        static async Task GetFromExchangeRateApi(string currency)
        {
            var currencyValue = new CurrencyValue();
            using (var httpClient = new HttpClient())
            {
                currencyValue.Value = ConvertStringTo4DigitFloat(JsonConvert.DeserializeObject<ApiModel>(
                    await httpClient.GetStringAsync("https://api.exchangerate.host/convert?from=" + currency.ToLower() + "&to=TRY"))?.Result.Replace(".", ","));
            }

            System.Console.WriteLine(currency + " Value : " + currencyValue.Value);
            System.Console.WriteLine("-------------------------------------------------");

        }

        static float ConvertStringTo4DigitFloat(string value)
        {
            double newValue = Double.Parse(value);
            return float.Parse($"{newValue:0.0000}");
        }

        public static async Task GetFromFreeCurrencyApi(string currency)
        {
            var value = new CurrencyValue();
            var apiKey = "c33a4390-6d40-11ec-9291-4df0a4bd7c60";
            using (var httpClient = new HttpClient())
            {
                var getCurrency = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel>(
                    await httpClient.GetStringAsync(
                        "https://freecurrencyapi.net/api/v2/latest?apikey=" + apiKey + "&base_currency=TRY")).Data;
                float deger;

                if (getCurrency.TryGetValue(currency, out deger))
                    value.Value = (float)Math.Round(1 / deger, 4);

                System.Console.WriteLine(currency + " Value : " + value.Value);
                System.Console.WriteLine("-------------------------------------------------");

            }
        }
    }
}