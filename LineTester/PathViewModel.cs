using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.ComponentModel;

namespace LineTester
{

    public class PathViewModel : INotifyPropertyChanged
    {
        private string data;
        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                var values = Regex.Matches(value, @"[A-Za-z] \s+ ([\d\.]+) \s+ ([\d\.]+)", RegexOptions.IgnorePatternWhitespace).Cast<Match>().ToArray();
                var xvalues = values.Select(m => double.Parse(m.Groups[1].Value));
                var yvalues = values.Select(m => double.Parse(m.Groups[2].Value));
                MaxX = xvalues.Max();
                MinX = xvalues.Min();
                MaxY = yvalues.Max();
                MinY = yvalues.Min();
            }
        }
        public double MaxX { get; private set; }
        public double MinX { get; private set; }
        public double MinY { get; private set; }
        public double MaxY { get; private set; }

        public Brush Color { get; set; }

        public string Name { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaiseNotifyPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseNotifyPropertyChanged(string propertyName)
        {
            var e = PropertyChanged;
            if (e != null)
            {
                e(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
