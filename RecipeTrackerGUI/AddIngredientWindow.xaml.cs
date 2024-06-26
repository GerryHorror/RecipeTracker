using System.Windows;
using System.Windows.Controls;
using RecipeTrackerGUI.Classes;

namespace RecipeTrackerGUI
{
    /// <summary>
    /// Interaction logic for AddIngredientWindow.xaml
    /// </summary>
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

            NewIngredient = new Ingredient(
                NameTextBox.Text,
                quantity,
                UnitComboBox.Text,
                calories,
                (FoodGroupComboBox.SelectedItem as ComboBoxItem).Content.ToString()
                );

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