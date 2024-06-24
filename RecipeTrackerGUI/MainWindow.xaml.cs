using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                MessageBox.Show("Please select a recipe to scale.", "No recipe selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                selectedRecipe.ResetQuantites();
                DisplayRecipe(selectedRecipe);
            }
            else
            {
                MessageBox.Show("Please select a recipe to reset quantities.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}