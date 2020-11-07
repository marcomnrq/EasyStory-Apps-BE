Feature: Users Resource
	In order to set up a new user account 
	As a client application
	I want to be able to create and update a user 

Scenario: Create new user
Given I am a client
When I make a post request to 'api/users' with the following data '{ "Username" : "moonloght", "Email" : "arrob@.com", "Password" : "easysstoryy", "FirstName" : "Gonzalo", "LastName" : "nosexdxd"}'
Then the response status code is '200'
