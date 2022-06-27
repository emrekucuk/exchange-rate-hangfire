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
            public Dictionary<string, float> Data { get; set; }
        }

        public async Task UpdateCurrencyExchange()
        {
            List<string> currencies = new List<string>();
            currencies.Add("USD");
            currencies.Add("EUR");

            foreach (var currency in currencies)
            {
                await GetFromFreeCurrencyApi(currency);
            }

        }

        public static async Task GetFromFreeCurrencyApi(string currency)
        {
            var degerler = new CurrencyValue();
            var apiKey = "c33a4390-6d40-11ec-9291-4df0a4bd7c60";
            using (var httpClient = new HttpClient())
            {
                var getCurrency = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel>(
                    await httpClient.GetStringAsync(
                        "https://freecurrencyapi.net/api/v2/latest?apikey=" + apiKey + "&base_currency=TRY")).Data;
                float deger;

                if (getCurrency.TryGetValue(currency, out deger))
                    degerler.Value = (float)Math.Round(1 / deger, 4);

                System.Console.WriteLine(currency + " Value : " + degerler.Value);
                System.Console.WriteLine("-------------------------------------------------");

            }
        }
    }
}