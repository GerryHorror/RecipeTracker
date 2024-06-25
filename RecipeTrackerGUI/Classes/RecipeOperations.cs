using System;
using System.Collections.Generic;
using System.Globalization;

namespace RecipeTrackerGUI.Classes
{
    public static class RecipeOperations
    {
        private static readonly Dictionary<string, double> conversionFactors = new Dictionary<string, double>
        {
            ["tsp"] = 1,
            ["tbsp"] = 3,
            ["cup"] = 48,
            ["ml"] = 0.202884,
            ["l"] = 202.884,
            ["g"] = 1,
            ["kg"] = 1000,
        };

        private static readonly Dictionary<string, string> aliases = new Dictionary<string, string>
        {
            ["tablespoons"] = "tbsp",
            ["tablespoon"] = "tbsp",
            ["teaspoons"] = "tsp",
            ["teaspoon"] = "tsp",
            ["cups"] = "cup",
            ["grams"] = "g",
            ["gram"] = "g",
            ["kilograms"] = "kg",
            ["kilogram"] = "kg",
            ["milliliters"] = "ml",
            ["milliliter"] = "ml",
            ["liters"] = "l",
            ["liter"] = "l"
        };

        public static (double, string) ConvertUnit(string unit, double qty, double scale)
        {
            string standardizedUnit = unit.ToLower().Trim();

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