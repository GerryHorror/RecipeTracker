/*
References:
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
*/

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Part 2
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
        public static List<Recipe> AddRecipes(List<Recipe> recipes, NotifyUser notifyCalories)
        {
            // Boolean to check if the user wants to add more recipes or return to the main menu
            bool addMore = true;

            while (addMore)
            {
                Console.WriteLine("Enter the name of the recipe:");
                // Read the recipe name from the user
                var recipeName = Console.ReadLine().Trim();
                // Validate the recipe name to ensure it is not empty
                while (string.IsNullOrWhiteSpace(recipeName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Recipe name cannot be empty. Please enter a valid name:");
                    Console.ResetColor();
                    recipeName = Console.ReadLine().Trim();
                }

                Console.WriteLine("Enter the number of ingredients:");
                int numOfIngs;
                // Validate the number of ingredients to ensure it is a positive integer
                while (!int.TryParse(Console.ReadLine(), out numOfIngs) || numOfIngs <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number of ingredients:");
                    Console.ResetColor();
                }
                // Create a new List to store the ingredients of the recipe
                List<Ingredient> ingredients = new List<Ingredient>();
                // Loop to read the ingredient details from the user
                for (var i = 0; i < numOfIngs; i++)
                {
                    Console.Write($"Enter the name of ingredient {i + 1}: ");
                    var ingName = Console.ReadLine().Trim();
                    while (string.IsNullOrWhiteSpace(ingName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Ingredient name cannot be empty. Please enter a valid name for ingredient {i + 1}:");
                        Console.ResetColor();
                        ingName = Console.ReadLine().Trim();
                    }

                    Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                    double ingQty;
                    while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out ingQty) || ingQty <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid input. Please enter a valid quantity for ingredient {i + 1}:");
                        Console.ResetColor();
                    }

                    Console.Write($"Enter the unit of measurement of ingredient {i + 1}: ");
                    var ingUnit = Console.ReadLine().Trim().ToLower();
                    while (string.IsNullOrWhiteSpace(ingUnit) || double.TryParse(ingUnit, out _) ||
                           (!conversionFactors.ContainsKey(ingUnit) && !aliases.ContainsKey(ingUnit)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Unit of measurement cannot be empty, a number, or an unrecognised unit. Please enter a valid unit for ingredient {i + 1}:");
                        Console.ResetColor();
                        ingUnit = Console.ReadLine().Trim().ToLower();
                    }

                    // Explanation of calories
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Calories represent the amount of energy provided by a food item. The higher the calorie count, the more energy the food provides.");
                    Console.ResetColor();
                    // Prompt the user to enter the number of calories for the ingredient and validate the input
                    Console.Write($"Enter the number of calories for ingredient {i + 1}: ");
                    int calories;
                    while (true)
                    {
                        var input = Console.ReadLine();
                        if (string.IsNullOrEmpty(input))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Calories cannot be null. Please enter a valid number of calories for ingredient {i + 1}:");
                            Console.ResetColor();
                            continue;
                        }
                        if (!int.TryParse(input, out calories))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Invalid input. Please enter a valid number of calories for ingredient {i + 1}:");
                            Console.ResetColor();
                            continue;
                        }
                        if (calories < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Calories cannot be negative. Please enter a valid number of calories for ingredient {i + 1}:");
                            Console.ResetColor();
                            continue;
                        }
                        if (calories == 0 && ingName.ToLower() != "water")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Calories cannot be 0 except for water. Please enter a valid number of calories for ingredient {i + 1}:");
                            Console.ResetColor();
                            continue;
                        }
                        break;
                    }

                    // Explanation of food groups
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Food groups classify foods based on their nutritional properties. The seven food groups are:");
                    Console.ResetColor();
                    // Display the list of food groups for the user to choose from and prompt the user to select a food group
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("1. Starchy foods");
                    Console.WriteLine("2. Vegetables and fruits");
                    Console.WriteLine("3. Dry beans, peas, lentils and soya");
                    Console.WriteLine("4. Chicken, fish, meat and eggs");
                    Console.WriteLine("5. Milk and dairy products");
                    Console.WriteLine("6. Fats and oil");
                    Console.WriteLine("7. Water");
                    Console.ResetColor();
                    Console.Write($"Select the food group for the ingredient {ingName}: ");
                    string foodGroup;
                    while (true)
                    {
                        // Validate the input to ensure it is a valid food group (1-7) and assign the selected food group to the variable
                        switch (Console.ReadLine())
                        {
                            case "1":
                                foodGroup = "Starchy foods";
                                break;

                            case "2":
                                foodGroup = "Vegetables and fruits";
                                break;

                            case "3":
                                foodGroup = "Dry beans, peas, lentils and soya";
                                break;

                            case "4":
                                foodGroup = "Chicken, fish, meat and eggs";
                                break;

                            case "5":
                                foodGroup = "Milk and dairy products";
                                break;

                            case "6":
                                foodGroup = "Fats and oil";
                                break;

                            case "7":
                                foodGroup = "Water";
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid choice. Please select a valid food group (1-7):");
                                Console.ResetColor();
                                continue;
                        }
                        break;
                    }

                    // Create a new Ingredient object with the provided details and add it to the ingredients list
                    ingredients.Add(new Ingredient(ingName, ingQty, ingUnit, calories, foodGroup));
                }

                Console.WriteLine("Enter the number of steps:");
                int numOfSteps;
                while (!int.TryParse(Console.ReadLine(), out numOfSteps) || numOfSteps <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number of steps:");
                }
                // Create a new List to store the steps of the recipe
                List<string> steps = new List<string>();
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

                // Calculate total calories and trigger notification if necessary
                var result = CalculateTotalCalories(recipe, notifyCalories);

                // Display a success message
                Console.WriteLine("Recipe added successfully!");

                // Ask the user if they want to add another recipe or return to the main menu
                Console.WriteLine("Do you want to add another recipe?");
                var addMoreInput = Console.ReadLine().Trim().ToLower();
                while (addMoreInput != "yes" && addMoreInput != "no")
                {
                    Console.WriteLine("Invalid input. Please enter 'Yes' or 'No':");
                    addMoreInput = Console.ReadLine().Trim().ToLower();
                }

                if (addMoreInput == "no")
                {
                    addMore = false;
                }
            }

            return recipes;
        }

        // <-------------------------------------------------------------------------------------->

        // Method to reset the quantities of all ingredients in the recipes array.
        public static void ResetQuantities(List<Recipe> recipes)
        {
            // Check if there are no recipes available
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No recipes available.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Available recipes:");
            // Display the list of available recipes for the user to choose from
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].recipeName}");
            }
            // Prompt the user to select the recipe to reset quantities for
            Console.WriteLine("Please select the recipe you want to reset quantities for:");
            // Validate the input to ensure it is a valid number within the range of available recipes
            int recipeIndex;
            while (!int.TryParse(Console.ReadLine(), out recipeIndex) || recipeIndex < 1 || recipeIndex > recipes.Count)
            {
                Console.WriteLine("Invalid input. Please select a valid recipe:");
            }
            // Get the selected recipe based on the index provided by the user
            Recipe selectedRecipe = recipes[recipeIndex - 1];
            // Reset the quantities of all ingredients in the selected recipe
            selectedRecipe.ResetQuantity();
            Console.WriteLine("Quantities reset successfully!");
        }

        // <-------------------------------------------------------------------------------------->

        // Method to display the details of a recipe. It takes a Recipe object as a parameter.
        public static void DisplayRecipes(List<Recipe> recipes)
        {
            // Check if there are no recipes available
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No recipes available.");
                Console.ResetColor();
                return;
            }

            // Sort the recipes by name in alphabetical order
            var sortedRecipes = recipes.OrderBy(recipe => recipe.recipeName).ToList();

            Console.WriteLine("Available Recipes:");
            Console.WriteLine("------------------");
            // Loop through each recipe in the sorted list and display its name and index
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].recipeName}");
                Console.ResetColor();
            }

            Console.WriteLine("------------------\n");
            Console.WriteLine("Enter the number of the recipe you want to view, or 0 to return to the menu:");
            // Validate the input to ensure it is a valid number within the range of available recipes
            int recipeIndex;
            while (!int.TryParse(Console.ReadLine(), out recipeIndex) || recipeIndex < 0 || recipeIndex > sortedRecipes.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid recipe number:");
                Console.ResetColor();
            }
            // Check if the user wants to return to the main menu
            if (recipeIndex > 0)
            {
                DisplayRecipeDetails(sortedRecipes[recipeIndex - 1], NotifyIfHighCalories);
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to display the details of a recipe from the recipes list based on the index provided by the user.
        public static void DisplayRecipeDetails(Recipe recipe, NotifyUser notifyCalories)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe Name:");
            Console.ResetColor();
            Console.WriteLine($"  {recipe.recipeName}");

            Console.WriteLine(new string('-', 40));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ingredients:");
            Console.ResetColor();
            Console.WriteLine(new string('-', 40));

            // Loop through each ingredient in the recipe and display its details
            for (var i = 0; i < recipe.ingredients.Count; i++)
            {
                var ingredient = recipe.ingredients[i];
                Console.WriteLine($"Ingredient {i + 1}:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  Name: {ingredient.ingName}");
                Console.ResetColor();
                Console.WriteLine($"  Quantity: {ingredient.ingQty} {ingredient.ingUnit}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"  Calories: {ingredient.Calories}");
                Console.ResetColor();
                Console.WriteLine($"  Food Group: {ingredient.FoodGroup}");
                Console.WriteLine(new string('-', 40));
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Steps:");
            Console.ResetColor();
            Console.WriteLine(new string('-', 40));

            // Loop through each step in the recipe and display it
            for (var i = 0; i < recipe.steps.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Step {i + 1}:");
                Console.ResetColor();
                Console.WriteLine($"  {recipe.steps[i]}");
                Console.WriteLine(new string('-', 40));
            }

            // Calculate the total calories in the recipe and display it
            var result = CalculateTotalCalories(recipe, notifyCalories);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total Calories: {result.TotalCalories}");
            Console.ResetColor();
            Console.ForegroundColor = result.InfoColor;
            Console.WriteLine(result.CalorieInfo);
            Console.ResetColor();
            Console.ReadKey();
        }

        // <-------------------------------------------------------------------------------------->

        // Method to calculate the total calories in a recipe based on the ingredients.
        // This method is a tuple (it returns multiple values) containing the total calories and a string with calorie information.
        public static (int TotalCalories, string CalorieInfo, ConsoleColor InfoColor) CalculateTotalCalories(Recipe recipe, NotifyUser notifyCalories)
        {
            // Initialise the total calories to 0
            int totalCalories = 0;

            // Loop through each ingredient in the recipe and add its calories to the total
            foreach (var ingredient in recipe.ingredients)
            {
                // Check if calories for the ingredient are null and return an error message
                if (ingredient.Calories == 0 && ingredient.ingName.ToLower() != "water")
                {
                    return (0, "Calories cannot be 0 except for water", ConsoleColor.Red);
                }

                // Check if the calories for the ingredient are negative and return an error message
                if (ingredient.Calories < 0)
                {
                    return (0, "Calories cannot be negative", ConsoleColor.Red);
                }
                totalCalories += ingredient.Calories;
            }

            // Notify the user if the total calories exceed 300 using the delegate
            if (totalCalories > 300)
            {
                notifyCalories?.Invoke(totalCalories);
            }

            // Return the total calories and a string with the calorie information
            string calorieInfo;
            ConsoleColor infoColor;
            if (totalCalories < 200)
            {
                calorieInfo = "This recipe is low in calories, making it a great option for a snack or a light meal.";
                infoColor = ConsoleColor.Cyan;
            }
            else if (totalCalories >= 200 && totalCalories <= 500)
            {
                calorieInfo = "This recipe has a moderate amount of calories, suitable for a balanced meal.";
                infoColor = ConsoleColor.Yellow;
            }
            else if (totalCalories > 500 && totalCalories <= 800)
            {
                calorieInfo = "This recipe is high in calories, so it should be consumed in moderation.";
                infoColor = ConsoleColor.DarkYellow;
            }
            else
            {
                calorieInfo = "This recipe is very high in calories and should be consumed sparingly.";
                infoColor = ConsoleColor.Red;
            }
            return (totalCalories, calorieInfo, infoColor);
        }

        // This is a delegate that will notify the user once calories for a recipe exceeds 300.
        public delegate void NotifyUser(int totalCalories);

        public static void NotifyIfHighCalories(int totalCalories)
        {
            if (totalCalories > 300)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning: This recipe exceeds 300 calories with a total of {totalCalories} calories!");
                Console.ResetColor();
            }
        }

        // <-------------------------------------------------------------------------------------->
        // Quality of life improvement: ViewAllRecipes method to display all recipes in alphabetical order.
        public static void ViewAllRecipes(List<Recipe> recipes)
        {
            // Check if there are no recipes available
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No recipes available.");
                Console.ResetColor();
                return;
            }

            // Sort the recipes by name in alphabetical order
            var sortedRecipes = recipes.OrderBy(recipe => recipe.recipeName).ToList();

            Console.Clear(); // Clear the console for a better viewing experience
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All Available Recipes:");
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();

            // Loop through each recipe in the sorted list and display its details
            foreach (var recipe in sortedRecipes)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Recipe Name: {recipe.recipeName}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ingredients:");
                Console.ResetColor();
                Console.WriteLine(new string('-', 50));

                // Loop through each ingredient in the recipe and display its details
                for (var i = 0; i < recipe.ingredients.Count; i++)
                {
                    var ingredient = recipe.ingredients[i];
                    Console.WriteLine($"{i + 1}. {ingredient.ingName}");
                    Console.WriteLine($"   Quantity: {ingredient.ingQty} {ingredient.ingUnit}");
                    Console.WriteLine($"   Calories: {ingredient.Calories}");
                    Console.WriteLine($"   Food Group: {ingredient.FoodGroup}");
                    Console.WriteLine(new string('-', 50));
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Steps:");
                Console.ResetColor();
                Console.WriteLine(new string('-', 50));

                // Loop through each step in the recipe and display it
                for (var i = 0; i < recipe.steps.Count; i++)
                {
                    Console.WriteLine($"Step {i + 1}: {recipe.steps[i]}");
                    Console.WriteLine(new string('-', 50));
                }

                Console.WriteLine(new string('=', 50));
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press any key to return to the menu...");
            Console.ResetColor();
            Console.ReadKey();
        }

        // <-------------------------------------------------------------------------------------->
        // Method to delete a recipe from the recipes list. It takes an array of Recipe objects as a parameter.
        public static void DeleteRecipe(List<Recipe> recipes)
        {
            // Check if there are no recipes available
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No recipes available.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Available recipes:");
            Console.WriteLine(new string('-', 40));
            // Display the list of available recipes for the user to choose from
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].recipeName}");
            }
            Console.WriteLine(new string('-', 40));

            // Prompt the user to select the recipe to delete
            Console.WriteLine("Please select the recipe you want to delete by entering the corresponding number:");
            // Validate the input to ensure it is a valid number within the range of available recipes
            int recipeIndex;
            while (!int.TryParse(Console.ReadLine(), out recipeIndex) || recipeIndex < 1 || recipeIndex > recipes.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please select a valid recipe number:");
                Console.ResetColor();
            }

            // Get the selected recipe based on the index provided by the user
            Recipe selectedRecipe = recipes[recipeIndex - 1];

            // Ask for confirmation before deleting
            Console.WriteLine($"Are you sure you want to delete '{selectedRecipe.recipeName}'? (yes/no)");
            string confirmation;
            // Validate the input to ensure it is either 'yes' or 'no'
            while (true)
            {
                confirmation = Console.ReadLine().ToLower();
                if (confirmation == "yes" || confirmation == "no")
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                Console.ResetColor();
            }

            if (confirmation == "yes")
            {
                // Remove the selected recipe from the recipes list
                recipes.Remove(selectedRecipe);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Recipe deleted successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Deletion cancelled.");
                Console.ResetColor();
            }
        }

        // <-------------------------------------------------------------------------------------->
        // This method scales a recipe based on a scaling factor provided by the user (0.5, 2, or 3).
        public static void ScaleRecipe(List<Recipe> recipes)
        {
            // Check if there are no recipes available in the list
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("Available recipes:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].recipeName}");
            }
            // Prompt the user to select a recipe to scale
            Console.WriteLine("Please select the recipe you want to scale:");
            int recipeIndex;

            // Ensuring the user inputs a valid recipe index
            while (!int.TryParse(Console.ReadLine(), out recipeIndex) || recipeIndex < 1 || recipeIndex > recipes.Count)
            {
                Console.WriteLine("Invalid input. Please select a valid recipe:");
            }

            // Get the recipe based on the index provided by the user
            Recipe recipe = recipes[recipeIndex - 1];

            Console.WriteLine("Enter the scaling factor (0.5, 2, or 3):");
            double factor;
            // Ensuring the user inputs a valid scaling factor
            if (!double.TryParse(Console.ReadLine(), out factor))
            {
                Console.WriteLine("Invalid input format. Please enter a valid number.");
                return;
            }
            // Check if the scaling factor is valid (0.5, 2, or 3) and display an error message if not.
            if (factor != 0.5 && factor != 2 && factor != 3)
            {
                Console.WriteLine("Invalid scaling factor. Please enter 0.5, 2, or 3.");
                return;
            }
            // Scaling the recipe
            recipe.ScaleRecipe(factor);
            // Display a success message if the recipe is scaled successfully
            Console.WriteLine("Recipe scaled successfully!");
        }

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