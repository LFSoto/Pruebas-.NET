@api
Feature: FakeStore Users
  As an API consumer
  I want to manage users
  So I can verify the Users API responds correctly

  Scenario: Get all users
    Given I have the API base "https://fakestoreapi.com"
    When I query all users
    Then the response should contain a list of users

  Scenario: Get user by id
    Given I have the API base "https://fakestoreapi.com"
    When I query the user with id 1
    Then the response should contain a user with id 1

  Scenario: Create a new user
    Given I have the API base "https://fakestoreapi.com"
    When I create a user with username "test_user" and email "test@example.com" and password "pass123"
    Then the created user should have an id

  Scenario: Update an existing user
    Given I have the API base "https://fakestoreapi.com"
    When I update user with id 1 to username "updated_user" and email "updated@example.com"
    Then the updated user should have username "updated_user"

  Scenario: Delete a user
    Given I have the API base "https://fakestoreapi.com"
    When I delete the user with id 1
    Then the user should be deleted successfully
