using BunningsProject.Bunnings.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Shouldly;

namespace BunningsProject.Bunnings.StepFiles
{
    [Binding]
    public class AddToCartSteps
    {

        [Given(@"the user is navigates on the Home page")]
        public void GivenTheUserIsNavigatesOnTheHomePage()
        {
            AllPageObjects.BunningsHomePage.IsBunningsHomePageVisible().ShouldBe(true);
        }

        [When(@"the user enters (.*) to search textbox")]
        public void GivenTheUserEntersTextToSearchTextbox(string searchText)
        {
            AllPageObjects.BunningsHomePage.SearchText(searchText);
        }

        [When(@"the user clicks on the perform search button")]
        public void GivenTheUserClicksOnTheSearchIconElement()
        {
            AllPageObjects.BunningsHomePage.PerformSearch();
        }

        [When(@"the user selects the (.*) st/nd/rd/th item and add it to cart")]
        public void WhenTheUserSelectsTheStNdRdThItemAndAddItToCart(char number)
        {
            AllPageObjects.SearchProductsPage.AddtoCart(number);
        }

        [When(@"the user clicks on ReviewNcheckout button")]
        public void WhenTheUserClicksOnReviewNcheckoutButton()
        {
            AllPageObjects.SearchProductsPage.ReviewNcheckout();
        }


        [When(@"the user saves the name of (.*) st/nd/rd/th item as alias (.*)")]
        public void WhenTheUserSavesTheNameOfStNdRdThItemAsAliasItemName(char number, string Alias)
        {
            string itemNameText = AllPageObjects.SearchProductsPage.GetItemName(number);
            ScenarioContext.Current.Add(Alias, itemNameText);
        }

        [When(@"the user saves the Price of (.*) st/nd/rd/th item as alias (.*)")]
        public void WhenTheUserSavesThePriceOfStNdRdThItemAsAliasItemPrice(char number, string Alias)
        {
            string itemPriceText = AllPageObjects.SearchProductsPage.GetItemPrice(number);
            ScenarioContext.Current.Add(Alias, itemPriceText);
        }

        [When(@"the user increases the quantity of items to (.*)")]
        public void WhenTheUserIncreasesTheQuantityOfItemsTo(string quantity)
        {
            AllPageObjects.SearchProductsPage.SetQuantity(quantity);
        }

        [When(@"the user clicks on the Cart icon")]
        public void WhenTheUserClicksOnTheCartIcon()
        {
            AllPageObjects.SearchProductsPage.ClickCartIcon();
        }

        [Then(@"Verify the (.*) matches the selected item quantity in cart")]
        public void ThenVerifyTheMatchesTheSelectedItemQuantityInCart(string quantity)
        {
            string itemTotalQuantityInCart = AllPageObjects.ReviewCartPage.GetTotalQuantity();
            quantity.ShouldBe(itemTotalQuantityInCart, "Total Item Quantity in cart not matching with selected item quantity");
        }

        [Then(@"Verify the item name matches the selected (.*) in cart")]
        public void ThenVerifyTheItemNameMatchesTheSelectedItemNameInCart(string Alias)
        {
            string itemNameText = ScenarioContext.Current[Alias].ToString();
            string itemNameInCart = AllPageObjects.ReviewCartPage.GetItemName();

            itemNameText.ShouldBe(itemNameInCart, "Item name in cart not matching with selected item");
        }


        [Then(@"Verify the item price matches the selected (.*) in cart")]
        public void ThenVerifyTheItemPriceMatchesTheSelectedItemNameInCart(string Alias)
        {
            string itemPriceText = ScenarioContext.Current[Alias].ToString();
            string itemPriceInCart = AllPageObjects.ReviewCartPage.GetItemPrice();

            itemPriceText.ShouldBe(itemPriceInCart, "Item price in cart not matching with selected item");
        }

        [Then(@"Verify the item total price matches the selected (.*) in cart")]
        public void ThenVerifyTheItemTotalPriceMatchesTheSelectedItemPriceInCart(string Alias)
        {
            string itemPriceText = ScenarioContext.Current[Alias].ToString();
            string itemTotalPriceInCart = AllPageObjects.ReviewCartPage.GetTotalPrice();

            itemPriceText.ShouldBe(itemTotalPriceInCart, "Total Item price in cart not matching with selected item");
        }
    }
}
