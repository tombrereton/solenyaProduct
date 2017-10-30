Feature: ProductAPIFeatures
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: Ability to access homepage
	Given I am in a browser
	When I enter the homepage url
	Then I should get an OK response
