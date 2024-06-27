<div align="center">
  <h1>Recipe Tracker Application (Console and WPF Versions)</h1>
  <img src="https://img.shields.io/badge/.NET-Framework%204.8-blue" alt=".NET Framework">
  <img src="https://img.shields.io/badge/language-C%23-blueviolet" alt="C#">
  <img src="https://img.shields.io/badge/IDE-Visual%20Studio-purple" alt="Visual Studio">
</div>

This repository contains both a console-based and a WPF-based version of the Recipe Tracker application. Both versions allow users to manage recipes by adding, displaying, scaling, and clearing recipe details.

## Switching Between Versions

This repository contains two separate projects:

1. **RecipeTracker** (Console Application)
2. **RecipeTrackerGUI** (WPF Application)

To switch between the console and WPF versions:

1. Open the solution file (RecipeTracker.sln) in Visual Studio.
2. In the Solution Explorer, you will see both projects.
3. To run the Console version:
   - Right-click on the "RecipeTracker" project.
   - Select "Set as Startup Project".
   - Press F5 or click "Start Debugging" to run the console application.
4. To run the WPF version:
   - Right-click on the "RecipeTrackerGUI" project.
   - Select "Set as Startup Project".
   - Press F5 or click "Start Debugging" to run the WPF application.

Ensure that you have the necessary prerequisites installed for both versions (primarily .NET Framework 4.8).

## Console Version

### Tech Stack
- **IDE:** ![Visual Studio](https://img.shields.io/badge/Visual_Studio-2019-purple.svg)
- **Language:** ![C#](https://img.shields.io/badge/C%23-9.0-blueviolet.svg)
- **Type:** Console Application
- **Framework:** ![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.8-blue.svg)

## Features

- **Add a Recipe**: Users can add new recipes with ingredients and preparation steps.
- **Display a Recipe**: Users can view the details of added recipes.
- **Scale a Recipe**: Users can scale the quantities of the ingredients by specified factors (0.5, 2, or 3).
- **Reset Quantities**: Resets the quantities of ingredients to their original values.
- **Clear a Recipe**: Clears all details of the recipe.

## WPF Version

### Tech Stack
- **IDE:** ![Visual Studio](https://img.shields.io/badge/Visual_Studio-2019-purple.svg)
- **Language:** ![C#](https://img.shields.io/badge/C%23-9.0-blueviolet.svg)
- **Framework:** ![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.8-blue.svg)
- **UI Framework:** ![WPF](https://img.shields.io/badge/WPF-Framework-orange.svg)

## Features

- **Add a Recipe**: Users can add new recipes with ingredients and preparation steps.
- **Display a Recipe**: Users can view the details of added recipes.
- **Scale a Recipe**: Users can scale the quantities of the ingredients by specified factors (0.5, 2, or 3).
- **Reset Quantities**: Resets the quantities of ingredients to their original values.
- **Clear a Recipe**: Clears all details of the recipe.
- **Create Chart**: Creates a pie chart based on selected recipes. The percentage calculation is based on calories per food group.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

To run this project, you will need:

- Visual Studio (2019 or later recommended)
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
### Console Version
Follow the on-screen prompts to interact with the application:

- Enter `1` to add a new recipe.
- Enter `2` to display a specific recipe.
- Enter `3` to view all recipes.
- Enter `4` to scale a recipe.
- Enter `5` to reset ingredient quantities.
- Enter `6` to clear a recipe.
- Enter `7` to exit the application.

### Running the Unit Tests
1. **Open the Test Explorer:**
   - In Visual Studio, go to `Test` > `Test Explorer` to open the Test Explorer window.

2. **Run All Tests:**
   - In the Test Explorer window, click on `Run All` to execute all unit tests. The results will be displayed, showing which tests passed or failed.
  
## WPF Version
The application provides a graphical user interface for easy interaction:
- Use the "Add Recipe" button to create a new recipe.
- Select a recipe from the list to view its details.
- Use the "Scale Recipe" button to adjust ingredient quantities.
- Use the "Reset Quantities" button to restore original ingredient amounts.
- Use the "Delete Recipe" button to remove a recipe.
- Use the "Create Menu" button to select multiple recipes and view nutritional information.

## Updates

- **Graphical User Interface:** Added a WPF version with improved user experience.
- **Visual Recipe Display:** In the WPF version, recipes are displayed with a more structured and visually appealing layout.
- **Interactive Recipe List:** In the WPF version, users can select recipes from a list to view and modify.
- **Color-Coded Calorie Information:** In the WPF version, calorie information is colour-coded based on the total calorie count.
- **Chart Creation:** The WPF version now has the functionality to create a pie chart from multiple recipes and view aggregated nutritional information.
- **Improved Ingredient Management:** Enhanced the process of adding and displaying ingredients with their respective food groups in both versions.
- **Visual Scaling and Resetting:** In the WPF version, scaling recipes and resetting quantities update the display in real-time.

Both versions maintain core functionality while the WPF version provides a more intuitive and user-friendly interface.

## References

- [How do I parse a string with a decimal point to a double?](https://stackoverflow.com/questions/1354924/how-do-i-parse-a-string-with-a-decimal-point-to-a-double)
- [Is it possible to write to the console in color in .NET?](https://stackoverflow.com/questions/2743260/is-it-possible-to-write-to-the-console-in-colour-in-net)
- [C# Dictionary with Examples](https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/)
- [C# List](https://www.c-sharpcorner.com/article/c-sharp-list/)
- [C# Data Structure for Multiple Unit Conversions](https://stackoverflow.com/questions/495110/c-sharp-data-structure-for-multiple-unit-conversions)
- [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
- [Array.Clear Method](https://learn.microsoft.com/en-us/dotnet/api/system.array.clear?view=net-8.0)
- [C# OrderBy Method](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby?view=net-5.0)
- [Food Groups](https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/)
- [Calorie Intake Chart](https://www.webmd.com/diet/calories-chart)
- [Tuple Method in C#](https://www.geeksforgeeks.org/c-sharp-tuple-class/)
- [C# Delegates](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)
- [C# Delegates and Events](https://www.tutorialsteacher.com/csharp/csharp-delegates)
- [C# Simple Unit Test](https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code)
- [C# Unit Testing](https://docs.microsoft.com/en-us/visualstudio/test/unit-test-basics)
- [C# Unit Testing with MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
 - [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
 - [Food Groups](https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/)
 - [Calorie Intake Chart](https://www.webmd.com/diet/calories-chart)
 - [Definitive Guide to WPF Colors](https://www.codeproject.com/Articles/5296124/Definitive-guide-to-WPF-colors-color-spaces-color)
 - [Binding declarations overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-8.0)
 - [Value Converters In WPF](https://www.c-sharpcorner.com/article/value-converters-in-wpf/#:~:text=Value%20converters%20are%20used%20to%20display%20values%20in,and%20values%20that%20derive%20from%20multiple%20bound%20elements.)
 - [Controls in WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-6.0)
 - [How to open a message box(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-message-box?view=netdesktop-6.0)
 - [Data binding overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0)
 - [XAML overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-6.0)
 - [Overview of WPF windows(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/?view=netdesktop-6.0)
 - [Routed events overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-6.0)
 - [Attached events overview (WPF .NET](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/attached-events-overview?view=netdesktop-6.0)
 - [Tutorial on How to Install Live Charts C # - WPF](https://www.youtube.com/watch?v=YlSl6myyeSs&list=PLqj54fKHGzJPLyW17twFDkLKoLTxHZFOO)
 - [How to create a template for a control (WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-6.0)

  
