using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    public class AchievementFeatureSteps
    {
        private readonly IWebDriver _driver;
        private readonly string _title = "Testowe";
        public AchievementFeatureSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"There is logged administrator")]
        public void GivenThereIsLoggedAdministrator()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/Account/TestLogin");
            Thread.Sleep(1000);
        }

        [Given(@"There are open administrator panel")]
        public void GivenThereAreOpenAdministratorPanel()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/AdminPanel");
            Thread.Sleep(1000);
        }

        [When(@"I select cv management from menu list")]
        public void WhenISelectCvManagementFromMenuList()
        {
            const string xPath = "//*[@id=\"menu\"]/li[6]/a";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }

        [When(@"I press Achievements button")]
        public void WhenIPressAchievementsButton()
        {
            const string xPath = "//*[@id=\"menu\"]/li[6]/ul/li[2]/a";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }

        [Then(@"Achievement panel is open")]
        public void ThenAchievementPanelIsOpen()
        {
            var result = _driver.Url.Equals("http://localhost:52048/Achievement/AchievementsManagement");
            Assert.True(result);
        }


        [Given(@"there is logged admin")]
        public void GivenThereIsLoggedAdmin()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/Account/TestLogin");
            Thread.Sleep(1000);
        }

        [Given(@"There is open Achivement pane")]
        public void GivenThereIsOpenAchivementPane()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/Achievement/AchievementsManagement");
            Thread.Sleep(1000);
        }

        [When(@"I press create btn")]
        public void WhenIPressCreateBtn()
        {
            const string xPath = "/html/body/div/div[2]/div[2]/div[1]/div/div/button";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }

        [When(@"I have fill the required data")]
        public void WhenIHaveFillTheRequiredData()
        {
            string xPath = "//*[@id=\"Title\"]";
            IWebElement title = _driver.FindElement(By.XPath(xPath));
            title.SendKeys(_title);
            Thread.Sleep(500);

            xPath = "//*[@id=\"Description\"]";
            IWebElement description = _driver.FindElement(By.XPath(xPath));
            description.SendKeys("testowy opis");
            Thread.Sleep(500);

            xPath = "//*[@id=\"Date\"]";
            IWebElement date = _driver.FindElement(By.XPath(xPath));
            date.SendKeys("12-12-2019");
            Thread.Sleep(500);
        }

        [Then(@"I press create btn")]
        public void ThenIPressCreateBtn()
        {
            const string xPath = "//*[@id=\"AddAchievement\"]/div/div/div[3]/button[3]";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();

            Thread.Sleep(1000);
        }

        [Then(@"Achievement has been created")]
        public void ThenAchievementHasBeenCreated()
        {
            var result = false;
            const string xPath = "//*[@id=\"dataTable\"]";
            IWebElement baseTable = _driver.FindElement(By.XPath(xPath));
            ReadOnlyCollection<IWebElement> tableRows = baseTable.FindElements(By.TagName("tr"));
            foreach (var webElement in tableRows)
            {
                Console.WriteLine(webElement.Text);
                if (webElement.Text.Contains(_title)) result = true;
            }

            Assert.True(result);
        }
    }
}
