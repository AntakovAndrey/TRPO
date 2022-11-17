using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TRPO
{
    public class SignUp
    {
        private SqlCommand command = new SqlCommand();
        public static bool SetTelephone(string telephone)
        {
            if(!String.IsNullOrEmpty(telephone)&&telephone!=""&&telephone!=" "&& Regex.IsMatch(telephone, @"([25]?[33]?[29]?\d{2}-\d{2}-\d{3})+", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        public static bool SetSeries(string series)
        {
            if (!String.IsNullOrEmpty(series) && series != "" && series != " " && Regex.IsMatch(series, @"([MC]?[MP])+", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        public static bool SetNumber(string number)
        {
            if (!String.IsNullOrEmpty(number) && number != "" && number != " " &&Regex.IsMatch(number, @"(\d{7})+", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        public static bool SetName(string name)
        {
            if (!String.IsNullOrEmpty(name) && name != "" && name != " " && Regex.IsMatch(name, @"([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1-23})+", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        public static bool SetSurname(string surname)
        {
            if (!String.IsNullOrEmpty(surname) && surname != "" && surname != " " && Regex.IsMatch(surname, @"([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1-23})+", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        public static bool SetPassword(string password)
        {
            if (!String.IsNullOrEmpty(password) && password != "" && password != " " && Regex.IsMatch(password, @"((?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,})+", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        public static bool SetNationality(string nationality)
        {
            if (!String.IsNullOrEmpty(nationality) && nationality != "" && nationality != " " && Regex.IsMatch(nationality, @"([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1-23})+", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
    }

}
