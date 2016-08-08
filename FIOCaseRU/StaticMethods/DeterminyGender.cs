using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIOCaseRU.StaticMethods
{
    public class DeterminyGender
    {
        public Sex BySurname(string str)
        {
            return Sex.Undefined;
        }
        public Sex ByFirstName(string str) { return Sex.Undefined; }

        public Sex ByPatronymic(string str)
        {

            var patronymic = str;
            Sex res;
            if (patronymic == string.Empty)
                return Sex.Undefined;
            string o2simend = patronymic.Substring(patronymic.Length - 2);
            switch (o2simend)
            {
                case "ич":
                    res = Sex.Male;
                    break;
                case "на":
                    res = Sex.Female;
                    break;
                case "лы":
                    res = Sex.Male;
                    break;
                case "ик":
                    res = Sex.Male;
                    break;
                case "зы":
                    res = Sex.Female;
                    break;
                default:
                    res = Sex.Undefined;
                    break;
            }
            return res;
        }

        public Sex ByFIO(string surname, string firstName, string patronymic)
        {
            return ByPatronymic(patronymic);
        }

        public Sex ByFI(string surname, string firstName)
        {
            
        }
    }
}
