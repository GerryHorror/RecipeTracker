using System;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>
namespace RecipeTrackerGUI.Classes
{
    public static class RecipeOperations
    {
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

        public static (double, string) ConvertUnit(string unit, double qty, double scale)
        {
            string standardizedUnit = unit.Trim();

            if (aliases.ContainsKey(standardizedUnit))
            {
                standardizedUnit = aliases[standardizedUnit];
            }

            if (!conversionFactors.ContainsKey(standardizedUnit))
            {
                throw new ArgumentException("Unit not recognised or supported.");
            }

            double baseQty = qty * conversionFactors[standardizedUnit];
            double scaledBaseQty = baseQty * scale;

            string bestUnit = standardizedUnit;
            double bestQty = scaledBaseQty;

            foreach (var unitPair in conversionFactors)
            {
                double convertedQty = scaledBaseQty / unitPair.Value;
                if (convertedQty >= 1 && (convertedQty < bestQty || bestQty < 1))
                {
                    bestUnit = unitPair.Key;
                    bestQty = convertedQty;
                }
            }

            return (Math.Round(bestQty, 2), bestUnit);
        }

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