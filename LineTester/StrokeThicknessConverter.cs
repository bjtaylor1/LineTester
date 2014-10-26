using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LineTester
{
    public class StrokeThicknessConverter : IValueConverter
    {
        private MainPage mainPage;

        public StrokeThicknessConverter(MainPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isSelected = (bool)value;
            var strokeThickness = (isSelected ? 3 : 1) * mainPage.ViewModel.StrokeWidth;
            Debug.WriteLine("StrokeThickness is " + strokeThickness);
            return strokeThickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
