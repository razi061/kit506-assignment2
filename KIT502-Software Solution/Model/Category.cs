using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT502_Software_Solution.Model
{
    class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string low_discount_level { get; set; }
        public string high_discount_level { get; set; }

        public Category() 
        { 
            this.id = 0;
            this.name = string.Empty;
            this.low_discount_level = string.Empty;
            this.high_discount_level = string.Empty;
        }
    }
}
