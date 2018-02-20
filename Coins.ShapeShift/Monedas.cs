using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coins.ShapeShift
{
    public static class Monedas
    {
        static HttpClient client = new HttpClient();

        public static async Task<List<Coin>> SupportedCoins(string uri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            var coins = await GetCoinsAsync(client.BaseAddress.ToString());
            return coins;
        }

        private static async Task<List<Coin>> GetCoinsAsync(string path)
        {
            List<Coin> coins = new List<Coin>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var coinsObjectAsync = await response.Content.ReadAsAsync<Object>();
                dynamic json = JsonConvert.DeserializeObject(coinsObjectAsync.ToString());
                foreach (JToken tempToken in json.Children())
                {

                    coins.Add(JsonConvert.DeserializeObject<Coin>(tempToken.First().ToString()));
                }
            }

            int i = 0;
            foreach (var coin in coins)
            {
                if (coin.Status == "available")
                {
                    client = new HttpClient();
                    var coinPair = await Invoca("https://shapeshift.io/rate/" + coin.Symbol + "_btc");
                    coins[i].Cotizacion = coinPair.Rate;
                }
                i++;
            }

            return coins;
        }

        private static async Task<CoinPair> Invoca(string uri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            var coinPair = await GetCoinPairAsync(client.BaseAddress.ToString());
            return coinPair;
        }

        private static async Task<CoinPair> GetCoinPairAsync(string path)
        {
            CoinPair coinPair = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                coinPair = await response.Content.ReadAsAsync<CoinPair>();
            }
            return coinPair;
        }
    }
}
