Feature: Hashtags
	In order to create a hashtag
	As a user
	I want to be able to create a hashtag


Scenario: create a hashtag
	Given I am a client
	When I make a post request to 'api/hashtags' with the following data '{ "Name" : "Danqwas" }'
	Then the response status code is '200'
