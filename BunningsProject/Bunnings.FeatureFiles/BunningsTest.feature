Feature: BunningsTest
	Testing previously selected item under "Review Cart" section

Scenario: Verify name of item added to cart matches with name of item in cart.
	Given the user is navigates on the Home page 
	When the user enters Hammer to search textbox
	And the user clicks on the perform search button
	And the user saves the name of 3 st/nd/rd/th item as alias ItemName
	And the user selects the 3 st/nd/rd/th item and add it to cart
	And the user clicks on ReviewNcheckout button
	Then Verify the item name matches the selected ItemName in cart


Scenario: Verify Price of item added to cart matches with price of item in cart.
	Given the user is navigates on the Home page 
	When the user enters Hammer to search textbox
	And the user clicks on the perform search button
	And the user saves the Price of 2 st/nd/rd/th item as alias ItemPrice
	And the user selects the 2 st/nd/rd/th item and add it to cart
	And the user clicks on ReviewNcheckout button
	Then Verify the item price matches the selected ItemPrice in cart
	Then Verify the item total price matches the selected ItemPrice in cart


Scenario Outline: Verify Number of item added to cart matches with number of items in cart.
	Given the user is navigates on the Home page 
	When the user enters Hammer to search textbox
	And the user clicks on the perform search button
	And the user saves the Price of 1 st/nd/rd/th item as alias ItemPrice
	And the user selects the 1 st/nd/rd/th item and add it to cart
	And the user increases the quantity of items to <ItemQuantity> 
	And the user clicks on ReviewNcheckout button
	Then Verify the <ItemQuantity> matches the selected item quantity in cart
	Examples: 
	| ItemQuantity |
	| 5            |
	| 3            |