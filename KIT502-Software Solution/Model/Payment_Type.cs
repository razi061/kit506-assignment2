using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class Payment_Type
    {
        public int id { get; set; }
        public string name { get; set; }

        public Payment_Type()
        {
            this.id = 0;
            this.name = string.Empty;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static IList<Payment_Type> Convert(MySqlDataReader? dr)
        {
            var paymentTypeList = new List<Payment_Type>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    paymentTypeList.Add(new Payment_Type
                    {
                        id = dr.GetInt32("id"),
                        name = dr.GetString("name")
                    });
                }
            }
            return paymentTypeList;
        }
    }
}
