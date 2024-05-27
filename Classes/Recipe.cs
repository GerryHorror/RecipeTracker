﻿/*
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

        // This is the default constructor for the Recipe class.
        public Recipe()
        {
            recipeName = string.Empty;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            originalQty = new List<double>();
        }

        // This is a parameterised constructor for the Recipe class.
        public Recipe(string name, List<Ingredient> ing, List<string> steps)
        {
            recipeName = name;
            ingredients = ing;
            this.steps = steps;
            originalQty = new List<double>();
            foreach (var ingredient in ing)
            {
                originalQty.Add(ingredient.ingQty);
            }
        }
        // <-------------------------------------------------------------------------------------->

        // Method to add an ingredient to the recipe. It takes the name, quantity, and unit of the ingredient as parameters.
        public void AddIngredient(string name, double qty, string unit)
        {
            Ingredient newIngredient = new Ingredient(name, qty, unit);
            ingredients.Add(newIngredient);
            originalQty.Add(qty);
        }

        // <-------------------------------------------------------------------------------------->

        // Method to add a step to the recipe. It takes the step as a parameter.
        public void AddStep(string step)
        {
            // Create a new array with increased size to accommodate the new step.
            string[] newSteps = new string[steps.Length + 1];
            // Copy the existing steps to the new array.
            Array.Copy(steps, newSteps, steps.Length);
            // Add the new step to the end of the new array.
            newSteps[newSteps.Length - 1] = step;
            // Update the steps property with the new array.
            steps = newSteps;
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
            for (var i = 0; i < ingredients.Length; i++)
            {
                // Pass the original quantity and unit with the factor to ConvertUnit
                // ConvertUnit should handle the scaling and conversion to a suitable unit.
                (double scaledQty, string newUnit) = RecipeOperations.ConvertUnit(ingredients[i].ingUnit, ingredients[i].ingQty, factor);

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
            for (var i = 0; i < ingredients.Length; i++)
            {
                ingredients[i].ingQty = originalQty[i];
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to remove a recipe from the list of recipes.
        public void ClearRecipe()
        {
            recipeName = string.Empty;
            ingredients = new Ingredient[0];
            steps = new string[0];
            originalQty = new double[0];
        }

        // <-------------------------------------------------------------------------------------->
    }
} // End of Recipe class

// < -------------------------------------------END------------------------------------------- >