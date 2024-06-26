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
                pieChart.Series.Clear();
                foreach (var foodGroup in foodGroups)
                {
                    var percentage = (double)foodGroup.Value / totalCalories * 100;
                    pieChart.Series.Add(new PieSeries
                    {
                        Title = foodGroup.Key,
                        Values = new ChartValues<double> { Math.Round(percentage, 2) },
                        DataLabels = true,
                        LabelPoint = PointLabel
                    });
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