using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for SalesHistoryForm.xaml
    /// </summary>
    public partial class SalesHistoryForm : Window
    {
        private Product Product;

        public SalesHistoryForm(int productId)
        {
            InitializeComponent();

            this.Product = Product.GetById(productId);
            this.lblProductName.Content = this.Product.name.ToString();

            this.dpFromDate.SelectedDate = DateTime.Now.Date.AddDays(-30);
            this.dpToDate.SelectedDate = DateTime.Now.Date;
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            var purchaseList = Purchase.GetPurchaseHistory(this.Product.id, this.dpFromDate.SelectedDate.Value.Date, 
                this.dpToDate.SelectedDate.Value.Date);

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Voucher Id");
            dt.Columns.Add("Payment Details");
            dt.Columns.Add("Total Amount");
            dt.Columns.Add("Purchase Datetime");

            foreach (var cat in purchaseList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = cat.id;
                dr[1] = cat.quantity;
                dr[2] = cat.voucher_id;
                dr[3] = cat.payment_details;
                dr[4] = cat.total;
                dr[5] = cat.purchase_datetime;

                dt.Rows.Add(dr);
            }

            this.dgvReport.ItemsSource = dt.DefaultView;
        }
    }
}
