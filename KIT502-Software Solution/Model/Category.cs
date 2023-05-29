using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT502_Software_Solution.DBUtility;
using System.Collections;

namespace KIT502_Software_Solution.Model
{
    class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? low_discount_level { get; set; }
        public int? high_discount_level { get; set; }

        public const string TABLE_NAME = "category";

        public Category() 
        { 
            this.id = 0;
            this.name = string.Empty;
            this.low_discount_level = null;
            this.high_discount_level = null;
        }

        public static IList<Category> LoadAll(bool addAll = false)
        {
            IList<Category> categoryList = new List<Category>();
            MySqlDataReader? dr = null;

            using (var conn = DbConnection.OpenDbConnection())
            {
                try
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM " + TABLE_NAME, conn))
                    {
                        dr = cmd.ExecuteReader();
                        categoryList = Convert(dr);
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

            if (addAll)
            {
                categoryList.Add(new Category { id = 0, name = "All" });
            }

            categoryList = categoryList.OrderBy(c => c.id).ToList();
            return categoryList;
        }

        private static IList<Category> Convert(MySqlDataReader? dr)
        {
            var categoryList = new List<Category>();

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    categoryList.Add(new Category
                    {
                        id = dr.GetInt32("id"),
                        name = dr.GetString("name"),
                        low_discount_level = dr.GetInt32("low_discount_level"),
                        high_discount_level = dr.GetInt32("high_discount_level")
                    });
                }
            }

            return categoryList;
        }
    }
}
