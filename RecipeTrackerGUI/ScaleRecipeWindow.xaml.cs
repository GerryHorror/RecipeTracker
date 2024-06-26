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
    public partial class ScaleRecipeWindow : Window
    {
        private Recipe recipe;

        public ScaleRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
        }

        private void Scale_Click(object sender, RoutedEventArgs e)
        {
            if (ScaleFactorComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a scaling factor.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double scaleFactor = double.Parse((ScaleFactorComboBox.SelectedItem as ComboBoxItem).Content.ToString());

            if (recipe.ScaleRecipe(scaleFactor))
            {
                MessageBox.Show("Recipe scaled successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Failed to scale the recipe. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}