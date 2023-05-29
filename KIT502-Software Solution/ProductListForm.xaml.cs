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
using System.Windows.Shapes;

namespace KIT502_Software_Solution
{
    /// <summary>
    /// Interaction logic for ProductListForm.xaml
    /// </summary>
    public partial class ProductListForm : Window
    {
        public ProductListForm()
        {
            InitializeComponent();
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            var pdf = new ProductEditForm();
            pdf.ShowDialog();
        }
    }
}
