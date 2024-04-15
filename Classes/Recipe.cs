// This import statement is used to import the System.Collections.Generic namespace which is used to access the List and Dictionary classes.
using System.Collections.Generic;

// Purpose: This file contains the Recipe class which is used to store the name, ingredients, steps, and original quantity of a recipe.
namespace RecipeTracker.Classes
{
    public class Recipe
    {
        // Properties for the Recipe class

        public string recipeName { get; set; }

        // A list of ingredients used in the recipe which is a list of Ingredient objects.
        public List<Ingredient> ingredients { get; set; }

        // A list of steps to follow to make the recipe.
        public List<string> steps { get; set; }

        // A dictionary of ingredients and their original quantity used in the recipe.
        public Dictionary<Ingredient, double> originalQty { get; set; }

        // Parameterless constructor for the Recipe class (default constructor).
        public Recipe()
        {
            recipeName = string.Empty;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            originalQty = new Dictionary<Ingredient, double>();
        }

        // Parameterised constructor for the Recipe class (overloaded constructor) which means it can be called with different parameters.
        public Recipe(string name, List<Ingredient> ing, List<string> steps, Dictionary<Ingredient, double> qty)
        {
            recipeName = name;
            ingredients = ing;
            this.steps = steps;
            originalQty = qty;
        }

        // <-------------------------------------------------------------------------------------->

        // Method to add an ingredient to the recipe. It takes the name, quantity, and unit of the ingredient as parameters.
        public void AddIngredient(string name, double qty, string unit)
        {
            // Create a new Ingredient object with the given name, quantity, and unit.
            Ingredient ing = new Ingredient(name, qty, unit);
            // Add the ingredient to the ingredients list.
            ingredients.Add(ing);
            // Add the ingredient and its original quantity to the originalQty dictionary.
            originalQty[ing] = qty;
        }

        // <-------------------------------------------------------------------------------------->

        // Method to add a step to the recipe. It takes the step as a parameter.
        public void AddStep(string step)
        {
            steps.Add(step);
        }

        // <-------------------------------------------------------------------------------------->

        // Method to scale the recipe by a factor of 0.5, 2, or 3. It multiplies the quantity of all ingredients by the given factor.
        public bool ScaleRecipe(double factor)
        {
            if (factor == 0.5 || factor == 2 || factor == 3)
            {
                foreach (var ing in ingredients) ing.ingQty = originalQty[ing] * factor;
                return true;
            }

            return false;
        }

        // <-------------------------------------------------------------------------------------->

        // Method to reset the quantity of all ingredients in the recipe to their original quantity.
        public void ResetQuantities()
        {
            // Loop through all ingredients in the recipe.
            foreach (var ing in ingredients) ing.ingQty = originalQty[ing];
        }

        // <-------------------------------------------------------------------------------------->

        // Method to clear all ingredients and steps from the recipe.
        public void ClearRecipe()
        {
            recipeName = string.Empty;
            ingredients.Clear();
            steps.Clear();
            originalQty.Clear();
        }
    }
}

// < -------------------------------------------END------------------------------------------- >