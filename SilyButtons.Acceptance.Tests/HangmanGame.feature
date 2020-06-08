Feature: Hangman Game
	The game consist in guessing all the characters of some substantive
	The player has six guesses, each guess coressponds to a body part from the hangman
	Each time the player makes a guess, the guessed character is blocked
	Each time the player makes an incorrect guess, a body part from the hangman is shown on the screen
	Each time the player guessess a character correctly 
		every appearance within the chosen word of the guessed character is shown on the screen and
		no hangman parts are shown


Scenario: Guess good character
	Given The word is "WORD"
	And the game has started
	When I guess the character 'O'
	Then 1 appearances of the character 'O' are shown
	And the character 'O' is blocked

Scenario: Guess bad character
	Given The word is "WORD"
	And the game has started
	When I guess the character 'A'
	Then 0 appearances of the character 'A' are shown
	And I have 5 guesses left
	And the character 'A' is blocked

Scenario: Hangman body
	Given The word is "WORD"
	And the game has started
	When I guess the characters "GH"
	Then I have 4 guesses left

Scenario: Hangman left leg
	Given The word is "WORD"
	And the game has started
	When I guess the characters "GHI"
	Then I have 3 guesses left

Scenario: Hangman right leg
	Given The word is "WORD"
	And the game has started
	When I guess the characters "GHIJ"
	Then I have 2 guesses left


Scenario: Hangman left arm
	Given The word is "WORD"
	And the game has started
	When I guess the characters "GHIJK"
	Then I have 1 guesses left

Scenario: Hangman right arm
	Given The word is "WORD"
	And the game has started
	When I guess the characters "GHIJKL"
	Then I have 0 guesses left
	And the game is lost


Scenario: Full game lost and guesses after loss
	Given The word is "HANGMAN"
	And the game has started
	When I guess the characters "HANGBCDEFXYGHIJKL"
	Then 1 appearances of the character 'H' are shown
	And 2 appearances of the character 'A' are shown
	And 2 appearances of the character 'N' are shown
	And 1 appearances of the character 'G' are shown
	And I have 0 guesses left
	And all characters are blocked
	And the game is lost
	And the characters "BCDEFX" are counted as wrong guesses
	And the charcters "YGHIJKL" are not counted as guesses


Scenario: Full game won and guesses after win
	Given The word is "WORD"
	And the game has started
	When I guess the characters "WAOBRCDVXYZ"
	Then 1 appearances of the character 'W' are shown
	And 1 appearances of the character 'O' are shown
	And 1 appearances of the character 'R' are shown
	And 1 appearances of the character 'D' are shown
	And all characters are blocked
	And the game is won
	And the characters "ABC" are counted as wrong guesses
	And the charcters "VXYZ" are not counted as guesses
	And I have 3 guesses left

Scenario: Game restart after defeat
	Given The word is "A"
	And the game has started
	When I guess the characters "BCDEFGHUJKL"
	And I restart the game
	Then the game is restarted

Scenario: Game restart after victory
	Given The word is "A"
	And the game has started
	When I guess the characters "DCBAKLMNOP"
	And I restart the game
	Then the game is restarted

