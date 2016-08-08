using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIOCaseRU.StaticMethods
{
    public class DeterminyGender
    {
        public Gender BySurname(string str)
        {
            return Gender.Undefined;
        }
        public Gender ByFirstName(string str) { return Gender.Undefined; }

        public Gender ByPatronymic(string str)
        {

            var patronymic = str;
            Gender res;
            if (patronymic == string.Empty)
                return Gender.Undefined;
            string o2simend = patronymic.Substring(patronymic.Length - 2);
            switch (o2simend)
            {
                case "ич":
                    res = Gender.Male;
                    break;
                case "на":
                    res = Gender.Female;
                    break;
                case "лы":
                    res = Gender.Male;
                    break;
                case "ик":
                    res = Gender.Male;
                    break;
                case "зы":
                    res = Gender.Female;
                    break;
                default:
                    res = Gender.Undefined;
                    break;
            }
            return res;
        }

        public Gender ByFIO(string surname, string firstName, string patronymic)
        {
            return ByPatronymic(patronymic);
        }

        public Gender ByFI(string surname, string firstName)
        {
            
        }
    }
}
