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