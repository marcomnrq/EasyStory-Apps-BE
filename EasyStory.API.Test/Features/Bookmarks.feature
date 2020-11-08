Feature: Bookmarks
	In order to check my favourite stories
	As a user
	I want to be able to see my bookmarks

Scenario: Bookmark was found
	Given I am a reader with bookmarks
	When I make a get request to 'api/users/' with the user id fof '1' and request '/bookmarks'
	Then the result should be 200

Scenario: Bookmark not found
	Given I am a reader with bookmarks
	When I make a get request to 'api/users/' with the user id fof '9' and request '/bookmarks'
	Then the result should be 200