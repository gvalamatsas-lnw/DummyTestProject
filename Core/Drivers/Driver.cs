using Microsoft.Playwright;

namespace DummyTestProject.Core.Drivers
{
    public class Driver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;
        public IBrowserContext Context;

        /// <summary>
        /// Constructor to launch init
        /// </summary>
        public Driver()
        {
            _page = Task.Run(InitializePlaywright);
        }

        /// <summary>
        /// Page1 returns result
        /// </summary>
        public IPage Page => _page.Result;

        /// <summary>
        /// Disposes of Browser Session
        /// </summary>
        public void Dispose()
        {
            _browser?.CloseAsync();
        }

        /// <summary>
        /// Initialize Playwright browser session with single tabs
        /// </summary>
        /// <returns>Returns Valid</returns>
        private async Task<IPage> InitializePlaywright()
        {
            await InitializeCommon();
            return await Context.NewPageAsync();
        }


        /// <summary>
        /// Common Initialization Method
        /// </summary>
        /// <returns></returns>
        private async Task<IBrowserContext> InitializeCommon()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });


            Context = await _browser.NewContextAsync();
            await Context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            return Context;
        }
    }
}