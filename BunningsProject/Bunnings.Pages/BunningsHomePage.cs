using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BunningsProject.Bunnings.Pages
{
    public interface IBunningsHomePage
    {
        bool IsBunningsHomePageVisible();
        void SearchText(string searchText);
        void PerformSearch();
     }
    public class BunningsHomePage : IBunningsHomePage
    {
        public const string searchTextbox = "#custom-css-outlined-input";
        public const string performSearch = "#crossIcon";
  
        IWebDriver Driver;
        public BunningsHomePage(IWebDriver _driver)
        {
            Driver = _driver;
        }

        public bool IsBunningsHomePageVisible()
        {
            return Driver.FindElement(By.CssSelector("#main-header")).Displayed;
        }

        public void SearchText(string searchText)
        {
            Driver.FindElement(By.CssSelector(searchTextbox)).SendKeys(searchText);
        }

        public void PerformSearch()
        {
            Driver.FindElement(By.CssSelector(performSearch)).Click();
            Thread.Sleep(3000);
        }
    }
}
