/*
References:
- [How do I parse a string with a decimal point to a double?](https://stackoverflow.com/questions/1354924/how-do-i-parse-a-string-with-a-decimal-point-to-a-double)
- [Is it possible to write to the console in color in .NET?](https://stackoverflow.com/questions/2743260/is-it-possible-to-write-to-the-console-in-colour-in-net)
- [C# Dictionary with Examples](https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/)
- [C# List](https://www.c-sharpcorner.com/article/c-sharp-list/)
- [C# Data Structure for Multiple Unit Conversions](https://stackoverflow.com/questions/495110/c-sharp-data-structure-for-multiple-unit-conversions)
- [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
*/

// Purpose: This file contains the Ingredient class which is used to store the name, quantity, and unit of an ingredient.
namespace RecipeTracker.Classes
{
    public class Ingredient
    {
        // Properties for the Ingredient class

        public string ingName { get; set; }
        public double ingQty { get; set; }
        public string ingUnit { get; set; }

        //Parameterless constructor for the Ingredient class (default constructor).
        //This is called when an object of the class is created without any parameters.
        public Ingredient()
        {
        }

        //Parameterised constructor for the Ingredient class (overloaded constructor) which means it can be called with different parameters.
        public Ingredient(string name, double qty, string unit)
        {
            ingName = name;
            ingQty = qty;
            ingUnit = unit;
        }
    }
}

// < -------------------------------------------END------------------------------------------- >