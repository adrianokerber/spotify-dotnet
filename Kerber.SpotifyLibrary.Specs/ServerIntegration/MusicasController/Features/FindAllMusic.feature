@Integration
@MusicasController
Feature: FindAllMusic from MusicasController
	In order to avoid silly mistakes
	As worried user
	I want to be able to retrieve all musics

@Success
Scenario: Retrieve all music
	Given I have these songs <givenSongs>
	When I call GET all songs
	Then the result should be these songs <expectedSongs>
	And the response code should be 200

Examples:
	| givenSongs            | expectedSongs          |
	| RequestMusicList.json | ResponseMusicList.json |