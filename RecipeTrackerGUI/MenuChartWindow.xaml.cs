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
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using RecipeTrackerGUI.Classes;
using System;

namespace RecipeTrackerGUI
{
    /// <summary>
    ///   Gérard Blankenberg
    ///   ST10046280
    ///   Module: PROG6221
    ///   POE Final Submission
    /// </summary>

    /*
        This class is responsible for displaying the pie chart that shows the distribution of calories in the selected recipes.
        The chart is displayed in a new window when the user clicks on the "Create Chart" button in the MenuWindow.
        The chart is created using the LiveCharts library, which is a modern charting library for interactive data visualization.
        The chart displays the distribution of calories in the selected recipes by food group.
        Each food group is represented by a slice of the pie chart, and the size of the slice corresponds to the percentage of calories contributed by that food group.
        The chart also displays the percentage of calories contributed by each food group when the user hovers over a slice.
        The chart is updated whenever the user selects or deselects recipes in the MenuWindow.
    */

    public partial class MenuChartWindow : Window
    {
        // Constructor for the MenuChartWindow class that takes a list of selected recipes as a parameter and initializes the window.
        public MenuChartWindow(List<Recipe> selectedRecipes)
        {
            InitializeComponent();
            UpdateChart(selectedRecipes);
        }

        // Method that updates the pie chart with the distribution of calories in the selected recipes.
        private void UpdateChart(List<Recipe> recipes)
        {
            // Create a dictionary to store the total calories for each food group.
            var foodGroups = new Dictionary<string, int>();
            // Iterate over each recipe in the selected recipes.
            foreach (var recipe in recipes)
            {
                // Iterate over each ingredient in the recipe.
                foreach (var ingredient in recipe.ingredients)
                {
                    // Check if the food group of the ingredient is already in the dictionary.
                    if (foodGroups.ContainsKey(ingredient.FoodGroup))
                        // If the food group is already in the dictionary, add the calories of the ingredient to the total calories for that food group.
                        foodGroups[ingredient.FoodGroup] += ingredient.Calories;
                    // If the food group is not in the dictionary, add the food group to the dictionary with the calories of the ingredient as the value.
                    else
                        foodGroups[ingredient.FoodGroup] = ingredient.Calories;
                }
            }
            // Calculate the total calories in the selected recipes.
            var totalCalories = foodGroups.Values.Sum();
            // Find the pie chart control in the window.
            var pieChart = (PieChart)FindName("Chart");
            // Check if the pie chart control was found.
            if (pieChart != null)
            {
                // Clear the series in the pie chart.
                pieChart.Series.Clear();
                // Iterate over each food group in the dictionary.
                foreach (var foodGroup in foodGroups)
                {
                    // Calculate the percentage of calories contributed by the food group.
                    var percentage = (double)foodGroup.Value / totalCalories * 100;
                    // Add a new pie series to the pie chart for the food group with the percentage of calories as the value.
                    pieChart.Series.Add(new PieSeries
                    {
                        // Set the title of the series to the food group.
                        Title = foodGroup.Key,
                        // Set the values of the series to the percentage of calories contributed by the food group.
                        Values = new ChartValues<double> { Math.Round(percentage, 2) },
                        // Enable data labels to display the percentage of calories contributed by the food group.
                        DataLabels = true,
                        // Set the point label to display the percentage of calories contributed by the food group.
                        LabelPoint = PointLabel
                    });
                }
            }
            // Set the data context of the window to itself.
            DataContext = this;
        }

        // <-------------------------------------------------------------------------------------------------------------------->

        // Property that defines the point label for the pie chart. The point label displays the percentage of calories contributed by each food group.
        public Func<ChartPoint, string> PointLabel { get; set; } = chartPoint => $"{chartPoint.Y:F2}%";

        // Event handler for the DataClick event of the pie chart. The event is raised when the user clicks on a slice of the pie chart.
        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            // Get the series of the chart point.
            var series = (PieSeries)chartpoint.SeriesView;
            // Display a message box with the title of the series and the percentage of calories contributed by the food group.
            MessageBox.Show($"{series.Title}: {chartpoint.Y:F2}%", "Food Group Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

// < -------------------------------------------END------------------------------------------- >