using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

namespace KIT502_Software_Solution.Model
{
    internal class Purchase
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public double sales_price { get; set; }
        public int quantity { get; set; }
        public int voucher_id { get; set; }
        public int buyer_id { get; set; }
        public int payment_type_id { get; set; }
        public string payment_details { get; set; }
        public double total { get; set; }
        public DateTime purchase_datetime { get; set; }
        public bool home_delivery { get; set; }

        public const string TABLE_NAME = "purchase";

        public Purchase()
        {
            this.id = 0;
            this.product_id = 0;
            this.sales_price = 0;
            this.quantity = 0;
            this.voucher_id = 0;
            this.buyer_id = 0;
            this.payment_type_id = 0;
            this.payment_details = string.Empty;
            this.total = 0;
            this.purchase_datetime = new DateTime();
            this.home_delivery = false;
        }

        public static Message Save(Purchase purchase)
        {
            Message msg = new Message(Message.MessageTypes.Information, "Saved successfully");

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    string str = "INSERT INTO " + TABLE_NAME + " (product_id, sales_price, quantity, voucher_id, buyer_id, payment_type_id, " +
                            "payment_details, total, purchase_datetime, home_delivery) " +
                            "VALUES (@product_id, @sales_price, @quantity, @voucher_id, @buyer_id, @payment_type_id, " +
                            "@payment_details, @total, @purchase_datetime, @home_delivery)";



                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = str;
                    comm.Parameters.AddWithValue("@product_id", purchase.product_id);
                    comm.Parameters.AddWithValue("@sales_price", purchase.sales_price);
                    comm.Parameters.AddWithValue("@quantity", purchase.quantity);
                    comm.Parameters.AddWithValue("@voucher_id", purchase.voucher_id == 0 ? DBNull.Value : purchase.voucher_id);
                    comm.Parameters.AddWithValue("@buyer_id", purchase.buyer_id);
                    comm.Parameters.AddWithValue("@payment_type_id", purchase.payment_type_id);
                    comm.Parameters.AddWithValue("@payment_details", purchase.payment_details);
                    comm.Parameters.AddWithValue("@total", purchase.total);
                    comm.Parameters.AddWithValue("@purchase_datetime", purchase.purchase_datetime);
                    comm.Parameters.AddWithValue("@home_delivery", purchase.home_delivery);

                    int result = comm.ExecuteNonQuery();

                    if (result <= 0)
                    {
                        msg = new Message(Message.MessageTypes.Error, "Save unsuccessful.");
                    }
                    else
                    {
                        result = (int)comm.LastInsertedId;
                        msg = new Message(Message.MessageTypes.Information, result.ToString());
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

        public static IList<Purchase> GetPurchaseHistory(int productId, DateTime fromDate, DateTime toDate)
        {
            IList<Purchase> purchaseList = new List<Purchase>();
            MySqlDataReader? dr = null;

            string query = "SELECT * FROM " + TABLE_NAME + " WHERE product_id=" + productId + " AND " +
                "DATE(purchase_datetime) >= '" + fromDate.ToString("yyyy-MM-dd") + "' AND " +
                "DATE(purchase_datetime) <= '" + toDate.ToString("yyyy-MM-dd") + "'";

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        dr = cmd.ExecuteReader();
                        purchaseList = Convert(dr);
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

            return purchaseList;
        }

        private static IList<Purchase> Convert(MySqlDataReader? dr)
        {
            var purchaseList = new List<Purchase>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    purchaseList.Add(new Purchase()
                    {
                        id = dr.GetInt32("id"),
                        product_id = dr.GetInt32("product_id"),
                        sales_price = dr.GetDouble("sales_price"),
                        quantity = dr.GetInt32("quantity"),
                        voucher_id = dr.GetInt32("voucher_id"),
                        buyer_id = dr.GetInt32("buyer_id"),
                        payment_type_id = dr.GetInt32("payment_type_id"),
                        payment_details = dr.GetString("payment_details"),
                        total = dr.GetDouble("total"),
                        purchase_datetime = dr.GetDateTime("purchase_datetime"),
                        home_delivery = dr.GetBoolean("home_delivery")
                    });
                }
            }

            return purchaseList;
        }
    }
}
