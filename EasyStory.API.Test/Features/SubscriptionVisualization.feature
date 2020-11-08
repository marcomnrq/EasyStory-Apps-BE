Feature: SubscriptionVisualization
	In order to manage my access to content
	As a user
	I want to see my subscriptions

@mytag
Scenario: Subscriptions were found
	Given I am a user
	When I make a get request to 'api/subscriber/1/subscribed/'
	Then the response list should be a status code of '200'

Scenario: No subscriptions were found
	Given I am a user
	When I make a get request to 'api/subscriber/./subscribed/' 
	Then the response list should be a status code of '404'