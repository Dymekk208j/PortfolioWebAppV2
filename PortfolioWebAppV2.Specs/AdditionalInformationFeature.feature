Feature: AdditionalInformationFeature

@mytag
Scenario: Open Additional Information panel
	Given there is logged admin
	And administrator panel is open
	When I select cv additional information from menu list
	And I press additional information button
	Then additional information panel is open

Scenario: Create new additional information - foreign language
	Given there is logged admin
	And There is open Additional Information panel
	And foreign language tab is selected
	When I press add Additional Information button
	And I have fill the required Additional Information data
	Then I press create Additional Information
	And Additional Information has been created