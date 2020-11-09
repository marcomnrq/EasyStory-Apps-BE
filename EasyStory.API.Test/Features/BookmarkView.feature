Feature: BookmarkView
	In order to check my favourite stories
	As a user
	I want to be able to see my bookmarks

Scenario: Bookmark was found
	Given I am a reader with bookmarks
	When I make a get bookmark request to 'api/users/' with the user id of '1' and request '/bookmarks'
	Then the result should be 200

Scenario: Bookmark not found
	Given I am a reader with bookmarks
	When I make a get bookmark request to 'api/users/' with the user id of '9' and request '/bookmarks'
	Then the result should be 200