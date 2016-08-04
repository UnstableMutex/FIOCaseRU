#region usings

using System;
using System.Globalization;
using System.Linq;

#endregion

namespace FIOCaseRU
{
    internal static class Ext
    {
        internal static string GetPart(this SimpleFIO simpleFio, PartType pt)
        {
            switch (pt)
            {
                case PartType.Patronymic:
                    return simpleFio.Patronymic;
                case PartType.Firstname:
                    return simpleFio.Firstname;
                case PartType.Surname:
                    return simpleFio.Surname;
            }
            throw new NotImplementedException();
        }
        internal static string RemoveEndIfExists(this string s, string end)
        {
            if (s.EndsWith(end))
            {
                return s.Substring(0, s.Length - end.Length);
            }
            else
            {
                return s;
            }
        }
    }
    public class SimpleFIO
    {
        public string ToString(string format)
        {
            if (format == "full")
            {
                format = "s&e&f&e&p";

            }
            if (format == "abbr")
            {
                format = "s&e&f1&d&p1&d";
            }
            if (format == "abbrs")
            {
                format = "s&e&f1&d&e&p1&d";
            }
            /*
             * s f p - property specifiers
             * d - dot
             * e - space
             * full
             * abbr
             * abbrs
             */
            var result = ToStringImpl(format);
            return result;

        }

        private string ToStringImpl(string format)
        {
            string[] formats = format.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Empty;
            foreach (var f in formats)
            {
                switch (f)
                {
                    case "d":
                        result += ".";
                        break;
                    case "e":
                        result += space;
                        break;
                    default:
                        result += GetOnePart(f);
                        break;
                }
            }
            result = result.Trim();
            result = result.RemoveEndIfExists(" .");
            return result;
        }

        private readonly Func<string, string> getfirst = s =>
            {
                if (string.IsNullOrEmpty(s))
                    return s;
                return s.Substring(0, 1);
            };
        private string GetOnePart(string format)
        {

            switch (format)
            {
                case "s":
                    return Surname;
                case "f":
                    return Firstname;
                case "p":
                    return Patronymic;
                case "s1":
                    return getfirst(Surname);
                case "f1":
                    return getfirst(Firstname);
                case "p1":
                    return getfirst(Patronymic);
                default:
                    throw new Exception();
            }

        }

        private const string space = " ";
        public SimpleFIO(string surname, string firstname, string patronymic)
        {
            Surname = surname;
            Firstname = firstname;
            Patronymic = patronymic;
        }
        public string Surname { get; private set; }
        public string Firstname { get; private set; }
        public string Patronymic { get; private set; }
        public string Full { get { return Surname + " " + Firstname + (Patronymic == string.Empty ? string.Empty : " " + Patronymic); } }
        public string Abbr
        {
            get
            {
                return Surname + " " + Firstname[0] + "." +
                    Patronymic == string.Empty ?
                    string.Empty :
                    ((FIO.Settings.SpaceBetweenAbbr() ?
                    space :
                    "") + Patronymic[0] + ".");
            }
        }
    }
    public class FIO : IFormattable
    {
        private static CaseSettings _defaultSettings = new CaseSettings();
        public static CaseSettings Settings { get { return _defaultSettings; } set { _defaultSettings = value; } }



        private const string Space = " ";
        // private Dictionary<Case, SimpleFIO> dic = new Dictionary<Case, SimpleFIO>();
        private readonly SimpleFIO _nominative;
        public FIO(string surname, string firstname, string patronymic = "")
        {

            _nominative = new SimpleFIO(_defaultSettings.Capitalizer(surname.Trim()), _defaultSettings.Capitalizer(firstname.Trim()), _defaultSettings.Capitalizer(patronymic.Trim()));
            _genitive = new Lazy<SimpleFIO>(() => CalcCase(Case.Genitive));
            _ablative = new Lazy<SimpleFIO>(() => CalcCase(Case.Ablative));
            _dative = new Lazy<SimpleFIO>(() => CalcCase(Case.Dative));
            _prepositional = new Lazy<SimpleFIO>(() => CalcCase(Case.Prepositional));
            // dic.Add(Case.Nominative, _nominative);
        }

        private Sex _sex = Sex.Undefined;

        public FIO(string surname, string firstname, Sex sex)
        {
            _nominative = new SimpleFIO(_defaultSettings.Capitalizer(surname), _defaultSettings.Capitalizer(firstname), string.Empty);
            _ablative = new Lazy<SimpleFIO>(() => CalcCase(Case.Ablative));
            _genitive = new Lazy<SimpleFIO>(() => CalcCase(Case.Genitive));
            _sex = sex;
        }
        /// <summary>
        /// Именительный падеж
        /// </summary>
        public SimpleFIO Nominative
        {
            get { return _nominative; }
        }
        private readonly Lazy<SimpleFIO> _genitive;
        /// <summary>
        /// Родительный падеж
        /// </summary>
        public SimpleFIO Genitive
        {
            get
            {

                return _genitive.Value;
            }
        }
        private readonly Lazy<SimpleFIO> _dative;
        /// <summary>
        /// Дательный падеж
        /// </summary>
        public SimpleFIO Dative
        {
            get { return _dative.Value; }
        }
        /// <summary>
        /// Винительный падеж
        /// </summary>
        public SimpleFIO Accusative
        {
            get { return Genitive; }
        }

        private readonly Lazy<SimpleFIO> _ablative;
        /// <summary>
        /// Творительный
        /// </summary>
        public SimpleFIO Ablative
        {
            get { return _ablative.Value; }
        }


        private readonly Lazy<SimpleFIO> _prepositional;
        /// <summary>
        /// Предложный
        /// </summary>
        public SimpleFIO Prepositional
        {
            get { return _prepositional.Value; }
        }
        public virtual Sex GetSex()
        {
            if (_sex == Sex.Undefined)
            {
                _sex = GetSexByPatronymic();
            }
            if (_sex == Sex.Undefined)
            {
                _sex = GetSexByFirstname();
            }
            if (_sex == Sex.Undefined)
            {
                _sex = GetSexBySurname();
            }
            return _sex;
        }
        protected virtual Sex GetSexByPatronymic()
        {
            Sex res;
            if (Nominative.Patronymic == string.Empty)
                return Sex.Undefined;
            string o2simend = Nominative.Patronymic.Substring(Nominative.Patronymic.Length - 2);
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
        protected virtual Sex GetSexByFirstname()
        {
            return Sex.Undefined;
        }
        protected virtual Sex GetSexBySurname()
        {
            return Sex.Undefined;
        }

        protected virtual SimpleFIO CalcCase(Case cCase)
        {
            Sex sex = GetSex();
            string firstName = string.Empty;
            string surname = string.Empty;
            string patronymic = string.Empty;
            if (cCase == Case.Genitive)
            {
                firstName = GetFirstNameRP(sex);
                surname = GetSurnameRP(sex);
                patronymic = GetPatronymicRP();
            }
            else if (cCase == Case.Dative)
            {
                firstName = GetFirstNameDP();
                surname = GetSurnameDP();
                patronymic = GetPatronymicDP();
            }
            else if (cCase == Case.Ablative)
            {
                firstName = GetFirstNameTP();
                surname = GetSurnameTP();
                patronymic = GetPatronymicTP();
            }
            else if (cCase == Case.Prepositional)
            {
                firstName = GetFirstNamePP();
                surname = GetSurnamePP();
                patronymic = GetPatronymicPP();
            }
            return new SimpleFIO(surname, firstName, patronymic);
        }

        #region RPHelpers

        /// <summary>
        /// Получение имени в родительном падеже
        /// </summary>
        /// <returns></returns>
        private string GetFirstNameRP(Sex sex)
        {
            string firstname = Nominative.Firstname;
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
                        if (sex == Sex.Female) iRP = firstname;
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
                    if (sex == Sex.Female) iRP = firstname;
                    else iRP = firstname.Substring(0, firstname.Length - 1) + "я";
                    break;
                case "я":
                    iRP = firstname.Substring(0, firstname.Length - 1) + "и";
                    break;
                case "ь":
                    if (sex == Sex.Female) iRP = firstname.Substring(0, firstname.Length - 1) + "и";
                    else iRP = firstname.Substring(0, firstname.Length - 1) + "я";
                    break;
            }
            return iRP;
        }

        /// <summary>
        /// Получение отчества в родительном падеже
        /// </summary>
        /// <returns>Отчество в родительном падеже</returns>
        private string GetPatronymicRP()
        {
            string oRP = String.Empty;
            string pat = Nominative.Patronymic;
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

        /// <summary>
        /// Получение фамилии в родительном падеже
        /// </summary>
        /// <returns>Фамилия в родительном падеже</returns>
        private string GetSurnameRP(Sex sex)
        {
            string surname = Nominative.Surname;
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
                    if (sex == Sex.Female) fRP = surname;
                    else fRP = surname + "а";
                    break;
                case "к":
                    switch (F2simend)
                    {
                        case "ук":
                            fRP = surname;
                            break;
                        default:
                            if (sex == Sex.Female) fRP = surname;
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
                            if (sex == Sex.Female) fRP = surname;
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
                    if (sex == Sex.Female) fRP = surname;
                    else fRP = surname.Substring(0, surname.Length - 1) + "я";
                    break;
                case "я":
                    if (F2simend == "ая") fRP = surname.Substring(0, surname.Length - 2) + "ой";
                    else fRP = surname.Substring(0, surname.Length - 1) + "и"; //данелия мужик
                    break;
                case "й":
                    if (sex == Sex.Female)
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

        #endregion
        #region DPHelpers
        /// <summary>
        /// Получение имени дп
        /// </summary>
        /// <returns>Имя дп</returns>
        private string GetFirstNameDP()
        {
            string firstname = Nominative.Firstname;
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
                        if (GetSex() == Sex.Female) pDP = firstname;
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
                        if (GetSex() == Sex.Female) pDP = firstname;
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
                        if (GetSex() == Sex.Female) pDP = firstname;
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
                    if (GetSex() == Sex.Female) pDP = firstname;
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
                    if (GetSex() == Sex.Female) pDP = firstname.Substring(0, firstname.Length - 1) + "и";
                    else pDP = firstname.Substring(0, firstname.Length - 1) + "ю";
                    break;
            }
            #endregion
            return pDP;
        }
        /// <summary>
        /// Получение фамилии дп
        /// </summary>
        /// <returns></returns>
        readonly string[] sdpoj = new[] { "ва", "на" };

        readonly string[] sdpe = new[] { "ба", "за", "ла", "ма", "па", "ра", "са", "та", "фа", "ца", "га", "да", "жа", "ка", "ша", "ща", "ха", "ча" };
        readonly string[] sdpnoend = new[] { "еа", "ёа", "иа", "йа", "уа", "оа", "ьа", "ыа", "эа", "юа", "яа" };
        private string GetSurnameDP()
        {


            var surname = Nominative.Surname;

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
                    if (GetSex() == Sex.Female) fDP = surname;
                    else fDP = surname + "у";
                    break;
                case "ц":
                    switch (F2simend)
                    {
                        case "ец":
                            fDP = surname;
                            break;
                        default:
                            if (GetSex() == Sex.Female) fDP = surname;
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
                            if (GetSex() == Sex.Female) fDP = surname;
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
                    if (GetSex() == Sex.Female) fDP = surname;
                    else fDP = surname.Substring(0, surname.Length - 1) + "ю";
                    break;
                case "я":
                    if (F2simend == "ая") fDP = surname.Substring(0, surname.Length - 2) + "ой";
                    else fDP = surname.Substring(0, surname.Length - 1) + "е";
                    break;
                case "й":
                    if (GetSex() == Sex.Female)
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
        /// <summary>
        /// Получение отчества дп
        /// </summary>
        /// <returns>Отчество в дп</returns>
        private string GetPatronymicDP()
        {
            var patronymic = Nominative.Patronymic;
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
        /// <summary>
        /// Проерка фамилии на геморность Дательный падеж
        /// </summary>
        /// <returns>Правильный дательный падеж если геморная фамилия ,string.Empty если нет исключений из правил</returns>
        private string SurnameDPExceptioned()
        {
            var surname = Nominative.Surname;
            if (surname.EndsWith("ок") && GetSex() == Sex.Female)
                return surname;
            return string.Empty;
        }
        #endregion
        #region TPHelpers
        /// <summary>
        /// Получение имени в тп
        /// </summary>
        /// <returns>Имя в тп</returns>
        private string GetFirstNameTP()
        {
            string firstname = Nominative.Firstname;
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
                        if (GetSex() == Sex.Female) pTP = firstname;
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
                        if (GetSex() == Sex.Female) pTP = firstname;
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
                        if (GetSex() == Sex.Female) pTP = firstname;
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
                    if (GetSex() == Sex.Female) pTP = firstname;
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
                    if (GetSex() == Sex.Female) pTP = firstname + "ю";
                    else pTP = firstname.Substring(0, firstname.Length - 1) + "ем";
                    break;
            }
            #endregion;
            return pTP;
        }
        /// <summary>
        /// Получение фамилии в тп
        /// </summary>
        /// <returns>Фамилия тп</returns>
        private string GetSurnameTP()
        {
            string surname = Nominative.Surname;
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
                    if (GetSex() == Sex.Female) pTP = surname;
                    else pTP = surname + "ым";
                    break;
                case "н":
                    if (GetSex() == Sex.Female) pTP = surname;
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
                    if (GetSex() == Sex.Female) pTP = surname;
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
                    if (GetSex() == Sex.Female) pTP = surname;
                    else pTP = surname + "ом";
                    break;
                case "л":
                    if (GetSex() == Sex.Female) pTP = surname;
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
                            if (GetSex() == Sex.Female) pTP = surname;
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
                    if (GetSex() == Sex.Female) pTP = surname;
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
                        if (GetSex() == Sex.Female) pTP = surname;
                        else pTP = surname.Substring(0, surname.Length - 1) + "ей";
                    }
                    break;
                case "й":
                    if (GetSex() == Sex.Female)
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
        /// <summary>
        /// Получение отчества тп
        /// </summary>
        /// <returns></returns>
        private string GetPatronymicTP()
        {
            string patronymic = Nominative.Patronymic;
            if (string.IsNullOrEmpty(Nominative.Patronymic))
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
        #endregion
        #region PPHelpers
        /// <summary>
        /// Получение имени в пп
        /// </summary>
        /// <returns>Имя в пп</returns>
        private string GetFirstNamePP()
        {
            string firstname = Nominative.Firstname;
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
                    if (GetSex() == Sex.Female) pPP = firstname;
                    else pPP = firstname + "е";
                    break;
                case "в":
                    if (firstname.ToLower() == "лев")
                    {
                        pPP = "Льве";
                    }
                    else
                    {
                        if (GetSex() == Sex.Female) pPP = firstname;
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
                        if (GetSex() == Sex.Female) pPP = firstname;
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
                    if (GetSex() == Sex.Female) pPP = firstname;
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
                    if (GetSex() == Sex.Female) pPP = firstname.Substring(0, firstname.Length - 1) + "и";
                    else pPP = firstname.Substring(0, firstname.Length - 1) + "е";
                    break;
            }
            #endregion
            return pPP;
        }
        /// <summary>
        /// Получение фамлии пп
        /// </summary>
        /// <returns>Фамилия пп</returns>
        private string GetSurnamePP()
        {
            string surname = Nominative.Surname;
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
                    if (GetSex() == Sex.Female) fPP = surname;
                    else fPP = surname + "е";
                    break;
                case "к":
                    switch (F2simend)
                    {
                        case "ук":
                            fPP = surname;
                            break;
                        default:
                            if (GetSex() == Sex.Female) fPP = surname;
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
                            if (GetSex() == Sex.Female) fPP = surname;
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
                    if (GetSex() == Sex.Female) fPP = surname;
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
                    if (GetSex() == Sex.Female)
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
        /// <summary>
        /// Получение отчества пп
        /// </summary>
        /// <returns>Отчество пп</returns>
        private string GetPatronymicPP()
        {
            string patronymic = Nominative.Patronymic;
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
        #endregion
        string readformatsymbols(int num, ref string format)
        {
            string casenum = format.Substring(0, num);
            format = format.Substring(num);
            return casenum;

        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            //string delimiter = "&";
            /*
             * 1&s&f&p
             * 1&s&f1&p1
             * 
             */

            string firsts = format.Substring(0, 1);
            int c;
            bool successParse = int.TryParse(firsts, out c);
            SimpleFIO fio;
            if (successParse)
            {
                switch (c)
                {
                    case 1:
                        fio = Nominative;
                        break;
                    case 2:
                        fio = Genitive;
                        break;
                    case 3:
                        fio = Dative;
                        break;
                    case 4:
                        fio = Accusative;
                        break;
                    case 5:
                        fio = Ablative;
                        break;
                    case 6:
                        fio = Prepositional;
                        break;
                    default:
                        throw new NotSupportedException();
                }
                format = format.Substring(1);

            }
            else
            {
                fio = Nominative;
            }
            return fio.ToString(format);



        }
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }
    }
    [Serializable]
    internal class FIOException : Exception
    {
    }
}
