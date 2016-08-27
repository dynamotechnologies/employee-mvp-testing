Feature: EmployeeMVP
	Scenario 1:  VD-24 - Peer Kudos Dashboard - Valid Login Credentials
	Enter valid username/password combo, verify Peer Kudos Dashboard page

	Scenario 2: VD-24 - Peer Kudos Dashboard - Invalid Login Credentials
	Enter invalid username/password, verify error message and prompt to reenter credentials

	Scenario 3: VD-25 - Peer Kudos Dashboard - Employee of the Month
	Verify First & Last Name of employee with the Maximum Stars during the last month
	View count of stars displayed
	Tie-Out ??? - view runner ups & count of stars for each

	Scenario 4: VD-26 - Peer Kudos Dashboard - Current Month Top 5 Employees Ranks
	Top 5 Ranked Employees 
	In Descending order of count of stars for current month

	Scenario 5: VD-28 - Peer Kudos Dashboard - Search Employee
	Search an employee using substring of First or Last Name (to select them for a kudo)
	Results are sorted by First Name

	Scenario 6: VD-29 - Peer Kudos Dashboard - Assign stars to employees in Top 5
	Click next to name of "top-5" employee and enter stars (1, 2, or 3 only)

	Scenario 7: VD-29 - Peer Kudos Dashboard - Search Employee and Assign stars
	Search for an employee
	Click next to name of employee in search results and enter stars (1, 2, or 3 only)
	
	Scenario 8: VD-27 - Peer Kudos Dashboard - My Current Month Ranking
	Next to my name, view count of stars I collected for current month
	Next to my name, view count of stars I have given out for current month
	Next to my name, view count of stars I have remaining for current month
	View my Star Ranking Order 

	Scenario 9: VD-32 - Admin - Login with Valid Credentials
	Admin users with valid credentials (marked as Admin users - where ??) are directed to Peer Kudos Admin Employee Directory page

	Scenario 10: VD-32 - Admin - Login with Invalid Credentials
	Admin users with invalid credentials (or not marked as Admin users - where ??) -- error message is displayed

	Scenario 11: VD-33 - Admin - View Employee Directory
	Admin can view list of all employees sorted by name

	Scenario 12: VD-35 - Admin - Update Employee Info
	Admin user can select any employee and update all info
	Admin user can save and verify updates
	Updates must follow uniqueness for Username, First & Last Name

	Scenario 13: VD-34 - Admin - Add Employee
	Admin user can create a new employee - First, Last Name, Username, Password, Admin (yes/no)

	Scenario 14: VD-34 - Admin - Add Employee (verify duplicate errors)
	Duplicate First + Last Name combination displays errors
	Updating to remove duplicates allows to save new employee
	Duplicate Username shoudl display errors
	Updating to remove duplicate username allows to save

	Scenario 15: VD-36 - Admin - Delete Employees
	Admin can delete an employee and accept confirmation dialog.
	Search for the deleted employee should return no records

	Scenario 16: VD-38 - Reports: Report by Month
	Admin selects 'Report by Month' - select Month/Year
	List of employees displayed with stars for that month in Descending Order followed by First/Last Name Asending Order

	Scenario 17: VD-38 - Reports: Report by Employee
	Admin selects 'Report by Employee' - enter partial employee First/Last Name
	Select from result employee list
	Result displays count of stars collected received each Month

	Scenario 18: VD-38 - Max 10 stars can be given out for current month
	Verify max 10 stars can be given out for current month
	Exceeding limit prompts error message

	Scenario 19: Peer Kudos Dashboard - Logout of the application

Scenario: 01-VD-24 - Peer Kudos Dashboard - Valid Login Credentials
	Given I navigate to the url "http://env03.cloud.capitissolutions.com" titled "Employee Kudos MVP"
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "alexrules"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Profile"
	And the page contains the text "Kudos Received"
	
Scenario: 02-VD-24 - Peer Kudos Dashboard - Invalid Login Credentials
	Given I navigate to the url "http://env03.cloud.capitissolutions.com" titled "Employee Kudos MVP"
	When I set the text box using element id "employee_username" with the value "invalidUserid"
	And I set the text box using element id "employee_password" with the value "invalidPassword"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Invalid Username or password"

Scenario: 03-Log back into the App
	Given the page is loaded
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "alexrules"
	And I click on the submit button labeled "Log in"
	Then the page contains the text "Profile"
	
	@ignore
Scenario Outline: 04-Verify Employee Table
	Given the page is loaded
	When I access the table using tag "div" attribute type "class" attribute value "panel"
	Then table row number <Table Row Number> contains the values <Data> from the following table

	Examples: 
	| Table Row Number | Data       |
	| 1                | Max        |
	| 1                | Brailovsky |
	| 1                | 7          |
	| 2                | Bella      |
	| 2                | Trenkova   |
	| 2                | 0          |

	@ignore
Scenario: 03-VD-25 - Peer Kudos Dashboard - Employee of the Previous Month
	# Verify First & Last Name of employee with the Maximum Stars during the last month
	# View count of stars displayed
	# Tie-Out - view runner ups & count of stars for each
	Given I navigate to the url "http://env03.cloud.capitissolutions.com" titled "Employee Kudos MVP"
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "alexrules"
	And I click on the submit button labeled "Log in"

	# Count ot Employee of the Month
	When I access the table using element id "top_employees_of_last_month"
	Then the count of rows in the table is ""

	# First & Last Name, Star Count, Ties - requires Employee Grid Processing
	#Then table row number "1" column number "1" contains the value ""
	
	@ignore
Scenario: 04-VD-26 - Peer Kudos Dashboard - Current Month Top 5 Employees Ranks
	# Top 5 Ranked Employees 
	Given the page is loaded
	When I access the table using element id "top_5_employees_of_the_current_month"
	Then the count of rows in the table is "5"

	# In Descending order of count of stars for current month ==> requires Employee Grid Processing
	
	@ignore
Scenario: 05-VD-28 - Peer Kudos Dashboard - Search Employee
	#Search an employee using substring of First or Last Name (to select them for a kudo)
	Given the page is loaded
	When I set the text box using element id "employee_username" with the value "alex"
	And I set the text box using element id "employee_password" with the value "alexrules"
	And I click on the submit button labeled "Log in"
	And I set the text box using name attribute "q" with the value "Max"
	And I click on the submit button containing the text "Blast Off!"
	And I access the table using element id "employee_table"
	Then table row number "1" column number "1" contains the value "Max" 
	And table row number "1" column number "2" contains the value "Brailovsky* 
	And table row number "1" column number "3" contains the value "4" 
	
	#Results are sorted by First Name

	@ignore
Scenario: 06-VD-29 - Peer Kudos Dashboard - Assign stars to employees in Top 5
	#Click next to name of "top-5" employee and select stars (1, 2, or 3 only), Verify New Total
	Given the page is loaded
	When I save the data using element id "top_1_of_5_employee" into the variable "currentStarCount"
	And I click on the button using element id "3-star"
	
	# New Step to add 2 integers
	# And I add the numbers contained in the string "[currentStarCount], 3" and store into the variable "totalStars"

	# New Step
	# Then the text box using element id "top_1_of_5_employee" contains the value stored in the variable "totalStars"
	
	@ignore	
Scenario: 07-VD-29 - Peer Kudos Dashboard - Assign stars to employees displayed in Search Results
	#Search for an employee
	#Click next to name of employee in search results and enter stars (1, 2, or 3 only)
	Given the page is loaded
	When I set the text box using element id "search_employee" with the value "xyz"
	And I access the table using element id "employee_table"
	And I save the data from table row number "1" column number "3" into the variable "currentStarCount"
	And I click on the button using element id "2-star"

	# New Step to add 2 integers
	# And I add the numbers contained in the string "[currentStarCount], 2" and store into the variable "totalStars"

	# New Step
	# Then the text box in table row number "1" column number "3" contains the value stored in the variable "totalStars"

	@ignore
Scenario: 08-VD-27 - Peer Kudos Dashboard - My Current Month Ranking
	#Next to my name, view count of stars I collected for current month
	#Next to my name, view count of stars I have given out for current month
	#Next to my name, view count of stars I have remaining for current month
	#View my Star Ranking Order
	Given the page is loaded

	#New Step using WebElementExtensions.getTextOfElement()
	Then the element using element Id "my_star_collection_count" contains the text ""
	Then the element using element Id "my_star_given_count" contains the text ""
	Then the element using element Id "my_star_remaining_count" contains the text ""
	And I close the current page

	@ignore
Scenario: 09-VD-32 - Admin - Login
	#Admin users with valid credentials (marked as Admin users - where ??) are directed to Peer Kudos Admin Employee Directory page
	#Admin users with invalid credentials (or not marked as Admin users - where ??) -- error message is displayed
	Given I navigate to the url "http://env03.cloud.capitissolutions.com/users/sign_in" titled "ShoppingDemo"
	When I set the text box using element id "login_email_field" with the value "admin@example.com"
	And I set the text box using element id "login_password_field" with the value "supersecret"
	And I click on the button using element id "login_submit_button"
	Then the page title is "ShoppingDemo"
	And the page contains the text "Catalog"

	@ignore
Scenario: 10-VD-32 - Admin - Login with Valid Credentials
	Given the page is loaded
	When I set the text box using element id "login_email_field" with the value "invalidUserId"
	And I set the text box using element id "login_password_field" with the value "invalidPassword"
	And I click on the button using element id "login_submit_button"
	Then the page contains the text "Invalid Email or password."

	@ignore
Scenario: 11-VD-33 - Admin - Login with Invalid Credentials


Scenario: 19-Peer Kudos Dashboard - Logout of the application
	Given the page is loaded
	When I click on the link containing the text "Sign out"
	Then the page contains the text "You need to sign in or sign up before continuing"
	And I close the current page

