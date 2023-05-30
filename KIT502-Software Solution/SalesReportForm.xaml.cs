using KIT502_Software_Solution.Model;
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

namespace KIT502_Software_Solution
{
    /// <summary>
    /// Interaction logic for SalesReportForm.xaml
    /// </summary>
    public partial class SalesReportForm : Window
    {
        public SalesReportForm()
        {
            InitializeComponent();
        }

        private void btnPoor_Click(object sender, RoutedEventArgs e)
        {
            var ppList = Product_Performance.GetReport(0, 20);
            this.ShowReport(ppList);
        }

        private void btnBelowExpectations_Click(object sender, RoutedEventArgs e)
        {
            var ppList = Product_Performance.GetReport(21, 29);
            this.ShowReport(ppList);
        }

        private void btnMeetingMinimum_Click(object sender, RoutedEventArgs e)
        {
            var ppList = Product_Performance.GetReport(30, 100);
            this.ShowReport(ppList);
        }

        private void btnStarPerformer_Click(object sender, RoutedEventArgs e)
        {
            var ppList = Product_Performance.GetReport(50, 100);
            this.ShowReport(ppList);
        }

        private void ShowReport(IList<Product_Performance> ppList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Performance");
            dt.Columns.Add("Product");
            dt.Columns.Add("Category");
            dt.Columns.Add("Total  Sales");
            dt.Columns.Add("Current Stock");
            dt.Columns.Add("Price");

            foreach (var cat in ppList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = cat.product_id;
                dr[1] = cat.performance;
                dr[2] = cat.product_name;
                dr[3] = cat.category_name;
                dr[4] = cat.total_sales;
                dr[5] = cat.current_stock;
                dr[6] = cat.price;

                dt.Rows.Add(dr);
            }

            this.dgvReport.ItemsSource = dt.DefaultView;
        }
    }
}
