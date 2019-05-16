using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    public class AdministratorPanelFeatureSteps
    {
        private readonly IWebDriver _driver;

        public AdministratorPanelFeatureSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I am logged as ""(.*)""")]
        public void GivenIAmLoggedAs(string login)
        {
            string xpath = "/html/body/section[1]/div/div[1]/div[2]/div/button[6]";
            IWebElement loginBtn = _driver.FindElement(By.XPath(xpath));
            loginBtn.Click();

            string loginXPath = "//*[@id=\"login\"]";
            IWebElement loginField = _driver.FindElement(By.XPath(loginXPath));
            loginField.SendKeys(login);

            string passwordXPath = "//*[@id=\"Password\"]";
            IWebElement passwordField = _driver.FindElement(By.XPath(passwordXPath));
            passwordField.SendKeys("Damian13");

            string singInBtnXPath = "//*[@id=\"LoginPanel\"]/form/div[1]/span/input";
            IWebElement singInBtn = _driver.FindElement(By.XPath(singInBtnXPath));
            singInBtn.Click();

            Thread.Sleep(1000);

            string stringWithLoginXPath = "/html/body/section[1]/div/div[2]/div/h6/strong";
            IWebElement loginString = _driver.FindElement(By.XPath(stringWithLoginXPath));
            string resultString = loginString.Text;

            var result = resultString.Contains(login);

            Assert.True(result);
        }

        [When(@"Administrator panel button is visible")]
        public void WhenAdministratorPanelButtonIsVisible()
        {
            string adminBtnXPath = "/html/body/section[1]/div/div[1]/div[2]/div/button[6]";
            IWebElement adminBtn = _driver.FindElement(By.XPath(adminBtnXPath));
            var result = adminBtn.Displayed;

            Assert.True(result);
        }

        [When(@"I press administrator panel button")]
        public void WhenIPressAdministratorPanelButton()
        {
            string adminBtnXPath = "/html/body/section[1]/div/div[1]/div[2]/div/button[6]";
            IWebElement adminBtn = _driver.FindElement(By.XPath(adminBtnXPath));

            adminBtn.Click();

            Thread.Sleep(1000);

        }

        [Then(@"Administrator panel is available")]
        public void ThenAdministratorPanelIsAvailable()
        {
            string stringXPath = "/html/body/div[1]/div[2]/div[1]/div/div[1]/div/h4";
            IWebElement dashboardString = _driver.FindElement(By.XPath(stringXPath));
            string resultString = dashboardString.Text;

            var result = resultString.Contains("Dashboard");

            Assert.True(result);
        }
    }
}
