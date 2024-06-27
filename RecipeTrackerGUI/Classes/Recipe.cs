/*
 References:
    -   [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
    -   [Food Groups](https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/)
    -   [Calorie Intake Chart](https://www.webmd.com/diet/calories-chart)
    -   [Definitive Guide to WPF Colors] (https://www.codeproject.com/Articles/5296124/Definitive-guide-to-WPF-colors-color-spaces-color)
    -   [Binding declarations overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-8.0)
    -   [Value Converters In WPF] (https://www.c-sharpcorner.com/article/value-converters-in-wpf/#:~:text=Value%20converters%20are%20used%20to%20display%20values%20in,and%20values%20that%20derive%20from%20multiple%20bound%20elements.)
    -   [Controls in WPF] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-6.0)
    -   [How to open a message box (WPF .NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-message-box?view=netdesktop-6.0)
    -   [How to create a template for a control (WPF.NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-6.0)
    -   [Data binding overview (WPF .NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0)
    -   [XAML overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-6.0)
    -   [Overview of WPF windows (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/?view=netdesktop-6.0)
    -   [Routed events overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-6.0)
    -   [Attached events overview (WPF .NET](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/attached-events-overview?view=netdesktop-6.0)
 */

using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/*
    This class is used to define the properties of a recipe.
    The properties include the name, ingredients, and steps of the recipe.
    The recipe class is used to create and manage recipes in the Recipe Tracker application.
*/

namespace RecipeTrackerGUI.Classes
{
    // Recipe class that defines the properties of a recipe
    public class Recipe : INotifyPropertyChanged
    {
        public string recipeName { get; set; }

        // List of ingredients in the recipe
        public List<Ingredient> ingredients { get; set; }

        // List of steps in the recipe
        public List<Step> steps { get; set; }

        public List<double> originalQty { get; set; }
        public List<string> originalUnits { get; set; }

        // Default constructor for the Recipe class
        public Recipe()
        {
            recipeName = string.Empty;
            ingredients = new List<Ingredient>();
            steps = new List<Step>();
            originalQty = new List<double>();
            originalUnits = new List<string>();
        }

        // Parameterized constructor for the Recipe class that initializes the properties of the recipe (name, ingredients, and steps)
        public Recipe(string name, List<Ingredient> ing, List<string> stepDescriptions)
        {
            recipeName = name;
            ingredients = ing;
            // Convert the list of step descriptions to a list of Step objects with IsCompleted set to false
            steps = stepDescriptions.Select(desc => new Step { Description = desc, IsCompleted = false }).ToList();
            // Save the original quantities and units of the ingredients for scaling and resetting
            originalQty = ing.Select(ingredient => ingredient.ingQty).ToList();
            originalUnits = ing.Select(ingredient => ingredient.ingUnit).ToList();
        }

        // PropertyChanged event that is raised when a property value changes (used for data binding)
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged method that raises the PropertyChanged event when a property value changes (used for data binding)
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // <-------------------------------------------------------------------------------------->

        // AddIngredient method that adds an ingredient to the recipe
        public void AddIngredient(string name, double qty, string unit, int calories, string foodGroup)
        {
            // Create a new Ingredient object with the specified properties and add it to the list of ingredients
            Ingredient newIngredient = new Ingredient(name, qty, unit, calories, foodGroup);
            ingredients.Add(newIngredient);
            originalQty.Add(qty);
            originalUnits.Add(unit);
        }

        // <-------------------------------------------------------------------------------------->

        // This method adds a step to the recipe by creating a new Step object with the specified description and adding it to the list of steps.
        public void AddStep(string stepDescription)
        {
            steps.Add(new Step { Description = stepDescription, IsCompleted = false });
            OnPropertyChanged(nameof(steps));
        }

        // <-------------------------------------------------------------------------------------->

        // This method scales the quantities of the ingredients in the recipe by the specified factor (0.5, 2, or 3).
        public bool ScaleRecipe(double factor)
        {
            // Check if the factor is valid (0.5, 2, or 3) and return false if it is not
            if (factor != 0.5 && factor != 2 && factor != 3)
            {
                return false;
            }
            // For loop to scale the quantities of the ingredients in the recipe. It stores the scaled quantity and new unit in the ingredient object.
            for (var i = 0; i < ingredients.Count; i++)
            {
                (double scaledQty, string newUnit) = RecipeOperations.ConvertUnit(ingredients[i].ingUnit, ingredients[i].ingQty, factor);
                ingredients[i].ingQty = scaledQty;
                ingredients[i].ingUnit = newUnit;
            }
            return true;
        }

        // <-------------------------------------------------------------------------------------->

        // This method resets the quantities of the ingredients in the recipe to their original values.
        public void ResetQuantity()
        {
            // For loop to reset the quantities of the ingredients in the recipe to their original values.
            for (var i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].ingQty = originalQty[i];
                ingredients[i].ingUnit = originalUnits[i];
            }
        }

        // <-------------------------------------------------------------------------------------->

        // This method clears the ingredients, steps, original quantities, and original units of the recipe.
        public void ClearRecipe()
        {
            ingredients.Clear();
            steps.Clear();
            originalQty.Clear();
            originalUnits.Clear();
        }

        // <-------------------------------------------------------------------------------------->

        // This method calculates the total calorie content of the recipe by summing the calorie values of all the ingredients.
        public int CalculateTotalCalories()
        {
            return ingredients.Sum(i => i.Calories);
        }

        // <-------------------------------------------------------------------------------------->

        // This method returns a string with information about the calorie content of the recipe based on the total calorie value.
        public string GetCalorieInfo()
        {
            // Variable to store the total calorie content of the recipe
            int totalCalories = CalculateTotalCalories();
            if (totalCalories < 200)
            {
                return "This recipe is low in calories, making it a great option for a snack or a light meal.";
            }
            else if (totalCalories >= 200 && totalCalories <= 500)
            {
                return "This recipe has a moderate amount of calories, suitable for a balanced meal.";
            }
            else if (totalCalories > 500 && totalCalories <= 800)
            {
                return "This recipe is high in calories, so it should be consumed in moderation.";
            }
            else
            {
                return "This recipe is very high in calories and should be consumed sparingly.";
            }
        }
    }
}

// < -------------------------------------------END------------------------------------------- >