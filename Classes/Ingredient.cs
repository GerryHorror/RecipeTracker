namespace RecipeTracker.Classes
{
    public class Ingredient
    {
        public string ingName { get; set; }
        public double ingQty { get; set; }
        public string ingUnit { get; set; }

        public Ingredient()
        {
        }

        public Ingredient(string name, double qty, string unit)
        {
            ingName = name;
            ingQty = qty;
            ingUnit = unit;
        }
    }
}