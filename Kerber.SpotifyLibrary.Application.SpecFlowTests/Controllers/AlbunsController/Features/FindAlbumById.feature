@AlbunsController
Feature: Find album my ID on AlbunsController
	In order to avoid silly mistakes
	As a worried user
	I want to be able to retrieve an album or fail using an album ID

@Success
Scenario: Retrieve an album by ID
	Given I have the id "5eec28eb6e50a13a88dfffb6"
	And the album for that ID exists
	When I call GET album by ID
	Then the result should be an album with the same "5eec28eb6e50a13a88dfffb6" id

@Failure
Scenario: Fail to retrieve an album by ID since the album does not exist
	Given I have the id "5eec28"
	But the album does not exist
	When I call GET album by ID
	Then the result should be null