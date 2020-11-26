Feature: Qualification Assignation
	In order to assign a qualification
	As a user
	I want to be able to assign a qualification to a post


Scenario: Qualification found
	Given I am a Client
	When I  make a get request to 'api/users/' with the user id of '1' and request '/posts/' with post id of '1' and request '/qualifications'
	Then the result of status code is '200'

Scenario: Qualification not found
    Given I am a Client
	When I  make a get request to 'api/users/' with the user id of '5' and request '/posts/' with post id of '8' and request '/qualifications'
	Then the result of status code is '404'