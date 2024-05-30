<div align="center">
  <h1>Recipe Tracker Console Application</h1>
  <img src="https://img.shields.io/badge/.NET-Framework%204.8-blue" alt=".NET Framework">
  <img src="https://img.shields.io/badge/language-C%23-blueviolet" alt="C#">
  <img src="https://img.shields.io/badge/IDE-Visual%20Studio-purple" alt="Visual Studio">
</div>

The Recipe Tracker is a console-based application that allows users to manage recipes by adding, displaying, scaling, and clearing recipe details. This application is developed using C# and is intended to be run on the .NET Framework 4.8.

## Tech Stack

- **IDE**: ![Visual Studio](https://img.shields.io/badge/Visual_Studio-2019-purple.svg)
- **Language**: ![C#](https://img.shields.io/badge/C%23-9.0-blueviolet.svg)
- **Type**: Console Application
- **Framework**: ![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.8-blue.svg)

## Features

- **Add a Recipe**: Users can add new recipes with ingredients and preparation steps.
- **Display a Recipe**: Users can view the details of added recipes.
- **Scale a Recipe**: Users can scale the quantities of the ingredients by specified factors (0.5, 2, or 3).
- **Reset Quantities**: Resets the quantities of ingredients to their original values.
- **Clear a Recipe**: Clears all details of the recipe.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

To run this project, you will need:

- Visual Studio (2017 or later recommended)
- .NET Framework 4.8 SDK

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/GerryHorror/RecipeTracker.git
2. Open the project:
   - Open RecipeTracker.sln with Visual Studio.
3. Restore NuGet packages:
   - Ensure that all necessary NuGet packages are restored in Visual Studio by right-clicking on the solution and selecting "Restore NuGet Packages".
4. Build the solution:
   - Build the solution to verify that everything compiles correctly.

### Running the Application

1. Set Startup Project:
   - In Visual Studio, right-click on the RecipeTracker project in the Solution Explorer and choose "Set as StartUp Project".
2. Start the application:
   - Press F5 or click on "Start Debugging" to run the application. The console window will open with instructions for interacting with the application.

## Usage

Follow the on-screen prompts to interact with the application:

- Enter `1` to add a new recipe.
- Enter `2` to display a specific recipe.
- Enter `3` to view all recipes.
- Enter `4` to scale a recipe.
- Enter `5` to reset ingredient quantities.
- Enter `6` to clear a recipe.
- Enter `7` to exit the application.

## Running the Unit Tests
1. **Open the Test Explorer:**
   - In Visual Studio, go to `Test` > `Test Explorer` to open the Test Explorer window.

2. **Run All Tests:**
   - In the Test Explorer window, click on `Run All` to execute all unit tests. The results will be displayed, showing which tests passed or failed.

## Updates

- **Ingredient Validation:** Improved validation for ingredient entries to ensure calories cannot be null or negative, with special handling for water which can have zero calories.
- **Enhanced Display:** Added colour coding and structured display for ingredients, steps, and calorie information.
- **Delegate Notification:** Implemented delegate notifications to alert users when the total calories of a recipe exceed 300.
- **Food Group Selection:** Users can now select a food group for each ingredient, ensuring better nutritional categorization.
- **Recipe Reset:** Fixed issue with resetting recipes to ensure the units of measurement are restored correctly.
- **Unit Tests:** Extensive unit tests added to cover scenarios such as multiple ingredients, single ingredients, zero calories, and negative calories.


## References

- [How do I parse a string with a decimal point to a double?](https://stackoverflow.com/questions/1354924/how-do-i-parse-a-string-with-a-decimal-point-to-a-double)
- [Is it possible to write to the console in color in .NET?](https://stackoverflow.com/questions/2743260/is-it-possible-to-write-to-the-console-in-colour-in-net)
- [C# Dictionary with Examples](https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/)
- [C# List](https://www.c-sharpcorner.com/article/c-sharp-list/)
- [C# Data Structure for Multiple Unit Conversions](https://stackoverflow.com/questions/495110/c-sharp-data-structure-for-multiple-unit-conversions)
- [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
- [Array.Clear Method](https://learn.microsoft.com/en-us/dotnet/api/system.array.clear?view=net-8.0)
