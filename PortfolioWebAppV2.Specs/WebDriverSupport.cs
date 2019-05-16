using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV2.Specs
{
    [Binding]
    class WebDriverSupport
    {
        private readonly IObjectContainer _objectContainer;

        public WebDriverSupport(IObjectContainer objectContainer)
        {
            this._objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            var webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl("http://localhost:52048/");

            _objectContainer.RegisterInstanceAs<IWebDriver>(webDriver);
        }

        [AfterScenario]
        public void CleanWebDriver()
        {
            var webDriver = _objectContainer.ResolveAll<IWebDriver>().FirstOrDefault();
            webDriver?.Close();
        }

    }
}
