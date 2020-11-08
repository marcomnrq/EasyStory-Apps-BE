Feature: CommentCreation
	In order to visualize recommendations
	As a user
	I want to be able to view a comment in a post

@mytag
Scenario: Comments were found
	Given I am a reader/writer
	When I make a get comment request to 'api/posts/1/comments'
	Then the response should be a status code of '200'


Scenario: Comments not found
	Given I am a reader/writer
	When I make a get comment request to 'api/posts/9/comments'
	Then the response should be a status code of '200'