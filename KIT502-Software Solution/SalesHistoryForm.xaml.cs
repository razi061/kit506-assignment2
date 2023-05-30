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

            int currentYear = DateTime.Now.Year;
            this.dpFromDate.SelectedDate = new DateTime(currentYear, 1, 1);
            this.dpToDate.SelectedDate = DateTime.Now.Date;

            for (int i = 0; i <= 5; i++)
            {
                this.cmbYear.Items.Add(currentYear.ToString());
                currentYear--;
            }

            this.ShowReport(this.dpFromDate.SelectedDate.Value.Date, this.dpToDate.SelectedDate.Value.Date);
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            this.ShowReport(this.dpFromDate.SelectedDate.Value.Date, this.dpToDate.SelectedDate.Value.Date);
        }

        private void cmbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int year = ValueConvert.ToInt(this.cmbYear.SelectedItem.ToString());
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime toDate = new DateTime(year, 12, 31);

            this.ShowReport(startDate, toDate);
        }

        private void ShowReport(DateTime startDate, DateTime enddate)
        {
            var purchaseList = Purchase.GetPurchaseHistory(this.Product.id, startDate, enddate);

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
