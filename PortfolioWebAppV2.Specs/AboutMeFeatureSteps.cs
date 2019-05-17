using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    public class AboutMeFeatureSteps
    {
        private readonly IWebDriver _driver;
        private readonly string _description = "Opis godzina: " + DateTime.Now.ToShortTimeString();

        public AboutMeFeatureSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"Edit about me information page is open")]
        public void GivenEditAboutMeInformationPageIsOpen()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/AboutMe/AboutMeManagement");
            Thread.Sleep(1000);
        }
        
        [When(@"I press Page menu")]
        public void WhenIPressPageMenu()
        {
            const string xPath = "//*[@id=\"menu\"]/li[2]/a";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
        
        [When(@"select about me edit page option")]
        public void WhenSelectAboutMeEditPageOption()
        {
            const string xPath = "//*[@id=\"menu\"]/li[2]/ul/li[1]/a";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
        
        [When(@"i change data")]
        public void WhenIChangeData()
        {
            string xPath = "//*[@id=\"Text\"]";
            IWebElement title = _driver.FindElement(By.XPath(xPath));
            title.Clear();
            title.SendKeys(_description);

            Thread.Sleep(500);
        }
        
        [When(@"i have press save button")]
        public void WhenIHavePressSaveButton()
        {
            const string xPath = "/html/body/div/div[2]/div[2]/form/input[3]";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(1000);
        }
        
        [Then(@"Edit about me page shoudl be visible")]
        public void ThenEditAboutMePageShoudlBeVisible()
        {
            var result = _driver.Url.Equals("http://localhost:52048/AboutMe/AboutMeManagement");
            Assert.True(result);
        }
        
        [Then(@"data should be updated in about me page")]
        public void ThenDataShouldBeUpdatedInAboutMePage()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/AboutMe/AboutMe");
            Thread.Sleep(1000);

            string xPath = "/html/body/section[3]/div/div/div/div";
            IWebElement title = _driver.FindElement(By.XPath(xPath));
            var result = title.Text.Contains(_description);

            Assert.True(result);

        }
    }
}
