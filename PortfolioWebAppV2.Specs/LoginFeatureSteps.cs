using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    public class LoginFeatureSteps
    {
        private readonly IWebDriver _driver;
        private string _login; 

        public LoginFeatureSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [When(@"I press Login button")]
        public void WhenIPressLoginButton()
        {
            string xpath = "/html/body/section[1]/div/div[1]/div[2]/div/button[6]";
            IWebElement loginBtn = _driver.FindElement(By.XPath(xpath));
            loginBtn.Click();

            Thread.Sleep(500);
        }

        [Then(@"login panel should be visible")]
        public void ThenLoginPanelShouldBeVisible()
        {
            string loginPanelXPath = "//*[@id=\"LoginPanel\"]";
            IWebElement loginPanel = _driver.FindElement(By.XPath(loginPanelXPath));
            var result = loginPanel.Displayed;

            Assert.True(result);
        }

        [Given(@"I have entered ""(.*)"" into the login field")]
        public void GivenIHaveEnteredIntoTheLoginField(string login)
        {
            _login = login;
            string loginXPath = "//*[@id=\"login\"]";
            IWebElement loginField = _driver.FindElement(By.XPath(loginXPath));
            loginField.SendKeys(login);
        }

        [Given(@"I have entered ""(.*)"" into the password field")]
        public void GivenIHaveEnteredIntoThePasswordField(string password)
        {
            string passwordXPath = "//*[@id=\"Password\"]";
            IWebElement passwordField = _driver.FindElement(By.XPath(passwordXPath));
            passwordField.SendKeys(password);
        }

        [When(@"I press Sign in button")]
        public void WhenIPressSignInButton()
        {
            string singInBtnXPath = "//*[@id=\"LoginPanel\"]/form/div[1]/span/input";
            IWebElement singInBtn = _driver.FindElement(By.XPath(singInBtnXPath));
            singInBtn.Click();

            Thread.Sleep(2000);
        }

        [Then(@"the user should be logged")]
        public void ThenTheUserShouldBeLogged()
        {
            string stringWithLoginXPath = "/html/body/section[1]/div/div[2]/div/h6/strong";
            IWebElement loginString = _driver.FindElement(By.XPath(stringWithLoginXPath));
            string resultString = loginString.Text;

            var result = resultString.Contains(_login);

            Assert.True(result);
        }
    }
}
