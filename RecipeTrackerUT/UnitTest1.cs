using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RecipeTracker.Classes;

namespace RecipeTrackerUT
{
    [TestClass]
    public class RecipeOperationsTests
    {
        [TestMethod]
        public void CalculateTotalCalories_MultipleIngredients_ShouldReturnCorrectTotalCalories()
        {
            // Arrange (i.e., set up the test) - Create a list of ingredients
            var ingredients = new List<Ingredient>
            {
            new Ingredient ("Chocolate", 100, "grams", 125, "Starchy foods"),
            new Ingredient ("Milk", 200, "ml", 42, "Milk and dairy products"),
            new Ingredient ("Sugar", 50, "grams", 387, "Starchy foods")
            };
            // Create a new recipe object with the ingredients list
            var recipe = new Recipe("Chocolate Cake", ingredients, new List<string> { "Mix all ingredients together", "Bake in the oven for 30 minutes" });
            // Act (i.e., perform the test) - Call the CalculateTotalCalories method
            var result = RecipeOperations.CalculateTotalCalories(recipe, null);
            // Assert (i.e., check the result) - Check if the result is equal to the expected value (125 + 42 + 387 = 554)
            Assert.AreEqual(554, result.TotalCalories);
        }

        [TestMethod]
        public void CalculateTotalCalories_SingleIngredient_ShouldReturnCorrectTotalCalories()
        {
            // Arrange - Create a list of ingredients
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Macaroni", 100, "grams", 371, "Starchy foods")
            };
            // Create a new recipe object with the ingredients list
            var recipe = new Recipe("Macaroni and Cheese", ingredients, new List<string> { "Boil macaroni" });
            // Act - Call the CalculateTotalCalories method
            var result = RecipeOperations.CalculateTotalCalories(recipe, null);
            // Assert - Check if the result is equal to the expected value (371)
            Assert.AreEqual(371, result.TotalCalories);
        }

        [TestMethod]
        public void CalculateTotalCalories_IngredientsWithZeroCalories_ShouldReturnCorrectTotalCalories()
        {
            // Arrange - Create a list of ingredients
            var ingredients = new List<Ingredient>
            {
            new Ingredient ("Water", 250, "ml", 0, "Water"),
            new Ingredient ("Protein powder", 20, "g", 200, "Starchy foods")
            };
            // Create a new recipe object with the ingredients list
            var recipe = new Recipe("Protein Shake", ingredients, new List<string> { "Mix water and protein powder" });
            // Act - Call the CalculateTotalCalories method
            var result = RecipeOperations.CalculateTotalCalories(recipe, null);
            // Assert - Check if the result is equal to the expected value (200)
            Assert.AreEqual(200, result.TotalCalories);
        }

        [TestMethod]
        public void CalculateTotalCalories_MixedCalories_ShouldReturnCorrectTotalCalories()
        {
            // Arrange - Create a list of ingredients
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Ice Cream", 100, "grams", 207, "Milk and dairy products"),
                new Ingredient("Chocolate Syrup", 50, "ml", 100, "Starchy foods"),
                new Ingredient("Whipped Cream", 30, "grams", 52, "Milk and dairy products"),
                new Ingredient("Water", 250, "ml", 0, "Water")
            };
            // Create a new recipe object with the ingredients list
            var recipe = new Recipe("Sundae", ingredients, new List<string> { "Scoop ice cream into a bowl", "Drizzle chocolate syrup", "Add whipped cream" });
            // Act - Call the CalculateTotalCalories method
            var result = RecipeOperations.CalculateTotalCalories(recipe, null);
            // Assert - Check if the result is equal to the expected value (207 + 100 + 52 = 359)
            Assert.AreEqual(359, result.TotalCalories);
        }

        [TestMethod]
        public void CalculateTotalCalories_CaloriesCannotBeZeroForNonWater_ShouldReturnErrorMessage()
        {
            // Arrange - Create a list of ingredients with zero calories for a non-water ingredient
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Ice Cream", 100, "grams", 0, "Milk and dairy products"),
                new Ingredient("Chocolate Syrup", 50, "ml", 100, "Starchy foods"),
                new Ingredient("Whipped Cream", 30, "grams", 52, "Milk and dairy products"),
                new Ingredient("Water", 250, "ml", 0, "Water")
            };
            // Create a new recipe object with the ingredients list
            var recipe = new Recipe("Invalid Recipe", ingredients, new List<string> { "Scoop ice cream into a bowl", "Drizzle chocolate syrup", "Add whipped cream" });
            // Act - Call the CalculateTotalCalories method
            var result = RecipeOperations.CalculateTotalCalories(recipe, null);
            // Assert - Check if the result is equal to the expected value (Error message)
            Assert.AreEqual("Calories cannot be 0 except for water", result.CalorieInfo);
        }

        [TestMethod]
        public void CalculateTotalCalories_NegativeCalories_ShouldReturnErrorMessage()
        {
            // Arrange - Create a list of ingredients with negative calories
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Ice Cream", 100, "grams", -207, "Milk and dairy products"),
                new Ingredient("Chocolate Syrup", 50, "ml", 100, "Starchy foods"),
                new Ingredient("Whipped Cream", 30, "grams", 52, "Milk and dairy products"),
                new Ingredient("Water", 250, "ml", 0, "Water")
            };
            var recipe = new Recipe("Negative Calories Recipe", ingredients, new List<string> { "Scoop ice cream into a bowl", "Drizzle chocolate syrup", "Add whipped cream" });

            // Act - Call the CalculateTotalCalories method
            var result = RecipeOperations.CalculateTotalCalories(recipe, null);

            // Assert - Check if the result is the expected error message
            Assert.AreEqual("Calories cannot be negative", result.CalorieInfo);
            Assert.AreEqual(0, result.TotalCalories);
            Assert.AreEqual(ConsoleColor.Red, result.InfoColor);
        }
    }
}