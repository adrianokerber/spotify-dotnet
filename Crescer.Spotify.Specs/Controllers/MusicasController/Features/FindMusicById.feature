Feature: FindMusicById
	In order to avoid silly mistakes
	As a worried user
	I want to retrieve a music with the same inputed id

@MusicasController
Scenario: Retrieve a music by ID
	Given I have the id "5eec28eb6e50a13a88dfffb6"
	When I call GET music
	Then the result should be a music with the same "5eec28eb6e50a13a88dfffb6" id
