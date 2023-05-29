using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
