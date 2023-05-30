using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

namespace KIT502_Software_Solution.Model
{
    internal class Product_Performance
    {
        public int product_id { get; set; }
        public double performance { get; set; }
        public string product_name { get; set; }
        public string category_name { get; set; }
        public int total_sales { get; set; }
        public int current_stock { get; set; }
        public double price { get; set; }

        public const string TABLE_NAME = "product_performance";

        public Product_Performance()
        {
            this.product_id = 0;
            this.performance = 0.0;
            this.product_name = "";
            this.category_name = "";
            this.total_sales = 0;
            this.current_stock = 0;
            this.price = 0.0;
        }

        public static IList<Product_Performance> GetReport(double min, double max)
        {
            IList<Product_Performance> ppList = new List<Product_Performance>();
            MySqlDataReader? dr = null;

            string query = "SELECT * FROM " + TABLE_NAME + " WHERE performance >= " + min + " AND performance <= " + max;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        dr = cmd.ExecuteReader();
                        ppList = Convert(dr);
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    DbConnection.CloseDbConnection();
                }
            }

            return ppList;
        }

        private static IList<Product_Performance> Convert(MySqlDataReader? dr)
        {
            var ppList = new List<Product_Performance>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    ppList.Add(new Product_Performance()
                    {
                        product_id = dr.GetInt32("product_id"),
                        performance = dr.GetDouble("performance"),
                        product_name = dr.GetString("product_name"),
                        category_name = dr.GetString("category_name"),
                        total_sales = dr.GetInt32("total_sales"),
                        current_stock = dr.GetInt32("current_stock"),
                        price = dr.GetDouble("price")
                    });
                }
            }

            return ppList;
        }
    }
}
