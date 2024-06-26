/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>
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

        public override string ToString()
        {
            return $"{ingName}: {ingQty} {ingUnit} ({Calories} calories, {FoodGroup})";
        }
    }
}