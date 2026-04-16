@api
Feature: FakeStore Auth
  As an API consumer
  I want to authenticate
  So I can verify the Auth API responds correctly

  Scenario: Login with valid credentials
    Given I have the API base "https://fakestoreapi.com"
    When I login with username "johnd" and password "m38rmF$"
    Then the response should contain a token
