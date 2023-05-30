using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KIT502_Software_Solution.DBUtility;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class Payment_Type
    {
        public int id { get; set; }
        public string name { get; set; }

        public const string TABLE_NAME = "payment_type";


        public Payment_Type()
        {
            this.id = 0;
            this.name = string.Empty;
        }


        public override string ToString()
        {
            return this.name;
        }

        public static IList<Payment_Type> LoadAll(bool addAll = false)
        {
            IList<Payment_Type> list = new List<Payment_Type>();
            MySqlDataReader? dr = null;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM " + TABLE_NAME, conn))
                    {
                        dr = cmd.ExecuteReader();
                        list = Convert(dr);
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

            if (addAll)
            {
                list.Add(new Payment_Type { id = 0, name = "All" });
            }

            list = list.OrderBy(c => c.id).ToList();
            return list;
        }

        private static IList<Payment_Type> Convert(MySqlDataReader? dr)
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
