Feature: SubscriptionVisualization
	In order to manage my access to content
	As a user
	I want to see my subscriptions

@mytag
Scenario: Subscriptions were found
	Given I am a user
	When I make a get subscription request to 'api/users/' with the user id of '1' and request '/subscriptions/' with the subscribed id of '3'
	Then the response list should be a status code of '200'
	
Scenario: No subscriptions were found
	Given I am a user
	When I make a get subscription request to 'api/users/' with the user id of '3' and request '/subscriptions/' with the subscribed id of '9'
	Then the response list should be a bad status code of '404'