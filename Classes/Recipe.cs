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
        }

        // Parameterised constructor for the Recipe class (overloaded constructor) which means it can be called with different parameters.
        public Recipe(string name, List<Ingredient> ing, List<string> steps, Dictionary<Ingredient, double> qty)
        {
            recipeName = name;
            ingredients = ing;
            this.steps = steps;
            originalQty = qty;
        }
    }
}