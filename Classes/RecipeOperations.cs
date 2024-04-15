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

using System;
using System.Globalization;

namespace RecipeTracker.Classes
{
    public class RecipeOperations
    {
        // Default constructor for the RecipeOperations class.
        public RecipeOperations()
        { }

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
            Console.WriteLine($"Recipe: {recipe.recipeName}");
            Console.WriteLine("Ingredients:");
            // For loop to display the ingredients in the recipe object
            for (var i = 0; i < recipe.ingredients.Count; i++)
                Console.WriteLine(
                    $"{i + 1}. {recipe.ingredients[i].ingName} - {recipe.ingredients[i].ingQty} {recipe.ingredients[i].ingUnit}");
            Console.WriteLine("Steps:");
            var stepNum = 1;
            // For loop to display the steps in the recipe object
            foreach (var step in recipe.steps)
            {
                Console.WriteLine($"{stepNum}. {step}");
                stepNum++;
            }
        } // End of DisplayRecipe method

        // <-------------------------------------------------------------------------------------->

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
} // End of namespace RecipeTracker.Classes