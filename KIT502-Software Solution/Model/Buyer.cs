using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

namespace KIT502_Software_Solution.Model
{
    internal class Buyer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }

        public const string TABLE_NAME = "buyer";

        public Buyer()
        {
            this.id = 0;
            this.name = string.Empty;
            this.email = string.Empty;
            this.address = string.Empty;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static Buyer GetByEmail(string email)
        {
            var buyer = new Buyer();
            MySqlDataReader? dr = null;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM " + TABLE_NAME + " WHERE email='" + email + "'", conn))
                    {
                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            buyer = Convert(dr)[0];
                        }
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

            return buyer;
        }

        public static Message Save(Buyer buyer)
        {
            Message msg = new Message(Message.MessageTypes.Information, "Saved successfully");

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    string str = "INSERT INTO " + TABLE_NAME + " (name, email, address) " +
                            "VALUES (@name, @email, @address)";

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = str;
                    comm.Parameters.AddWithValue("@name", buyer.name);
                    comm.Parameters.AddWithValue("@email", buyer.email);
                    comm.Parameters.AddWithValue("@address", buyer.address);

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

        private static IList<Buyer> Convert(MySqlDataReader? dr)
        {
            var buyerList = new List<Buyer>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    buyerList.Add(new Buyer
                    {
                        id = dr.GetInt32("id"),
                        name = dr.GetString("name"),
                        email = dr.GetString("email"),
                        address = dr.GetString("address")
                    });
                }
            }
            return buyerList;
        }
    }
}
