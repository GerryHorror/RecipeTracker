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
using System.Globalization;
using System.Linq;

namespace RecipeTracker.Classes
{
    /*
 * RecipeOperations class contains methods to add a new recipe, display a recipe, and scale a recipe.
 * The AddRecipe method prompts the user to enter the recipe name, ingredients, and steps.
 * The DisplayRecipe method displays the recipe name, ingredients, and steps.
 * The ScaleRecipe method prompts the user to enter a scaling factor and scales the recipe accordingly.
 * The ParseDoubleInvariant method is a helper method to parse a double value using the invariant culture.

 The aim of this class is to separate the recipe operations from the main program logic. This separation
 of concerns helps in organizing the code and making it more modular, reusable, and easier to maintain.
 The core principles of OOP, such as encapsulation and separation of concerns, are demonstrated in this class.
 */

    public class RecipeOperations
    {
        // Method to add a new recipe. It prompts the user to enter the recipe name, ingredients, and steps.
        public static List<Recipe> AddRecipes(List<Recipe> recipes)
        {
            Console.WriteLine("Enter the name of the recipe:");
            // Read the recipe name from the user
            var recipeName = Console.ReadLine().Trim();
            // Validate the recipe name to ensure it is not empty
            while (string.IsNullOrWhiteSpace(recipeName))
            {
                Console.WriteLine("Recipe name cannot be empty. Please enter a valid name:");
                recipeName = Console.ReadLine().Trim();
            }

            Console.WriteLine("Enter the number of ingredients:");
            int numOfIngs;
            // Validate the number of ingredients to ensure it is a positive integer
            while (!int.TryParse(Console.ReadLine(), out numOfIngs) || numOfIngs <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number of ingredients:");
            }
            // Loop to read the ingredient details from the user
            for (var i = 0; i < numOfIngs; i++)
            {
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                var ingName = Console.ReadLine().Trim();
                while (string.IsNullOrWhiteSpace(ingName))
                {
                    Console.WriteLine($"Ingredient name cannot be empty. Please enter a valid name for ingredient {i + 1}:");
                    ingName = Console.ReadLine().Trim();
                }

                Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                double ingQty;
                while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out ingQty) || ingQty <= 0)
                {
                    Console.WriteLine($"Invalid input. Please enter a valid quantity for ingredient {i + 1}:");
                }

                Console.Write($"Enter the unit of measurement of ingredient {i + 1}: ");
                var ingUnit = Console.ReadLine().Trim();
                while (string.IsNullOrWhiteSpace(ingUnit))
                {
                    Console.WriteLine($"Unit of measurement cannot be empty. Please enter a valid unit for ingredient {i + 1}:");
                    ingUnit = Console.ReadLine().Trim();
                }
                // Create a new Ingredient object with the provided details and add it to the ingredients list
                ingredients.Add(new Ingredient(ingName, ingQty, ingUnit));
            }

            Console.WriteLine("Enter the number of steps:");
            int numOfSteps;
            while (!int.TryParse(Console.ReadLine(), out numOfSteps) || numOfSteps <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number of steps:");
            }
            // Create a new List to store the steps of the recipe
            List steps = new List();
            // Loop to read the steps from the user
            for (var i = 0; i < numOfSteps; i++)
            {
                Console.Write($"Enter step {i + 1}: ");
                var step = Console.ReadLine().Trim();
                while (string.IsNullOrWhiteSpace(step))
                {
                    Console.WriteLine($"Step {i + 1} cannot be empty. Please enter a valid step:");
                    step = Console.ReadLine().Trim();
                }
                // Add the step to the steps list.
                steps.Add(step);
            }
            // Create a new Recipe object with the provided details
            Recipe recipe = new Recipe(recipeName, ingredients, steps);
            // Add the new recipe to the recipes list
            recipes.Add(recipe);
            // Display a success message
            Console.WriteLine("Recipe added successfully!");
            return recipes;
        }

        // <-------------------------------------------------------------------------------------->

        // Method to reset the quantities of all ingredients in the recipes array. It takes an array of Recipe objects as a parameter.
        public static void ResetQuantities(Recipe[] recipes)
        {
            // Check if there are no recipes available
            if (recipes.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No recipes available.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Available recipes:");
            // Display the list of available recipes for the user to choose from
            for (int i = 0; i < recipes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].recipeName}");
            }
            // Prompt the user to enter the index of the recipe to reset quantities for
            Console.WriteLine("Enter the index of the recipe you want to reset quantities for:");
            // Validate the input to ensure it is a valid number within the range of available recipes
            int recipeIndex;
            while (!int.TryParse(Console.ReadLine(), out recipeIndex) || recipeIndex < 1 || recipeIndex > recipes.Length)
            {
                Console.WriteLine("Invalid input. Please enter a valid recipe index:");
            }
            // Get the selected recipe based on the index provided by the user
            Recipe selectedRecipe = recipes[recipeIndex - 1];
            // Reset the quantities of all ingredients in the selected recipe
            selectedRecipe.ResetQuantity();
            Console.WriteLine("Quantities reset successfully!");
        }

        // <-------------------------------------------------------------------------------------->

        // Method to display multiple recipes with their name, ingredients, and steps.
        // It takes an array of Recipe objects as a parameter.
        public static void DisplayRecipes(Recipe[] recipes)
        {
            // Check if there are no recipes available
            foreach (var recipe in recipes)
            {
                if (!string.IsNullOrEmpty(recipe.recipeName))
                {
                    Console.WriteLine($"Recipe: {recipe.recipeName}");
                    Console.WriteLine("Ingredients:");
                    // Display the ingredients of the recipe
                    for (var i = 0; i < recipe.ingredients.Length; i++)
                    {
                        var ingredient = recipe.ingredients[i];
                        Console.WriteLine($"{i + 1}. {ingredient.ingName} - {ingredient.ingQty} {ingredient.ingUnit}");
                    }
                    Console.WriteLine("Steps:");
                    for (var i = 0; i < recipe.steps.Length; i++)
                    {
                        var step = recipe.steps[i];
                        Console.WriteLine($"{i + 1}. {step}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine();
                }
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to delete a recipe from the recipes array. It takes an array of Recipe objects as a parameter.
        public static Recipe[] DeleteRecipe(Recipe[] recipes)
        {
            // Check if there are no recipes available
            if (recipes.Length == 0)
            {
                Console.WriteLine("No recipes available.");
                return recipes;
            }
            // Display the list of available recipes for the user to choose from and prompt the user to enter the index of the recipe to delete
            Console.WriteLine("Available recipes:");
            for (int i = 0; i < recipes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].recipeName}");
            }

            Console.WriteLine("Enter the index of the recipe you want to delete:");
            int recipeIndex;
            while (!int.TryParse(Console.ReadLine(), out recipeIndex) || recipeIndex < 1 || recipeIndex > recipes.Length)
            {
                Console.WriteLine("Invalid input. Please enter a valid recipe index:");
            }
            // Confirm with the user before deleting the recipe to avoid accidental deletions
            Console.WriteLine("Are you sure you want to delete this recipe? (Y/N)");
            var confirmation = Console.ReadLine();
            while (!string.Equals(confirmation, "Y", StringComparison.OrdinalIgnoreCase) &&
                   !string.Equals(confirmation, "N", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No:");
                confirmation = Console.ReadLine();
            }
            // If the user confirms the deletion, delete the recipe
            if (string.Equals(confirmation, "Y", StringComparison.OrdinalIgnoreCase))
            {
                recipes[recipeIndex - 1].ClearRecipe();
                recipes[recipeIndex - 1].recipeName = string.Empty;
                // Create a new array to store the updated recipes without the deleted recipe
                Recipe[] updatedRecipes = new Recipe[recipes.Length - 1];
                int index = 0;
                for (int i = 0; i < recipes.Length; i++)
                {
                    if (i != recipeIndex - 1)
                    {
                        updatedRecipes[index] = recipes[i];
                        index++;
                    }
                }
                // Update the recipes array with the updated recipes
                recipes = updatedRecipes;
                Console.WriteLine("Recipe deleted successfully!");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }

            return recipes;
        }

        // Method to scale a recipe by a factor of 0.5, 2, or 3. It takes a Recipe object as a parameter.
        public static void ScaleRecipe(Recipe[] recipes)
        {
            if (recipes.Length == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("Available recipes:");
            for (int i = 0; i < recipes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].recipeName}");
            }

            Console.WriteLine("Enter the index of the recipe you want to scale:");
            int recipeIndex;

            // While loop to ensure the recipe index is a valid number
            while (!int.TryParse(Console.ReadLine(), out recipeIndex) || recipeIndex < 1 || recipeIndex > recipes.Length) Console.WriteLine("Invalid input. Please enter a valid recipe index:");
            // Get the recipe based on the index provided by the user
            Recipe recipe = recipes[recipeIndex - 1];

            Console.WriteLine("Enter the scaling factor (0.5, 2, or 3):");
            // If the input is not a valid number, display an error message
            if (!double.TryParse(Console.ReadLine(), out var factor))
            {
                Console.WriteLine("Invalid input format. Please enter a valid number.");
                return;
            }
            // If the recipe scaling fails, display an error message
            if (!recipe.ScaleRecipe(factor))
            {
                Console.WriteLine("Invalid scaling factor. Please enter 0.5, 2, or 3.");
                return;
            }
            // Display a success message if the recipe is scaled successfully
            Console.WriteLine("Recipe scaled successfully!");
        } // End of ScaleRecipe method

        // <-------------------------------------------------------------------------------------->

        // Define a dictionary to store the conversion factors for different units of measurement.
        private static readonly Dictionary<string, double> conversionFactors = new Dictionary<string, double>
        {
            // Conversion factors to convert each unit to the base unit (teaspoon).
            ["tsp"] = 1, // Base unit for volume
            ["tbsp"] = 3, // 1 tbsp = 3 tsp
            ["cup"] = 48, // 1 cup = 48 tsp
            ["ml"] = 0.202884, // 1 ml = 0.202884 tsp
            ["l"] = 202.884, // 1 l = 202.884 tsp
            ["g"] = 1, // Base unit for weight
            ["kg"] = 1000, // 1 kg = 1000 g
        };

        // Define a dictionary to store common aliases (abbreviations) for units of measurement.
        private static readonly Dictionary<string, string> aliases = new Dictionary<string, string>
        {
            ["tablespoons"] = "tbsp", // Alias for tablespoon
            ["tablespoon"] = "tbsp", // Alias for tablespoon (singular)
            ["teaspoons"] = "tsp", // Alias for teaspoon
            ["teaspoon"] = "tsp", // Alias for teaspoon (singular)
            ["cups"] = "cup", // Alias for cup (plural)
            ["grams"] = "g", // Alias for gram
            ["gram"] = "g", // Alias for gram (singular)
            ["kilograms"] = "kg", // Alias for kilogram
            ["kilogram"] = "kg", // Alias for kilogram (singular)
            ["milliliters"] = "ml", // Alias for milliliter
            ["milliliter"] = "ml", // Alias for milliliter (singular)
            ["liters"] = "l", // Alias for liter
            ["liter"] = "l" // Alias for liter (singular)
        };

        // <-------------------------------------------------------------------------------------->

        // This method converts a quantity from one unit to another based on a scaling factor.

        public static (double, string) ConvertUnit(string unit, double qty, double scale)
        {
            // Standardize the unit by converting it to lowercase and removing leading/trailing whitespace.
            string standardizedUnit = unit.ToLower().Trim();

            // Check if the unit is an alias and replace it with the standard unit.
            if (aliases.ContainsKey(standardizedUnit))
            {
                standardizedUnit = aliases[standardizedUnit];
            }

            // Check if the unit is not recognised or supported.
            if (!conversionFactors.ContainsKey(standardizedUnit))
            {
                throw new ArgumentException("Unit not recognised or supported.");
            }

            // Calculate the scaled quantity in the base unit defined by 'tsp' (teaspoon).
            double baseQty = qty * conversionFactors[standardizedUnit];
            double scaledBaseQty = baseQty * scale;

            // Initialize to use the original unit if no better unit is found.
            string bestUnit = standardizedUnit;
            double bestQty = scaledBaseQty;

            // Find the best suitable unit by comparing each unit's scaled value to find the smallest suitable magnitude greater than 1.
            foreach (var unitPair in conversionFactors)
            {
                double convertedQty = scaledBaseQty / unitPair.Value;
                // Check if the converted quantity is practical and more suitable than the current best.
                if (convertedQty >= 1 && (convertedQty < bestQty || bestQty < 1))
                {
                    bestUnit = unitPair.Key;
                    bestQty = convertedQty;
                }
            }

            // Return the best suitable unit and quantity rounded to two decimal places.
            return (Math.Round(bestQty, 2), bestUnit);
        }

        // <-------------------------------------------------------------------------------------->

        // Method to parse a double value using the invariant culture to handle different decimal separators (e.g. '.' and ',')

        /* The ParseDoubleInvariant method is a helper method that attempts to parse a double value from a string input.
         * It first tries to parse the input using the invariant culture (which uses '.' as the decimal separator).
         * If parsing with the invariant culture fails, it then tries to parse the input using the current culture.
         * If both attempts fail, it throws a FormatException indicating that the input is not a valid number.
         */

        private static double ParseDoubleInvariant(string input)
        {
            double number;
            // Try parsing with invariant culture
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
                return number;
            // Try parsing with current culture
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out number))
                return number;
            // If both fail, throw an exception
            throw new FormatException("Input is not a valid number.");
        }// End of ParseDoubleInvariant method
    } // End of RecipeOperations class
}

// < -------------------------------------------END------------------------------------------- >