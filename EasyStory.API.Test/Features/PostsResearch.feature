Feature: PostsResearch
	In order to find a post
	As a reader
	I want to be able to find a post


@mytag
Scenario: Post was found
	Given I am a reader
	When I make a get the post request to 'api/posts/' with the post id of '1'
	Then the result should be a status code of '200'

Scenario: Post was not found
	Given I am a reader
	When I make a get the post request to 'api/posts/' with the post id of '9'
	Then the result should be a status code of '404'