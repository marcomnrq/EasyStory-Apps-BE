Feature: Qualification Assignation
	In order to assign a qualification
	As a user
	I want to be able to assign a qualification to a post


Scenario: Get a Qualification
	Given I am a Client
	When I  make a get request to 'api/users/' with the user id of '2' and request '/posts/' with post id of '4' and request '/qualifications'
	Then the result of status code is '200'