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

using System.Collections.Generic;
using System;
using System.Windows;
using RecipeTrackerGUI.Classes;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/* This class is responsible for the Add Recipe Window.
 * It allows the user to add a new recipe.
 * The user must provide the recipe name, ingredients, and steps.
 * The user can click the Add Ingredient button to add an ingredient to the recipe.
 * The user can click the Save button to save the recipe.
 * The window will validate the input and display an error message if any of the fields are missing or invalid.
 * The user can add multiple ingredients before saving the recipe.
 * The user can click the Cancel button to close the window without saving the recipe.
 */

namespace RecipeTrackerGUI
{
    // Interaction logic for AddRecipeWindow.xaml (Add Recipe Window)
    public partial class AddRecipeWindow : Window
    {
        // The new recipe that the user wants to add to the recipe tracker
        public Recipe NewRecipe { get; private set; }

        // List of ingredients that the user has added to the recipe
        private List<Ingredient> ingredients;

        // Constructor for the Add Recipe Window (to initialize the window)
        public AddRecipeWindow()
        {
            InitializeComponent();
            // Initialize the list of ingredients
            ingredients = new List<Ingredient>();
        }

        // <-------------------------------------------------------------------------------------->

        // <-- Event Handlers -->

        // Event handler for when the user clicks the Add Ingredient button to add an ingredient to the recipe
        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Open the Add Ingredient Window
            AddIngredientWindow addIngredientWindow = new AddIngredientWindow();
            // Show the Add Ingredient Window as a dialog
            if (addIngredientWindow.ShowDialog() == true)
            {
                // Add the new ingredient to the list of ingredients
                ingredients.Add(addIngredientWindow.NewIngredient);
                // Update the ingredients list
                UpdateIngredientsList();
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to update the list of ingredients in the IngredientsListBox
        private void UpdateIngredientsList()
        {
            // Set the ItemsSource of the IngredientsListBox to the list of ingredients
            IngredientsListBox.ItemsSource = null;
            IngredientsListBox.ItemsSource = ingredients;
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Save button to save the recipe
        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Validation checks for the input fields. It returns an error message if any of the fields are missing or invalid.
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
            // Split the steps by new line and add them to a list of step descriptions (removing any empty entries)
            List<string> stepDescriptions = new List<string>(StepsTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            // Create a new recipe object with the recipe name, ingredients, and step descriptions
            NewRecipe = new Recipe(RecipeNameTextBox.Text, ingredients, stepDescriptions);
            DialogResult = true;
            Close();
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Cancel button to close the window without saving the recipe
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

// < -------------------------------------------END------------------------------------------- >