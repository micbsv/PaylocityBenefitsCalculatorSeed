# Instructions

## Run Server-side

* In Visual Studio 2022 open the solution file: "PaylocityBenefitsCalculatorSeed\PaylocityBenefitsCalculator\PaylocityBenefitsCalculator.sln"
* In Test Explorer run unit tests from ApiTests project and make sure that all of them are green
* Run the Api project in the Debugger (IIS Express)

A Swagger "Employee Benefit Cost Calculation Api" page should show up in a browser - leave page open.

## Run Client-side

In Command Promplt open project app folder: "PaylocityBenefitsCalculatorSeed\app" and run:
### `npm start`

## Updating employee

At this moment there is a limitation at the functionality of creating a new employee - the dependencies may be added only to an existing employee, so first create a new employee and then add dependents.\
On Edit Employee dialog update the dependencies information and click "Update dependents" to save changes.\
Click "Save employee" to update last name, first name and salary for the selected employee.


# What is this?

A project seed containing a React app ("app") with a C# dotnet API ("PaylocityBenefitsCalculator").  It is meant to get you started on the Paylocity Coding Challenge by taking some initial setup decisions away.

The goal is to respect your time, avoid live coding, and get a sense for how you work.

# Coding Challenge

**Show us how you work.**

Each of our Paylocity product teams operates like a small startup, empowered to deliver business value in
whatever way they see fit. Because our teams are close knit and fast moving it is imperative that you are able
to work collaboratively with your fellow developers. 

This coding challenge is designed to allow you to demonstrate your abilities and discuss your approach to
design and implementation with your potential colleagues. You are free to use whatever technologies you
prefer but please be prepared to discuss the choices you’ve made. We encourage you to focus on creating a
logical and functional solution rather than one that is completely polished and ready for production.

The challenge can be used as a canvas to capture your strengths in addition to reflecting your overall coding
standards and approach. There’s no right or wrong answer.  It’s more about how you think through the
problem. We’re looking to see your skills in all three tiers so the solution can be used as a conversation piece
to show our teams your abilities across the board.

Requirements will be given separately.