namespace RecipeTrackerGUI.Classes
{
    public class Ingredient
    {
        public string ingName { get; set; }
        public double ingQty { get; set; }
        public string ingUnit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient()
        { }

        public Ingredient(string name, double qty, string unit, int calories, string foodGroup)
        {
            ingName = name;
            ingQty = qty;
            ingUnit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}