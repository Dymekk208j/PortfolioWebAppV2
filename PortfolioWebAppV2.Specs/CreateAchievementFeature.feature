Feature: CreateAchievementFeature

@mytag
Scenario: Open Achievement panel
	Given There is logged administrator
	And There are open administrator panel
	When I select cv management from menu list
	And I press Achievements button
	Then Achievement panel is open