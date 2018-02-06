using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Coins.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coins.Controllers
{
    public class CoinController : Controller
    {
        static HttpClient client = new HttpClient();

        // GET: Coin
        public async Task<ActionResult> Index()
        {
            List<CoinModel> coins = await InvocaSupportedCoins("https://shapeshift.io/getcoins");
            return View(coins);
        }

        static async Task<List<CoinModel>> InvocaSupportedCoins(string uri)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            var coins = await GetCoinsAsync(client.BaseAddress.ToString());
            return coins;
        }

        static async Task<List<CoinModel>> GetCoinsAsync(string path)
        {
            List<CoinModel> coins = new List<CoinModel>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var coinsObjectAsync = await response.Content.ReadAsAsync<Object>();
                dynamic json = JsonConvert.DeserializeObject(coinsObjectAsync.ToString());
                foreach (JToken tempToken in json.Children())
                {

                    coins.Add(JsonConvert.DeserializeObject<CoinModel>(tempToken.First().ToString()));
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

        static async Task<CoinPairModel> Invoca(string uri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            var coinPair = await GetCoinPairAsync(client.BaseAddress.ToString());
            return coinPair;
        }

        static async Task<CoinPairModel> GetCoinPairAsync(string path)
        {
            CoinPairModel coinPair = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                coinPair = await response.Content.ReadAsAsync<CoinPairModel>();
            }
            return coinPair;
        }

        // GET: Coin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Coin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Coin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Coin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Coin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Coin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
