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
        public Sex ByPatronymic(string str) { return Sex.Undefined; }
    }
}
