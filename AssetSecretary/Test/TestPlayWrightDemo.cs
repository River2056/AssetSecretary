using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AssetSecretary.Test
{
    public class TestPlayWrightDemo
    {
        static async Task FetchStockPriceFromGoodInfo(string stockId)
        {
            // https://goodinfo.tw/StockInfo/StockDetail.asp?STOCK_ID=0050
            var baseUrl = "https://goodinfo.tw/StockInfo/StockDetail.asp?STOCK_ID=";
            var url = $"{baseUrl}{stockId}";

            using (var playwright = await Playwright.CreateAsync())
            {
                await using (var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false
                }))
                {
                    var page = await browser.NewPageAsync();
                    await page.GotoAsync(url);
                    // get price and percentage
                    // document.querySelector("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(1)")
                    var price = await page.EvaluateAsync("el => el.innerText", await page.QuerySelectorAsync("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(1)"));
                    
                    // document.querySelector("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)")
                    var percentage = await page.EvaluateAsync("el => el.innerText", await page.QuerySelectorAsync("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(3)"));
                    
                    // document.querySelector("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr.bg_h0.fw_normal > th > table > tbody > tr > td:nth-child(1) > nobr > a")
                    var title = await page.QuerySelectorAsync("body > table:nth-child(8) > tbody > tr > td:nth-child(3) > table > tbody > tr:nth-child(1) > td > table > tbody > tr:nth-child(1) > td:nth-child(1) > table > tbody > tr.bg_h0.fw_normal > th > table > tbody > tr > td:nth-child(1) > nobr > a");
                    var titleStr = await page.EvaluateHandleAsync("el => el.innerText", title);
                    Console.WriteLine(titleStr);
                }
            }
        }
        
        static async Task Main(string[] args)
        {
            using (var playwright = await Playwright.CreateAsync())
            {
                await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Headless = false
                });
                var page = await browser.NewPageAsync();
                await page.GotoAsync("https://www.google.com.tw/webhp?hl=zh-TW");
                var expression = await page.EvaluateAsync(@"document.querySelector('body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.FPdoLc.lJ9FBc > center > input.gNO89b').value");
                await page.TypeAsync("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.RNNXgb > div > div.a4bIc > input", expression == null ? "" : expression.ToString(), new PageTypeOptions { Delay = 3000 });
            }
        }
    }
}