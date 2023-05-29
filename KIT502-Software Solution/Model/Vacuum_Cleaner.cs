using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class Vacuum_Cleaner
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string colour { get; set; }
        public double max_capacity { get; set; }
        public string vacuum_bag { get; set; }
        public int standard_run_time { get; set; }

        public const string TABLE_NAME = "vaccum_cleaner";

        public Vacuum_Cleaner()
        {
            this.id = 0;
            this.product_id = 0;
            this.colour = string.Empty;
            this.max_capacity = 0;
            this.vacuum_bag = string.Empty;
            this.standard_run_time = 0;
        }

        private static IList<Vacuum_Cleaner> Convert(MySqlDataReader? dr)
        {
            var vacuumCleanerList = new List<Vacuum_Cleaner>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    vacuumCleanerList.Add(new Vacuum_Cleaner
                    {
                        id = dr.GetInt32("id"),
                        product_id = dr.GetInt32("product_id"),
                        colour = dr.GetString("colour"),
                        max_capacity = dr.GetDouble("max_capacity"),
                        vacuum_bag = dr.GetString("vacuum_bag"),
                        standard_run_time = dr.GetInt32("standard_run_time")
                    });
                }
            }
            return vacuumCleanerList;
        }
    }
}
