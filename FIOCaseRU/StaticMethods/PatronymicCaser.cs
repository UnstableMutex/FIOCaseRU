using System;

namespace FIOCaseRU.StaticMethods
{
   public class PatronymicCaser:Caser
    {
        protected override string GetGenitive(string toCase, Gender gender)
        {
            string oRP = String.Empty;
            string pat = toCase;
            if (string.IsNullOrEmpty(pat))
            {
                return string.Empty;
            }
            string o2simend = pat.Substring(pat.Length - 2);
            switch (o2simend)
            {
                case "ич":
                    oRP = pat + "а";
                    break;
                case "на":
                    oRP = pat.Substring(0, pat.Length - 1) + "ы";
                    break;
                case "лы":
                    oRP = pat;
                    break;
                case "ик":
                    oRP = pat + "а";
                    break;
                case "зы":
                    oRP = pat;
                    break;
            }
            return oRP;
        }

        protected override string GetAblative(string toCase, Gender gender)
        {
            string patronymic = toCase;
            if (string.IsNullOrEmpty(patronymic))
            {
                return string.Empty;
            }
            string oTP = String.Empty;
            string o2simend = patronymic.Substring(patronymic.Length - 2);
            switch (o2simend)
            {
                case "ич":
                    oTP = patronymic + "ем";
                    break;
                case "на":
                    oTP = patronymic.Substring(0, patronymic.Length - 1) + "ой";
                    break;
                case "лы":
                    oTP = patronymic;
                    break;
                case "зы":
                    oTP = patronymic;
                    break;
            }
            return oTP;
        }

        protected override string GetDative(string toCase, Gender gender)
        {
            var patronymic = toCase;
            string oDP = String.Empty;
            string o2simend = patronymic.Substring(patronymic.Length - 2);
            switch (o2simend)
            {
                case "ич":
                    oDP = patronymic + "у";
                    break;
                case "на":
                    oDP = patronymic.Substring(0, patronymic.Length - 1) + "е";
                    break;
                case "лы":
                    oDP = patronymic;
                    break;
                case "зы":
                    oDP = patronymic;
                    break;
            }
            return oDP;
        }

        protected override string GetPrepositional(string toCase, Gender gender)
        {
            string patronymic = toCase;
            string oPP = String.Empty;
            string o2simend = patronymic.Substring(patronymic.Length - 2);
            switch (o2simend)
            {
                case "ич":
                    oPP = patronymic + "е";
                    break;
                case "на":
                    oPP = patronymic.Substring(0, patronymic.Length - 1) + "е";
                    break;
                case "лы":
                    oPP = patronymic;
                    break;
                case "зы":
                    oPP = patronymic;
                    break;
            }
            return oPP;
        }

      
    }
}