using System;
using System.Linq;

namespace FIOCaseRU.StaticMethods
{
  public  class FirstNameCaser:Caser
    {
        protected override string GetGenitive(string toCase, Gender gender)
        {
         
            string firstname = toCase;
            string i1simEnd = firstname.Last().ToString();
            string i2simend = firstname.Substring(firstname.Length - 2);
            string iRP = null;
            switch (i1simEnd)
            {
                case "а":
                    switch (i2simend)
                    {
                        case "ва":
                        case "на":
                        case "ба":
                        case "да":
                        case "за":
                        case "ла":
                        case "ма":
                        case "па":
                        case "ра":
                        case "са":
                        case "та":
                        case "фа":
                        case "ца":
                            iRP = firstname.Substring(0, firstname.Length - 1) + "ы";
                            break;
                        case "га":
                        case "жа":
                        case "ка":
                        case "ча":
                        case "ша":
                        case "ха":
                        case "ща":
                            iRP = firstname.Substring(0, firstname.Length - 1) + "и";
                            break;
                        case "еа":
                        case "аа":
                        case "ёа":
                        case "иа":
                        case "йа":
                        case "уа":
                        case "оа":
                        case "ьа":
                        case "ыа":
                        case "эа":
                        case "юа":
                        case "яа":
                            iRP = firstname;
                            break;
                    }
                    break;
                case "л":
                case "в":
                case "б":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "к":
                case "м":
                case "н":
                case "п":
                case "р":
                case "с":
                case "т":
                case "у":
                case "ф":
                case "х":
                case "ц":
                case "ч":
                case "ш":
                case "щ":
                    if (firstname.ToLower() == "павел")
                    {
                        iRP = "Павла";
                    }
                    else if (firstname.ToLower() == "лев")
                    {
                        iRP = "Льва";
                    }
                    else
                    {
                        if (gender == Gender.Female) iRP = firstname;
                        else iRP = firstname + "а";
                    }
                    break;
                case "е":
                case "ё":
                case "и":
                case "о":
                case "ы":
                case "э":
                case "ю":
                    iRP = firstname;
                    break;
                case "й":
                    if (gender == Gender.Female) iRP = firstname;
                    else iRP = firstname.Substring(0, firstname.Length - 1) + "я";
                    break;
                case "я":
                    iRP = firstname.Substring(0, firstname.Length - 1) + "и";
                    break;
                case "ь":
                    if (gender == Gender.Female) iRP = firstname.Substring(0, firstname.Length - 1) + "и";
                    else iRP = firstname.Substring(0, firstname.Length - 1) + "я";
                    break;
            }
            return iRP;
        
    }

        protected override string GetAblative(string toCase, Gender gender)
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
                case "а":
                    #region switch case
                    switch (i2simend)
                    {
                        case "ва":
                        case "на":
                        case "ба":
                        case "да":
                        case "за":
                        case "ла":
                        case "ма":
                        case "па":
                        case "ра":
                        case "са":
                        case "та":
                        case "фа":
                        case "ца":
                        case "га":
                        case "жа":
                        case "ка":
                        case "ча":
                        case "ща":
                        case "ша":
                        case "ха":
                            pTP = firstname.Substring(0, firstname.Length - 1) + "ой";
                            break;
                        case "еа":
                        case "ёа":
                        case "иа":
                        case "йа":
                        case "аа":
                        case "уа":
                        case "оа":
                        case "ьа":
                        case "ыа":
                        case "эа":
                        case "юа":
                        case "яа":
                            pTP = firstname;
                            break;
                    }
                    #endregion
                    break;
                case "л":
                case "б":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "к":
                case "м":
                case "н":
                case "п":
                case "с":
                case "т":
                case "у":
                case "ф":
                case "х":
                case "ц":
                case "ч":
                case "ш":
                case "щ":
                    if (firstname.ToLower() == "павел")
                    {
                        pTP = "Павлом";
                    }
                    else
                    {
                        if (gender == Gender.Female) pTP = firstname;
                        else pTP = firstname + "ом";
                    }
                    break;
                case "в":
                    if (firstname.ToLower() == "лев")
                    {
                        pTP = firstname + "Львом";
                    }
                    else
                    {
                        if (gender == Gender.Female) pTP = firstname;
                        else pTP = firstname + "ом";
                    }
                    break;
                case "р":
                    if (firstname.ToLower() == "пётр")
                    {
                        pTP = "Петром";
                    }
                    else
                    {
                        if (gender == Gender.Female) pTP = firstname;
                        else pTP = firstname + "ом";
                    }
                    break;
                case "е":
                case "ё":
                case "и":
                case "о":
                case "ы":
                case "э":
                case "ю":
                    pTP = firstname;
                    break;
                case "й":
                    if (gender == Gender.Female) pTP = firstname;
                    else pTP = firstname.Substring(0, firstname.Length - 1) + "ем";
                    break;
                case "я":
                    if (firstname.ToLower() == "илья")
                    {
                        pTP = "Ильёй";
                    }
                    else
                    {
                        pTP = firstname.Substring(0, firstname.Length - 1) + "ей";
                    }
                    break;
                case "ь":
                    if (gender == Gender.Female) pTP = firstname + "ю";
                    else pTP = firstname.Substring(0, firstname.Length - 1) + "ем";
                    break;
            }
            #endregion;
            return pTP;
        }

        protected override string GetDative(string toCase, Gender gender)
        {
            string firstname = toCase;
            string i1simEnd = firstname.Substring(firstname.Length - 1);
            string i2simEnd = firstname.Substring(firstname.Length - 2);
            string pDP = null;
            #region switch case
            switch (i1simEnd)
            {
                case "а":
                    #region switch case
                    switch (i2simEnd)
                    {
                        case "ва":
                        case "на":
                        case "ба":
                        case "да":
                        case "за":
                        case "ла":
                        case "ма":
                        case "па":
                        case "ра":
                        case "са":
                        case "та":
                        case "фа":
                        case "ца":
                        case "га":
                        case "жа":
                        case "ка":
                        case "ха":
                        case "ча":
                        case "ша":
                        case "ща":
                            pDP = firstname.Substring(0, firstname.Length - 1) + "е";
                            break;
                        case "еа":
                        case "ёа":
                        case "аа":
                        case "иа":
                        case "йа":
                        case "уа":
                        case "оа":
                        case "ьа":
                        case "ыа":
                        case "эа":
                        case "юа":
                        case "яа":
                            pDP = firstname;
                            break;
                    }
                    #endregion
                    break;
                case "л":
                case "б":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "к":
                case "м":
                case "н":
                case "п":
                case "с":
                case "т":
                case "у":
                case "ф":
                case "х":
                case "ц":
                case "ч":
                case "ш":
                case "щ":
                    if (firstname.ToLower() == "павел")
                    {
                        pDP = "Павлу";
                    }
                    else
                    {
                        if (gender == Gender.Female) pDP = firstname;
                        else pDP = firstname + "у";
                    }
                    break;
                case "в":
                    if (firstname.ToLower() == "лев")
                    {
                        pDP = "Льву";
                    }
                    else
                    {
                        if (gender == Gender.Female) pDP = firstname;
                        else pDP = firstname + "у";
                    }
                    break;
                case "р":
                    if (firstname.ToLower() == "пётр")
                    {
                        pDP = "Петру";
                    }
                    else
                    {
                        if (gender == Gender.Female) pDP = firstname;
                        else pDP = firstname + "у";
                    }
                    break;
                case "е":
                case "ё":
                case "и":
                case "о":
                case "ы":
                case "э":
                case "ю":
                    pDP = firstname;
                    break;
                case "й":
                    if (gender == Gender.Female) pDP = firstname;
                    else pDP = firstname.Substring(0, firstname.Length - 1) + "ю";
                    break;
                case "я":
                    if (i2simEnd == "ия")
                    {
                        string exceptNamesStr = " Фания Гузалия Асия Гульфия Дания Раушания Румия Рузалия Лия Сария Рушания Разия Залия Гулия Надия Фаузия Марзия Гузелия Назия Кадрия Нурания Ильсия Зилия Савия Амалия Ия Нажия Рания Фавзия Валия Алфия Закия Ралия Равия ";
                        if (exceptNamesStr.IndexOf(" " + firstname + " ") == -1)
                        {
                            pDP = firstname.Substring(0, firstname.Length - 1) + "и";
                        }
                        else
                        {
                            pDP = firstname;
                        }
                    }
                    else
                    {
                        pDP = firstname.Substring(0, firstname.Length - 1) + "е";
                    }
                    break;
                case "ь":
                    if (gender == Gender.Female) pDP = firstname.Substring(0, firstname.Length - 1) + "и";
                    else pDP = firstname.Substring(0, firstname.Length - 1) + "ю";
                    break;
            }
            #endregion
            return pDP;
        }

        protected override string GetPrepositional(string toCase, Gender gender)
        {
            string firstname = toCase;
            string i1simEnd = firstname.Substring(firstname.Length - 1);
            string i2simEnd = firstname.Substring(firstname.Length - 2);
            string pPP = null;
            #region switch case
            switch (i1simEnd)
            {
                case "а":
                    #region switch case
                    switch (i2simEnd)
                    {
                        case "ва":
                        case "на":
                        case "ба":
                        case "да":
                        case "за":
                        case "ла":
                        case "ма":
                        case "па":
                        case "ра":
                        case "са":
                        case "та":
                        case "фа":
                        case "ца":
                        case "га":
                        case "жа":
                        case "ка":
                        case "ха":
                        case "ча":
                        case "ша":
                        case "ща":
                            pPP = firstname.Substring(0, firstname.Length - 1) + "е";
                            break;
                        case "еа":
                        case "ёа":
                        case "иа":
                        case "йа":
                        case "уа":
                        case "аа":
                        case "оа":
                        case "ьа":
                        case "ыа":
                        case "эа":
                        case "юа":
                        case "яа":
                            pPP = firstname;
                            break;
                    }
                    #endregion
                    break;
                case "л":
                case "б":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "к":
                case "м":
                case "н":
                case "п":
                case "с":
                case "т":
                case "у":
                case "ф":
                case "х":
                case "ц":
                case "ч":
                case "ш":
                case "щ":
                    if (gender == Gender.Female) pPP = firstname;
                    else pPP = firstname + "е";
                    break;
                case "в":
                    if (firstname.ToLower() == "лев")
                    {
                        pPP = "Льве";
                    }
                    else
                    {
                        if (gender == Gender.Female) pPP = firstname;
                        else pPP = firstname + "е";
                    }
                    break;
                case "р":
                    if (firstname.ToLower() == "пётр")
                    {
                        pPP = "Петре";
                    }
                    else if (firstname.ToLower() == "павел")
                    {
                        pPP = "Павле";
                    }
                    else
                    {
                        if (gender == Gender.Female) pPP = firstname;
                        else pPP = firstname + "е";
                    }
                    break;
                case "е":
                case "ё":
                case "и":
                case "о":
                case "ы":
                case "э":
                case "ю":
                    pPP = firstname;
                    break;
                case "й":
                    if (gender == Gender.Female) pPP = firstname;
                    else if (i2simEnd == "ий")
                    {
                        pPP = firstname.Substring(0, firstname.Length - 1) + "и";
                    }
                    else pPP = firstname.Substring(0, firstname.Length - 1) + "е";
                    break;
                case "я":
                    if (i2simEnd == "ия")
                    {
                        string exceptNamesStr = " Фания Гузалия Асия Гульфия Дания Раушания Румия Рузалия Лия Сария Рушания Разия Залия Гулия Надия Фаузия Марзия Гузелия Назия Кадрия Нурания Ильсия Зилия Савия Амалия Ия Нажия Рания Фавзия Валия Алфия Закия Ралия Равия ";
                        if (exceptNamesStr.IndexOf(" " + firstname + " ") == -1)
                        {
                            pPP = firstname.Substring(0, firstname.Length - 1) + "и";
                        }
                        else
                        {
                            pPP = firstname;
                        }
                    }
                    else
                    {
                        pPP = firstname.Substring(0, firstname.Length - 1) + "е";
                    }
                    break;
                case "ь":
                    if (gender == Gender.Female) pPP = firstname.Substring(0, firstname.Length - 1) + "и";
                    else pPP = firstname.Substring(0, firstname.Length - 1) + "е";
                    break;
            }
            #endregion
            return pPP;
        }

       
    }
}