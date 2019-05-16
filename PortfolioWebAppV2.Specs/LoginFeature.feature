Feature: LoginFeature
	Testing system login module.

@mytag
Scenario: Login to the system
	When I press Login button
	Then login panel should be visible
	Given I have entered "Dymek" into the login field
	And I have entered "Damian13" into the password field
	When I press Sign in button
	Then the user should be logged
