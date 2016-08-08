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
                case "��":
                    oRP = pat + "�";
                    break;
                case "��":
                    oRP = pat.Substring(0, pat.Length - 1) + "�";
                    break;
                case "��":
                    oRP = pat;
                    break;
                case "��":
                    oRP = pat + "�";
                    break;
                case "��":
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
                case "��":
                    oTP = patronymic + "��";
                    break;
                case "��":
                    oTP = patronymic.Substring(0, patronymic.Length - 1) + "��";
                    break;
                case "��":
                    oTP = patronymic;
                    break;
                case "��":
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
                case "��":
                    oDP = patronymic + "�";
                    break;
                case "��":
                    oDP = patronymic.Substring(0, patronymic.Length - 1) + "�";
                    break;
                case "��":
                    oDP = patronymic;
                    break;
                case "��":
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
                case "��":
                    oPP = patronymic + "�";
                    break;
                case "��":
                    oPP = patronymic.Substring(0, patronymic.Length - 1) + "�";
                    break;
                case "��":
                    oPP = patronymic;
                    break;
                case "��":
                    oPP = patronymic;
                    break;
            }
            return oPP;
        }

      
    }
}