using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Core22SwaggerClientConsoleApp
{
    public class CurrencyGetViewModel
    {
        public string IsoCode { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MainAsync(args).GetAwaiter().GetResult();

            Console.WriteLine("Hit a key to exit...");
            Console.ReadKey();
        }

        public static async Task MainAsync(string[] args)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync("api/values").ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var currencyItems =
                            await response.Content.ReadAsAsync<IEnumerable<CurrencyGetViewModel>>();

                        foreach (var currencyItem in currencyItems)
                        {
                            Console.WriteLine(
                                $"IsoCode {currencyItem.IsoCode} : " +
                                $"Symbol: {currencyItem.Symbol} : " +
                                $"Name: {currencyItem.Name} : " +
                                $"IsDefault {currencyItem.IsDefault}"
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
