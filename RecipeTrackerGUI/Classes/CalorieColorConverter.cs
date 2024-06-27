using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RecipeTrackerGUI.Classes
{
    public class CalorieColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}