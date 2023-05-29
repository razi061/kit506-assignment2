using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class Voucher
    {
        public int id { get; set; }
        public double discount { get; set; }
        public DateTime expiry_date { get; set; }
        public int buyer_id { get; set; }

        public const string TABLE_NAME = "voucher";

        public Voucher()
        {
            this.id = 0;
            this.discount = 0;
            this.expiry_date = new DateTime();
            this.buyer_id = 0;
        }

        private static IList<Voucher> Convert(MySqlDataReader? dr)
        {
            var voucherList = new List<Voucher>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    voucherList.Add(new Voucher
                    {
                        id = dr.GetInt32("id"),
                        discount = dr.GetInt32("discount"),
                        expiry_date = dr.GetDateTime("expiry_date"),
                        buyer_id = dr.GetInt32("buyer_id")
                    });
                }
            }

            return voucherList;
        }
    }
}
