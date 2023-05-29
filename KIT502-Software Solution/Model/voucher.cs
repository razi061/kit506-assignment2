using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class voucher
    {
        public int id { get; set; }
        public double discount { get; set; }
        public DateTime expiry_date { get; set; }
        public int buyer_id { get; set; }

        public const string TABLE_NAME = "voucher";


        public voucher()
        {
            this.id = 0;
            this.discount = 0;
            this.expiry_date = new DateTime();
            this.buyer_id = 0;
        }
    }
}
