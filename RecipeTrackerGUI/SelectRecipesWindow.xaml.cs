using RecipeTrackerGUI.Classes;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace RecipeTrackerGUI
{
    /// <summary>
    /// Interaction logic for SelectRecipesWindow.xaml
    /// </summary>
    public partial class SelectRecipesWindow : Window
    {
        public List<Recipe> SelectedRecipes { get; private set; }

        public SelectRecipesWindow(List<Recipe> allRecipes)
        {
            InitializeComponent();
            RecipesListBox.ItemsSource = allRecipes.Select(r => new RecipeSelection { Recipe = r, IsSelected = false }).ToList();
        }

        private void CreateMenu_Click(object sender, RoutedEventArgs e)
        {
            SelectedRecipes = RecipesListBox.Items.Cast<RecipeSelection>()
                                            .Where(rs => rs.IsSelected)
                                            .Select(rs => rs.Recipe)
                                            .ToList();

            if (SelectedRecipes.Count == 0)
            {
                MessageBox.Show("Please select at least one recipe for the menu.", "No Recipes Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        public class RecipeSelection
        {
            public Recipe Recipe { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}