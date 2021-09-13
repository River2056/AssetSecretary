using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Input;

namespace AssetSecretary.Test
{
    public class TestPuppeteerSharpDemo
    {
        // static void Main(string[] args)
        // {
        //     FetchTitle().Wait();
        // }

        static async Task FetchTitle()
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            using (Browser  browser = await Puppeteer.LaunchAsync(new LaunchOptions()
            {
                Headless = false
            }))
            {
                using (Page page = await browser.NewPageAsync())
                {
                    await page.GoToAsync("https://www.google.com.tw/webhp?hl=zh-TW");
                    // document.querySelector("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.FPdoLc.lJ9FBc > center > input.gNO89b")
                    await page.WaitForSelectorAsync("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.FPdoLc.lJ9FBc > center > input.gNO89b");
                    // Task<ElementHandle> btnTextHandle = page.QuerySelectorAsync("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.FPdoLc.lJ9FBc > center > input.gNO89b").Result.JsonValueAsync<ElementHandle>();
                    // Console.WriteLine(btnTextHandle);
                    string jsSelect =
                        @"document.querySelector('body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.FPdoLc.lJ9FBc > center > input.gNO89b').value";
                    string text = await page.EvaluateExpressionAsync<string>(jsSelect);
                    Console.WriteLine(text);

                    // document.querySelector("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.RNNXgb > div > div.a4bIc > input")
                    await page.TypeAsync("body > div.L3eUgb > div.o3j99.ikrT4e.om7nvf > form > div:nth-child(1) > div.A8SBwf > div.RNNXgb > div > div.a4bIc > input", text, new TypeOptions()
                    {
                        Delay = 3000
                    });
                }
            }
        }
    }
}