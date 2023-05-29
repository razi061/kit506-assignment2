using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using KIT502_Software_Solution.Model;
using KIT502_Software_Solution.Utility;

namespace KIT502_Software_Solution
{
    /// <summary>
    /// Interaction logic for ReorderForm.xaml
    /// </summary>
    public partial class ReorderForm : Window
    {
        private int Product_Id = 0;

        public ReorderForm(int productId)
        {
            InitializeComponent();

            this.Product_Id = productId;
            this.PopulateData();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.Product_Id <= 0)
            {
                MessageBox.Show("Invalid product.");
                return;
            }

            var quantity = ValueConvert.ToInt(this.txtReorderQuantity.Text.Trim());
            var comments = ValueConvert.ToString(this.txtComments.Text.Trim());

            if (quantity <= 0)
            {
                MessageBox.Show("Please enter quantity.");
                return;
            }

            if (comments.Length <= 0)
            {
                MessageBox.Show("Please enter comments.");
                return;
            }

            Message msg = Reorder.Insert(Product_Id, quantity, comments);
            MessageBox.Show(msg.Msg);
            this.Clear();
        }

        private void PopulateData()
        {
            if(this.Product_Id == 0)
            {
                return;
            }

            var product = Product.GetById(this.Product_Id);

            if(product != null && product.id > 0)
            {
                this.lblProductName.Content = product.ToString();
                this.lblCurrentStock.Content = ValueConvert.ToString(product.stock.ToString());
                this.lblReordeDate.Content = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void Clear()
        {
            this.txtReorderQuantity.Clear();
            this.txtComments.Clear();
        }
    }
}
