Feature: GameHistoryScreen
			The game history of a given player can be seen on the screen

Scenario: Player game history
	Given My name is "Bill"
	And I have played 3 games
	When I open the history screen
	And I select "Bill" on the player's list
	Then 3 games appear on my history

	
Scenario: Player never played
	Given My name is "Johnny"
	And I have never played any game
	When I open the history screen
	Then "Johnny" should not be player's list

Scenario: Detailed game history
	Given My name is "Foley"
	And I have played 1 game
	And the date was "2004-01-01"
	And I have guessed the characters "ABCDEF" in this game
	And I had 6 wrong guesses
	And the secret word was "XYZ"
	And the result was a defeat
	When I open the history screen
	And I select "Foley" on the player's list
	Then 1 games appear on my history
	And my history shows the date as "2004-01-01"
	And my history shows the guessed characters as "ABCDEF"
	And my history shows 6 wrong guesses
	And my hisotry shows the secret word as "XYZ"
	And my history shows that I have lost
