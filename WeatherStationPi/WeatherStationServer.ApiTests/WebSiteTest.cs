using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WeatherStationServer.ApiTests
{
    [TestFixture]
    [Category("WebSiteIntegration")]
    public class WebSiteTest
    {
        [Test]
        public void Go()
        {
            IWebDriver webDriver = new ChromeDriver();

            webDriver
                .Navigate()
                .GoToUrl("http://localhost:59653/V2/");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            IWebElement dropDown = webDriver.FindElement(By.Id("single-button"));
            Assert.That(dropDown.Text, Is.EqualTo("apiTestsStationId"));

            //var testStationOption = dropDown.FindElement(By.LinkText("testStation"));
            //testStationOption.Click();

            //webDriver.Close();
        }
    }
}