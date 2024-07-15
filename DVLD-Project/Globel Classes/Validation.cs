using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DVLD_Project
{
    public class Validation
    {
        public static bool IsValidName(string Name)
        {
            return Regex.IsMatch(Name, @"\b[A-Za-z]+-?[A-Za-z]+\b");
        }
        public static bool IsValidEmail(string Email)
        {
            return Regex.IsMatch(Email, @"\b[A-Za-z-_/.]+[0-9]*@gmail.com\b");
        }
        public static bool IsValidNumber(string Number)
        {
            return Regex.IsMatch(Number, @"\b[0-9]+\b");
        }
    }
}
