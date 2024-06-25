using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RecipeTrackerGUI.Classes;

namespace RecipeTrackerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
            UpdateRecipeList();
            Recipe.CalorieNotification += NotifyHighCalories;
        }

        private void UpdateRecipeList()
        {
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes.Select(r => r.recipeName);
        }

        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                DisplayRecipe(selectedRecipe);
            }
        }

        private void DisplayRecipe(Recipe recipe)
        {
            RecipeNameTextBlock.Text = recipe.recipeName;

            IngredientsItemsControl.ItemsSource = recipe.ingredients.Select(i =>
                $"{i.ingName}: {i.ingQty} {i.ingUnit} ({i.Calories} calories, {i.FoodGroup})");

            StepsItemsControl.ItemsSource = recipe.steps.Select((s, index) => $"{index + 1}. {s}");

            int totalCalories = recipe.CalculateTotalCalories();
            CaloriesTextBlock.Text = $"Total Calories: {totalCalories}";
            CalorieInfoTextBlock.Text = recipe.GetCalorieInfo();
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
                    RecipeNameTextBlock.Text = "";
                    IngredientsItemsControl.ItemsSource = null;
                    StepsItemsControl.ItemsSource = null;
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
    }
}