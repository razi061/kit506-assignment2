using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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

        private static IList<Reorder> Convert(MySqlDataReader? dr)
        {
            var reorderList = new List<Reorder>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    reorderList.Add(new Reorder()
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
