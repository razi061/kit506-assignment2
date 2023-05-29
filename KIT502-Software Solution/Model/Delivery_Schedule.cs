using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class Delivery_Schedule
    {
        public int id { get; set; }
        public int purchase_id { get; set; }
        public int product_id { get; set; }
        public string status { get; set; }

        public Delivery_Schedule()
        {
            this.id = 0;
            this.purchase_id = 0;
            this.product_id = 0;
            this.status = string.Empty;

        }

        public static IList<Delivery_Schedule> Convert(MySqlDataReader? dr)
        {
            var deliveryScheduleList = new List<Delivery_Schedule>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    deliveryScheduleList.Add(new Delivery_Schedule
                    {
                        id = dr.GetInt32("id"),
                        purchase_id = dr.GetInt32("purchase_id"),
                        product_id = dr.GetInt32("product_id"),
                        status = dr.GetString("status")
                    });
                }
            }
            return deliveryScheduleList;
        }
    }
}
