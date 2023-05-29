using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using KIT502_Software_Solution.Model;
using KIT502_Software_Solution.DBUtility;
using System.Runtime.CompilerServices;
using Renci.SshNet.Messages;

namespace KIT502_Software_Solution.Model
{
    internal class Reorder
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public string comments { get; set; }
        public DateTime reorder_datetime { get; set; }

        public const string TABLE_NAME = "reorder";

        public Reorder()
        {
            this.id = 0;
            this.product_id = 0;
            this.quantity = 0;
            this.comments = string.Empty;
            this.reorder_datetime = new DateTime();
        }

        public static Message Insert(int product_id, int quantity, string comments)
        {
            Message msg = new Message(Message.MessageTypes.Information, "Inserted successfully");

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO " + TABLE_NAME + "(product_id, quantity, comments, reorder_datetime) " +
                        "VALUES(@product_id, @quantity, @comments, @reorder_datetime)";
                    comm.Parameters.AddWithValue("@product_id", product_id);
                    comm.Parameters.AddWithValue("@quantity", quantity);
                    comm.Parameters.AddWithValue("@comments", comments);
                    comm.Parameters.AddWithValue("@reorder_datetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    int result = comm.ExecuteNonQuery();

                    if (result <= 0)
                    {
                        msg = new Message(Message.MessageTypes.Error, "Insert unsuccessfull.");
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

        private static IList<Reorder> Convert(MySqlDataReader? dr)
        {
            var reorderList = new List<Reorder>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    reorderList.Add(new Reorder
                    {
                        id = dr.GetInt32("id"),
                        product_id = dr.GetInt32("product_id"),
                        quantity = dr.GetInt32("quantity"),
                        comments = dr.GetString("comments"),
                        reorder_datetime = dr.GetDateTime("reorder_datetime")
                    });
                }
            }

            return reorderList;
        }
    }
}
