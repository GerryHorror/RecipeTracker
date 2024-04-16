using System;
using System.Collections.Generic;
using System.Globalization;

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
        public static void AddRecipe(Recipe recipe)
        {
            Console.WriteLine("Enter the name of the recipe:");
            var recipeName = Console.ReadLine();
            // While loop to ensure the recipe name is not empty
            while (string.IsNullOrWhiteSpace(recipeName))
            {
                Console.WriteLine("Recipe name cannot be empty. Please enter a valid name:");
                recipeName = Console.ReadLine();
            }
            // Set the recipe name in the recipe object
            recipe.recipeName = recipeName;
            Console.WriteLine("Enter the number of ingredients:");
            int numOfIngs;
            // While loop to ensure the number of ingredients is a valid number
            while (!int.TryParse(Console.ReadLine(), out numOfIngs) || numOfIngs <= 0)
                Console.WriteLine("Invalid input. Please enter a valid number of ingredients:");
            // For loop to add ingredients to the recipe object
            for (var i = 0; i < numOfIngs; i++)
            {
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                var ingName = Console.ReadLine();
                // While loop to ensure the ingredient name is not null
                while (string.IsNullOrWhiteSpace(ingName))
                {
                    Console.WriteLine(
                        $"Ingredient name cannot be empty. Please enter a valid name for ingredient {i + 1}:");
                    ingName = Console.ReadLine();
                }
                Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                double ingQty;
                // While loop to ensure the quantity is a valid number
                while (true)
                {
                    try
                    {
                        ingQty = ParseDoubleInvariant(Console.ReadLine());
                        if (ingQty <= 0)
                        {
                            Console.WriteLine($"Invalid input. Please enter a valid quantity for ingredient {i + 1}:");
                            continue;
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Invalid input format. Please enter a valid number for ingredient {i + 1}:");
                    }
                }
                Console.Write($"Enter the unit of measurement of ingredient {i + 1}: ");
                var ingUnit = Console.ReadLine();
                // While loop to ensure the unit of measurement is not null
                while (string.IsNullOrWhiteSpace(ingUnit))
                {
                    Console.WriteLine(
                        $"Unit of measurement cannot be empty. Please enter a valid unit for ingredient {i + 1}:");
                    ingUnit = Console.ReadLine();
                }

                // Add the ingredient to the recipe object array
                recipe.AddIngredient(ingName, ingQty, ingUnit);
            }
            Console.WriteLine("Enter the number of steps:");
            int numOfSteps;
            // While loop to ensure the number of steps is a valid number
            while (!int.TryParse(Console.ReadLine(), out numOfSteps) || numOfSteps <= 0)
                Console.WriteLine("Invalid input. Please enter a valid number of steps:");
            // For loop to add steps to the recipe object
            for (var i = 0; i < numOfSteps; i++)
            {
                Console.Write($"Enter step {i + 1}: ");
                var step = Console.ReadLine();
                // While loop to ensure the step is not null
                while (string.IsNullOrWhiteSpace(step))
                {
                    Console.WriteLine($"Step {i + 1} cannot be empty. Please enter a valid step:");
                    step = Console.ReadLine();
                }
                recipe.AddStep(step);
            }
            Console.WriteLine("Recipe added successfully!");
        } // End of AddRecipe method

        // <-------------------------------------------------------------------------------------->

        // Method to display a recipe with its name, ingredients, and steps. It takes a Recipe object as a parameter.
        public static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine($"Recipe: {recipe.recipeName}\n");
            Console.WriteLine("Ingredients:");
            for (var i = 0; i < recipe.ingredients.Length; i++)
            {
                var ingredient = recipe.ingredients[i];
                Console.WriteLine($"{i + 1}. {ingredient.ingName} - {ingredient.ingQty} {ingredient.ingUnit}\n");
            }
            Console.WriteLine("Steps:");
            for (var i = 0; i < recipe.steps.Length; i++)
            {
                var step = recipe.steps[i];
                Console.WriteLine($"{i + 1}. {step}");
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to delete a recipe. It takes a Recipe object as a parameter.
        public static void DeleteRecipeConfirmation(Recipe recipe)
        {
            Console.WriteLine("Are you sure you want to delete the recipe? (Y/N)");
            var confirmation = Console.ReadLine();

            // While loop to ensure the user enters either "Y" or "N"
            while (!confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase) && !confirmation.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid input. Please enter Y (Yes) or N (No):");
                confirmation = Console.ReadLine();
            }

            if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                recipe.ClearRecipe();
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }

        // Method to scale a recipe by a factor of 0.5, 2, or 3. It takes a Recipe object as a parameter.
        public static void ScaleRecipe(Recipe recipe)
        {
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