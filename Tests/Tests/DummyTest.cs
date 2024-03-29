namespace DummyTestProject.Tests.Tests
{
    public class DummyTest : TestBase
    {
        private IPage _tab1;
        private IPage _tab2;

        public DummyTest()
        {
            _tab1 = GetDriver().Page;
            _tab2 = GenerateNewTabPage();
        }

        [Test, Description("Dummy description")]
        public async Task DummyTestStep1()
        {
            await _tab1.GotoAsync("https://www.google.com/");
            await _tab2.GotoAsync("https://www.linkedin.com/");
            await Task.Delay(2000);
            Assert.True(false);
        }
    }
}