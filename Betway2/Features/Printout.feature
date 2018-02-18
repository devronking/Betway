Feature: Printout
	My feature to print out details from various websites
	Here we want to get a printout of details from various websites
	#Details are contained in an Excel file, which is saved to C:\Temp

Scenario: Printout all news headlines from the Google News url
	Given I have navigated to the specified website
	| browser | url                |
	| chrome  | http://news.google.com |
	And I see that the page has fully loaded
	Then I want a printout of all the NewsHeadlines displayed on the page

Scenario: Printout all live games from the Betway Page url
	Given I have navigated to the specified website
	| browser | url                |
	| chrome  | https://www.betway.co.za |
	And I see that the page has fully loaded
	Then I want a printout of all the LiveGames displayed on the page