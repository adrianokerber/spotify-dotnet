Feature: Calculator
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@CalculatorTest
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have also entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen

@CalculatorTest
Scenario: Add other two numbers
	Given I have entered 8 into the calculator
	And I have also entered 5 into the calculator
	When I press add
	Then the result should be 13 on the screen

@CalculatorTest
Scenario: Add two numbers but one is negative
	Given I have entered 8 into the calculator
	And I have also entered -5 into the calculator
	When I press add
	Then the result should be 3 on the screen
