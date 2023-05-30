using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

namespace KIT502_Software_Solution.Model
{
    internal class Air_Fryer
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string colour { get; set; }

        public const string TABLE_NAME = "air_fryer";

        public Air_Fryer()
        {
            this.id = 0;
            this.product_id = 0;
            this.colour = string.Empty;
        }

        public static Air_Fryer GetByProductId(int productId)
        {
            Air_Fryer product = new Air_Fryer();
            MySqlDataReader? dr = null;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM " + TABLE_NAME + " WHERE product_id=" + productId, conn))
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

        public static Message Save(Air_Fryer af)
        {
            Message msg = new Message(Message.MessageTypes.Information, "Saved successfully");

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    string str = "";

                    if (af.id <= 0)
                    {
                        str = "INSERT INTO " + TABLE_NAME + " (product_id, colour) " +
                            "VALUES (@product_id, @colour)";
                    }
                    else
                    {
                        str = "UPDATE " + TABLE_NAME + " SET product_id=@product_id, colour=@colour WHERE id=" + af.id;
                    }

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = str;
                    comm.Parameters.AddWithValue("@product_id", af.product_id);
                    comm.Parameters.AddWithValue("@colour", af.colour);

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

        private static IList<Air_Fryer> Convert(MySqlDataReader? dr)
        {
            var airFryerList = new List<Air_Fryer>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    airFryerList.Add(new Air_Fryer
                    {
                        id = dr.GetInt32("id"),
                        product_id = dr.GetInt32("product_id"),
                        colour = dr.GetString("colour")
                    });
                }
            }

            return airFryerList;
        }
    }
}
