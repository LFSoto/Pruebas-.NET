Feature: User Registration

      Scenario: Successful registration of a new user
            Given I am on the start page
            When I navigate to the "Sign Up/Login" page
            Then I should see the title "New User Signup!"

            When I enter "name" and "email"
            And I click the signup form button
            Then I should see the title "ENTER ACCOUNT INFORMATION"

            When I fill in account information with: Name,Title,Password,Date of Birth
            Then the email field should contain the "email"

            When I fill in address information with:First Name,Last Name,Company,Address,Address2,Country,State,City,Zipcode,Mobile Number
            And I Click on the Signup button
            Then I should see the title "ACCOUNT CREATED!"
            And I should see the message "Congratulations! Your new account has been successfully created! You can now take advantage of member privileges to enhance your online shopping experience with us."

            When I click "Continue" button
            Then I should be logged in and see the text "Logout"


