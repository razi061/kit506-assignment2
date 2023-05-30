using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

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

        public static Washing_Machine GetByProductId(int productId)
        {
            Washing_Machine product = new Washing_Machine();
            MySqlDataReader? dr = null;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM " + TABLE_NAME + " WHERE product_id=" + productId, conn))
                    {
                        dr = cmd.ExecuteReader();
                        product = Convert(dr)[0];
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

            return product;
        }

        public static Message Save(Washing_Machine wm)
        {
            Message msg = new Message(Message.MessageTypes.Information, "Saved successfully");

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    string str = "";

                    if (wm.id <= 0)
                    {
                        str = "INSERT INTO " + TABLE_NAME + " (product_id, colour, power_consumption, wels_water_efficiency, wels_water_consumption, " +
                            "wels_registration_number, delay_start, washing_capacity, internal_tube_material) " +
                            "VALUES (@product_id, @colour, @power_consumption, @wels_water_efficiency, @wels_water_consumption, " +
                            "@wels_registration_number, @delay_start, @washing_capacity, @internal_tube_material)";
                    }
                    else
                    {
                        str = "UPDATE " + TABLE_NAME + " SET product_id=@product_id, colour=@colour, power_consumption=@power_consumption, " +
                            "wels_water_efficiency=@wels_water_efficiency, wels_water_consumption=@wels_water_consumption, wels_registration_number=@wels_registration_number, " +
                            "delay_start=@delay_start, washing_capacity=@washing_capacity, internal_tube_material=@internal_tube_material WHERE id=" + wm.id;
                    }

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = str;
                    comm.Parameters.AddWithValue("@product_id", wm.product_id);
                    comm.Parameters.AddWithValue("@colour", wm.colour);
                    comm.Parameters.AddWithValue("@power_consumption", wm.power_consumption);
                    comm.Parameters.AddWithValue("@wels_water_efficiency", wm.wels_water_efficiency);
                    comm.Parameters.AddWithValue("@wels_water_consumption", wm.wels_water_consumption);
                    comm.Parameters.AddWithValue("@wels_registration_number", wm.wels_registration_number);
                    comm.Parameters.AddWithValue("@delay_start", wm.delay_start);
                    comm.Parameters.AddWithValue("@washing_capacity", wm.washing_capacity);
                    comm.Parameters.AddWithValue("@internal_tube_material", wm.internal_tube_material);

                    int result = comm.ExecuteNonQuery();

                    if (result <= 0)
                    {
                        msg = new Message(Message.MessageTypes.Error, "Save unsuccessful.");
                    }
                    else
                    {
                        result = (int)comm.LastInsertedId;
                        msg = new Message(Message.MessageTypes.Information, result.ToString());
                    }
                }
                catch (Exception ex)
                {
                    msg = new Message(Message.MessageTypes.Error, "An error occurred. Details: " + ex.Message);
                }
                finally
                {
                    DbConnection.CloseDbConnection();
                }
            }

            return msg;
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
                        wels_water_efficiency = dr.GetDouble("wels_water_efficiency"),
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
