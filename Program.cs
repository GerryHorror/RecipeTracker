/*
References:
- [How do I parse a string with a decimal point to a double?](https://stackoverflow.com/questions/1354924/how-do-i-parse-a-string-with-a-decimal-point-to-a-double)
- [Is it possible to write to the console in color in .NET?](https://stackoverflow.com/questions/2743260/is-it-possible-to-write-to-the-console-in-colour-in-net)
- [C# Dictionary with Examples](https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/)
- [C# List](https://www.c-sharpcorner.com/article/c-sharp-list/)
- [C# Data Structure for Multiple Unit Conversions](https://stackoverflow.com/questions/495110/c-sharp-data-structure-for-multiple-unit-conversions)
- [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
- [Array.Clear Method](https://learn.microsoft.com/en-us/dotnet/api/system.array.clear?view=net-8.0)
*/

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Part 1
/// </summary>

using System;
using System.Collections.Generic;
using RecipeTracker.Classes;

namespace RecipeTracker
{
    // This is the entry point of the program (i.e., where the program starts).
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a new List to store the recipes.
            List<Recipe> recipes = new List<Recipe>();
            // Boolean to check if the user hasn't exited the program
            var appRunning = true;
            // While loop to keep the program running (i.e., appRunning is true)
            while (appRunning)
            {
                // Clear the console window before displaying the menu
                Console.Clear();
                // Welcome message to the user
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to Recipe Tracker!");
                Console.ResetColor();
                // Menu options for the user
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine(
                   "1. Add a new recipe\n2. Display a specific recipe\n3. View all recipes\n4. Scale a recipe\n5. Reset quantities\n6. Clear a recipe\n7. Exit");
                string userChoice = Console.ReadLine();
                Console.ResetColor();
                // Switch statement to handle the user's choice
                switch (userChoice)
                {
                    // Case 1: Add a new recipe to the Recipe object
                    case "1":
                        recipes = RecipeOperations.AddRecipes(recipes);
                        break;
                    // Case 2: Display the recipe details stored in the Recipe object
                    case "2":
                        RecipeOperations.DisplayRecipes(recipes);
                        break;
                    // Case 3: View all recipes
                    case "3":
                        RecipeOperations.ViewAllRecipes(recipes);
                        break;
                    // Case 4: Scale the recipe by a factor of 0.5, 2, or 3
                    case "4":
                        RecipeOperations.ScaleRecipe(recipes);
                        break;
                    // Case 5: Reset the quantities of all ingredients in the recipe
                    case "5":
                        RecipeOperations.ResetQuantities(recipes);
                        break;
                    // Case 6: Clear the recipe details stored in the Recipe object
                    case "6":
                        RecipeOperations.DeleteRecipe(recipes);
                        break;
                    // Case 7: Exit the program by setting appRunning to false
                    case "7":
                        Console.WriteLine("Thank you for using Recipe Tracker! Goodbye!");
                        appRunning = false;
                        break;
                    // Default case: Display an error message for invalid choices (i.e., choices other than 1-7)
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                        Console.ResetColor();
                        break;
                }
                // Pause the program and wait for user input before displaying the menu again
                Console.WriteLine("Press any key to continue...");
                // Read the user's input (any key) before displaying the menu again
                Console.ReadKey();
            }
        }

        // < -------------------------------------------------------------------------------------- >

        private static void NotifyUser(int totalCalories)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Warning: This recipe exceeds 300 calories with a total of {totalCalories} calories!");
            Console.ResetColor();
        }
    }
}