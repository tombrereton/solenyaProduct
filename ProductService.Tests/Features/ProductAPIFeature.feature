Feature: ProductAPIFeature
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Ability to access url
	Given I am in a browser
	When I enter the homepage url
	Then I should get an OK response

Scenario: Ability to see list of Plp Items
	Given I am in a browser
	When I hit the productAPI endpoint
	Then I should get a response containing JSON data for the plp



