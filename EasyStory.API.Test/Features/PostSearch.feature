Feature: PostSearch
	In order to start reading a post
	As a new user
	I want to search posts using a hashtag

@mytag
Scenario: List of Post found
	Given I am a new user
	When I make a get request to path 'api/hashtags/2/posts'
	Then the search result should be a status code of '200'

Scenario: List of Post was not found
	Given I am a new user
	When I make a get request to path 'api/hashtags/8/posts'
	Then the search result should be a status code of '200'