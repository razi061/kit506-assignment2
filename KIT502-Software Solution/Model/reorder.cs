using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class reorder
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public string comments { get; set; }
        public DateTime reorder_datetime { get; set; }

        public const string TABLE_NAME = "reorder";

        public reorder()
        {
            this.id = 0;
            this.product_id = 0;
            this.quantity = 0;
            this.comments = string.Empty;
            this.reorder_datetime = new DateTime();
        }
    }
}
