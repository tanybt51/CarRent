using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities.ExtentionMethods
{
    public static class ExMethod
    {
        public static string ToStringArray(this List<Role> roles)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            try
            {
                var last = roles.Last();

                foreach (var role in roles)
                {
                    if (role != last)
                        sb.Append(role.Name.Trim() + ",");
                    else
                        sb.Append(role.Name);

                }
            }
            catch (Exception)
            {

                
            }
            return sb.ToString();

        }

        public static DateTime DateParse(this string _date)
        {
            string[] tempdate = _date.Split(',')[0].Split('/');
            return new DateTime(int.Parse(tempdate[2]), int.Parse(tempdate[0]), int.Parse(tempdate[1]));
            
            

        }
    }
}
