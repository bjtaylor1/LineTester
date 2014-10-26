using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LineTester
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            Resources.Add("strokeThicknessConverter", new StrokeThicknessConverter(this));
            InitializeComponent();
            lineSpace.SizeChanged += LineSpace_SizeChanged;

        }

        private MainPageViewModel viewModel;
        public MainPageViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
                viewModel.SetCanvas(lineSpace);
                DataContext = value;
            }
        }

        void LineSpace_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel.Width = e.NewSize.Width;
            ViewModel.Height = e.NewSize.Height;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems.Cast<PathViewModel>())
            {
                item.IsSelected = false;
            }
            foreach (var item in e.AddedItems.Cast<PathViewModel>())
            {
                item.IsSelected = true;
            }
        }
    }
}
