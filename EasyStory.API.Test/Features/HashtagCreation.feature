Feature: Hashtag Creation
	In order to create a hashtag
	As a user
	I want to be able to create a hashtag


Scenario: Hashtag created 
	Given I am a user in the application
	When I make a post hashtag request to 'api/hashtags' with the following data '{ "Name" : "Danqwas" }'
	Then the status response code is '200'

Scenario: Hashtag could not be created
    Given I am a user in the application
	When I make a post hashtag request to 'api/hashtags' with the following data '{ }'
	Then the status response code is '400'
