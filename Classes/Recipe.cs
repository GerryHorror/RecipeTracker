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

        // An array of ingredients used in the recipe.
        public Ingredient[] ingredients { get; set; }

        // A list of steps to follow to make the recipe.
        public string[] steps { get; set; }

        // A dictionary of ingredients and their original quantity used in the recipe.
        public double[] originalQty { get; set; }

        // This is the default constructor for the Recipe class.
        public Recipe()
        {
            recipeName = string.Empty;
            ingredients = new Ingredient[0];
            steps = new string[0];
            originalQty = new double[0];
        }

        // This is a parameterized constructor for the Recipe class.
        public Recipe(string name, Ingredient[] ing, string[] steps, double[] qty)
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
            // Create a new array with increased size to accommodate the new ingredient.
            Ingredient[] newIngredients = new Ingredient[ingredients.Length + 1];
            // Copy the existing ingredients to the new array.
            Array.Copy(ingredients, newIngredients, ingredients.Length);
            // Add the new ingredient to the end of the new array.
            newIngredients[newIngredients.Length - 1] = ing;
            // Update the ingredients property with the new array.
            ingredients = newIngredients;
            // Create a new array with increased size to accommodate the new quantity.
            double[] newOriginalQty = new double[originalQty.Length + 1];
            // Copy the existing quantities to the new array.
            Array.Copy(originalQty, newOriginalQty, originalQty.Length);
            // Add the new quantity to the end of the new array.
            newOriginalQty[newOriginalQty.Length - 1] = qty;
            // Update the originalQty property with the new array.
            originalQty = newOriginalQty;
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
                // Calculate the new quantity by multiplying the original quantity by the factor.
                double newQty = ingredients[i].ingQty * factor;
                // Convert the unit of measurement to a suitable unit.
                (double scaledQty, string newUnit) = ConvertUnit(ingredients[i].ingUnit, newQty, factor);
                // Update the quantity and unit of the ingredient.
                ingredients[i].ingQty = scaledQty;
                ingredients[i].ingUnit = newUnit;
            }
            return true;
        }

        // <-------------------------------------------------------------------------------------->

        // Method to reset the quantity of all ingredients in the recipe to their original quantity.
        public void ResetQuantities()
        {
            // Loop through all ingredients in the recipe.
            for (var i = 0; i < ingredients.Length; i++)
            {
                // Reset the quantity of the ingredient to the original quantity.
                ingredients[i].ingQty = originalQty[i];
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to clear all ingredients and steps from the recipe.
        public void ClearRecipe()
        {
            recipeName = string.Empty;
            ingredients = new Ingredient[0];
            steps = new string[0];
            originalQty = new double[0];
        }

        // <-------------------------------------------------------------------------------------->

        // Method to convert the quantity of an ingredient to a different unit of measurement.
        private (double, string) ConvertUnit(string unit, double qty, double scale)
        {
            // Dictionary to store the conversion factors for different units of measurement.
            var conversionFactors = new Dictionary<string, double>
    {
        // Conversion factors for different units of measurement.
        {"tsp", 1},      // 1 tsp
        {"tbsp", 3},     // 3 tsp
        {"oz", 6},       // 6 tsp (for liquid)
        {"cup", 48},     // 48 tsp
        {"lb", 454},     // 454 grams
        {"g", 1},        // 1 gram
        {"kg", 1000}     // 1000 grams
    };
            // Standardise the unit by converting it to lowercase and removing leading/trailing whitespace.
            string standardizedUnit = unit.ToLower().Trim();
            // Dictionary to store common aliases (abbreviations) for units of measurement.
            var aliases = new Dictionary<string, string>
    {
        // Common aliases (abbreviations) for units of measurement.
        {"tablespoons", "tbsp"},
        {"teaspoons", "tsp"},
        {"ounces", "oz"},
        {"pounds", "lb"},
        {"grams", "g"},
        {"kilograms", "kg"}
    };
            // Check if the unit is an alias and replace it with the standard unit.
            if (aliases.ContainsKey(standardizedUnit))
                standardizedUnit = aliases[standardizedUnit];
            // Check if the unit is not recognised or supported.
            if (!conversionFactors.ContainsKey(standardizedUnit))
            {
                throw new ArgumentException("Unit not recognised or supported.");
            }
            // Calculate the base quantity in terms of the standard unit (grams).
            double scaledQty = qty * scale;
            double baseQty = scaledQty * conversionFactors[standardizedUnit];

            // Attempt to find the best suitable unit
            string newUnit = standardizedUnit;
            double newQty = baseQty;

            // Sort the units based on their conversion factor, largest to smallest, for better unit fit
            var sortedUnits = conversionFactors.OrderByDescending(u => u.Value);
            foreach (var unitPair in sortedUnits)
            {
                double unitQty = baseQty / unitPair.Value;
                if (unitQty >= 1)
                {
                    newUnit = unitPair.Key;
                    newQty = unitQty;
                    break; // Stop at the first suitable larger unit
                }
            }
            // Return the new quantity and unit.
            return (Math.Round(newQty, 2), newUnit);
        }
    }
} // End of Recipe class

// < -------------------------------------------END------------------------------------------- >