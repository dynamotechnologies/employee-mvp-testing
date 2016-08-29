Feature: NewEmployeeMVP
	Scenario 1:  Peer Kudos Dashboard - Valid Login Credentials
	Enter valid username/password combo, verify Peer Kudos Dashboard page

	Scenario 2: Peer Kudos Dashboard - Invalid Login Credentials
	Enter invalid username/password, verify error message and prompt to reenter credentials

	Scenario 3: Peer Kudos Dashboard - Employee of the Month
	Verify First & Last Name of employee with the Maximum Stars during the last month
	View count of stars displayed
	Tie-Out ??? - view runner ups & count of stars for each

	Scenario 4: Peer Kudos Dashboard - Current Month Top 5 Employees Ranks
	Top 5 Ranked Employees 
	In Descending order of count of stars for current month

	Scenario 5: Peer Kudos Dashboard - Search Employee
	Search an employee using substring of First or Last Name (to select them for a kudo)
	Results are sorted by First Name

	Scenario 6: Peer Kudos Dashboard - Assign stars to employees in Top 5
	Click next to name of "top-5" employee and enter stars (1, 2, or 3 only)

	Scenario 7: Peer Kudos Dashboard - Search Employee and Assign stars
	Search for an employee
	Click next to name of employee in search results and enter stars (1, 2, or 3 only)
	
	Scenario 8: Peer Kudos Dashboard - My Current Month Ranking
	Next to my name, view count of stars I collected for current month
	Next to my name, view count of stars I have given out for current month
	Next to my name, view count of stars I have remaining for current month
	View my Star Ranking Order 

	Scenario 9: Admin - Login with Valid Credentials
	Admin users with valid credentials (marked as Admin users - where ??) are directed to Peer Kudos Admin Employee Directory page

	Scenario 10: Admin - Login with Invalid Credentials
	Admin users with invalid credentials (or not marked as Admin users - where ??) -- error message is displayed

	Scenario 11: Admin - View Employee Directory
	Admin can view list of all employees sorted by name

	Scenario 12: Admin - Update Employee Info
	Admin user can select any employee and update all info
	Admin user can save and verify updates
	Updates must follow uniqueness for Username, First & Last Name

	Scenario 13: Admin - Add Employee
	Admin user can create a new employee - First, Last Name, Username, Password, Admin (yes/no)

	Scenario 14: Admin - Add Employee (verify duplicate errors)
	Duplicate First + Last Name combination displays errors
	Updating to remove duplicates allows to save new employee
	Duplicate Username shoudl display errors
	Updating to remove duplicate username allows to save

	Scenario 15: Admin - Delete Employees
	Admin can delete an employee and accept confirmation dialog.
	Search for the deleted employee should return no records

	Scenario 16: Reports: Report by Month
	Admin selects 'Report by Month' - select Month/Year
	List of employees displayed with stars for that month in Descending Order followed by First/Last Name Asending Order

	Scenario 17: Reports: Report by Employee
	Admin selects 'Report by Employee' - enter partial employee First/Last Name
	Select from result employee list
	Result displays count of stars collected received each Month

	Scenario 18: Max 10 stars can be given out for current month
	Verify max 10 stars can be given out for current month
	Exceeding limit prompts error message

	Scenario 19: Peer Kudos Dashboard - Logout of the application

Scenario: 01-Invalid Non-Admin Login Credentials
	Given I navigate to the url "http://env03.cloud.capitissolutions.com" titled "Employee Kudos MVP"
	When I set the text box using element id "employee_username" with the value "max"
	And I set the text box using element id "employee_password" with the value "invalidPassword"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Invalid Username or password"

Scenario: 02-Invalid Admin Login Credentials
	Given the page is loaded
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "invalidPassword"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Invalid Username or password"

Scenario: 03-Valid Non-Admin Login Credentials 
	Given the page is loaded
	When I set the text box using element id "employee_username" with the value "max"
	And I set the text box using element id "employee_password" with the value "maximumax"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Signed in successfully"
	
Scenario: 04-Logout as a Regular User
	Given the page is loaded
	When I click on the link containing the text "Sign out"
	Then the page contains the text "You need to sign in or sign up before continuing"
	
Scenario: 05-Valid Admin Login Credentials 
	Given the page is loaded
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "alexrules"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Signed in successfully"

Scenario Outline: 06-Non-Admin-Validate Profile fields
	Given the page is loaded
	Then the page contains the text <TextOnPage> from the following table

	Examples: 
	| TextOnPage      |
	| Name            |
	| Kudos Received  |
	| Kudos to Give   |
	| Current Ranking |
	
Scenario: 06-Non-Admin-Search Employee Table with Full First Name
	Given the page is loaded
	When I set the text box using name attribute "q" with the value "Max"
	And I click on the submit button containing the text "Search!"
	And I access the table using tag "table" attribute type "id" attribute value "Give-Kudos-Table"
	Then table row number "2" column number "1" contains the value "Max"
	And table row number "2" column number "2" contains the value "Brailovsky"

Scenario: 07-Non-Admin-Search Employee Table with Partial Name
	Given the page is loaded
	When I set the text box using name attribute "q" with the value "a"
	And I click on the submit button containing the text "Search!"
	And I access the table using tag "table" attribute type "id" attribute value "Give-Kudos-Table"
	Then the count of rows in the table is greater than "1"

Scenario: 08-Non-Admin-Peer Kudos Dashboard - Current Month Top 5 Employees Ranks
	# Top 5 Ranked Employees 
	Given the page is loaded
	When I access the table using element id "leaderboard-table"
	Then the count of rows in the table is greater than "2"

	# Row 1 is header; Ranks of Employees displayed starting row 2
	And table row number "2" column number "1" contains the value "1"
	And table row number "3" column number "1" contains the value "2"

Scenario: 07-Non-Admin-Assign stars to employees displayed in Search Results
	Given the page is loaded
	When I set the text box using name attribute "q" with the value "Max"
	And I click on the submit button containing the text "Search!"
	And I search
	And I access the table using tag "table" attribute type "id" attribute value "Give-Kudos-Table"
	And I save the data from table row number "2" column number "3" into the variable "initialKudos"
	# And I add the numbers contained in the string "[initialKudos], 2" and store into the variable "updatedKudos"
	And I click on the element using tag "span" attribute type "data-kudos-amount" attribute value "1" in row number "2"
	And I set the text box using element id "kudo_transaction" with the value "Awesome!"
	#And I click on the button using class name "give-btn"
	#Then table row number "2" column number "3" contains the value stored in the variable "updatedKudos"
	
Scenario: 09-Admin Login Credentials
	Given I navigate to the url "http://env03.cloud.capitissolutions.com" titled "Employee Kudos MVP"
	# Given I navigate to the url "https://employee-mvp.herokuapp.com" titled "Employee Kudos MVP"
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "alexrules"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Signed in successfully"

Scenario: 10-Admin-Manage Employees
	Given the page is loaded
	When I click on the link containing the text "Manage Employees"
	Then the page contains the text "Admin Dashboard"

Scenario: 11-Admin-Add New Employee
	Given the page is loaded
	When I click on the link containing the text "Add New Employee"
	And the page contains the text "Add New Employee"

	# Generate Unique User Name
	And I save a random string with "8" characters into the variable "username"
	And I set the text box using element id "employee_username" with the value stored in the variable "username"
	#And I set the text box using element id "employee_username" with the value "testuser"
	
	And I set the text box using element id "employee_password" with the value "Autotest123!"
	And I set the text box using element id "employee_password_confirmation" with the value "Autotest123!"

	# Generate Unique Email Address
	And I append the following strings "[username],@,test.com" and save the result into the variable "emailAddress"
	And I set the text box using element id "employee_email" with the value stored in the variable "emailAddress"
	
	And I set the text box using element id "employee_first_name" with the value "TestUser-FN"
	And I set the text box using element id "employee_last_name" with the value "User-LN"
	And I set the checkbox with element id "employee_is_admin" to "true"
	And I click on the submit button labeled "Save changes"
	Then the page contains the text "Employee was successfully created"


	@ignore
Scenario: 72-Peer Kudos Dashboard - Employee of the Previous Month
	# Verify First & Last Name of employee with the Maximum Stars during the last month
	Given the page is loaded
	Then the page using element id "Panel-Last Month's Winner" and attribute type "div" contains the text "Alex Duan" 


	@ignore
Scenario: 73-Peer Kudos Dashboard - Assign stars to employees in Top 5
	#Click next to name of "top-5" employee and select stars (1, 2, or 3 only), Verify New Total
	Given the page is loaded
	When I save the data using element id "top_1_of_5_employee" into the variable "currentStarCount"
	And I click on the button using element id "3-star"
	
	# New Step to add 2 integers
	# And I add the numbers contained in the string "[currentStarCount], 3" and store into the variable "totalStars"

	# New Step
	# Then the text box using element id "top_1_of_5_employee" contains the value stored in the variable "totalStars"
	

	@ignore
Scenario: 75-Peer Kudos Dashboard - My Current Month Ranking
	#Next to my name, view count of stars I collected for current month
	#Next to my name, view count of stars I have given out for current month
	#Next to my name, view count of stars I have remaining for current month
	#View my Star Ranking Order
	Given the page is loaded

	#New Step using WebElementExtensions.getTextOfElement()
	Then the element using element Id "Panel-Last Month's Winner" contains the text ""
	Then the element using element Id "my_star_given_count" contains the text ""
	Then the element using element Id "my_star_remaining_count" contains the text ""
	And I close the current page

	@ignore
Scenario: 76-Admin - Login
	#Admin users with valid credentials (marked as Admin users - where ??) are directed to Peer Kudos Admin Employee Directory page
	#Admin users with invalid credentials (or not marked as Admin users - where ??) -- error message is displayed
	Given I navigate to the url "http://env03.cloud.capitissolutions.com" titled "ShoppingDemo"
	When I set the text box using element id "login_email_field" with the value "admin@example.com"
	And I set the text box using element id "login_password_field" with the value "supersecret"
	And I click on the button using element id "login_submit_button"
	Then the page title is "ShoppingDemo"
	And the page contains the text "Catalog"

	@ignore
Scenario: 77-Peer Kudos Dashboard - Valid Admin Login Credentials
	Given I navigate to the url "http://env03.cloud.capitissolutions.com" titled "Employee Kudos MVP"
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "alexrules"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Profile"
	And the page contains the text "Kudos Received"

	@ignore
Scenario: 78-Admin - Login with Valid Credentials
	Given the page is loaded
	When I set the text box using element id "login_email_field" with the value "invalidUserId"
	And I set the text box using element id "login_password_field" with the value "invalidPassword"
	And I click on the button using element id "login_submit_button"
	Then the page contains the text "Invalid Email or password."


