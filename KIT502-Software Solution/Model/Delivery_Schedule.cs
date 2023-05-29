using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
