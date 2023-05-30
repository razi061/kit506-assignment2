using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT502_Software_Solution.Utility
{
    internal class ValueConvert
    {
        public static int ToInt(string? value)
        {
            var result = 0;

            if(value == null || value.Length <= 0 || value.ToLower() == "null" || value == DBNull.Value.ToString())
            {
                
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(value);
                }
                catch (Exception) { }
            }

            return result;
        }

        public static string ToString(string? value)
        {
            var result = "";

            if (value == null || value.Length <= 0 || value.ToLower() == "null" || value == DBNull.Value.ToString())
            {

            }
            else
            {
                try
                {
                    result = Convert.ToString(value);
                }
                catch (Exception) { }
            }

            return result;
        }

        public static bool ToBoolean(string? value)
        {
            var result = false;

            if (value != null && value.Length > 0)
            {
                try
                {
                    result = Convert.ToBoolean(value);
                }
                catch (Exception) { }
            }

            return result;
        }
        public static double ToDouble(string? value)
        {
            var result = 0.0;

            if (value != null && value.Length > 0)
            {
                try
                {
                    result = Convert.ToDouble(value);
                }
                catch (Exception) { }
            }

            return result;
        }
    }
}
