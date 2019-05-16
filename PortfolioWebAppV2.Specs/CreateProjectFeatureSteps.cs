using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    public class CreateProjectFeatureSteps
    {
        private readonly IWebDriver _driver;

        public CreateProjectFeatureSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"There is logged admin")]
        public void GivenThereIsLoggedAdmin()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/Account/TestLogin");

            Thread.Sleep(1000);
        }
        
        [Given(@"there are open administrator panel")]
        public void GivenThereAreOpenAdministratorPanel()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/AdminPanel");
            Thread.Sleep(1000);

            var result = _driver.Url.Equals("http://localhost:52048/AdminPanel");
            Assert.True(result);
        }
        
        [Given(@"There is open create project panel")]
        public void GivenThereIsOpenCreateProjectPanel()
        {
            const string url = "http://localhost:52048/Projects/CreateProject";

            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(1000);

            var result = _driver.Url.Equals(url);
            Assert.True(result);
        }
        
        [When(@"I select Project from menu list")]
        public void WhenISelectProjectFromMenuList()
        {
            string projectXPath = "//*[@id=\"menu\"]/li[3]/a";
            IWebElement projectButton = _driver.FindElement(By.XPath(projectXPath));
            projectButton.Click();
            Thread.Sleep(500);
        }
        
        [When(@"I select Create project")]
        public void WhenISelectCreateProject()
        {
            string createProjectXPath = "//*[@id=\"menu\"]/li[3]/ul/li[3]/a";
            IWebElement createProjectButton = _driver.FindElement(By.XPath(createProjectXPath));
            createProjectButton.Click();
            Thread.Sleep(500);
        }

        [Then(@"there is open create project panel")]
        public void ThenThereIsOpenCreateProjectPanel()
        {
            var result = _driver.Url.Equals("http://localhost:52048/Projects/CreateProject");
            Assert.True(result);
        }
    }
}
