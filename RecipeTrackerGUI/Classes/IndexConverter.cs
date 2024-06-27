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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/*
    This class is used to convert the index of an item in a list to a string.
    The string is used to display the index of the item in the list.
    The index is determined by the position of the item in the list.
    The index is displayed as a number followed by a period.
*/

namespace RecipeTrackerGUI.Classes
{
    // IndexConverter class that implements the IValueConverter interface to convert the index of an item in a list to a string
    public class IndexConverter : IValueConverter
    {
        // Convert method that converts the index of the item in the list to a string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If statement to determine the index of the item in the list and return the index as a string
            var item = value as FrameworkElement;
            if (item == null) return string.Empty;
            // Get the ItemsControl that contains the item and the index of the item in the list
            var itemsControl = ItemsControl.ItemsControlFromItemContainer(item);
            // If statement to check if the ItemsControl is null and return an empty string if it is
            if (itemsControl == null) return string.Empty;
            // Get the index of the item in the list and return the index as a string
            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(item);
            return (index + 1).ToString() + ".";
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