using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace PortfolioWebAppV3.Specs
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("D:\\chromedriver.exe");
        }

        [Test]
        public void test()
        {
            driver.Url = "http://www.google.pl";
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

        private Calculator _calculator = new Calculator();
        private int result;
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            _calculator.FirstNumber = number;
        }
        
        [Given(@"I have also entered (.*) into the calculator")]
        public void GivenIHaveAlsoEnteredIntoTheCalculator(int number)
        {
            _calculator.SecondNumber = number;
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            result = _calculator.Add();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int exceptedResult)
        {
            Assert.AreEqual(exceptedResult, result);
        }
    }
}
