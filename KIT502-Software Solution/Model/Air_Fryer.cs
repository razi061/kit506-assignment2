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
