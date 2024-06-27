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

/* This class is responsible for the Main Window.
 * It is the main window of the application and displays the list of recipes.
 * The user can select a recipe from the list to view the details of the recipe.
 * The user can add a new recipe, scale a recipe, reset the quantities of a recipe, or delete a recipe.
 * The user can also create a menu by selecting multiple recipes from the list.
 */

namespace RecipeTrackerGUI
{
    // Interaction logic for MainWindow.xaml (Main Window)
    public partial class MainWindow : Window
    {
        // List of recipes that the user has added to the recipe tracker
        private List<Recipe> recipes;

        // Boolean to track if the high calorie warning has been shown
        private bool calorieWarningShown = false;

        // Constructor for the Main Window (to initialize the window)
        public MainWindow()
        {
            InitializeComponent();
            // Initialize the list of recipes
            recipes = new List<Recipe>();
            // Update the recipe list
            UpdateRecipeList();
        }

        // <-------------------------------------------------------------------------------------->

        // <-- Event Handlers -->

        // Method to update the recipe list
        private void UpdateRecipeList()
        {
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes.Select(r => r.recipeName);
        }

        // <-------------------------------------------------------------------------------------->

        // This method displays the details of the selected recipe (name, ingredients, steps, calories, and calorie info)
        private void DisplayRecipe(Recipe recipe)
        {
            // Display the recipe name in the RecipeNameTextBlock
            RecipeNameTextBlock.Text = recipe.recipeName;

            // Display the ingredients in the IngredientsItemsControl (with quantity, unit, calories, and food group)
            IngredientsItemsControl.ItemsSource = recipe.ingredients.Select(i =>
                $"{i.ingName}: {i.ingQty} {i.ingUnit}{(i.ingQty != 1 ? "s" : "")} ({i.Calories} calories, {i.FoodGroup})");

            // Display the steps in the StepsItemsControl (with step number and description)
            StepsItemsControl.ItemsSource = recipe.steps;
            // Calculate the total calories of the recipe
            int totalCalories = recipe.CalculateTotalCalories();
            // Display the total calories in the CaloriesTextBlock
            CaloriesTextBlock.Text = totalCalories.ToString();
            // Display the calorie info in the CalorieInfoTextBlock
            CalorieInfoTextBlock.Text = $"Total Calories: {totalCalories}\n{recipe.GetCalorieInfo()}";

            // If the total calories exceed 300 and the high calorie warning has not been shown, show the warning message
            if (totalCalories > 300 && !calorieWarningShown)
            {
                MessageBox.Show($"Warning: This recipe exceeds 300 calories with a total of {totalCalories} calories!",
                                "High Calorie Warning",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                calorieWarningShown = true;
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user selects a recipe from the list
        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If a recipe is selected, display the recipe details
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                // Reset the high calorie warning flag
                calorieWarningShown = false;
                DisplayRecipe(selectedRecipe);
            }
            // Else, clear the recipe display
            else
            {
                ClearRecipeDisplay();
            }
        }

        // <-------------------------------------------------------------------------------------->

        // This method displays a warning message if the total calories of the recipe exceed 300
        private void NotifyHighCalories(int totalCalories)
        {
            MessageBox.Show($"Warning: This recipe exceeds 300 calories with a total of {totalCalories} calories!",
                            "High Calorie Warning",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Add Recipe button to add a new recipe
        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Create a new AddRecipeWindow and display it as a dialog
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            // If the user clicks the Save button to save the recipe, add the new recipe to the list of recipes and update the recipe list
            if (addRecipeWindow.ShowDialog() == true)
            {
                recipes.Add(addRecipeWindow.NewRecipe);
                UpdateRecipeList();
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Scale Recipe button to scale a recipe
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            // If a recipe is selected, open the ScaleRecipeWindow to scale the recipe
            if (RecipeListBox.SelectedIndex != -1)
            {
                // Get the selected recipe from the list of recipes and open the ScaleRecipeWindow
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                ScaleRecipeWindow scaleRecipeWindow = new ScaleRecipeWindow(selectedRecipe);
                // If the user clicks the Scale button to scale the recipe, display the scaled recipe
                if (scaleRecipeWindow.ShowDialog() == true)
                {
                    // Reset the high calorie warning flag
                    calorieWarningShown = false;
                    DisplayRecipe(selectedRecipe);
                   
                }
            }
            // Else, display a warning message to select a recipe if no recipe is selected
            else
            {
                MessageBox.Show("Please select a recipe to scale.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Reset Quantities button to reset the quantities of a recipe
        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            // If a recipe is selected, reset the quantities of the recipe and display the recipe
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                selectedRecipe.ResetQuantity();
                DisplayRecipe(selectedRecipe);
                // Display a success message to inform the user that the recipe has been reset
                MessageBox.Show($"Quantities for '{selectedRecipe.recipeName}' have been reset to their original values.", "Quantities Reset", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // Else, display a warning message to select a recipe if no recipe is selected
            else
            {
                MessageBox.Show("Please select a recipe to reset quantities.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Delete Recipe button to delete a recipe
        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // If a recipe is selected, delete the recipe from the list of recipes and update the recipe list
            if (RecipeListBox.SelectedIndex != -1)
            {
                Recipe selectedRecipe = recipes[RecipeListBox.SelectedIndex];
                // This a safety check to confirm if the user wants to delete the recipe before deleting it
                if (MessageBox.Show($"Are you sure you want to delete the recipe '{selectedRecipe.recipeName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    recipes.Remove(selectedRecipe);
                    UpdateRecipeList();
                    ClearRecipeDisplay();
                }
            }
            // Else, display a warning message to select a recipe if no recipe is selected
            else
            {
                MessageBox.Show("Please select a recipe to delete.", "No Recipe Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Exit menu item to close the application
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Close();
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Create Menu button to create a menu from selected recipes (pie chart)
        private void CreateMenu_Click(object sender, RoutedEventArgs e)
        {
            // Create a new SelectRecipesWindow and display it as a dialog
            var selectRecipesWindow = new SelectRecipesWindow(recipes);
            // If the user clicks the Create Menu button to create a menu, open the MenuChartWindow to display the menu chart
            if (selectRecipesWindow.ShowDialog() == true)
            {
                // Get the selected recipes from the SelectRecipesWindow and open the MenuChartWindow
                var selectedRecipes = selectRecipesWindow.SelectedRecipes;
                var menuChartWindow = new MenuChartWindow(selectedRecipes);
                // Display the menu chart window
                menuChartWindow.Show();
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Method to clear the recipe display (name, ingredients, steps, calories, and calorie info)
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

// < -------------------------------------------END------------------------------------------- >