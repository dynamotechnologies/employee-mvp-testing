Feature: Online Shopping Cart
	1. Register New User with Email Address
		a. Navigate to the Online Shopping Cart (OSC) and verify login page
		b. Register using Email Address
			- Click on Registration link
			- First & Last Name 
			- Email Address
			- Password: 8 Chars, 1 special, 1 number
			- Address: Line 1 & 2, City, State, Zip
			- CC: 16 digits, 2 digit Mon & Yr, 3 digit Security Code
			- 5 Security Q, atleast 1 question to be filled
		c. Submit Registration & verify Product Catalog home page 
		d. logout

	2. Register using Duplicate Email
		a. Navigate to OSC
		b. Use same Email Address and other info to register
		c. Verify error message - Duplicate Email Address used

	3. Register New User with User ID
		a. Navigate to OSC
		b. Register using User Id
		c. Verify home page & logout

	4. Register using Duplicate User ID
		a. Navigate to OSC
		b. Use same User ID and other info to register
		c. Verify error message - Duplicate User ID used

	5. Login Validation
		a. No User ID & No Password, 
		b. Invalid User ID & Password
		c. Valid User ID & Invalid password
		d. Invalid User ID & Valid Password
		e. Correct User ID & Password should now work

	6. Product Catalog home page - Verify Item Name & Price
		a. We're already logged in
		b. Verify 10 catalog items
		c. Verify image for each item/product
		d. Verify Price is displayed

	7. Product Catalog Page - Select items and click on Shopping Cart
		a. Add a number for a few items
		b. Remove item count for one of the selected items
		c. Click on Shopping Cart 
		
	8. Shopping Cart Page
		a. Validate selected items, quantities, and total cost
		b. Remove item count for one item and change the quantity for another item
		c. Update Shopping Cart and verify updates (0 quantity item is removed and quantity updated for the other)
		
	9. From Shopping Cart, go to Product Catalog Page
		a. Click on 'Continue Shopping' and verify Product Catalog Vpage
		b. Verify selected items and count based on updates made in previous scenario
		c. Select 2 additional items, remove 1 item already selected
		d. Click on Shopping Cart 

	10. Shopping Cart Page - Verify final details of product selections
		a. Validate final selection of items, quantities, and total cost

	11. Go to Checkout
		a. Click on Checkout button
		b. Verify Review Order page display

	12. Review Order Page 
		a. Verify selected items, count, and total cost
		b. Verify CC info
		c. Verify Shipping Address (entered during Registration)
		d. Enter a new Shipping Address

	13. Click on Submit Order and verify Confirmation Page
		a. Click on Submit Order 
		b. Verify contents
		c. Log out

	14. When not logged in, navigating to any of the OSC URLs will redirect to Login Page
		a. Without loggin in, navigate to any of the OSC URLs
		b. Verify Login page is displayed

Scenario: 001-Register new user with Email Address
	Given I navigate to the url "http://qa01.cloud.capitissolutions.com/users/sign_in" titled "ShoppingDemo"
	When I click on the link containing the text "Sign up"
	And I set the text box using element id "user_login_id" with the value "olaf"
	And I set the text box using element id "user_email" with the value "olaf_stark@putsbox.com"  
	And I set the text box using element id "user_first_name" with the value "olaf"
	And I set the text box using element id "user_last_name" with the value "stark"
	And I set the text box using element id "user_street_address" with the value "2600 Tower Oaks Blvd"
	And I set the text box using element id "user_city" with the value "Rockville"
	And I set the text box using element id "user_state" with the value "Maryland"
	And I set the text box using element id "user_zip" with the value "20876"
	And I set the text box using element id "user_cc_number" with the value "1234809223001601" 
	And I set the text box using element id "user_cvv" with the value "999"
	And I set the text box using element id "user_cc_exp_date" with the value "01/2020"  
	And I set the text box using element id "user_password" with the value "Autotest123!"
	And I set the text box using element id "user_password_confirmation" with the value "Autotest123!"
	# And I select the drop down text "*** Any Security Question ****" using element id ""
	And I set the text box using element id "user_security_answer" with the value "ruby"
	And I click on the submit button labeled "Sign up"
	# Then the page contains the text "Catalog"

Scenario: 01-Login with valid credentials
	Given I navigate to the url "http://qa01.cloud.capitissolutions.com/users/sign_in" titled "ShoppingDemo"
	When I set the text box using element id "login_email_field" with the value "admin@example.com"
	And I set the text box using element id "login_password_field" with the value "supersecret"
	And I click on the button using element id "login_submit_button"
	Then the page title is "ShoppingDemo"
	And the page contains the text "Catalog"
	
Scenario: 02-Logout
	Given the page is loaded
	When I click on the link containing the text "Logout"
	Then the page contains the text "You need to sign in or sign up before continuing."
	And I close the current page

	@ignore
Scenario: 03- Login Validation - Empty User ID & Password
	Given the page is loaded
	When I set the text box using element id "login_email_field" with the value " "
	And I set the text box using element id "login_password_field" with the value " "
	And I click on the button using element id "login_submit_button"
	Then the page contains the text "Invalid Email or password."

	@ignore
Scenario: 005- Login Validation - Invalid User ID & Invalid Password
	Given the page is loaded
	When I set the text box using element id "login_email_field" with the value "invalidUserId"
	And I set the text box using element id "login_password_field" with the value "invalidPassword"
	And I click on the button using element id "login_submit_button"
	Then the page contains the text "Invalid Email or password."

	@ignore
Scenario: 006- Login Validation - Valid User ID & Invalid Password
	Given the page is loaded
	When I set the text box using element id "login_email_field" with the value "clara@putsbox.com"
	And I set the text box using element id "login_password_field" with the value "invalidPassword"
	And I click on the button using element id "login_submit_button"
	Then the page contains the text "Invalid Email or password."

	@ignore
Scenario: 007- Login Validation - Invalid User ID & Valid Password
	Given the page is loaded
	When I set the text box using element id "login_email_field" with the value "invalidUserId"
	And I set the text box using element id "login_password_field" with the value "Autotest123!"
	And I click on the button using element id "login_submit_button"
	Then the page contains the text "Invalid Email or password."

	@ignore
Scenario: 008- Succesful Login - Valid User ID & Password
	Given the page is loaded
	When I set the text box using element id "login_email_field" with the value "clara@putsbox.com"
	And I set the text box using element id "login_password_field" with the value "Autotest123!"
	And I click on the button using element id "login_submit_button"
	Then the page title is "ShoppingDemo"
	And I close the current page
	
	@ignore
Scenario: 02- Duplicate Email Address
	Given I navigate to the url "" titled ""
	When I click on the link containing the text "" 
	And I set the text box using element id "" with the value "Ravi"
	And I set the text box using element id "" with the value "Radha-EmailAddr"
	And I set the text box using element id "" with the value "ravi.radhakrishnan@variq.com"   
	And I set the text box using element id "" with the value "mypasswd$0"
	And I set the text box using element id "" with the value "2600 Tower Oaks Blvd"
	And I set the text box using element id "" with the value "Rockville"
	And I set the text box using element id "" with the value "Maryland"
	And I set the text box using element id "" with the value "20876"
	And I set the text box using element id "" with the value "5466809223001601" 
	And I set the text box using element id "" with the value "12"  
	And I set the text box using element id "" with the value "19"  
	And I set the text box using element id "" with the value "216" 
	And I select the drop down text "*** Any Security Question ****" using element id ""
	And I click on the button using element id ""  
	Then the page title is ""  
	And the page contains the text ""
	And I close the current page  

	@ignore
Scenario: 03-Register new user with User ID
	Given I navigate to the url "" titled ""
	When I click on the link containing the text ""  
	And I set the text box using element id "" with the value "Ravi"
	And I set the text box using element id "" with the value "Radha-UserID"
	And I set the text box using element id "" with the value "ravi.radhakrishnan"  
	And I set the text box using element id "" with the value "mypasswd$0"
	And I set the text box using element id "" with the value "2600 Tower Oaks Blvd"
	And I set the text box using element id "" with the value "Rockville"
	And I set the text box using element id "" with the value "Maryland"
	And I set the text box using element id "" with the value "20876"
	And I set the text box using element id "" with the value "5466809223001601" 
	And I set the text box using element id "" with the value "12"  
	And I set the text box using element id "" with the value "19"  
	And I set the text box using element id "" with the value "216" 
	And I select the drop down text "*** Any Security Question ****" using element id ""
	And I click on the button using element id ""  
	Then the page title is ""  
	And I click on the link containing the text ""  

	@ignore
Scenario: 04- Duplicate User ID
	Given I navigate to the url "" titled ""
	When I click on the link containing the text ""  
	And I set the text box using element id "" with the value "Ravi"
	And I set the text box using element id "" with the value "Radha-UserId"
	And I set the text box using element id "" with the value "ravi.radhakrishnan"   
	And I set the text box using element id "" with the value "mypasswd$0"
	And I set the text box using element id "" with the value "2600 Tower Oaks Blvd"
	And I set the text box using element id "" with the value "Rockville"
	And I set the text box using element id "" with the value "Maryland"
	And I set the text box using element id "" with the value "20876"
	And I set the text box using element id "" with the value "5466809223001601" # CC Number
	And I set the text box using element id "" with the value "12"  # CC Exp Month
	And I set the text box using element id "" with the value "19"  # CC Exp Year
	And I set the text box using element id "" with the value "216" # CC Sec Code
	And I select the drop down text "*** Any Security Question ****" using element id ""
	And I click on the button using element id ""  # Register Button
	Then the page title is ""  # Duplicate Email Address error page
	And the page contains the text ""
	And I close the current page  # Close browser

	@ignore
Scenario Outline: 10- Verify Product Catalog page - Item Name & Price
	Given the page is loaded
	When I access the table using element id ""  # Product Catalog table
	Then table row number <Table Row Number> contains the values <Data> from the following table
	
	Examples: 
	| Table Row Number | Data   |
	| 1                | Item 1 |
	| 1                | 60.00  |
	| 2                | Item 2 |
	| 2                | 70     |
	| 3                | Item 3 |
	| 3                | 80     |
	| 4                | Item 4 |
	| 4                | 90     |
	| 5                | Item 5 |
	| 5                | 100    |

	@ignore
Scenario: 11- Product Catalog - Select items and click on Shopping Cart
	Given the page is loaded
	# Set Quantity required for required items in Product Catalog
	And I click on the button using element id ""  # Shopping Cart Button
	Then the page title is ""  # Shopping Cart page

	@ignore
Scenario Outline: 12-Verify Shopping Cart 
	Given the page is loaded
	When I access the table using element id ""  # Shopping Cart table
	Then the text box using element id "" contains the value ""  # Total Cost
	Then table row number <Table Row Number> contains the values <Data> from the following table
	
	Examples: 
	| Table Row Number | Data   |
	| 1                | Item 1 |
	| 1                | 1      |
	| 1                | 60.00  |
	| 2                | Item 2 |
	| 2                | 2      |
	| 2                | 70     |

	@ignore
Scenario: 13-From Shopping Cart go to Product Catalog Page
	Given the page is loaded
	When I click on the button using element id ""  # Return to Product Catalog
	And the page title is ""  # Product Catalog page
	# Set 1 additional item's quantity
	And I click on the button using element id ""  # Shopping Cart Button
	Then the page title is ""  # Shopping Cart page

	@ignore
Scenario Outline: 14-Shopping Cart Page - Verify final details of product selections
	Given the page is loaded
	Then the text box using element id "" contains the value ""  # Total Cost
	And table row number <Table Row Number> contains the values <Data> from the following table
	
	Examples: 
	| Table Row Number | Data   |
	| 1                | Item 1 |
	| 1                | 1      |
	| 1                | 60.00  |
	| 2                | Item 2 |
	| 2                | 2      |
	| 2                | 70     |
	| 3                | Item 3 |
	| 3                | 3      |
	| 3                | 90     |

	@ignore
Scenario: 15- Go to Checkout
	Given the page is loaded
	When I click on the button using element id ""  # Review Order page
	Then the page title is ""  # Review Order page

	@ignore
Scenario Outline: 16- Verify Review Order Page - Item Names, Count, Cost, CC, Shipping Address
	Given the page is loaded
	Then the text box using element id "" contains the value ""  # Total Cost
	Then the text box using element id "" contains the value ""  # CC Info
	Then the text box using element id "" contains the value ""  # Mailing Address
	And table row number <Table Row Number> contains the values <Data> from the following table
	
	Examples: 
	| Table Row Number | Data   |
	| 1                | Item 1 |
	| 1                | 1      |
	| 1                | 60.00  |
	| 2                | Item 2 |
	| 2                | 2      |
	| 2                | 70     |
	| 3                | Item 3 |
	| 3                | 3      |
	| 3                | 90     |

	@ignore
Scenario: 17- Click on Submit Order and verify Confirmation Page
	Given the page is loaded
	When I click on the button using element id ""  # Submit Order button
	Then the page title is ""  # Confirmation Page
	And the page contains the text ""  # Confirmation message