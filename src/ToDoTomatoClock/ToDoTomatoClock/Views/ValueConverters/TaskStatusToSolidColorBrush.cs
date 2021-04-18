using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace ToDoTomatoClock.Views.ValueConverters
{
    public class TaskStatusToSolidColorBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            switch (status)
            {
                case "Completed":
                    return new SolidColorBrush(ColorStrToColor("green"));
                default:
                    return new SolidColorBrush(ColorStrToColor("red"));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Undefined";
        }

        private System.Windows.Media.Color ColorStrToColor(string colorStr) =>
            (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorStr);
    }
}
