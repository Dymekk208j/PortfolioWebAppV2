Feature: CreateProjectFeature	

@mytag
Scenario: Create project panel is available
	Given There is logged admin
	And there are open administrator panel
	When I select Project from menu list
	And I select Create project
	Then there is open create project panel