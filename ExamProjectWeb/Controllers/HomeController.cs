using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using ExamProjectWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamProjectWeb.Controllers
{
    //Home controller
    public class HomeController : Controller
    {
        //Creating an instance of CoinGeckoClient
        static CoinGeckoClient CG = new();

        //Changing the route, and setting up the main page
        [Route("", Name = "HomePage")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Web Page";
            return View(await GetTopTenCoins());
        }

        //A method which gives back the top 10 crypto by marketcap from the CG API
        public async Task<List<CoinMarkets>> GetTopTenCoins()
        {
            return await CG.CoinsClient.GetCoinMarkets("usd", new string[] { }, null, 10, null, false, null, null);
        }
    }
}