using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;

namespace KIT502_Software_Solution.Model
{
    internal class Tv
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string tv_range { get; set; }
        public string screen_type { get; set; }
        public double screen_size { get; set; }
        public string screen_definition { get; set; }
        public string screen_resolution { get; set; }
        public int no_hdmi_ports { get; set; }
        public int no_usb_ports { get; set; }
        public string connectivity { get; set; }

        public const string TABLE_NAME = "tv";

        public Tv()
        {
            this.id = 0;
            this.product_id = 0;
            this.tv_range = string.Empty;
            this.screen_type = string.Empty;
            this.screen_size = 0;
            this.screen_definition = string.Empty;
            this.screen_resolution = string.Empty;
            this.no_hdmi_ports = 0;
            this.no_usb_ports = 0;
            this.connectivity = string.Empty;
        }

        public static Tv GetByProductId(int productId)
        {
            Tv product = new Tv();
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

        public static Message Save(Tv tv)
        {
            Message msg = new Message(Message.MessageTypes.Information, "Saved successfully");

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    string str = "";

                    if (tv.id <= 0)
                    {
                        str = "INSERT INTO " + TABLE_NAME + " (product_id, tv_range, screen_type, screen_size, screen_definition, " +
                            "screen_resolution, no_hdmi_ports, no_usb_ports, connectivity) " +
                            "VALUES (@product_id, @tv_range, @screen_type, @screen_size, @screen_definition, " +
                            "@screen_resolution, @no_hdmi_ports, @no_usb_ports, @connectivity)";
                    }
                    else
                    {
                        str = "UPDATE " + TABLE_NAME + " SET product_id=@product_id, tv_range=@tv_range, screen_type=@screen_type, " +
                            "screen_size=@screen_size, screen_definition=@screen_definition, screen_resolution=@screen_resolution, " +
                            "no_hdmi_ports=@no_hdmi_ports, no_usb_ports=@no_usb_ports, connectivity=@connectivity WHERE id=" + tv.id;
                    }

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = str;
                    comm.Parameters.AddWithValue("@product_id", tv.product_id);
                    comm.Parameters.AddWithValue("@tv_range", tv.tv_range);
                    comm.Parameters.AddWithValue("@screen_type", tv.screen_type);
                    comm.Parameters.AddWithValue("@screen_size", tv.screen_size);
                    comm.Parameters.AddWithValue("@screen_definition", tv.screen_definition);
                    comm.Parameters.AddWithValue("@screen_resolution", tv.screen_resolution);
                    comm.Parameters.AddWithValue("@no_hdmi_ports", tv.no_hdmi_ports);
                    comm.Parameters.AddWithValue("@no_usb_ports", tv.no_usb_ports);
                    comm.Parameters.AddWithValue("@connectivity", tv.connectivity);

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

        private static IList<Tv> Convert(MySqlDataReader? dr)
        {
            var tvList = new List<Tv>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    tvList.Add(new Tv()
                    {
                        id = dr.GetInt32("id"),
                        product_id = dr.GetInt32("product_id"),
                        tv_range = dr.GetString("tv_range"),
                        screen_type = dr.GetString("screen_type"),
                        screen_size = dr.GetDouble("screen_size"),
                        screen_definition = dr.GetString("screen_definition"),
                        screen_resolution = dr.GetString("screen_resolution"),
                        no_hdmi_ports = dr.GetInt32("no_hdmi_ports"),
                        no_usb_ports = dr.GetInt32("no_usb_ports"),
                        connectivity = dr.GetString("connectivity")
                    });
                }
            }
            
            return tvList;
        }
    }
}
