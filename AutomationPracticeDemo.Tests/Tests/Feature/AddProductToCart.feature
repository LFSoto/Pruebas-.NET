Feature: Add Products to Cart

      Scenario: Add products to the cart and verify their presence and prices
            Given I am on the start page
            When I navigate to the "Products" page
            Then I should see the title "ALL PRODUCTS"

            When I view the first product details
            Then the product name should be "Blue Top"
            And the product price should be "Rs. 500"

            When I add the first product to the cart
            Then I should see a modal with title "Added!" and message "Your product has been added to cart."
            And I click continue button on the modal

            When I view the second product details
            Then the product name should be "Men Tshirt"
            And the product price should be "Rs. 400"

            When I add the second product to the cart
            And I check the items in the cart
            Then I should see "Blue Top" in the cart
            And I should see "Men Tshirt" in the cart
            And the total price should be "Rs. 900"
