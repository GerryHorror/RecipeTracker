using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RecipeTrackerGUI.Classes
{
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as FrameworkElement;
            if (item == null) return string.Empty;

            var itemsControl = ItemsControl.ItemsControlFromItemContainer(item);
            if (itemsControl == null) return string.Empty;

            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(item);
            return (index + 1).ToString() + ".";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}