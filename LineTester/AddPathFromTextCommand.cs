using System;
using System.Collections.Generic;
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
using System.Reflection;
namespace LineTester
{
    public class AddPathFromFileCommand : ICommand
    {
        private readonly Dictionary<string, PropertyInfo> colors;
        private readonly IAddPath addPath;
        public AddPathFromFileCommand(IAddPath addPath)
        {

            colors = typeof(Colors).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToDictionary(p => p.Name.ToLower());
            this.addPath = addPath;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if(true.Equals(ofd.ShowDialog()))
            {
                foreach (var file in ofd.Files)
                {
                    var name = Regex.Match(file.Name, @"[^\.]+").Value;
                    Color color = Colors.Black;
                    var colorMatch = Regex.Match(name, @"_([A-Za-z]+)$");
                    PropertyInfo colorProperty;
                    if (colorMatch.Success && colors.TryGetValue(colorMatch.Groups[1].Value.Trim().ToLower(), out colorProperty))
                    {
                        color = (Color)colorProperty.GetValue(null, new object[] { });
                    }

                    using (var sr = file.OpenText())
                    {
                        addPath.AddPath(new PathViewModel { Data = sr.ReadToEnd(), Color = new SolidColorBrush(color), Name = name });
                    }
                }
            }
        }
    }
}
