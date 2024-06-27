/*
 References:
    - [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
    - [Food Groups](https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/)
    - [Calorie Intake Chart](https://www.webmd.com/diet/calories-chart)
    - [Definitive Guide to WPF Colors](https://www.codeproject.com/Articles/5296124/Definitive-guide-to-WPF-colors-color-spaces-color)
    - [Binding declarations overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-8.0)
    - [Value Converters In WPF](https://www.c-sharpcorner.com/article/value-converters-in-wpf/#:~:text=Value%20converters%20are%20used%20to%20display%20values%20in,and%20values%20that%20derive%20from%20multiple%20bound%20elements.)
    - [Controls in WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-6.0)
    - [How to open a message box(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-message-box?view=netdesktop-6.0)
    - [How to create a template for a control (WPF.NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-6.0)
    - [Data binding overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0)
    - [XAML overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-6.0)
    - [Overview of WPF windows(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/?view=netdesktop-6.0)
    - [Routed events overview(WPF.NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-6.0)
    - [Attached events overview (WPF .NET](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/attached-events-overview?view=netdesktop-6.0)
    - [Tutorial on How to Install Live Charts C # - WPF](https://www.youtube.com/watch?v=YlSl6myyeSs&list=PLqj54fKHGzJPLyW17twFDkLKoLTxHZFOO)
 */

using RecipeTrackerGUI.Classes;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace RecipeTrackerGUI
{
    /// <summary>
    ///   Gérard Blankenberg
    ///   ST10046280
    ///   Module: PROG6221
    ///   POE Final Submission
    /// </summary>

    /*
        This class is responsible for displaying the window that allows the user to select recipes for the pie chart.
        The user can select one or more recipes from a list of all recipes and click the "Create Menu" button to create a pie chart with the selected recipes.
        The selected recipes are then returned to the MenuWindow, where they are displayed in the pie chart.
        The user can also click the "Cancel" button to close the window without creating a pie chart.
    */

    public partial class SelectRecipesWindow : Window
    {
        // Property to store the list of selected recipes.
        public List<Recipe> SelectedRecipes { get; private set; }

        // Constructor for the SelectRecipesWindow class that takes a list of all recipes as a parameter and initializes the window.
        public SelectRecipesWindow(List<Recipe> allRecipes)
        {
            InitializeComponent();
            RecipesListBox.ItemsSource = allRecipes.Select(r => new RecipeSelection { Recipe = r, IsSelected = false }).ToList();
        }

        // Event handler for the "Create Chart" button click event.
        private void CreateMenu_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected recipes from the list box.
            SelectedRecipes = RecipesListBox.Items.Cast<RecipeSelection>()
                                            .Where(rs => rs.IsSelected)
                                            .Select(rs => rs.Recipe)
                                            .ToList();

            // Check if at least one recipe has been selected. If not, display a warning message and return.
            if (SelectedRecipes.Count == 0)
            {
                MessageBox.Show("Please select at least one recipe for the pie chart.", "No Recipes Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        // <-------------------------------------------------------------------------------------->

        // Method that handles the selection of a recipe in the list box.
        public class RecipeSelection
        {
            // Property to store the recipe.
            public Recipe Recipe { get; set; }

            // Boolean property to store whether the recipe is selected.
            public bool IsSelected { get; set; }
        }
    }
}

// < -------------------------------------------END------------------------------------------- >