namespace Hangfire.RecurringJobs.KurOperation
{
    public class KurManager
    {
        public class Degerler
        {
            public float Alis { get; set; }
            public float Satis { get; set; }
        }
        public class ApiModel
        {
            public string? Date { get; set; }
            public string? Try { get; set; }
            public string? Result { get; set; }
            public Dictionary<string, float>? Data { get; set; }
        }

        public static async Task GetFromFreeCurrencyApi(string currency)
        {
            var degerler = new Degerler();
            var apiKey = "c33a4390-6d40-11ec-9291-4df0a4bd7c60";
            using (var httpClient = new HttpClient())
            {
                var cekilenDegerler = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel>(
                    await httpClient.GetStringAsync(
                        "https://freecurrencyapi.net/api/v2/latest?apikey=" + apiKey + "&base_currency=TRY")).Data;
                float deger;

                if (cekilenDegerler.TryGetValue(currency, out deger))
                {
                    degerler.Satis = (float)Math.Round(1 / deger, 4);
                    degerler.Alis = (float)Math.Round(1 / deger, 4);
                }
                System.Console.WriteLine(currency + " Alış : " + degerler.Alis);
                System.Console.WriteLine(currency + " Satış :  " + degerler.Satis);
                System.Console.WriteLine("-------------------------------------------------");

            }
        }

        public static async Task UpdateKurAsync()
        {
            List<string> currencies = new List<string>();
            currencies.Add("USD");
            currencies.Add("EUR");

            foreach (var currency in currencies)
            {
                await GetFromFreeCurrencyApi(currency);
            }

        }
    }
}