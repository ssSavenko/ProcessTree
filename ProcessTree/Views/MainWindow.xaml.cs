using System.Windows;
using ProcessTree.ViewModels;

namespace ProcessTree.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void TreeView_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
