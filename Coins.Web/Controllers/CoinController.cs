using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Coins.ShapeShift;
using Coins.Web.Models;

namespace Coins.Web.Controllers
{
    public class CoinController : Controller
    {
        // GET: Coin
        public async Task<ActionResult> Index()
        {
            var coins = await Monedas.GetSupportedCoins("https://shapeshift.io/getcoins", true);
            var coinsModel = coins.Select(coin => coin.ToCoinModel()).ToList();
            return View(coinsModel);
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
