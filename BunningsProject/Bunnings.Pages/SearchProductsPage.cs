using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BunningsProject.Bunnings.Pages
{

    public interface ISearchProductsPage
    {
        string GetItemName(char number);
        void AddtoCart(char number);
        void ReviewNcheckout();
        string GetItemPrice(char number);
        void SetQuantity(string quantity);
        void ClickCartIcon();
    }
    public class SearchProductsPage : ISearchProductsPage
    {
        public string itemName = "article:nth-child(N) .text-rating-container a p";
        public string itemPrice = "article:nth-child(N) .bottom-cart p";
        public string reviewNcheckout = "#confirmation-drawer a > button";
        public string setQuantity = "article .wrap input";
        public string clickCartIcon = "#icon-cart";
        public string addtoCart = "article:nth-child(N) button span.MuiButton-label";

        IWebDriver Driver;
        public SearchProductsPage(IWebDriver _driver)
        {
            Driver = _driver;
        }

        public string GetItemName(char number)
        {
            itemName = itemName.Replace('N', number);
            string itemNameText = Driver.FindElement(By.CssSelector(itemName)).Text;
            return itemNameText;
        }

        public void AddtoCart(char number)
        {
            addtoCart = addtoCart.Replace('N', number);

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(Driver => Driver.FindElement(By.CssSelector(addtoCart)));

            IWebElement button = Driver.FindElement(By.CssSelector(addtoCart));
            Actions action = new Actions(Driver);
            action.MoveToElement(button).Click().Build().Perform();
        }

        public string GetItemPrice(char number)
        {
            itemPrice = itemPrice.Replace('N', number);

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(Driver => Driver.FindElement(By.CssSelector(itemPrice)));

            string itemPriceText = Driver.FindElement(By.CssSelector(itemPrice)).Text;
            return itemPriceText;
        }

        public void ReviewNcheckout()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(Driver => Driver.FindElement(By.CssSelector(reviewNcheckout)));

            Driver.FindElement(By.CssSelector(reviewNcheckout)).Click();
        }

        public void SetQuantity(string quantity)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(Driver => Driver.FindElement(By.CssSelector(setQuantity)));

            Driver.FindElement(By.CssSelector(setQuantity)).Clear();
            Driver.FindElement(By.CssSelector(setQuantity)).SendKeys(quantity);
        }

        public void ClickCartIcon()
        {
            Driver.FindElement(By.CssSelector(clickCartIcon)).Click();
        }

    }
}
