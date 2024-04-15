using System.Collections.Generic;

namespace RecipeTracker.Classes
{
    public class Recipe
    {
        public string recipeName { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<string> steps { get; set; }
        public Dictionary<Ingredient, double> originalQty { get; set; }

        public Recipe()
        {
        }

        public Recipe(string name, List<Ingredient> ing, List<string> steps, Dictionary<Ingredient, double> qty)
        {
            recipeName = name;
            ingredients = ing;
            this.steps = steps;
            originalQty = qty;
        }
    }
}