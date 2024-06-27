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
    public partial class AddIngredientWindow : Window
    {
        public Ingredient NewIngredient { get; private set; }

        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(QuantityTextBox.Text) || string.IsNullOrWhiteSpace(UnitComboBox.Text) || string.IsNullOrWhiteSpace(CaloriesTextBox.Text) || FoodGroupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(QuantityTextBox.Text, out double quantity))
            {
                MessageBox.Show("Please enter a valid number for quantity.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(CaloriesTextBox.Text, out int calories))
            {
                MessageBox.Show("Please enter a valid number for calories.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string foodGroup = (FoodGroupComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if ((foodGroup == "Water" && calories == 0) || (foodGroup != "Water" && calories > 0))
            {
                NewIngredient = new Ingredient(
                    NameTextBox.Text,
                    quantity,
                    (UnitComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    calories,
                    foodGroup
                );

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Only water can have 0 calories. All other ingredients must have more than 0 calories.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}