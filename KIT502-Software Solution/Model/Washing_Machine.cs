using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KIT502_Software_Solution.Model
{
    internal class Washing_Machine
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string colour { get; set; }
        public int power_consumption { get; set; }
        public double wels_water_efficiency { get; set; }
        public int wels_water_consumption { get; set; }
        public string wels_registration_number { get; set; }
        public string delay_start { get; set; }
        public int washing_capacity { get; set; }
        public string internal_tube_material { get; set; }

        public const string TABLE_NAME = "washing_machine";

        public Washing_Machine()
        {
            this.id = 0;
            this.product_id = 0;
            this.colour = string.Empty;
            this.power_consumption = 0;
            this.wels_water_efficiency = 0;
            this.wels_water_consumption = 0;
            this.wels_registration_number = string.Empty;
            this.delay_start = string.Empty;
            this.washing_capacity = 0;
            this.internal_tube_material = string.Empty;
        }

        private static IList<Washing_Machine> Convert(MySqlDataReader? dr)
        {
            var washingMachineList = new List<Washing_Machine>();
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    washingMachineList.Add(new Washing_Machine
                    {
                        id = dr.GetInt32("id"),
                        product_id = dr.GetInt32("product_id"),
                        colour = dr.GetString("colour"),
                        power_consumption = dr.GetInt32("power_consumption"),
                        wels_water_efficiency = dr.GetInt32("wels_water_efficiency"),
                        wels_water_consumption = dr.GetInt32("wels_water_consumption"),
                        wels_registration_number = dr.GetString("wels_registration_number"),
                        delay_start = dr.GetString("delay_start"),
                        washing_capacity = dr.GetInt32("washing_capacity"),
                        internal_tube_material = dr.GetString("internal_tube_material")
                    });
                }
            }
            return washingMachineList;
        }

    }
}
