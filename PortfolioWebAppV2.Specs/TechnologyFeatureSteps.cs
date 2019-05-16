using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    public class TechnologyFeatureSteps
    {
        private const string TechnologyName = "Testowa";
        private readonly IWebDriver _driver;

        public TechnologyFeatureSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"There is open create technology panel")]
        public void GivenThereIsOpenCreateTechnologyPanel()
        {
            _driver.Navigate().GoToUrl("http://localhost:52048/Account/TestLogin");
            Thread.Sleep(1000);

            _driver.Navigate().GoToUrl("http://localhost:52048/Technology/TechnologyManagement");
            Thread.Sleep(1000);

            const string xPath = "/html/body/div/div[2]/div[2]/div[1]/div/div/button";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
   
        [When(@"I have fill required data")]
        public void WhenIHaveFillRequiredData()
        {
            const string xPath = "//*[@id=\"Name\"]";
            IWebElement textfield = _driver.FindElement(By.XPath(xPath));
            textfield.SendKeys(TechnologyName);
            Thread.Sleep(500);
        }
        
        [When(@"I press Create button")]
        public void WhenIPressCreateButton()
        {
            const string xPath = "//*[@id=\"AddTechnology\"]/div/div/div[3]/button[1]";
            IWebElement button = _driver.FindElement(By.XPath(xPath));
            button.Click();
            Thread.Sleep(500);
        }
        
        [Then(@"Create panel should redirect to list")]
        public void ThenCreatePanelShouldRedirectToList()
        {
            var result = _driver.Url.Equals("http://localhost:52048/Technology/TechnologyManagement");
            Assert.True(result);
        }
        
        [Then(@"Technology is available in project list")]
        public void ThenTechnologyIsAvailableInProjectList()
        {
            var result = false;
            const string xPath = "//*[@id=\"dataTable\"]";
            IWebElement baseTable = _driver.FindElement(By.XPath(xPath));
            ReadOnlyCollection<IWebElement> tableRows = baseTable.FindElements(By.TagName("tr"));
            foreach (var webElement in tableRows)
            {
                Console.WriteLine(webElement.Text);
                if (webElement.Text.Contains(TechnologyName)) result = true;
            }

            Assert.True(result);
        }
    }
}
