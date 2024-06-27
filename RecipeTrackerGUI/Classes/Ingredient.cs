/*
 References:
    - [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
    - [Food Groups](https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/)
    - [Calorie Intake Chart](https://www.webmd.com/diet/calories-chart)
    - [Definitive Guide to WPF Colors](https://www.codeproject.com/Articles/5296124/Definitive-guide-to-WPF-colors-color-spaces-color)
    - [Binding declarations overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-8.0)
    - [Value Converters In WPF](https://www.c-sharpcorner.com/article/value-converters-in-wpf/#:~:text=Value%20converters%20are%20used%20to%20display%20values%20in,and%20values%20that%20derive%20from%20multiple%20bound%20elements.)
    - [Controls in WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-6.0)
    - [How to open a message box(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-message-box?view=netdesktop-6.0)
    - [How to create a template for a control (WPF.NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-6.0)
    - [Data binding overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0)
    - [XAML overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-6.0)
    - [Overview of WPF windows(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/?view=netdesktop-6.0)
    - [Routed events overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-6.0)
    - [Attached events overview (WPF .NET](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/attached-events-overview?view=netdesktop-6.0)
    - [Tutorial on How to Install Live Charts C # - WPF](https://www.youtube.com/watch?v=YlSl6myyeSs&list=PLqj54fKHGzJPLyW17twFDkLKoLTxHZFOO)
 */

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/*
    This class is used to define the properties of an ingredient.
    The properties include the name, quantity, unit, calorie value, and food group of the ingredient.
    The ingredient is used in the recipe class to define the ingredients of a recipe.
*/

namespace RecipeTrackerGUI.Classes
{
    // Ingredient class that defines the properties of an ingredient
    public class Ingredient
    {
        public string ingName { get; set; }
        public double ingQty { get; set; }
        public string ingUnit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        // Default constructor for the Ingredient class
        public Ingredient()
        { }

        // Parameterized constructor for the Ingredient class that initializes the properties of the ingredient
        public Ingredient(string name, double qty, string unit, int calories, string foodGroup)
        {
            ingName = name;
            ingQty = qty;
            ingUnit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        // ToString method that returns a string representation of the ingredient object (name, quantity, unit, calorie value, and food group)
        public override string ToString()
        {
            return $"{ingName}: {ingQty} {ingUnit} ({Calories} calories, {FoodGroup})";
        }
    }
}

// < -------------------------------------------END------------------------------------------- >