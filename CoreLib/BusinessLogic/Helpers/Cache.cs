using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErkerCore.BusinessLogic.Helpers
{
    public static class Cache
    {
        public static List<string> countries = new List<string>();
        static DateTime lastCachedDateTime = DateTime.Now;

        static public List<string> Countries
        {

            get
            {
                if ((DateTime.Now - lastCachedDateTime).TotalMinutes > 10)
                {
                    var dbdengelen=  new List<string>();
                    //dbden çek refresh et
                    countries = dbdengelen;
                    lastCachedDateTime = DateTime.Now;
                }
                return countries;
              


            }



        }
       public static string GetCountry(long id)
        {

            return Countries.FirstOrDefault(o => o == id.ToString());
        
        }
    }
}
