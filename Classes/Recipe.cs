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

// This import statement is used to import the System.Collections.Generic namespace which is used to access the List and Dictionary classes.
using System;
using System.Collections.Generic;
using System.Linq;

// Purpose: This file contains the Recipe class which is used to store the name, ingredients, steps, and original quantity of a recipe.
namespace RecipeTracker.Classes
{
    public class Recipe
    {
        // Properties for the Recipe class

        public string recipeName { get; set; }
        // A list of ingredients used in the recipe.
        public List<Ingredient> ingredients { get; set; }
        // A list of steps to follow to make the recipe.
        public <List>string steps { get; set; }
        // A list of original quantities of ingredients used in the recipe.
        public <List>double originalQty { get; set; }
        // A list of original units of ingredients used in the recipe.
        public <List>string originalUnits { get; set; }

        // This is the default constructor for the Recipe class.
        public Recipe()
        {
            recipeName = string.Empty;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            originalQty = new List<double>();
            originalUnits = new List<string>();
        }

        // This is a parameterised constructor for the Recipe class.
        public Recipe(string name, List<Ingredient> ing, List<string> steps)
        {
            recipeName = name;
            ingredients = ing;
            this.steps = steps;
            // Create a list of original quantities and units for the ingredients.
            originalQty = ing.Select(ingredient => ingredient.ingQty).ToList();
            originalUnits = ing.Select(ingredient => ingredient.ingUnit).ToList();
        }
        // <-------------------------------------------------------------------------------------->

        // Method to add an ingredient to the recipe. It takes the name, quantity, and unit of the ingredient as parameters.
        public void AddIngredient(string name, double qty, string unit)
        {
            // Create a new Ingredient object with the given name, quantity, and unit.
            Ingredient newIngredient = new Ingredient(name, qty, unit);
            // Add the new ingredient to the list of ingredients.
            ingredients.Add(newIngredient);
            // Add the original quantity of the ingredient to the list of original quantities.
            originalQty.Add(qty);
        }

        // <-------------------------------------------------------------------------------------->

        // Method to add a step to the recipe. It takes the step as a parameter.
        public void AddStep(string step)
        {
            // Add the step to the list of steps.
            steps.Add(step);
        }

        // <-------------------------------------------------------------------------------------->

        // Method to scale the recipe by a factor of 0.5, 2, or 3. It multiplies the quantity of all ingredients by the given factor.
        public bool ScaleRecipe(double factor)
        {
            if (factor != 0.5 && factor != 2 && factor != 3)
            {
                return false; // Ensure factor is one of the accepted values.
            }

            // Loop through all ingredients in the recipe.
            for (var i = 0; i < ingredients.Count; i++)
            {
                // Pass the original quantity and unit with the factor to ConvertUnit
                // ConvertUnit should handle the scaling and conversion to a suitable unit.
                (double scaledQty, string newUnit) = RecipeOperations.ConvertUnit(ingredients[i].ingUnit, ingredients[i].ingQty[i], factor);

                // Update the quantity and unit of the ingredient.
                ingredients[i].ingQty = scaledQty;
                ingredients[i].ingUnit = newUnit;
            }
            return true;
        }

        // <-------------------------------------------------------------------------------------->

        // Method to reset the quantity of all ingredients in a recipe to the original quantity.
        public void ResetQuantity()
        {
            // Loop through all ingredients in the recipe.
            for (var i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].ingQty = originalQty[i];
                ingredients[i].ingUnit = originalUnits[i];
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to remove a recipe from the list of recipes.
        public void ClearRecipe()
        {
            // Clear the list of ingredients.
            ingredients.Clear();
            // Clear the list of steps.
            steps.Clear();
            // Clear the list of original quantities.
            originalQty.Clear();
        }
        // <-------------------------------------------------------------------------------------->
    }
} // End of Recipe class

// < -------------------------------------------END------------------------------------------- >