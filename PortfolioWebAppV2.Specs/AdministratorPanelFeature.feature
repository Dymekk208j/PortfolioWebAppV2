Feature: AdministratorPanelFeature
	Checking if administrator panel is available

@mytag
Scenario: Check if administrator panel is available
	Given I am logged as "Dymek"
	When Administrator panel button is visible
	And I press administrator panel button
	Then Administrator panel is available