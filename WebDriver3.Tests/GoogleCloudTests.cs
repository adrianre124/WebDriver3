using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V124.IndexedDB;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriver3.Tests;

public class Tests
{
    public IWebDriver Driver { get; set; }
    public WebDriverWait Wait { get; set; }

    [SetUp]
    public void Setup()
    {
        this.Driver = new ChromeDriver();
        this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(30));
        this.Driver.Manage().Window.Maximize();
    }

    //[TearDown]
    //public void TeardownTest()
    //{
    //    this.Driver.Dispose();
    //}

    [Test]
    public void GoogleCloud_EstimateEngineCost_ReturnEstimatedCost()
    {
        string searchMessage = "Google Cloud Platform Pricing Calculator";
        string numberOfInstances = "4";
        string expected = "Total Estimated Cost: USD 899.76 per 1 month";

        var googleCloud = new GoogleCloud(this.Driver);

        googleCloud.Navigate();
        googleCloud.Search(searchMessage);

        // New Caculator - TODO
        //this.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"___gcse_0\"]/div/div/div/div[5]/div[2]/div/div/div[1]/div[1]/div/div[1]/div/a"))).Click();
        //this.Driver.FindElement(By.XPath("//*[@id=\"ucj-1\"]/div/div/div/div/div/div/div/div[1]/div/div/div/div/button")).Click();
        //this.Driver.FindElement(By.XPath("//*[@id=\"yDmH0d\"]/div[6]/div[2]/div/div[3]/div/div[2]/div/div/div[1]/div/div")).Click();

        //var numOfInstances = this.Driver.FindElement(By.XPath("//*[@id=\"c50\"]"));
        //numOfInstances.Clear();
        //numOfInstances.SendKeys(numberOfInstances);

        //this.Driver.FindElement(By.XPath("//*[@id=\"ow4\"]/div/div/div/div/div/div/div[1]/div/div/div/div/div[2]/div[3]/div[11]/div/div/div[2]/div/div[1]/div[3]/div/div/div/div[1]")).Click();


        // Old Calculator
        this.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"___gcse_0\"]/div/div/div/div[5]/div[2]/div/div/div[1]/div[2]/div/div[1]/div/a"))).Click();
        this.Driver.SwitchTo().Frame(0);
        this.Driver.SwitchTo().Frame(0);
        this.Driver.FindElement(By.XPath("//*[@id=\"input_100\"]")).SendKeys(numberOfInstances);

        var js = (IJavaScriptExecutor)this.Driver;
        js.ExecuteScript("window.scrollBy(0,500)", "");

        IWebElement seriesSelect = this.Driver.FindElement(By.XPath("//*[@id=\"select_125\"]"));
        js.ExecuteScript("arguments[0].click()", seriesSelect);
        IWebElement n1 = this.Driver.FindElement(By.XPath("//*[@id=\"select_option_224\"]"));
        js.ExecuteScript("arguments[0].click()", n1);

        IWebElement machineTypeSelect = this.Driver.FindElement(By.XPath("//*[@id=\"select_127\"]"));
        js.ExecuteScript("arguments[0].click()", machineTypeSelect);
        IWebElement n1Standard8 = this.Driver.FindElement(By.XPath("//*[@id=\"select_option_474\"]"));
        js.ExecuteScript("arguments[0].click()", n1Standard8);

        js.ExecuteScript("window.scrollBy(0,1000)", "");

        // Add GPUs checkbox
        IWebElement gpuCheckbox = this.Driver.FindElement(By.XPath("//*[@id=\"mainForm\"]/div[2]/div/md-card/md-card-content/div/div[1]/form/div[13]/div[1]/md-input-container/md-checkbox"));
        js.ExecuteScript("arguments[0].click()", gpuCheckbox);
        Thread.Sleep(1000);
        // GPU Type
        IWebElement gpuType = this.Driver.FindElement(By.XPath("//*[@id=\"select_510\"]"));
        js.ExecuteScript("arguments[0].click()", gpuType);
        IWebElement teslaT4 = this.Driver.FindElement(By.XPath("//*[@id=\"select_option_517\"]"));
        js.ExecuteScript("arguments[0].click()", teslaT4);
        js.ExecuteScript("window.scrollBy(0,500)", "");
        Thread.Sleep(1000);
        // Number of GPUs
        IWebElement gpuNumber = this.Driver.FindElement(By.XPath("//*[@id=\"select_512\"]"));
        js.ExecuteScript("arguments[0].click()", gpuNumber);
        Thread.Sleep(1000);
        IWebElement gpu1 = this.Driver.FindElement(By.XPath("//*[@id=\"select_option_520\"]"));
        js.ExecuteScript("arguments[0].click()", gpu1);
        Thread.Sleep(1000);

        js.ExecuteScript("window.scrollBy(0,500)", "");

        IWebElement locationSelect = this.Driver.FindElement(By.XPath("//*[@id=\"select_133\"]"));
        js.ExecuteScript("arguments[0].click()", locationSelect);
        IWebElement location = this.Driver.FindElement(By.XPath("//*[@id=\"select_option_268\"]"));
        js.ExecuteScript("arguments[0].click()", location);

        IWebElement usageSelect = this.Driver.FindElement(By.XPath("//*[@id=\"select_140\"]"));
        js.ExecuteScript("arguments[0].click()", usageSelect);
        Thread.Sleep(1000);
        IWebElement usage = this.Driver.FindElement(By.XPath("//*[@id=\"select_option_138\"]"));
        js.ExecuteScript("arguments[0].click()", usage);

        Thread.Sleep(20000);

        this.Driver.FindElement(By.XPath("//*[@id=\"mainForm\"]/div[2]/div/md-card/md-card-content/div/div[1]/form/div[20]/button")).Click();

        var totalEstimate = this.Driver.FindElement(By.XPath("//*[@id=\"resultBlock\"]/md-card/md-card-content/div/div/div/div[1]/h2/b")).Text;

        Assert.AreEqual(expected, totalEstimate);
    }
}