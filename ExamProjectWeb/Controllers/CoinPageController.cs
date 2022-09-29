using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject_Net6_Test.Controllers
{
    public class CoinPageController : Controller
    {
        static CoinGeckoClient CG = new();

        //Changing the route, and setting up the Coins page
        [Route("Coins", Name = "coinList")]
        public async Task<IActionResult> Coins()
        {
            return View(await GetHundredCoins());
        }

        //Changing the route, and setting up the coin page
        [Route("coins/{coin}", Name = "coinPage")]
        public async Task<IActionResult> CoinPage(string coin)
        {
            return View(await GetTopNCoin(coin));
        }
        //Getting the top 100 coin {work in progress}
        public async Task<List<CoinMarkets>> GetHundredCoins()
        {
            return await CG.CoinsClient.GetCoinMarkets("usd", new string[] { }, null, 100, null, false, null, null);
        }


        //A method which gets a certain crypto's data
        public async Task<CoinFullDataById> GetTopNCoin(string id)
        {
            return await CG.CoinsClient.GetAllCoinDataWithId(id); ;
        }
    }
}
