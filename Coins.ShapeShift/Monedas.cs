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
        private static HttpClient _client = new HttpClient();

        public static async Task<List<Coin>> GetSupportedCoins(string uri, bool pConCotizaciones)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            var coins = await GetCoins(_client.BaseAddress.ToString(), pConCotizaciones);
            return coins;
        }

        private static async Task<List<Coin>> GetCoins(string pUri, bool pConCotizaciones)
        {
            var coins = new List<Coin>();
            var response = await _client.GetAsync(pUri, HttpCompletionOption.ResponseContentRead);
            if (response.IsSuccessStatusCode)
            {
                var coinsObjectAsync = await response.Content.ReadAsAsync<Object>();
                dynamic json = JsonConvert.DeserializeObject(coinsObjectAsync.ToString());
                foreach (JToken tempToken in json.Children())
                {
                    coins.Add(JsonConvert.DeserializeObject<Coin>(tempToken.First().ToString()));
                }
            }

            if (pConCotizaciones)
            {
                var i = 0;
                foreach (var coin in coins)
                {
                    if (coin.Status == "available")
                    {
                        _client = new HttpClient();
                        var coinPair = await GetCoinPair("https://shapeshift.io/rate/" + coin.Symbol + "_btc");
                        coins[i].Cotizacion = coinPair.Rate;
                    }
                    i++;
                }
            }
            return coins;
        }

        public static async Task<CoinPair> GetCoinPair(string pUri)
        {
            _client = new HttpClient {BaseAddress = new Uri(pUri) };
            _client.DefaultRequestHeaders.Accept.Clear();
            CoinPair coinPair = null;
            var response = await _client.GetAsync(pUri, HttpCompletionOption.ResponseContentRead);
            if (response.IsSuccessStatusCode)
            {
                coinPair = await response.Content.ReadAsAsync<CoinPair>();
            }
            return coinPair;
        }
    }
}
