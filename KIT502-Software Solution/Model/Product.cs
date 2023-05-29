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
        public double user_rating { get; set; }
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

        public static Product GetById(int id)
        {
            Product product = new Product();
            MySqlDataReader? dr = null;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM " + TABLE_NAME + " WHERE id=" + id, conn))
                    {
                        dr = cmd.ExecuteReader();
                        product = Convert(dr)[0];
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

            return product;
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

        public static Message Save(Product product)
        {
            Message msg = new Message(Message.MessageTypes.Information, "Saved successfully");

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    string str = "";

                    if(product.id <= 0)
                    {
                        str = "INSERT INTO " + TABLE_NAME + " (name, category_id, barcode, product_type, brand, model, energy_rating, " +
                            "width, height, depth, weight, warrenty, stock, listed_price, minimum_price, base_price, home_delivery, user_rating) " +
                            "VALUES (@name, @category_id, @barcode, @product_type, @brand, @model, @energy_rating, @width, @height, " +
                            "@depth, @weight, @warrenty, @stock, @listed_price, @minimum_price, @base_price, @home_delivery, @user_rating)";
                    }
                    else
                    {
                        str = "UPDATE " + TABLE_NAME + " SET name=@name, barcode=@barcode, product_type=@product_type, brand=@brand, model=@model " +
                            "energy_rating=@energy_rating, width=@width, height=@height, depth=@depth, weight=@weight, warrenty=@warrenty, " +
                            "stock=@stock, listed_price=@listed_price, minimum_price=@minimum_price, base_price=@base_price, " +
                            "home_delivery=@home_delivery, user_rating=@user_rating WHERE id="+product.id;
                    }



                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = str;
                    comm.Parameters.AddWithValue("@name", product.brand + " " + product.product_type + " " + product.model);
                    comm.Parameters.AddWithValue("@barcode", product.barcode);
                    comm.Parameters.AddWithValue("@product_type", product.barcode);
                    comm.Parameters.AddWithValue("@brand", product.brand);
                    comm.Parameters.AddWithValue("@model", product.model);
                    comm.Parameters.AddWithValue("@energy_rating", product.energy_rating);
                    comm.Parameters.AddWithValue("@width", product.width);
                    comm.Parameters.AddWithValue("@height", product.height);
                    comm.Parameters.AddWithValue("@depth", product.depth);
                    comm.Parameters.AddWithValue("@weight", product.weight);
                    comm.Parameters.AddWithValue("@warrenty", product.warranty);
                    comm.Parameters.AddWithValue("@home_delivery", product.home_delivery);
                    comm.Parameters.AddWithValue("@user_rating", product.user_rating);

                    int result = comm.ExecuteNonQuery();

                    if (result <= 0)
                    {
                        msg = new Message(Message.MessageTypes.Error, result.ToString());
                    }
                }
                catch (Exception ex)
                {
                    msg = new Message(Message.MessageTypes.Error, "An error occurred. Details: " + ex.Message);
                }
                finally
                {
                    DbConnection.CloseDbConnection();
                }
            }

            return msg;
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
                        energy_rating = dr.GetDouble("energy_rating"),
                        width = dr.GetDouble("width"),
                        height = dr.GetDouble("height"),
                        depth = dr.GetDouble("depth"),
                        weight = dr.GetDouble("weight"),
                        warranty = dr.GetInt32("warranty"),
                        stock = dr.GetInt32("stock"),
                        listed_price = dr.GetDouble("listed_price"),
                        minimum_price = dr.GetDouble("minimum_price"),
                        base_price = dr.GetDouble("base_price"),
                        home_delivery = dr.GetBoolean("home_delivery"),
                        user_rating = dr.GetDouble("user_rating"),
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
