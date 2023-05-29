using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

namespace KIT502_Software_Solution.Model
{
    internal class Fridge
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string colour { get; set; }
        public string fridge_features { get; set; }
        public int fridge_capacity { get; set; }
        public string freezer_features { get; set; }
        public int freezer_capacity { get; set; }

        public const string TABLE_NAME = "fridge";

        public Fridge()
        {
            this.id = 0;
            this.product_id = 0;
            this.colour = string.Empty;
            this.fridge_features = string.Empty;
            this.fridge_capacity = 0;
            this.freezer_features = string.Empty;
            this.freezer_capacity = 0;
        }

        public static Fridge GetByProductId(int productId)
        {
            Fridge product = new Fridge();
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

        private static IList<Fridge> Convert(MySqlDataReader? dr)
        {
            var fridgeList = new List<Fridge>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    fridgeList.Add(new Fridge
                    {
                        id = dr.GetInt32("id"),
                        product_id = dr.GetInt32("product_id"),
                        colour = dr.GetString("colour"),
                        fridge_features = dr.GetString("fridge_features"),
                        fridge_capacity = dr.GetInt32("fridge_capacity"),
                        freezer_features = dr.GetString("freezer_features"),
                        freezer_capacity = dr.GetInt32("freezer_capacity")

                    });
                }
            }
            
            return fridgeList;
        }
    }
}
