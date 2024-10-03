using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop_Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new TextBlock
            {
                Text = "Welcome to the Dashboard",
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new CategoryControl(UpdateCategoryList);
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ProductControl();
        }

        private void UpdateCategoryList()
        {
            if (contentControl.Content is ProductControl productControl)
            {
                productControl.LoadCategories();
            }
        }
    }
}