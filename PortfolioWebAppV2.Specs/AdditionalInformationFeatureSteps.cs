using System;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    public class AdditionalInformationFeatureSteps
    {
        private readonly IWebDriver _driver;
        private readonly string _title = "Testowe godzina: " + DateTime.Now.ToShortTimeString();

        public AdditionalInformationFeatureSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"administrator panel is open")]
        public void GivenAdministratorPanelIsOpen()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/AdminPanel");
            Thread.Sleep(1000);
        }
        
        [Given(@"There is open Additional Information panel")]
        public void GivenThereIsOpenAdditionalInformationPanel()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/AdditionalInformation/AdditionalInformationManagement");
            Thread.Sleep(1000);
        }
        
        [Given(@"foreign language tab is selected")]
        public void GivenForeignLanguageTabIsSelected()
        {
            const string xPath = "//*[@id=\"pills-ForeignLanguages-tab\"]";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
        
        [When(@"I select cv additional information from menu list")]
        public void WhenISelectCvAdditionalInformationFromMenuList()
        {
            const string xPath = "//*[@id=\"menu\"]/li[6]/a";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
        
        [When(@"I press additional information button")]
        public void WhenIPressAdditionalInformationButton()
        {
            const string xPath = "//*[@id=\"menu\"]/li[6]/ul/li[3]/a";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
        
        [When(@"I press add Additional Information button")]
        public void WhenIPressAddAdditionalInformationButton()
        {
            const string xPath = "/html/body/div/div[2]/div[2]/div[1]/div[2]/button";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
        
        [When(@"I have fill the required Additional Information data")]
        public void WhenIHaveFillTheRequiredAdditionalInformationData()
        {
            string xPath = "//*[@id=\"Title\"]";
            IWebElement title = _driver.FindElement(By.XPath(xPath));
            title.SendKeys(_title);
            Thread.Sleep(500);
        }
        
        [Then(@"additional information panel is open")]
        public void ThenAdditionalInformationPanelIsOpen()
        {
            var result = _driver.Url.Equals("http://localhost:52048/AdditionalInformation/AdditionalInformationManagement");
            Assert.True(result);
        }
        
        [Then(@"I press create Additional Information")]
        public void ThenIPressCreateAdditionalInformation()
        {
            string xPath = "//*[@id=\"AddNewAdditionalInfo\"]/div/div/div[3]/button[1]";
            IWebElement btn = _driver.FindElement(By.XPath(xPath));
            btn.Click();
            Thread.Sleep(1500);
        }

        [Then(@"Additional Information has been created")]
        public void ThenAdditionalInformationHasBeenCreated()
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
