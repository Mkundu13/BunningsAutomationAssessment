using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunningsProject.Bunnings.Pages
{
    public static class AllPageObjects
    {
        public static void LoadDesktop(IWebDriver driver)
        {
            BunningsHomePage = new BunningsHomePage(driver);
            SearchProductsPage = new SearchProductsPage(driver);
            ReviewCartPage = new ReviewCartPage(driver);
        }

        //Pages 
        public static BunningsHomePage BunningsHomePage;
        public static SearchProductsPage SearchProductsPage;
        public static ReviewCartPage ReviewCartPage;
    }
}
