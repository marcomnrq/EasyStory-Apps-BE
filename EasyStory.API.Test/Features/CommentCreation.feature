Feature: CommentCreation
	In order to give recommendations
	As a user
	I want to be able to comment a  post

@mytag
Scenario: Comment was published
	Given I am a reader or writer
	When I  make a post comment request to 'api/users/' with the user id of '1' and request '/posts/' with post id of '1' and request '/comments' with the data: '{ "Content" : "Buenardo" }'
	Then the response should be this status code of '200'


Scenario: Comment not found
	Given I am a reader or writer
	When I  make a post comment request to 'api/users/' with the user id of '1' and request '/posts/' with post id of '8' and request '/comments' with the data: '{ "Content" : "Buenardo" }'
	Then the response should be this status code of '400'