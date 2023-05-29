using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT502_Software_Solution.Model
{
    internal class Payment_Type
    {
        public int id { get; set; }
        public string name { get; set; }

        public Payment_Type()
        {
            this.id = 0;
            this.name = string.Empty;
        }
    }
}
