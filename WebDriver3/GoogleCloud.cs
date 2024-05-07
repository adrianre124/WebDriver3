using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WebDriver3;

public class GoogleCloud
{
    private readonly IWebDriver driver;
    private readonly string url = @"https://cloud.google.com";

    public GoogleCloud(IWebDriver browser)
    {
        this.driver = browser;
        PageFactory.InitElements(browser, this);
    }

    [FindsBy(How = How.ClassName, Using = "p1o4Hf")]
    public IWebElement SearchButton { get; set; }

    [FindsBy(How = How.ClassName, Using = "mb2a7b")]
    public IWebElement SearchBar { get; set; }

    public void Navigate()
    {
        this.driver.Navigate().GoToUrl(url);
    }

    public void Search(string searchText)
    {
        this.SearchButton.Click();
        this.SearchBar.SendKeys(searchText + Keys.Enter);
    }
}
