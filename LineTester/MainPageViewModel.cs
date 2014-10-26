using System;
using System.Collections.ObjectModel;
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
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Data;

namespace LineTester
{
    public class MainPageViewModel : INotifyPropertyChanged, IAddPath
    {
        public MainPageViewModel()
        {
            Paths = new ObservableCollection<PathViewModel>();
            Paths.CollectionChanged += Paths_CollectionChanged;
            AddPathFromFileCommand = new AddPathFromFileCommand(this);
            SaveCommand = new SaveCommand();
            SaveCommand.SetCanExecute(Paths.Any());
        }

        public AddPathFromFileCommand AddPathFromFileCommand { get; private set; }
        public SaveCommand SaveCommand { get; set; }

        void Paths_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ContentLeft = Paths.Min(p => p.MinX);
            ContentTop = Paths.Min(p => p.MinY);
            ContentWidth = Paths.Max(p => p.MaxX) - ContentLeft;
            ContentHeight = Paths.Max(p => p.MaxY) - ContentTop;
            SaveCommand.SetCanExecute(Paths.Any());
        }

        public void AddPath(PathViewModel path)
        {
            Paths.Add(path);
        }

        public ObservableCollection<PathViewModel> Paths { get; set; }

        private double contentLeft, contentWidth, contentTop, contentHeight, width, height;
        public double ContentLeft
        {
            get{return contentLeft;}
            set
            {
                contentLeft = value;
                RaiseNotifyPropertyChanged("ContentLeft");
                RaiseNotifyPropertyChanged("MinusContentLeft");
            }
        }

        public double ContentTop
        {
            get { return contentTop; }
            set
            {
                contentTop = value;
                RaiseNotifyPropertyChanged("ContentTop");
                RaiseNotifyPropertyChanged("MinusContentTop");
            }
        }

        public double ContentHeight
        {
            get { return contentHeight; }
            set
            {
                contentHeight = value;
                RaiseSizeBasedPropertyChanges();
            }
        }

        private void RaiseSizeBasedPropertyChanges()
        {
            RaiseNotifyPropertyChanged("ContentHeight");
            RaiseNotifyPropertyChanged("ScaleX");
            RaiseNotifyPropertyChanged("ScaleY");
            RaiseNotifyPropertyChanged("StrokeWidth");
        }

        public double StrokeWidth
        {
            get
            {
                if (ScaleUp == 0) return 1;
                else return 1 / ScaleUp;
            }
        }

        public double MinusContentLeft { get { return -ContentLeft; } }
        public double MinusContentTop { get { return -ContentTop; } }

        private double ScaleUp
        {
            get
            {
                if (ContentWidth == 0 || ContentHeight == 0) return 1;

                var desiredScaleUpX = Width / ContentWidth;
                var desiredScaleUpY = Height / ContentHeight;
                var scaleUp = Math.Min(desiredScaleUpX, desiredScaleUpY);
                return scaleUp;
            }
        }

        public double ScaleX { get { return ScaleUp; } }
        public double ScaleY { get { return ScaleUp; } }
        public double HalfHeight { get { return Height / 2; } }

        public double ContentWidth
        {
            get { return contentWidth; }
            set
            {
                contentWidth = value;
                RaiseSizeBasedPropertyChanges();

            }
        }
        

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                RaiseSizeBasedPropertyChanges();

            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                RaiseSizeBasedPropertyChanges();

            }
        }

        protected void RaiseNotifyPropertyChanged(string propertyName)
        {
            var e = PropertyChanged;
            if (e != null)
            {
                e(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        internal void SetCanvas(Canvas lineSpace)
        {
            SaveCommand.SetCanvas(lineSpace);
        }
    }

}
