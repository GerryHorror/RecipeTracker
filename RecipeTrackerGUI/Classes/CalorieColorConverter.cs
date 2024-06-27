/*
 References:
    -   [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
    -   [Food Groups](https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/)
    -   [Calorie Intake Chart](https://www.webmd.com/diet/calories-chart)
    -   [Definitive Guide to WPF Colors] (https://www.codeproject.com/Articles/5296124/Definitive-guide-to-WPF-colors-color-spaces-color)
    -   [Binding declarations overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-8.0)
    -   [Value Converters In WPF] (https://www.c-sharpcorner.com/article/value-converters-in-wpf/#:~:text=Value%20converters%20are%20used%20to%20display%20values%20in,and%20values%20that%20derive%20from%20multiple%20bound%20elements.)
    -   [Controls in WPF] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-6.0)
    -   [How to open a message box (WPF .NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-message-box?view=netdesktop-6.0)
    -   [How to create a template for a control (WPF.NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-6.0)
    -   [Data binding overview (WPF .NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0)
    -   [XAML overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-6.0)
    -   [Overview of WPF windows (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/?view=netdesktop-6.0)
    -   [Routed events overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-6.0)
    -   [Attached events overview (WPF .NET](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/attached-events-overview?view=netdesktop-6.0)
 */

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/*
    This class is used to convert the calorie value of a recipe to a color.
    The color is used to indicate the calorie content of the recipe.
    The color is determined by the following rules:
        - The default color is LightGray
        - If the calorie value is less than 200, the color is Green
        - If the calorie value is between 200 and 500, the color is Orange
        - If the calorie value is greater than 500, the color is Red
*/

namespace RecipeTrackerGUI.Classes
{
    // CalorieColorConverter class that implements the IValueConverter interface to convert the calorie value of a recipe to a color
    public class CalorieColorConverter : IValueConverter
    {
        // Convert method that converts the calorie value to a color based on the rules defined above
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If statement to determine the color based on the calorie value of the recipe and return the appropriate color
            if (int.TryParse(value?.ToString(), out int calories))
            {
                if (calories == 0)
                    return Colors.LightGray;
                else if (calories < 200)
                    return Colors.Green;
                else if (calories >= 200 && calories <= 500)
                    return Colors.Orange;
                else
                    return Colors.Red;
            }
            return Colors.LightGray;
        }

        // <-------------------------------------------------------------------------------------->

        // ConvertBack method that is not implemented and throws a NotImplementedException if called
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

// < -------------------------------------------END------------------------------------------- >