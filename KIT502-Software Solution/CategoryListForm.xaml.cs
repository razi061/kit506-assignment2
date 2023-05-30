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
    /// Interaction logic for CategoryListForm.xaml
    /// </summary>
    public partial class CategoryListForm : Window
    {
        public CategoryListForm()
        {
            InitializeComponent();

            this.LoadData();
        }

        private void LoadData()
        {
            var categoryList = Category.LoadAll(false);

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Low Discount Level");
            dt.Columns.Add("High Discount Level");

            foreach(var cat in categoryList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = cat.id; 
                dr[1] = cat.name;
                dr[2] = cat.low_discount_level;
                dr[3] = cat.high_discount_level;

                dt.Rows.Add(dr);
            }

            this.dgvCategoryList.ItemsSource = dt.DefaultView;
        }
    }
}
