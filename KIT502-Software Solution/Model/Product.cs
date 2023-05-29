using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

namespace KIT502_Software_Solution.Model
{
    internal class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int category_id { get; set; }
        public double energy_rating { get; set; }
        public string barcode { get; set; }
        public string product_type { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double depth { get; set; }
        public double weight { get; set; }
        public int warranty { get; set; }
        public int stock { get; set; }
        public double listed_price { get; set; }
        public double minimum_price { get; set; }
        public double base_price { get; set; }
        public bool home_delivery { get; set; }
        public int user_rating { get; set; }
        public string photo { get; set; }

        public const string TABLE_NAME = "product";

        public Product()
        {
            this.id = 0;
            this.name = string.Empty;
            this.category_id = 0;
            this.barcode = string.Empty;
            this.product_type = string.Empty;
            this.brand = string.Empty;
            this.model = string.Empty;
            this.energy_rating = 0;
            this.width = 0;
            this.height = 0;
            this.depth = 0;
            this.weight = 0;
            this.warranty = 0;
            this.stock = 0;
            this.listed_price = 0;
            this.minimum_price = 0;
            this.base_price = 0;
            this.home_delivery = false;
            this.user_rating = 0;
            this.photo = string.Empty;
        }

        public static IList<Product> Find(int categoryId, string query)
        {
            IList<Product> productList = new List<Product>();
            MySqlDataReader? dr = null;
            var whereClause = "";

            if(categoryId > 0)
            {
                whereClause += "category_id = " + categoryId;
            }

            if(query.Length > 0)
            {
                if(whereClause.Length > 0)
                {
                    whereClause += " and ";
                }

                whereClause += "(name LIKE '%" + query + "%' OR barcode LIKE '%" + query + "%' OR product_type LIKE '%" +
                    query + "%' OR brand LIKE '%" + query + "%' OR model LIKE '%" + query + "%')";
            }

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    query = "SELECT * FROM `" + TABLE_NAME + "`" +
                        (whereClause.Length > 0 ? " WHERE " + whereClause : "") + " ORDER BY id DESC";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        dr = cmd.ExecuteReader();
                        productList = Convert(dr);
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    DbConnection.CloseDbConnection();
                }
            }

            return productList;
        }

        private static IList<Product> Convert(MySqlDataReader? dr)
        {
            var productList = new List<Product>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    productList.Add(new Product
                    {
                        id = dr.GetInt32("id"),
                        name = dr.GetString("name"),
                        category_id = dr.GetInt32("category_id"),
                        barcode = dr.GetString("barcode"),
                        product_type = dr.GetString("product_type"),
                        brand = dr.GetString("brand"),
                        model = dr.GetString("model"),
                        energy_rating = dr.GetInt32("energy_rating"),
                        width = dr.GetInt32("width"),
                        height = dr.GetInt32("height"),
                        depth = dr.GetInt32("depth"),
                        weight = dr.GetInt32("weight"),
                        warranty = dr.GetInt32("warranty"),
                        stock = dr.GetInt32("stock"),
                        listed_price = dr.GetInt32("listed_price"),
                        minimum_price = dr.GetInt32("minimum_price"),
                        base_price = dr.GetInt32("base_price"),
                        home_delivery = dr.GetBoolean("home_delivery"),
                        user_rating = dr.GetInt32("user_rating"),
                        photo = dr.GetString("photo")
                    });
                }
            }

            return productList;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
