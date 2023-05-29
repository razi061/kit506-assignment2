using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class purchase
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public double sales_price { get; set; }
        public int quantity { get; set; }
        public int voucher_id { get; set; }
        public int buyer_id { get; set; }
        public int payment_type_id { get; set; }
        public string payment_details { get; set; }
        public double total { get; set; }
        public DateTime purchase_datetime { get; set; }
        public bool home_delivery { get; set; }

        public const string TABLE_NAME = "purchase";

        public purchase()
        {
            this.id = 0;
            this.product_id = 0;
            this.sales_price = 0;
            this.quantity = 0;
            this.voucher_id = 0;
            this.buyer_id = 0;
            this.payment_type_id = 0;
            this.payment_details = string.Empty;
            this.total = 0;
            this.purchase_datetime = new DateTime();
            this.home_delivery = false;
        }
    }
}
