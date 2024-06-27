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

/*
 * This class is responsible for the Add Ingredient Window.
 * It allows the user to add a new ingredient.
 * The user must provide the name, quantity, unit, calories, and food group of the ingredient.
 * The user can then click the Add button to add the ingredient.
 * The window will validate the input and display an error message if any of the fields are missing or invalid.
 * The user can add multiple ingredients before closing the window.
 * The user can click the Cancel button to close the window without adding the ingredient.
 */

namespace RecipeTrackerGUI
{
    // Interaction logic for AddIngredientWindow.xaml (Add Ingredient Window)
    public partial class AddIngredientWindow : Window
    {
        // The new ingredient that the user wants to add to the recipe
        public Ingredient NewIngredient { get; private set; }

        // Constructor for the Add Ingredient Window (to initialize the window)
        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        // <-------------------------------------------------------------------------------------->

        // <-- Event Handlers -->

        // Event handler for when the user clicks the Add button to add the ingredient
        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Validation checks for the input fields
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(QuantityTextBox.Text) || string.IsNullOrWhiteSpace(UnitComboBox.Text) || string.IsNullOrWhiteSpace(CaloriesTextBox.Text) || FoodGroupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the quantity is a valid number
            if (!double.TryParse(QuantityTextBox.Text, out double quantity))
            {
                MessageBox.Show("Please enter a valid number for quantity.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the quantity is greater than 0
            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Check if the calories is a valid number
            if (!int.TryParse(CaloriesTextBox.Text, out int calories))
            {
                MessageBox.Show("Please enter a valid number for calories.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Get the selected food group from the combobox and create a new ingredient object
            string foodGroup = (FoodGroupComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            // Check if the food group is water and the calories are 0 or if the food group is not water and the calories are greater than 0
            if ((foodGroup == "Water" && calories == 0) || (foodGroup != "Water" && calories > 0))
            {
                NewIngredient = new Ingredient(
                    NameTextBox.Text,
                    quantity,
                    (UnitComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    calories,
                    foodGroup
                );
                // Set the dialog result to true and close the window
                DialogResult = true;
                Close();
            }
            // Display an error message if the input is invalid (calories must be 0 for water and greater than 0 for other ingredients)
            else
            {
                MessageBox.Show("Only water can have 0 calories. All other ingredients must have more than 0 calories.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for when the user clicks the Cancel button to close the window
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Set the dialog result to false and close the window
            DialogResult = false;
            Close();
        }
    }
}

// < -------------------------------------------END------------------------------------------- >