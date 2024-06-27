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

using System.Windows;
using System.Windows.Controls;
using RecipeTrackerGUI.Classes;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/* This class is responsible for displaying the window that allows the user to scale a recipe by a specified factor.
 * The user can select a scaling factor from a dropdown list and click the "Scale" button to scale the recipe.
 * The scaled recipe is then displayed in the RecipeWindow.
 * The user can also click the "Cancel" button to close the window without scaling the recipe.
 */

namespace RecipeTrackerGUI
{
    // ScaleRecipeWindow class that represents the window for scaling a recipe.
    public partial class ScaleRecipeWindow : Window
    {
        // Private field to store the recipe to be scaled.
        private Recipe recipe;

        // Constructor for the ScaleRecipeWindow class that takes a recipe as a parameter and initializes the window.
        public ScaleRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
        }

        // Event handler for the "Scale" button click event.
        private void Scale_Click(object sender, RoutedEventArgs e)
        {
            // Check if a scaling factor has been selected. If not, display a warning message and return.
            if (ScaleFactorComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a scaling factor.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Get the selected scaling factor from the dropdown list.
            double scaleFactor = double.Parse((ScaleFactorComboBox.SelectedItem as ComboBoxItem).Content.ToString());
            // Scale the recipe by the selected factor. If successful, display a success message and close the window.
            if (recipe.ScaleRecipe(scaleFactor))
            {
                MessageBox.Show("Recipe scaled successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            // If scaling the recipe fails, display an error message.
            else
            {
                MessageBox.Show("Failed to scale the recipe. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for the "Cancel" button click event.
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

// < -------------------------------------------END------------------------------------------- >