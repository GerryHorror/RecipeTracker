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
using System.Collections.Generic;
using System.Globalization;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/*
    This class is used to define the operations that can be performed on a recipe.
    The operations include converting the unit of an ingredient, and parsing a string to a double.
    The RecipeOperations class is used to perform operations on recipes in the Recipe Tracker application.
*/

namespace RecipeTrackerGUI.Classes
{
    public static class RecipeOperations
    {
        // Dictionary that contains the conversion factors for different units of measurement
        private static readonly Dictionary<string, double> conversionFactors = new Dictionary<string, double>
        {
            ["Teaspoon"] = 1,
            ["Tablespoon"] = 3,
            ["Cup"] = 48,
            ["Milliliter"] = 0.202884,
            ["Liter"] = 202.884,
            ["Gram"] = 1,
            ["Kilogram"] = 1000,
        };

        // <-------------------------------------------------------------------------------------->

        // Dictionary that contains the aliases for different units of measurement
        private static readonly Dictionary<string, string> aliases = new Dictionary<string, string>
        {
            ["Teaspoons"] = "Teaspoon",
            ["Tablespoons"] = "Tablespoon",
            ["Cups"] = "Cup",
            ["Milliliters"] = "Milliliter",
            ["Liters"] = "Liter",
            ["Grams"] = "Gram",
            ["Kilograms"] = "Kilogram",
        };

        // <-------------------------------------------------------------------------------------->

        // ConvertUnit method that converts the unit of an ingredient to a different unit of measurement
        public static (double, string) ConvertUnit(string unit, double qty, double scale)
        {
            string standardizedUnit = unit.Trim();
            // If statement to check if the unit is an alias and convert it to the standard unit
            if (aliases.ContainsKey(standardizedUnit))
            {
                standardizedUnit = aliases[standardizedUnit];
            }
            // If statement to check if the unit is not recognized and throw an exception
            if (!conversionFactors.ContainsKey(standardizedUnit))
            {
                throw new ArgumentException("Unit not recognised or supported.");
            }

            // Calculate the quantity in the base unit and scale it
            double baseQty = qty * conversionFactors[standardizedUnit];
            double scaledBaseQty = baseQty * scale;

            string bestUnit = standardizedUnit;
            double bestQty = scaledBaseQty;

            // Find the best unit to convert the quantity to
            foreach (var unitPair in conversionFactors)
            {
                double convertedQty = scaledBaseQty / unitPair.Value;
                if (convertedQty >= 1 && (convertedQty < bestQty || bestQty < 1))
                {
                    bestUnit = unitPair.Key;
                    bestQty = convertedQty;
                }
            }
            // Return the scaled quantity and the best unit to convert it to
            return (Math.Round(bestQty, 2), bestUnit);
        }

        // <-------------------------------------------------------------------------------------->

        // ParseDoubleInvariant method that parses a string to a double using the invariant culture and the current culture
        public static double ParseDoubleInvariant(string input)
        {
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                return number;
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out number))
                return number;
            throw new FormatException("Input is not a valid number.");
        }
    }
}

// < -------------------------------------------END------------------------------------------- >