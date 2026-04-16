@api
Feature: FakeStore Products
  As an API consumer
  I want to manage products
  So I can verify the Products API responds correctly

  Scenario: Get all products
    Given I have the API base "https://fakestoreapi.com"
    When I query all products
    Then the response should contain a list of products

  Scenario: Get product by id
    Given I have the API base "https://fakestoreapi.com"
    When I query the product with id 1
    Then the response should contain a product with id 1

  Scenario: Create a new product
    Given I have the API base "https://fakestoreapi.com"
    When I create a product with title "Test Product" and price 29.99
    Then the created product should have title "Test Product"

  Scenario: Update an existing product
    Given I have the API base "https://fakestoreapi.com"
    When I update product with id 1 to title "Updated Product" and price 39.99
    Then the updated product should have title "Updated Product"

  Scenario: Delete a product
    Given I have the API base "https://fakestoreapi.com"
    When I delete the product with id 1
    Then the product should be deleted successfully
