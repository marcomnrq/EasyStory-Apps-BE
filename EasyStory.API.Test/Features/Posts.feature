Feature: Posts Resource
	In order to find a post
	As a reader
	I want to be able to find a post


@mytag
Scenario: Post was found
	Given I am a reader
	When I make a get request to 'api/posts/' with the post id of '2'
	Then the result should be a status code of '200'