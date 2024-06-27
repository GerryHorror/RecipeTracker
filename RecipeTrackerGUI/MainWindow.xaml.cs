using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RecipeTrackerGUI.Classes;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>
namespace RecipeTrackerGUI
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;
        private bool calorieWarningShown = false;

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
            UpdateRecipeList();
        }

        private void UpdateRecipeList()
        {
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes.Select(r => r.recipeName);
        }

        private void DisplayRecipe(Recipe recipe)
        {
            RecipeNameTextBlock.Text = recipe.recipeName;

            IngredientsItemsControl.ItemsSource = recipe.ingredients.Select(i =>
                $"{i.ingName}: {i.ingQty} {i.ingUnit}{(i.ingQty != 1 ? "s" : "")} ({i.Calories} calories, {i.FoodGroup})");

            StepsItemsControl.ItemsSource = recipe.steps;

            int totalCalories = recipe.CalculateTotalCalories();
            CaloriesTextBlock.Text = $"Total Calories: {totalCalories}";
            CalorieInfoTextBlock.Text = recipe.GetCalorieInfo();

            if (totalCalories > 300 && !calorieWarningShown)
            {
                MessageBox.Show($"Warning: This recipe exceeds 300 calories with a total of {totalCalories} calories!",
                                "High Calorie Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                calorieWarningShown = true;
            }
        }

        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                calorieWarningShown = false;
                DisplayRecipe(selectedRecipe);
            }
        }

        private void NotifyHighCalories(int totalCalories)
        {
            MessageBox.Show($"Warning: This recipe exceeds 300 calories with a total of {totalCalories} calories!",
                            "High Calorie Warning",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            if (addRecipeWindow.ShowDialog() == true)
            {
                recipes.Add(addRecipeWindow.NewRecipe);
                UpdateRecipeList();
            }
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                ScaleRecipeWindow scaleRecipeWindow = new ScaleRecipeWindow(selectedRecipe);
                if (scaleRecipeWindow.ShowDialog() == true)
                {
                    calorieWarningShown = false;
                    DisplayRecipe(selectedRecipe);
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to scale.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                selectedRecipe.ResetQuantity();
                DisplayRecipe(selectedRecipe);
            }
            else
            {
                MessageBox.Show("Please select a recipe to reset quantities.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                if (MessageBox.Show($"Are you sure you want to delete the recipe '{selectedRecipe.recipeName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    recipes.Remove(selectedRecipe);
                    UpdateRecipeList();
                    ClearRecipeDisplay();
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to delete.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateMenu_Click(object sender, RoutedEventArgs e)
        {
            var selectRecipesWindow = new SelectRecipesWindow(recipes);
            if (selectRecipesWindow.ShowDialog() == true)
            {
                var selectedRecipes = selectRecipesWindow.SelectedRecipes;
                var menuChartWindow = new MenuChartWindow(selectedRecipes);
                menuChartWindow.Show();
            }
        }

        private void ClearRecipeDisplay()
        {
            RecipeNameTextBlock.Text = "";
            IngredientsItemsControl.ItemsSource = null;
            StepsItemsControl.ItemsSource = null;
            CaloriesTextBlock.Text = "";
            CalorieInfoTextBlock.Text = "";
            calorieWarningShown = false;
        }
    }
}