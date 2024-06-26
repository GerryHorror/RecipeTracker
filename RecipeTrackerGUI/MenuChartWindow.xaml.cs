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
    /// Interaction logic for MenuChartWindow.xaml
    /// </summary>
    public partial class MenuChartWindow : Window
    {
        public MenuChartWindow(List<Recipe> selectedRecipes)
        {
            InitializeComponent();
            UpdateChart(selectedRecipes);
        }

        private void UpdateChart(List<Recipe> recipes)
        {
            var foodGroups = new Dictionary<string, int>();

            foreach (var recipe in recipes)
            {
                foreach (var ingredient in recipe.ingredients)
                {
                    if (foodGroups.ContainsKey(ingredient.FoodGroup))
                        foodGroups[ingredient.FoodGroup] += ingredient.Calories;
                    else
                        foodGroups[ingredient.FoodGroup] = ingredient.Calories;
                }
            }

            var totalCalories = foodGroups.Values.Sum();

            var pieChart = (PieChart)FindName("Chart");
            if (pieChart != null)
            {
                foreach (var series in pieChart.Series.OfType<PieSeries>())
                {
                    if (foodGroups.ContainsKey(series.Title))
                    {
                        var percentage = (double)foodGroups[series.Title] / totalCalories * 100;
                        series.Values = new ChartValues<double> { Math.Round(percentage, 2) };
                    }
                    else
                    {
                        series.Values = new ChartValues<double> { 0 };
                    }
                }
            }

            DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; } = chartPoint => $"{chartPoint.Y:F2}%";

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var series = (PieSeries)chartpoint.SeriesView;
            MessageBox.Show($"{series.Title}: {chartpoint.Y:F2}%", "Food Group Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}