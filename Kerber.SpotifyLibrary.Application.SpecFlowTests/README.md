# Test project using BDD throught SpecFlow

This is the test project for *${XXX} dotnet CRUD* and has the main goal to test business rules throught the `Controller`'s classes of the REST API.

## Structuring tests

The SpecFlow utilizes a global scope, meaning that all the steps are shared among all scenarios and features.
In order to create a new test you muste create a file `.feature` that must be written in **Gherkin**.

We have three main base classes for our test structures:
1. `BaseSteps.cs` the base classe that all feature step definitions should inherit from.
2. `CommonSteps.cs` the classe that has the Context Injection (CI) and must be inherited from in order to keep objects among different step classes.
3. `ParameterNameGuide.cs` has only one purpose - keep all keys for the objects injected via CI.

**TIP**: Always define tests in a verbose form to keep clear all the test intentions and the desired behaviours.

>Do you not now SpecFlow? Learn more about it on https://specflow.org/

## Current test structure

The test project is divided by goals:
1. `Controllers` folder - responsible for testing only the controller rules itself.
2. `ServerMigration` folder - responsible for testing the controllers with all the server mocked to test any authentication barriers defined on the project.