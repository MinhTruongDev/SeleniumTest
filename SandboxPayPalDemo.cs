using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest
{
	public class SandboxPayPalDemo
	{
		const string SANDBOX_PAYPAL_URL = "https://sandbox.paypal.com";
		const string SANDBOX_PAYPAL_URL_DONE = "https://sandbox.paypal.com/home";
		const string SANDBOX_PAYPAL_EMAIL = "pmt1998@gmail.com";
		const string SANDBOX_PAYPAL_PASSWORD = "Abc12345";
		IWebDriver driver;

		[SetUp]
		public void startBrowser()
		{
			var chromeOption = new ChromeOptions();
			chromeOption.PageLoadStrategy = PageLoadStrategy.Normal;
			chromeOption.AddArgument("no-sandbox");
			driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOption, TimeSpan.FromMinutes(3));
			driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
			driver.Manage().Window.Maximize();
		}

		[Test]
		public void FlowLogIn()
		{
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2.5);

			driver.Url = SANDBOX_PAYPAL_URL;

			driver.FindElement(By.Id("ul-btn")).Click();

			IWebElement tbEmail = driver.FindElement(By.Id("email"));
			tbEmail.SendKeys(SANDBOX_PAYPAL_EMAIL);
			driver.FindElement(By.Id("btnNext")).Click();

			IWebElement tbPassword = driver.FindElement(By.Id("password"));
			tbPassword.SendKeys(SANDBOX_PAYPAL_PASSWORD);

			driver.FindElement(By.Id("btnLogin")).Click();

			Assert.That(driver.Title, Is.EqualTo("PayPal: Summary"), "Complete");

			driver.Quit();
		}

		[TearDown]
		public void closeBrowser()
		{
			driver.Quit();
		}
	}
}
