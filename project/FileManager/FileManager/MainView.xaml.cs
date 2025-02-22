using System;
using System.Windows;
using FileManager.ViewModels;

namespace FileManager.Views
{
    public partial class MainView : Window
    {
        private FileManagerViewModel viewModel;

        public MainView()
        {
            InitializeComponent();
            viewModel = new FileManagerViewModel();
            DataContext = viewModel;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
