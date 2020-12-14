@MusicasController
Feature: Find song by ID on MusicasController
	In order to avoid silly mistakes
	As a worried user
	I want to be able to retrieve a song or fail using a song ID

@Success
Scenario: Retrieve a song by ID
	Given I have the id "5eec28eb6e50a13a88dfffb6"
	And The song for that id exists
	When I call GET song
	Then the result should be a song with the same "5eec28eb6e50a13a88dfffb6" id

@Failure
Scenario: Fail to retrieve a song by ID since the song does not exist
	Given I have the id "5eec28"
	But The song does not exist
	When I call GET song
	Then the result should be null

@Success
Scenario: Retrieve a song by ID and receive success response
	Given I have the id "5eec28eb6e50a13a88dfffb6"
	And The song for that id exists
	When I call GET song
	Then the result should be a song with the same "5eec28eb6e50a13a88dfffb6" id
	And the response code should be 200

@Failure
Scenario: Fail to retrieve a song by ID and receive not found response
	Given I have the id "5eec28"
	But The song does not exist
	When I call GET song
	Then the result should be null
	And the response code should be 404