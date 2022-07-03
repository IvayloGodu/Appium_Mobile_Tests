using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Runtime.Serialization;
using System.Threading;

namespace ContactBook.AndroidTests
{
    public class AndroidTest
    {
        private const string AppiumUrl = "http://[::1]:4723/wd/hub";
        private const string VivinoAppPackage = "vivino.web.app";
        private const string VivinoAppStartUpActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestEmail = "test_vivino@gmail.com";
        private const string VivinoTestPassword = "p@ss987654321";

        private AndroidDriver<AndroidElement> driver;
        
       

        [OneTimeSetUp]
        public void StartApp()
        {
            var options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("appPackage", VivinoAppPackage);
            options.AddAdditionalCapability("appActivity", VivinoAppStartUpActivity);

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
            
        }


        [OneTimeTearDown]
        public void CloseApp()
        {
            driver.Quit();
        }


        [Test]
        public void Test1()
        {
            AndroidElement startbutton = driver.FindElement(By.Id("vivino.web.app:id/getstarted_layout"));
            startbutton.Click();

            //AndroidElement resetemailButton = driver.FindElement(By.Id("com.google.android.gms:id/cancel"));
            //resetemailButton.Click(); 

            AndroidElement emailfield = driver.FindElement(By.Id("vivino.web.app:id/edtEmail"));
            emailfield.SendKeys(VivinoTestEmail);
        
            
            AndroidElement passwordfield = driver.FindElement(By.Id("vivino.web.app:id/edtPassword"));
            passwordfield.SendKeys(VivinoTestPassword);

            AndroidElement buttonnext = driver.FindElement(By.Id("vivino.web.app:id/action_next"));
            buttonnext.Click();


            AndroidElement buttonsurch = driver.FindElement(By.XPath("/hierarchy/android.widget.FrameLayout / android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup / android.widget.LinearLayout/android.widget.HorizontalScrollView/android.widget.LinearLayout/androidx.appcompat.app.ActionBar.b[2]/android.widget.ImageView"));
            buttonsurch.Click();

            AndroidElement buttonsurchfield = driver.FindElement(By.Id("vivino.web.app:id/search_header_text"));
            buttonsurchfield.Click();
            
            AndroidElement surchfield = driver.FindElement(By.Id("vivino.web.app:id/editText_input"));
            surchfield.Click();

            surchfield.SendKeys("Katarzyna Reserve Red 2006");

            AndroidElement startsurchbutton = driver.FindElement(By.Id("vivino.web.app:id/imageView_arrow_back"));
            startsurchbutton.Click();
            
            AndroidElement wineimage = driver.FindElement(By.Id("vivino.web.app:id/wineimage"));
            wineimage.Click();


            AndroidElement wineiname = driver.FindElement(By.Id("vivino.web.app:id/wine_name"));
            Assert.That(wineiname.Text, Is.EqualTo("Reserve Red 2006"));

            AndroidElement witingneirating = driver.FindElement(By.Id("vivino.web.app:id/rating"));
            double rating = double.Parse(witingneirating.Text);
            Assert.That(rating is <= 5.00 and >= 1.00);

            

            AndroidElement wineHighlights = driver.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" + 
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"vivino.web.app:id/highlight_description\"))");
            wineHighlights.Click();
            Assert.That(wineHighlights.Text, Is.EqualTo("Among top 1% of all wines in the world"));

            Thread.Sleep(3000);

            AndroidElement factsbutton = driver.FindElement(By.XPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.widget.FrameLayout[2]/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.HorizontalScrollView/android.widget.LinearLayout/android.widget.TextView[2]"));
            factsbutton.Click();

            

            AndroidElement grapestitle = driver.FindElement(By.Id("vivino.web.app:id/wine_fact_title"));
            AndroidElement grapestypes = driver.FindElement(By.Id("vivino.web.app:id/wine_fact_text"));
            string factshold = grapestitle.Text + ": " + grapestypes.Text;
            Assert.That(factshold, Is.EqualTo("Grapes: Cabernet Sauvignon,Merlot"));



        }
    }
}