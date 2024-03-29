using DummyTestProject.Core.Drivers;
using Serilog;

namespace DummyTestProject.Tests.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class TestBase : PageTest
    {
        private readonly Driver driver;
        
        public TestBase()
        {
            driver = new();
        }

        public Driver GetDriver()
        {
            return driver;
        }

        protected IPage GenerateNewTabPage()
        {
            return GetDriver().Page.Context.NewPageAsync().Result;
        }


        [TearDown]
        public async Task TearDownActions()
        {
            await TakeScreenshotOnFailure();
        }

        private async Task TakeScreenshotOnFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Log.Information("Test wasn't successful. Will create screenshots.");
                foreach (var page in driver.Context.Pages)
                {
                    Log.Information($"Will create screenshot for page with url: {page.Url}");
                    byte[] screenshot = await page.ScreenshotAsync();
                    AllureLifecycle.Instance.AddAttachment(await page.TitleAsync(), "image/png", screenshot, ".png");
                }
            }
        }

        [OneTimeTearDown]
        public void CloseHubConnection()
        {
            GetDriver().Dispose();
        }
    }
}