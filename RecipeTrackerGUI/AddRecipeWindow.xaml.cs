using System.Collections.Generic;
using System;
using System.Windows;
using RecipeTrackerGUI.Classes;

namespace RecipeTrackerGUI
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public Recipe NewRecipe { get; private set; }
        private List<Ingredient> ingredients;

        public AddRecipeWindow()
        {
            InitializeComponent();
            ingredients = new List<Ingredient>();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientWindow addIngredientWindow = new AddIngredientWindow();
            if (addIngredientWindow.ShowDialog() == true)
            {
                ingredients.Add(addIngredientWindow.NewIngredient);
                UpdateIngredientsList();
            }
        }

        private void UpdateIngredientsList()
        {
            IngredientsListBox.ItemsSource = null;
            IngredientsListBox.ItemsSource = ingredients;
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RecipeNameTextBox.Text))
            {
                MessageBox.Show("Please enter a recipe name.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ingredients.Count == 0)
            {
                MessageBox.Show("Please add at least one ingredient.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(StepsTextBox.Text))
            {
                MessageBox.Show("Please enter the recipe steps.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<string> stepDescriptions = new List<string>(StepsTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));

            NewRecipe = new Recipe(RecipeNameTextBox.Text, ingredients, stepDescriptions);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}