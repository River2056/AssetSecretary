using System;
using System.Linq;
using System.Threading.Tasks;
using AssetSecretary.Db;
using Microsoft.Playwright;

namespace AssetSecretary
{
    class Program
    {
        private static async Task UpdateInfoToDB()
        {
            // fetch all prices
            using (var db = new DbConnectionEntity())
            {
                var currentPriceList = db.DailyPrice.ToList();
                foreach (var currentPrice in currentPriceList)
                {
                    var priceData = await FetchStockPriceFromGoodInfo(currentPrice.StockId);
                    double price = double.Parse(priceData[0]);
                    currentPrice.CurrentPrice = price;
                    currentPrice.Percentage = priceData[1];
                    currentPrice.ModifyDate = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    db.DailyPrice.Update(currentPrice);
                    Console.WriteLine($"Done saving to database: {currentPrice}");
                }
                db.SaveChanges();
            }
        }
        private static async Task<string[]> FetchStockPriceFromGoodInfo(string stockId)
        {
            // https://goodinfo.tw/StockInfo/StockDetail.asp?STOCK_ID=0050
            var baseUrl = "https://goodinfo.tw/StockInfo/StockDetail.asp?STOCK_ID=";
            var url = $"{baseUrl}{stockId}";

            using (var playwright = await Playwright.CreateAsync())
            {
                await using (var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true
                }))
                {
                    var page = await browser.NewPageAsync();
                    await page.GotoAsync(url);
                    // get price and percentage
                    // price
                    // document.querySelector("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(1)")
                    var price = await page.EvaluateAsync("el => el.innerText", await page.QuerySelectorAsync("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(1)"));
                    
                    // percentage
                    // document.querySelector("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)")
                    var percentage = await page.EvaluateAsync("el => el.innerText", await page.QuerySelectorAsync("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)"));

                    return new string[] { price.ToString(), percentage.ToString() };
                }
            }
        }
        
        static void Main(string[] args)
        {
            UpdateInfoToDB().Wait();
        }
    }
}