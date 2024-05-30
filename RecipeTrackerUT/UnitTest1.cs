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