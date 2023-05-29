using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class Buyer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }

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

        public static IList<Buyer> Convert(MySqlDataReader? dr)
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
