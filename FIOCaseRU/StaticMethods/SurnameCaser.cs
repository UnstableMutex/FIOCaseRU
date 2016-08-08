using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIOCaseRU.StaticMethods
{
  public  class SurnameCaser : Caser
    {


        protected override string GetGenitive(string surname, Gender gender)
        {

            string fRP = String.Empty;
            string F1simend = surname.Substring(surname.Length - 1);
            string F2simend = surname.Substring(surname.Length - 2);

            #region switch case

            switch (F1simend)
            {
                case "а":

                    #region switch case

                    switch (F2simend)
                    {
                        case "ва":
                        case "на":
                            fRP = surname.Substring(0, surname.Length - 1) + "ой";
                            break;
                        case "ба":
                        case "за":
                        case "ла":
                        case "ма":
                        case "па":
                        case "ра":
                        case "са":
                        case "та":
                        case "фа":
                        case "ца":
                            fRP = surname.Substring(0, surname.Length - 1) + "ы";
                            break;
                        case "га":
                        case "да":
                        case "жа":
                        case "ка":
                        case "ча":
                        case "ша":
                        case "ща":
                        case "ха":
                            fRP = surname.Substring(0, surname.Length - 1) + "и";
                            break;
                        case "еа":
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
                            fRP = surname;
                            break;
                    }

                    #endregion

                    break;
                case "б":
                case "в":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "л":
                case "м":
                case "н":
                case "п":
                case "р":
                case "с":
                case "т":
                case "ф":
                case "ч":
                case "ш":
                case "щ":
                    if (gender == Gender.Female) fRP = surname;
                    else fRP = surname + "а";
                    break;
                case "к":
                    switch (F2simend)
                    {
                        case "ук":
                            fRP = surname;
                            break;
                        default:
                            if (gender == Gender.Female) fRP = surname;
                            else fRP = surname + "а";
                            break;
                    }
                    break;
                case "ц":
                    switch (F2simend)
                    {
                        case "ец":
                            fRP = surname;
                            break;
                        default:
                            if (gender == Gender.Female) fRP = surname;
                            else fRP = surname + "а";
                            break;
                    }
                    break;
                case "е":
                case "ё":
                case "и":
                case "о":
                case "у":
                case "ы":
                case "э":
                case "ю":
                case "х":
                    fRP = surname;
                    break;
                case "ь":
                    if (gender == Gender.Female) fRP = surname;
                    else fRP = surname.Substring(0, surname.Length - 1) + "я";
                    break;
                case "я":
                    if (F2simend == "ая") fRP = surname.Substring(0, surname.Length - 2) + "ой";
                    else fRP = surname.Substring(0, surname.Length - 1) + "и"; //данелия мужик
                    break;
                case "й":
                    if (gender == Gender.Female)
                    {
                        fRP = surname;
                    }
                    else
                    {
                        //на ый - Белый
                        if (F2simend == "ий" | F2simend == "ой" | F2simend == "ый")
                        {
                            fRP = surname.Substring(0, surname.Length - 2) + "ого";
                        }
                        else
                        {
                            fRP = surname.Substring(0, surname.Length - 1) + "я";
                        }
                    }
                    break;
                default:
                    throw new FIOException();
            }

            #endregion

            return fRP;
        }

        protected override string GetAblative(string toCase, Gender gender)
        {

            string surname = toCase;
            string pTP = String.Empty;
            string F1simend = surname.Substring(surname.Length - 1);
            string F2simend = surname.Substring(surname.Length - 2);
            #region switch case
            switch (F1simend)
            {
                case "а":
                    #region switch case
                    switch (F2simend)
                    {
                        case "ща":
                            pTP = surname.Substring(0, surname.Length - 1) + "ей";
                            break;
                        case "ва":
                        case "на":
                        case "ба":
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
                        case "да":
                        case "жа":
                        case "ка":
                        case "ша":
                        case "ха":
                        case "ча":
                            pTP = surname.Substring(0, surname.Length - 1) + "ой";
                            break;
                        case "еа":
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
                            pTP = surname;
                            break;
                    }
                    #endregion
                    break;
                case "в":
                    if (gender == Gender.Female) pTP = surname;
                    else pTP = surname + "ым";
                    break;
                case "н":
                    if (gender == Gender.Female) pTP = surname;
                    else
                    {
                        if (F2simend == "ин") pTP = surname + "ым";
                        else pTP = surname + "ом";
                    }
                    break;
                case "ц":
                    pTP = surname;
                    break;
                case "ч":
                    if (gender == Gender.Female) pTP = surname;
                    else
                    {
                        if (F2simend == "ич") pTP = surname + "ем";
                        else pTP = surname + "ом";
                    }
                    break;
                case "б":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "м":
                case "п":
                case "р":
                case "с":
                case "т":
                case "ф":
                case "ш":
                case "щ":
                    if (gender == Gender.Female) pTP = surname;
                    else pTP = surname + "ом";
                    break;
                case "л":
                    if (gender == Gender.Female) pTP = surname;
                    else
                    {
                        if (surname.ToLower() == "орел")
                        {
                            pTP = "Орлом";
                        }
                        else
                        {
                            pTP = surname + "ом";
                        }
                    }
                    break;
                case "к":
                    switch (F2simend)
                    {
                        case "ук":
                            pTP = surname;
                            break;
                        default:
                            if (gender == Gender.Female) pTP = surname;
                            else pTP = surname + "ом";
                            break;
                    }
                    break;
                case "е":
                case "ё":
                case "и":
                case "о":
                case "у":
                case "ы":
                case "э":
                case "ю":
                case "х":
                    pTP = surname;
                    break;
                case "ь":
                    if (gender == Gender.Female) pTP = surname;
                    else pTP = surname.Substring(0, surname.Length - 1) + "ем";
                    break;
                case "я":
                    if (F2simend == "ая")
                    {
                        if (surname.ToLower() == "осадчая")
                        {
                            pTP = "Осадчей";
                        }
                        else
                        {
                            pTP = surname.Substring(0, surname.Length - 2) + "ой";
                        }
                    }
                    else
                    {
                        if (gender == Gender.Female) pTP = surname;
                        else pTP = surname.Substring(0, surname.Length - 1) + "ей";
                    }
                    break;
                case "й":
                    if (gender == Gender.Female)
                    {
                        pTP = surname;
                    }
                    else
                    {
                        if ((F2simend == "ой") || (F2simend == "ый"))
                        {
                            if (surname.ToLower() == "цой") pTP = "Цоем";
                            else if (surname.ToLower() == "донской") pTP = "Донским";
                            else pTP = surname.Substring(0, surname.Length - 2) + "ым";
                        }
                        else if ((F2simend == "ей") || (F2simend == "ай"))
                        {
                            if (surname.ToLower() == "соловей") pTP = "Соловьём";
                            else pTP = surname.Substring(0, surname.Length - 1) + "ем";
                        }
                        else if (F2simend == "ий")
                        {
                            if (surname.ToLower() == "палий") pTP = "Палием";
                            else pTP = surname.Substring(0, surname.Length - 1) + "м";
                        }
                        else pTP = surname.Substring(0, surname.Length - 1) + "ем";
                    }
                    break;
            }
            #endregion
            return pTP;

        }

        protected override string GetDative(string toCase, Gender gender)
        {

            string[] sdpoj = new[] { "ва", "на" };

            string[] sdpe = new[] { "ба", "за", "ла", "ма", "па", "ра", "са", "та", "фа", "ца", "га", "да", "жа", "ка", "ша", "ща", "ха", "ча" };
            string[] sdpnoend = new[] { "еа", "ёа", "иа", "йа", "уа", "оа", "ьа", "ыа", "эа", "юа", "яа" };

            var surname = toCase;

            if (surname == "Цой")
            {
                return surname;
            }

            string fDP = String.Empty;
            string F1simend = surname.Substring(surname.Length - 1);
            string F2simend = surname.Substring(surname.Length - 2);
            #region switch case
            switch (F1simend)
            {
                case "а":
                    if (sdpoj.Contains(F2simend))
                    { fDP = surname.Substring(0, surname.Length - 1) + "ой"; }
                    else if (sdpe.Contains(F2simend))
                    { fDP = surname.Substring(0, surname.Length - 1) + "е"; }
                    else if (sdpnoend.Contains(F2simend))
                    { fDP = surname; }
                    #region switch case
                    switch (F2simend)
                    {
                        case "ва":
                        case "на":
                            fDP = surname.Substring(0, surname.Length - 1) + "ой";
                            break;
                        case "ба":
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
                        case "да":
                        case "жа":
                        case "ка":
                        case "ша":
                        case "ща":
                        case "ха":
                        case "ча":
                            fDP = surname.Substring(0, surname.Length - 1) + "е";
                            break;
                        case "еа":
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
                            fDP = surname;
                            break;
                    }
                    #endregion
                    break;
                case "б":
                case "в":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "л":
                case "м":
                case "н":
                case "п":
                case "р":
                case "с":
                case "т":
                case "ф":
                case "ш":
                case "щ":
                case "ч":
                    if (gender == Gender.Female) fDP = surname;
                    else fDP = surname + "у";
                    break;
                case "ц":
                    switch (F2simend)
                    {
                        case "ец":
                            fDP = surname;
                            break;
                        default:
                            if (gender == Gender.Female) fDP = surname;
                            else fDP = surname + "у";
                            break;
                    }
                    break;
                case "к":
                    switch (F2simend)
                    {
                        case "ук":
                            fDP = surname;
                            break;
                        default:
                            if (gender == Gender.Female) fDP = surname;
                            else fDP = surname + "у";
                            break;
                    }
                    break;
                case "х":
                case "е":
                case "ё":
                case "и":
                case "о":
                case "у":
                case "ы":
                case "э":
                case "ю":
                    fDP = surname;
                    break;
                case "ь":
                    if (gender == Gender.Female) fDP = surname;
                    else fDP = surname.Substring(0, surname.Length - 1) + "ю";
                    break;
                case "я":
                    if (F2simend == "ая") fDP = surname.Substring(0, surname.Length - 2) + "ой";
                    else fDP = surname.Substring(0, surname.Length - 1) + "е";
                    break;
                case "й":
                    if (gender == Gender.Female)
                    {
                        fDP = surname;
                    }
                    else
                    {
                        if (F2simend == "ий" |
                            F2simend == "ой" |
                            F2simend == "ый")
                        {
                            fDP = surname.Substring(0, surname.Length - 2) + "ому";
                        }
                        else
                        {
                            fDP = surname.Substring(0, surname.Length - 1) + "ю";
                        }
                    }
                    break;
                default:
                    throw new FIOException();
            }
            #endregion
            return fDP;

        }

        protected override string GetPrepositional(string toCase, Gender gender)
        {
            string surname = toCase;
            string fPP = String.Empty;
            string F1simend = surname.Substring(surname.Length - 1);
            string F2simend = surname.Substring(surname.Length - 2);
            #region switch case
            switch (F1simend)
            {
                case "а":
                    #region switch case
                    switch (F2simend)
                    {
                        case "ва":
                        case "на":
                            fPP = surname.Substring(0, surname.Length - 1) + "ой";
                            break;
                        case "ба":
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
                        case "да":
                        case "жа":
                        case "ка":
                        case "ша":
                        case "ща":
                        case "ха":
                        case "ча":
                            fPP = surname.Substring(0, surname.Length - 1) + "е";
                            break;
                        case "еа":
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
                            fPP = surname;
                            break;
                    }
                    #endregion
                    break;
                case "б":
                case "в":
                case "г":
                case "д":
                case "ж":
                case "з":
                case "л":
                case "м":
                case "н":
                case "п":
                case "р":
                case "с":
                case "т":
                case "ф":
                case "ш":
                case "щ":
                case "ч":
                    if (gender == Gender.Female) fPP = surname;
                    else fPP = surname + "е";
                    break;
                case "к":
                    switch (F2simend)
                    {
                        case "ук":
                            fPP = surname;
                            break;
                        default:
                            if (gender == Gender.Female) fPP = surname;
                            else fPP = surname + "е";
                            break;
                    }
                    break;
                case "ц":
                    switch (F2simend)
                    {
                        case "ец":
                            fPP = surname;
                            break;
                        default:
                            if (gender == Gender.Female) fPP = surname;
                            else fPP = surname + "е";
                            break;
                    }
                    break;
                case "х":
                case "е":
                case "ё":
                case "и":
                case "о":
                case "у":
                case "ы":
                case "э":
                case "ю":
                    fPP = surname;
                    break;
                case "ь":
                    if (gender == Gender.Female) fPP = surname;
                    else
                    {
                        if (surname.ToLower() == "пивень") fPP = "Пивне";
                        else if (surname.ToLower() == "журавель") fPP = "Журавле";
                        else fPP = surname.Substring(0, surname.Length - 1) + "е";
                    }
                    break;
                case "я":
                    if (F2simend == "ая")
                    {
                        if (surname.ToLower() == "осадчая") fPP = "Осадчей";
                        else if (surname.ToLower() == "саая") fPP = "Саае";
                        else fPP = surname.Substring(0, surname.Length - 2) + "ой";
                    }
                    else if (F2simend == "яя")
                    {
                        fPP = surname.Substring(0, surname.Length - 2) + "ей";
                    }
                    else fPP = surname.Substring(0, surname.Length - 1) + "е";
                    break;
                case "й":
                    if (gender == Gender.Female)
                    {
                        fPP = surname;
                    }
                    else
                    {
                        if ((F2simend == "ий") || (F2simend == "ой") || (F2simend == "ый"))
                        {
                            if (surname.ToLower() == "осадчий") fPP = "Осадчем";
                            else fPP = surname.Substring(0, surname.Length - 2) + "ом";
                        }
                        else
                        {
                            fPP = surname.Substring(0, surname.Length - 1) + "е";
                        }
                    }
                    break;
                default:
                    throw new FIOException();
            }
            #endregion
            return fPP;
        }

       
    }
}
