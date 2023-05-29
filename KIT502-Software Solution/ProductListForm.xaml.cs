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

using KIT502_Software_Solution.Model;
using KIT502_Software_Solution.Utility;

namespace KIT502_Software_Solution
{
    /// <summary>
    /// Interaction logic for ProductListForm.xaml
    /// </summary>
    public partial class ProductListForm : Window
    {
        private Product Selected_Product = new Product();

        public ProductListForm()
        {
            InitializeComponent();

            this.PopulateData();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.lbProductList.ItemsSource = null;
            this.Selected_Product = new Product();
            this.SearchProducts();
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            var pdf = new ProductEditForm();
            pdf.ShowDialog();
        }

        private void btnReorderProduct_Click(object sender, RoutedEventArgs e)
        {
            if(this.Selected_Product.id > 0)
            {
                var rof = new ReorderForm(this.Selected_Product.id);
                rof.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a product and try again.");
            }
        }

        private void PopulateData()
        {
            var categoryList = Category.LoadAll(true);
            this.cmbCategory.ItemsSource = categoryList;
            this.cmbCategory.SelectedValuePath = "id";
            this.cmbCategory.DisplayMemberPath = "name";
            this.cmbCategory.SelectedValue = 0;
            this.SearchProducts();
        }

        private void SearchProducts()
        {
            var categoryId = ValueConvert.ToInt(this.cmbCategory.SelectedValue.ToString());
            var query = this.txtSearch.Text.Trim();

            var productList = Product.Find(categoryId, query);
            this.lbProductList.ItemsSource = productList;
            this.lbProductList.SelectedValuePath = "id";
            this.lbProductList.DisplayMemberPath = "name";
        }

        private void lbProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Selected_Product = (Product)lbProductList.SelectedItem;            
        }
    }
}
