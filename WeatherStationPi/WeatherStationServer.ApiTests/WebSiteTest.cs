using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;

namespace WeatherStationServer.ApiTests
{
    [TestFixture]
    [Category("WebSiteIntegration")]
    public class WebSiteTest
    {
        //private string _url = "http://localhost:59653/V2/";
        private string _url = "http://weatherstat.azurewebsites.net/v2";

        [Test]
        public void Go()
        {
            var myWeatherStationPage = GoToMyWeatherStationPage();

            Assert.That(myWeatherStationPage.SelectedDropDownText, Is.EqualTo("weatherStation1_Calf"));

            //var testStationOption = dropDown.FindElement(By.LinkText("testStation"));
            //testStationOption.Click();

            //webDriver.Close();
        }

        private MyWeatherStationPage GoToMyWeatherStationPage()
        {
            IWebDriver webDriver = new ChromeDriver();

            webDriver
                .Navigate()
                .GoToUrl(_url)
                ;

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            var page = SeleniumPageFactory<MyWeatherStationPage>(webDriver);
            return page;
        }

        public T SeleniumPageFactory<T>(IWebDriver webDriver) where T : new()
        {
            var page = new T();

            PageFactory.InitElements(page, new RetryingElementLocator(webDriver, TimeSpan.FromSeconds(1)));

            return page;
        }

        public class MyWeatherStationPage
        {
            [FindsBy(How = How.Id, Using = "single-button")]
            private IWebElement DropDown { get; set; }

            public string SelectedDropDownText
            {
                get { return DropDown.Text; }
            }
        }
    }
}