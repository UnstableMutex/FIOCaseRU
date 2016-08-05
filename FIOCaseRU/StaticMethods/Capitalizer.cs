using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIOCaseRU.StaticMethods
{
    class Capitalize
    {

        public virtual string Capitalizer(string str)
        {
            string result = string.Empty;
            char[] separators = { ' ', '-' };
            int startIndex = 0;
            int index = str.IndexOfAny(separators, startIndex);
            if (index == -1)
            {
                return CapitalizeAWord(str);
            }
            while (index > -1)
            {
                string word = CapitalizeAWord(str.Substring(startIndex, index - startIndex));
                char sep = str[index];
                result += word + sep;
                startIndex = index + 1;
                index = str.IndexOfAny(separators, startIndex);
            }
            result += CapitalizeAWord(str.Substring(startIndex));
            return result;
        }
        string CapitalizeAWord(string str)
        {
            if (str == string.Empty)
                return str;
            return str[0].ToString().ToUpper() + str.Substring(1).ToLower();
        }
    }
}
