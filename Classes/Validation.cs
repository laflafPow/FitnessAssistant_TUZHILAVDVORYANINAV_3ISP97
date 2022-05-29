using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessAssistant_TUZHILAVDVORYANINAV_3ISP97.Classes
{
    public class Validation
    {
        public static bool ValidationPassword(String password)
        {
            if (password.Length < 8 || password.Length > 20)
                return false;
            if (!password.Any(Char.IsUpper))
                return false;
            if (!password.Any(Char.IsLower))
                return false;
            if (!password.Any(Char.IsDigit))
                return false;
            if (!password.Any(Char.IsPunctuation))
                return false;
            if (password.Any(Char.IsWhiteSpace))
                return false;

            return true;
        }

        public static bool ValidationName(String Name)
        {
            if (Name.Length < 2 || Name.Length > 50)
                return false;
            if (!Name.Any(Char.IsLower))
                return false;
            if (!Name.Any(Char.IsUpper))
                return false;
            if (Name.Any(Char.IsDigit))
                return false;
            if (!Name.Contains("-") && Name.Contains(" ") && Name.Any(Char.IsWhiteSpace))
                return false;
            if (Name.Contains("  ") || Name.Contains("--"))
                return false;

            return true;
        }

        public static bool ValidationWeightAndHeight(String WeightAndHeight)
        {
            try 
            {
                double a = Convert.ToDouble(WeightAndHeight);
            }
            catch (FormatException)
            {
                return false;
            }
            if (Convert.ToDouble(WeightAndHeight) > 300 || Convert.ToDouble(WeightAndHeight) <= 10)
                return false;

            return true;
            
        }

        public static bool ValidationLogin(String Login)
        {
            if (Login.Length < 2 || Login.Length > 50)
                return false;
            if (Login.Any(Char.IsUpper))
                return false;
            if (Login.Any(Char.IsPunctuation))
                return false;
            if (Login.Any(Char.IsWhiteSpace))
                return false;

            var authUser = ClassHelper.AppData.Context.User.ToList().
            Where(i => i.Login == Login).FirstOrDefault();

            if (authUser == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidationDateBirthday(DateTime DateBirthday)
        {
            if (DateBirthday > DateTime.Now) return false;
            return true;
        }

        public static double CalculatorBMI(double height, double weight)
        {
            height = height / 100;
            double bmi = weight / Math.Pow(height, 2);
            bmi = (Math.Round(bmi, 2));
            return bmi;
        }

        public static double CalculatorBMR(int gender, int age, double height, double weight)
        {
            double bmr;
            if(gender == 1)
            {
                bmr = 88.362 + 13.397 * weight + 4.799 * height - 5.677 * age;
                bmr = (Math.Round(bmr, 2));
                return bmr;
            }
            else
            {
                bmr = 447.593 + 9.247 * weight + 3.098 * height - 4.33 * age;
                bmr = (Math.Round(bmr, 2));
                return bmr;
            }
        }
    }
}

