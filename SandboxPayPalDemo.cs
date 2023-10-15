using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.DefaultWaitHelpers;
using System.Drawing;

namespace SeleniumTest
{
	public class SandboxPayPalDemo
	{
		const string SANDBOX_PAYPAL_URL = "https://sandbox.paypal.com";
		const string SANDBOX_PAYPAL_URL_DONE = "https://sandbox.paypal.com/home";
		const string SANDBOX_PAYPAL_EMAIL = "pmt1998@gmail.com";
		const string SANDBOX_PAYPAL_PASSWORD = "Abc12345";
		IWebDriver driver;
		WebDriverWait wait;

		[SetUp]
		public void startBrowser()
		{
			var chromeOption = new ChromeOptions();
			chromeOption.PageLoadStrategy = PageLoadStrategy.Normal;
			chromeOption.AddArgument("--headless");
			driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOption, TimeSpan.FromMinutes(3));
			driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
			driver.Manage().Window.Maximize();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
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

			var btnLogin = driver.FindElement(By.Id("btnLogin"));
			var currentUrl = driver.Url;
			Thread.Sleep(1000);
			btnLogin.Click();
			wait.Until(driver => driver.Url != currentUrl);

			TakeScreenShotAndSaveIn();
			Assert.That(driver.Title, Is.EqualTo("PayPal: Summary"), "Complete");
		}

		[Test]
		public void TestFullPageScreenShot()
		{
			driver.Url = "https://www.smashingmagazine.com/2017/05/long-scrolling/";
			TakeScreenShotAndSaveIn();
		}

		[TearDown]
		public void closeBrowser()
		{
			driver.Quit();
		}


		public void TakeScreenShotAndSaveIn()
		{
			var path = "D:\\Test";
			var fileName = "Abc.png";

			var fullwidth = ((IJavaScriptExecutor)driver).ExecuteScript("return document.body.parentNode.scrollWidth");
			var fullheight = ((IJavaScriptExecutor)driver).ExecuteScript("return document.body.parentNode.scrollHeight");


			driver.Manage().Window.Size = new Size(
				int.Parse(fullwidth.ToString()),
				int.Parse(fullheight.ToString())
				);
			var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

			screenshot.SaveAsFile(Path.Combine(path, fileName),ScreenshotImageFormat.Png);
			screenshot.ToString();

		}

	}

}
