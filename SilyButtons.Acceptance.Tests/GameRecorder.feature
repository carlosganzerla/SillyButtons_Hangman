Feature: Game recorder
		Each time a game ends or the player quits
		The game is recorded

Scenario: Record game won
	Given My names is "Joe"
	And the word is "WORD"
	When I guess "WAORCDXYZ"
	Then the a game is added to "Joe" history
	And the history records 2 wrong guesses
	And the hisotry records the "WAORCD" guessed characters
	And the history records "WORD" as the secret word
	And the history records the result as a win
	And the history records the game start date


Scenario: Record game lost
	Given My names is "Bill"
	And the word is "WORD"
	When I guess "ABCDEFGHIJL"
	Then the a game is added to "Bill" history
	And the history records 6 wrong guesses
	And the hisotry records the "ABCDEFG" guessed characters
	And the history records "WORD" as the secret word
	And the history records the result as a loss
	And the history records the game start date

Scenario: Record game quit
	Given My names is "Quitter"
	And the word is "WORD"
	When I guess "ABCD"
	And I quit the game
	Then the a game is added to "Quitter" history
	And the history records 3 wrong guesses
	And the hisotry records the "ABCD" guessed characters
	And the history records "WORD" as the secret word
	And the history records the result as a loss
	And the history records the game start date

Scenario: Record another game lost
	Given My names is "Bill"
	And the word is "ANOTHER"
	When I guess "UVWXYZ"
	Then the a game is added to "Bill" history
	And the history records 6 wrong guesses
	And the hisotry records the "UVWXYZ" guessed characters
	And the history records "ANOTHER" as the secret word
	And the history records the result as a loss
	And the history records the game start date
