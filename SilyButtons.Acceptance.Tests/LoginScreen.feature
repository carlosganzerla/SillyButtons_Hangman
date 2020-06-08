Feature: LoginScreen
	If any name is entered the game should be able to start
	If a given name has never played this name should not be on the list
	If a given name has already played this name should be on the list
	If an existing name from the list is selected the game should be able to start
	If nothing is entered the game should not be able to start
	If something is entered and deleted the game should not be able to start
	If start game is pressed then a new game should begin

Scenario: User logs in with their name
	Given I am on the login screen
	When I enter "my name" in the name field
	Then start button is enabled

Scenario: User has never played the game
	Given I am on the login screen
	And I "my name" have never played the game
	When I check the existing names list
	Then "my name" should not be on the list

Scenario: User has already played the game
	Given I am on the login screen
	And I "my name" have already played the game
	When I check the existing names list
	Then "my name" should be on the list

Scenario: User selects from the list their own name if they already played
	Given I am on the login screen
	And I "my name" have already played the game
	When I select "my name" from the list
	Then start button is enabled

Scenario: User does not enter anything in the name field
	Given I am on the login screen
	When I enter nothing
	Then start button is disabled

Scenario: User enters something but then deletes it
	Given I am on the login screen
	When I enter "my name" in the name field
	And I delete the name which I entered
	Then start button is disabled

Scenario: User starts game
	Given I am on the login screen
	When I enter "my name" in the name field
	And I press start button
	Then a new game starts
	And "my name" is stored

