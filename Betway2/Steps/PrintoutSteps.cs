using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Collections.Generic;
using Betway2.Utils;
using Betway2.Pages;
using System.Threading;

namespace Betway2
{

    [Binding]
    public class PrintoutSteps
    {

        public static IWebDriver driver;

        [Given(@"I have navigated to the specified website")]
        public void GivenIHaveNavigatedToTheSpecifiedWebsite(Table table)
        {
            //Store paramters in dictionary table
            var website = table.CreateInstance<Website>();
            //Initialise browser
            driver = FunctionLibrary.BrowserInit(website.browser);
            //Go to specified URL
            driver.Url = website.url;
        }

        [Given(@"I see that the page has fully loaded")]
        public void GivenISeeThatThePageHasFullyLoaded()
        {
            //Wait fo page to be fully loaded
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            int timeoutSec = 15;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
            
        }

        [Then(@"I want a printout of all the (.*) displayed on the page")]
        public void ThenIWantAPrintOutOfDetailsOnThePage(string details)
        {
            IList<IWebElement> elements = null;

            //Swicth code path based on scenario
            switch (details)
            {
                case "NewsHeadlines":
                    //Get all elements with headlines
                    elements = driver.FindElements(By.XPath(GoogleNewsPage.Headlines));
                    break;
                case "LiveGames":
                    //Get the Live Now link element
                    IWebElement LiveNowLink = driver.FindElement(By.XPath(BetwayPage.LiveNowLink));
                    //Wait for Live Now link to be displayed
                    FunctionLibrary.WaitForElement(LiveNowLink, 5);
                    //Click the Live Now link
                    LiveNowLink.Click();
                    //Get all Live Games displayed on page
                    elements = driver.FindElements(By.XPath(BetwayPage.LiveGames));
                    break;
            }

            driver = null;
            //Print out the list to Excel file
            FunctionLibrary.PrintoutToExcel(elements, details);
        }
        
    [AfterScenario]
    public void CloseBrowser()
        {
            //Close Browser
            FunctionLibrary.CurrentDriver.Quit();
        }
    }
}
