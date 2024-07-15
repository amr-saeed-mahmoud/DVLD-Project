using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAcess;

namespace DVLD_Business
{
    public class CountryInfo
    {
        public string CountryName { get; set; }
        public int CountryID { get; set; }

        public static string GetCountryName(int CountryID)
        {
            string CountryName = "";
            CountryData.GetCountryName(CountryID,ref CountryName);
            return CountryName;
        }

        public static int GetCountryID(string CountryName)
        {
            int CountryID = -1;
            CountryData.GetCountryID(CountryName,ref CountryID);
            return CountryID;
        }
        public static DataTable GetAllCountries()
        {
            return CountryData.GetAllCountries();
        }
    }
}
