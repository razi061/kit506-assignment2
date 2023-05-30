using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

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

        public static Voucher GetById(int id)
        {
            var voucher = new Voucher();
            MySqlDataReader? dr = null;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM " + TABLE_NAME + " WHERE id=" + id, conn))
                    {
                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            voucher = Convert(dr)[0];
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

            return voucher;
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
                        discount = dr.GetDouble("discount"),
                        expiry_date = dr.GetDateTime("expiry_date"),
                        buyer_id = dr.GetInt32("buyer_id")
                    });
                }
            }

            return voucherList;
        }
    }
}
