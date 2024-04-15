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
            if (factor != 0.5 && factor != 2 && factor != 3)
            {
                return false; // Ensure factor is one of the accepted values.
            }

            foreach (var ing in ingredients)
            {
                if (originalQty.ContainsKey(ing))
                {
                    // Retrieve the original quantity for this ingredient from the dictionary.
                    double originalQuantity = originalQty[ing];

                    // Scale the quantity and convert the units accordingly.
                    (double newQty, string newUnit) = ConvertUnit(ing.ingUnit, originalQuantity, factor);

                    // Update the ingredient object with the new quantity and unit.
                    ing.ingQty = newQty;
                    ing.ingUnit = newUnit;
                }
                else
                {
                    return false; // If the original quantity is not found, return false.
                }
            }
            return true; // Return true if all ingredients are scaled successfully.
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

        // <-------------------------------------------------------------------------------------->

        // Method to convert the quantity of an ingredient to a different unit of measurement.
        private (double, string) ConvertUnit(string unit, double qty, double scale)
        {
            // Dictionary to store the conversion factors for different units of measurement.
            Dictionary<string, double> conversionFactors = new Dictionary<string, double>
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