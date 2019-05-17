Feature: AchievementFeature

@mytag
Scenario: Open Achievement panel
	Given There is logged administrator
	And There are open administrator panel
	When I select cv management from menu list
	And I press Achievements button
	Then Achievement panel is open

Scenario: Create new Achivement
	Given there is logged admin
	And There is open Achivement pane
	When I press create btn
	And I have fill the required data
	Then I press create btn
	And Achievement has been created