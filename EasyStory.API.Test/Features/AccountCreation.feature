Feature: AccountCreation
	In order to set up a new user account 
	As a client application
	I want to be able to create and update a user 

Scenario: Account was created
Given I am a client
When I make a post request to 'api/users' with the following data '{ "Username" : "disney", "Email" : "disney@.com", "Password" : "easysstoryy", "FirstName" : "Gonzalo", "LastName" : "nosexdxd"}'
Then the response status code is '200'

Scenario: Account could not be created
Given I am a client
When I make a post request to 'api/users' with the following data '{ "FirstName" : "Gonzalo", "LastName" : "Miranda"}'
Then the response status code is '400'

