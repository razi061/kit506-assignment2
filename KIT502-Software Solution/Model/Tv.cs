﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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