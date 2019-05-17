Feature: AboutMeFeature
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Edit about me information page is available
	Given there is logged admin
	And administrator panel is open
	When I press Page menu
	And select about me edit page option
	Then Edit about me page shoudl be visible 
	

	Scenario: update about me information
	Given there is logged admin
	And Edit about me information page is open
	When i change data
	And i have press save button
	Then data should be updated in about me page
