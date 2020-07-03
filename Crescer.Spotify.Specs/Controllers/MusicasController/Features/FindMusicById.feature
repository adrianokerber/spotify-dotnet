@MusicasController
Feature: Find music by ID on MusicasController
	In order to avoid silly mistakes
	As a worried user
	I want to be able to retrieve musics or fail using a music ID

@Success
Scenario: Retrieve a music by ID
	Given I have the id "5eec28eb6e50a13a88dfffb6"
	And The music for that id exists
	When I call GET music
	Then the result should be a music with the same "5eec28eb6e50a13a88dfffb6" id

@Failure
Scenario: Fail to retrieve a music by ID since the music does not exist
	Given I have the id "5eec28"
	But The music does not exist
	When I call GET music
	Then the result should be null

@Success
Scenario: Retrieve a music by ID and receive success response
	Given I have the id "5eec28eb6e50a13a88dfffb6"
	And The music for that id exists
	When I call GET music
	Then the result should be a music with the same "5eec28eb6e50a13a88dfffb6" id
	And the response code should be 200

@Failure
Scenario: Fail to retrieve a music by ID and receive not found response
	Given I have the id "5eec28"
	But The music does not exist
	When I call GET music
	Then the result should be null
	And the response code should be 404