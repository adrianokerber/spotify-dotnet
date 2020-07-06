Feature: Calculator
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@CalculatorTest
Scenario: Add two numbers
	Given I have entered <first> into the calculator
	And I have also entered <second> into the calculator
	When I press add
	Then the result should be <result> on the screen

Examples:
	| first | second | result |
	| 50    | 70     | 120    |
	| 8     | 2      | 10     |
	| 13    | 17     | 30     |

@CalculatorTest
Scenario: Add two numbers but one is negative
	Given I have entered 8 into the calculator
	And I have also entered -5 into the calculator
	When I press add
	Then the result should be 3 on the screen
