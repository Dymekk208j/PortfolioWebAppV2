Feature: TechnologyFeature

@mytag
Scenario: Create technology
	Given There is open create technology panel
	When I have fill required data
	And I press Create button
	Then Create panel should redirect to list
	And Technology is available in project list