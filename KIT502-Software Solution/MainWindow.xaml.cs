using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KIT502_Software_Solution
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

        private void btnCategoryList_Click_1(object sender, RoutedEventArgs e)
        {
            CategoryListForm ctf = new CategoryListForm();
            ctf.ShowDialog();
        }

        private void btnProductSearch_Click_1(object sender, RoutedEventArgs e)
        {
            ProductListForm plf = new ProductListForm();
            plf.ShowDialog();
        }

        private void btnSalesReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
