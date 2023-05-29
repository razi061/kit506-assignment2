using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT502_Software_Solution.Model
{
    internal class Buyer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }

        public Buyer()
        {
            this.id = 0;
            this.name = string.Empty;
            this.email = string.Empty;
            this.address = string.Empty;
        }
    }
}
