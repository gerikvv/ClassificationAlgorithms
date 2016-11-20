using System.Windows;
using Classification.Model;
using Classification.ViewModel;
using Syncfusion.Windows.Chart;
using Syncfusion.Windows.Controls.Grid;

namespace Classification.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChartViewModel();
        }
    }
}
