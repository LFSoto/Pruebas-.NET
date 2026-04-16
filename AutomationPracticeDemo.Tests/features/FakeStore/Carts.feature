@api
Feature: FakeStore Carts
  As an API consumer
  I want to manage carts
  So I can verify the Carts API responds correctly

  Scenario: Get all carts
    Given I have the API base "https://fakestoreapi.com"
    When I query all carts
    Then the response should contain a list of carts

  Scenario: Get cart by id
    Given I have the API base "https://fakestoreapi.com"
    When I query the cart with id 1
    Then the response should contain a cart with id 1

  Scenario: Create a new cart
    Given I have the API base "https://fakestoreapi.com"
    When I create a cart for user 1 with product 1
    Then the created cart should have an id

  Scenario: Update an existing cart
    Given I have the API base "https://fakestoreapi.com"
    When I update cart with id 1 to have product 2
    Then the updated cart should contain product 2

  Scenario: Delete a cart
    Given I have the API base "https://fakestoreapi.com"
    When I delete the cart with id 1
    Then the cart should be deleted successfully
