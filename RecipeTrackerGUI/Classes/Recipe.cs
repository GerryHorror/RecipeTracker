using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>
namespace RecipeTrackerGUI.Classes
{
    public class Recipe : INotifyPropertyChanged
    {
        public string recipeName { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<Step> steps { get; set; }
        public List<double> originalQty { get; set; }
        public List<string> originalUnits { get; set; }

        public Recipe()
        {
            recipeName = string.Empty;
            ingredients = new List<Ingredient>();
            steps = new List<Step>();
            originalQty = new List<double>();
            originalUnits = new List<string>();
        }

        public Recipe(string name, List<Ingredient> ing, List<string> stepDescriptions)
        {
            recipeName = name;
            ingredients = ing;
            steps = stepDescriptions.Select(desc => new Step { Description = desc, IsCompleted = false }).ToList();
            originalQty = ing.Select(ingredient => ingredient.ingQty).ToList();
            originalUnits = ing.Select(ingredient => ingredient.ingUnit).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddIngredient(string name, double qty, string unit, int calories, string foodGroup)
        {
            Ingredient newIngredient = new Ingredient(name, qty, unit, calories, foodGroup);
            ingredients.Add(newIngredient);
            originalQty.Add(qty);
            originalUnits.Add(unit);
        }

        public void AddStep(string stepDescription)
        {
            steps.Add(new Step { Description = stepDescription, IsCompleted = false });
            OnPropertyChanged(nameof(steps));
        }

        public bool ScaleRecipe(double factor)
        {
            if (factor != 0.5 && factor != 2 && factor != 3)
            {
                return false;
            }
            for (var i = 0; i < ingredients.Count; i++)
            {
                (double scaledQty, string newUnit) = RecipeOperations.ConvertUnit(ingredients[i].ingUnit, ingredients[i].ingQty, factor);
                ingredients[i].ingQty = scaledQty;
                ingredients[i].ingUnit = newUnit;
            }
            return true;
        }

        public void ResetQuantity()
        {
            for (var i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].ingQty = originalQty[i];
                ingredients[i].ingUnit = originalUnits[i];
            }
        }

        public void ClearRecipe()
        {
            ingredients.Clear();
            steps.Clear();
            originalQty.Clear();
            originalUnits.Clear();
        }

        public int CalculateTotalCalories()
        {
            return ingredients.Sum(i => i.Calories);
        }

        public string GetCalorieInfo()
        {
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