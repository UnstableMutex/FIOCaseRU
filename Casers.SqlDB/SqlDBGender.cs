using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIOCaseRU;

namespace Casers.SqlDB
{
    class SqlDBGender
    {

        public SqlDBGender(string conn)
        {

        }
        public Gender BySurname(string str)
        {
            return Gender.Undefined;
        }
        public Gender ByFirstName(string str) { return Gender.Undefined; }

        public Gender ByPatronymic(string str)
        {
            Gender res = 0;



            return res;
        }
        public Gender ByFIO(string surname, string firstName, string patronymic)
        {
            return ByPatronymic(patronymic);
        }

        public Gender ByFI(string surname, string firstName)
        {
            Gender res = 0;



            return res;
        }
    }
}
