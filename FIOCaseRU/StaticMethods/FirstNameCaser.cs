using System;
using System.Linq;

namespace FIOCaseRU.StaticMethods
{
    class FirstNameCaser:Caser
    {
        protected override string GetGenitive(string toCase, Sex gender)
        {
         
            string firstname = toCase;
            string i1simEnd = firstname.Last().ToString();
            string i2simend = firstname.Substring(firstname.Length - 2);
            string iRP = null;
            switch (i1simEnd)
            {
                case "�":
                    switch (i2simend)
                    {
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            iRP = firstname.Substring(0, firstname.Length - 1) + "�";
                            break;
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            iRP = firstname.Substring(0, firstname.Length - 1) + "�";
                            break;
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            iRP = firstname;
                            break;
                    }
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    if (firstname.ToLower() == "�����")
                    {
                        iRP = "�����";
                    }
                    else if (firstname.ToLower() == "���")
                    {
                        iRP = "����";
                    }
                    else
                    {
                        if (gender == Sex.Female) iRP = firstname;
                        else iRP = firstname + "�";
                    }
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    iRP = firstname;
                    break;
                case "�":
                    if (gender == Sex.Female) iRP = firstname;
                    else iRP = firstname.Substring(0, firstname.Length - 1) + "�";
                    break;
                case "�":
                    iRP = firstname.Substring(0, firstname.Length - 1) + "�";
                    break;
                case "�":
                    if (gender == Sex.Female) iRP = firstname.Substring(0, firstname.Length - 1) + "�";
                    else iRP = firstname.Substring(0, firstname.Length - 1) + "�";
                    break;
            }
            return iRP;
        
    }

        protected override string GetAblative(string toCase, Sex gender)
        {
            string firstname = toCase;
            string i1simEnd = null;
            string i2simend = null;
            string pTP = null;
            i1simEnd = firstname.Substring(firstname.Length - 1);
            i2simend = firstname.Substring(firstname.Length - 2);
            #region switch case
            switch (i1simEnd)
            {
                case "�":
                    #region switch case
                    switch (i2simend)
                    {
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            pTP = firstname.Substring(0, firstname.Length - 1) + "��";
                            break;
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            pTP = firstname;
                            break;
                    }
                    #endregion
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    if (firstname.ToLower() == "�����")
                    {
                        pTP = "������";
                    }
                    else
                    {
                        if (gender == Sex.Female) pTP = firstname;
                        else pTP = firstname + "��";
                    }
                    break;
                case "�":
                    if (firstname.ToLower() == "���")
                    {
                        pTP = firstname + "�����";
                    }
                    else
                    {
                        if (gender == Sex.Female) pTP = firstname;
                        else pTP = firstname + "��";
                    }
                    break;
                case "�":
                    if (firstname.ToLower() == "���")
                    {
                        pTP = "������";
                    }
                    else
                    {
                        if (gender == Sex.Female) pTP = firstname;
                        else pTP = firstname + "��";
                    }
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    pTP = firstname;
                    break;
                case "�":
                    if (gender == Sex.Female) pTP = firstname;
                    else pTP = firstname.Substring(0, firstname.Length - 1) + "��";
                    break;
                case "�":
                    if (firstname.ToLower() == "����")
                    {
                        pTP = "�����";
                    }
                    else
                    {
                        pTP = firstname.Substring(0, firstname.Length - 1) + "��";
                    }
                    break;
                case "�":
                    if (gender == Sex.Female) pTP = firstname + "�";
                    else pTP = firstname.Substring(0, firstname.Length - 1) + "��";
                    break;
            }
            #endregion;
            return pTP;
        }

        protected override string GetDative(string toCase, Sex gender)
        {
            string firstname = toCase;
            string i1simEnd = firstname.Substring(firstname.Length - 1);
            string i2simEnd = firstname.Substring(firstname.Length - 2);
            string pDP = null;
            #region switch case
            switch (i1simEnd)
            {
                case "�":
                    #region switch case
                    switch (i2simEnd)
                    {
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            pDP = firstname.Substring(0, firstname.Length - 1) + "�";
                            break;
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            pDP = firstname;
                            break;
                    }
                    #endregion
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    if (firstname.ToLower() == "�����")
                    {
                        pDP = "�����";
                    }
                    else
                    {
                        if (gender == Sex.Female) pDP = firstname;
                        else pDP = firstname + "�";
                    }
                    break;
                case "�":
                    if (firstname.ToLower() == "���")
                    {
                        pDP = "����";
                    }
                    else
                    {
                        if (gender == Sex.Female) pDP = firstname;
                        else pDP = firstname + "�";
                    }
                    break;
                case "�":
                    if (firstname.ToLower() == "���")
                    {
                        pDP = "�����";
                    }
                    else
                    {
                        if (gender == Sex.Female) pDP = firstname;
                        else pDP = firstname + "�";
                    }
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    pDP = firstname;
                    break;
                case "�":
                    if (gender == Sex.Female) pDP = firstname;
                    else pDP = firstname.Substring(0, firstname.Length - 1) + "�";
                    break;
                case "�":
                    if (i2simEnd == "��")
                    {
                        string exceptNamesStr = " ����� ������� ���� ������� ����� �������� ����� ������� ��� ����� ������� ����� ����� ����� ����� ������ ������ ������� ����� ������ ������� ������ ����� ����� ������ �� ����� ����� ������ ����� ����� ����� ����� ����� ";
                        if (exceptNamesStr.IndexOf(" " + firstname + " ") == -1)
                        {
                            pDP = firstname.Substring(0, firstname.Length - 1) + "�";
                        }
                        else
                        {
                            pDP = firstname;
                        }
                    }
                    else
                    {
                        pDP = firstname.Substring(0, firstname.Length - 1) + "�";
                    }
                    break;
                case "�":
                    if (gender == Sex.Female) pDP = firstname.Substring(0, firstname.Length - 1) + "�";
                    else pDP = firstname.Substring(0, firstname.Length - 1) + "�";
                    break;
            }
            #endregion
            return pDP;
        }

        protected override string GetPrepositional(string toCase, Sex gender)
        {
            string firstname = toCase;
            string i1simEnd = firstname.Substring(firstname.Length - 1);
            string i2simEnd = firstname.Substring(firstname.Length - 2);
            string pPP = null;
            #region switch case
            switch (i1simEnd)
            {
                case "�":
                    #region switch case
                    switch (i2simEnd)
                    {
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            pPP = firstname.Substring(0, firstname.Length - 1) + "�";
                            break;
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                        case "��":
                            pPP = firstname;
                            break;
                    }
                    #endregion
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    if (gender == Sex.Female) pPP = firstname;
                    else pPP = firstname + "�";
                    break;
                case "�":
                    if (firstname.ToLower() == "���")
                    {
                        pPP = "����";
                    }
                    else
                    {
                        if (gender == Sex.Female) pPP = firstname;
                        else pPP = firstname + "�";
                    }
                    break;
                case "�":
                    if (firstname.ToLower() == "���")
                    {
                        pPP = "�����";
                    }
                    else if (firstname.ToLower() == "�����")
                    {
                        pPP = "�����";
                    }
                    else
                    {
                        if (gender == Sex.Female) pPP = firstname;
                        else pPP = firstname + "�";
                    }
                    break;
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                case "�":
                    pPP = firstname;
                    break;
                case "�":
                    if (gender == Sex.Female) pPP = firstname;
                    else if (i2simEnd == "��")
                    {
                        pPP = firstname.Substring(0, firstname.Length - 1) + "�";
                    }
                    else pPP = firstname.Substring(0, firstname.Length - 1) + "�";
                    break;
                case "�":
                    if (i2simEnd == "��")
                    {
                        string exceptNamesStr = " ����� ������� ���� ������� ����� �������� ����� ������� ��� ����� ������� ����� ����� ����� ����� ������ ������ ������� ����� ������ ������� ������ ����� ����� ������ �� ����� ����� ������ ����� ����� ����� ����� ����� ";
                        if (exceptNamesStr.IndexOf(" " + firstname + " ") == -1)
                        {
                            pPP = firstname.Substring(0, firstname.Length - 1) + "�";
                        }
                        else
                        {
                            pPP = firstname;
                        }
                    }
                    else
                    {
                        pPP = firstname.Substring(0, firstname.Length - 1) + "�";
                    }
                    break;
                case "�":
                    if (gender == Sex.Female) pPP = firstname.Substring(0, firstname.Length - 1) + "�";
                    else pPP = firstname.Substring(0, firstname.Length - 1) + "�";
                    break;
            }
            #endregion
            return pPP;
        }

       
    }
}