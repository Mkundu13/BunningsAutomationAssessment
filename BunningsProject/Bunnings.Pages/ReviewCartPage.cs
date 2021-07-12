using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BunningsProject.Bunnings.Pages
{
    public interface IReviewCartPage
    {
        string GetItemName();
        string GetItemPrice();
        string GetTotalPrice();
        string GetTotalQuantity();
    }
    public class ReviewCartPage: IReviewCartPage
    {
        public string itemName = ".ProductName";
        public string itemPrice = "#reviewCartPanel .MuiGrid-root p";
        public string itemTotalPrice = ".orderSummarystyle__StyledTotalPriceContainer-biwwjh-6";
        public string itemTotalQuantity = ".quantityEdit";
        
        IWebDriver Driver;
        public ReviewCartPage(IWebDriver _driver)
        {
            Driver = _driver;
        }

        public string GetItemName()
        {
            string itemNameText = Driver.FindElement(By.CssSelector(itemName)).Text;
            return itemNameText;
        }

        public string GetItemPrice()
        {
            string itemPriceText = Driver.FindElement(By.CssSelector(itemPrice)).Text;
            return itemPriceText;
        }

        public string GetTotalPrice()
        {
            string itemTotalPriceText = Driver.FindElement(By.CssSelector(itemTotalPrice)).Text;
            return itemTotalPriceText;
        }

        public string GetTotalQuantity()
        {
            ReadOnlyCollection<IWebElement> itemTotalQuantityElement = Driver.FindElements(By.CssSelector(itemTotalQuantity));
            string itemTotalQuantityText = itemTotalQuantityElement[0].GetAttribute("value");
            return itemTotalQuantityText;
        }
    }
}
