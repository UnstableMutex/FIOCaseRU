using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Casers.SqlDB;
using FIOCaseRU;
using FIOCaseRU.StaticMethods;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CasingController : ApiController
    {
        FIOCaser mixedCasers = CreateCaser();
        public CasingController()
        {

        }

        private static FIOCaser CreateCaser()
        {
            var mixedCasers = new FIOCaser();
            var cs = "";
            var surnameLL = new LinkedList<ICaser>();
            surnameLL.AddLast(new SurnameCaser());
            surnameLL.AddFirst(new SurnameSqlDBCaser(cs));
            ChainedCaser surname = new ChainedCaser(surnameLL);

            var firstnameLL = new LinkedList<ICaser>();
            firstnameLL.AddLast(new FirstNameCaser());
            firstnameLL.AddFirst(new FirstNameSqlDBCaser(cs));
            ChainedCaser firstname = new ChainedCaser(firstnameLL);

            var patronymicLL = new LinkedList<ICaser>();
            patronymicLL.AddLast(new PatronymicCaser());
            patronymicLL.AddFirst(new PatronymicSqlDBCaser(cs));
            ChainedCaser patronymic = new ChainedCaser(patronymicLL);
            mixedCasers.PatronymicCaser = patronymic;
            mixedCasers.FirstNameCaser = firstname;
            mixedCasers.SurnameCaser = surname;




            return mixedCasers;
        }

        // GET: api/Casing
        public CasedFIO Get(string surname, string firstName, string patronymic)
        {


            CasedFIO result = new CasedFIO();
            Capitalize cap = new Capitalize();
            surname = cap.Capitalizer(surname);
            firstName = cap.Capitalizer(firstName);
            patronymic = cap.Capitalizer(patronymic);


            var sc = mixedCasers.SurnameCaser;
            var fc = mixedCasers.FirstNameCaser;
            var pc = mixedCasers.PatronymicCaser;
            result.Nominative.Surname = sc.GetCase(surname, Sex.Undefined, Case.Nominative);
            result.Nominative.FirstName = fc.GetCase(firstName, Sex.Undefined, Case.Nominative);
            result.Nominative.Patronymic = pc.GetCase(patronymic, Sex.Undefined, Case.Nominative);



            var gender = GetSex(result.Nominative);
            var g = GetGender(result.Nominative);

            result.Nominative.Gender = g;
            result.Ablative.Gender = g;
            result.Dative.Gender = g;
            result.Genitive.Gender = g;
            result.Prepositional.Gender = g;


            result.Ablative.Surname = sc.GetCase(surname, gender, Case.Ablative);
            result.Dative.Surname = sc.GetCase(surname, gender, Case.Dative);
            result.Genitive.Surname = sc.GetCase(surname, gender, Case.Genitive);
            result.Prepositional.Surname = sc.GetCase(surname, gender, Case.Prepositional);

            result.Ablative.FirstName = fc.GetCase(firstName, gender, Case.Ablative);
            result.Dative.FirstName = fc.GetCase(firstName, gender, Case.Dative);
            result.Genitive.FirstName = fc.GetCase(firstName, gender, Case.Genitive);
            result.Prepositional.FirstName = fc.GetCase(firstName, gender, Case.Prepositional);

            result.Ablative.Patronymic = pc.GetCase(patronymic, gender, Case.Ablative);
            result.Dative.Patronymic = pc.GetCase(patronymic, gender, Case.Dative);
            result.Genitive.Patronymic = pc.GetCase(patronymic, gender, Case.Genitive);
            result.Prepositional.Patronymic = pc.GetCase(patronymic, gender, Case.Prepositional);
            Action<CasedSimpleFIO> fullcalcer =
                (fio) => fio.Full = fio.Surname + " " + fio.FirstName + " " + fio.Patronymic;
            Action<CasedSimpleFIO> abbrcalcer =
                (fio) => fio.Abbr = fio.Surname + " " + fio.FirstName[0] + "." + fio.Patronymic[0] + ".";

            Action<CasedSimpleFIO> iocalcer = (fio) => fio.IO = fio.FirstName + " " + fio.Patronymic;

            Action<CasedSimpleFIO> complexcalcer = fio =>
            {
                fullcalcer(fio);
                abbrcalcer(fio);
                iocalcer(fio);
            };
            complexcalcer(result.Genitive);
            complexcalcer(result.Ablative);
            complexcalcer(result.Dative);
            complexcalcer(result.Prepositional);
            complexcalcer(result.Nominative);
            return result;

        }

        private static Sex GetSex(CasedSimpleFIO nominative)
        {
            DeterminyGender gdet = new DeterminyGender();

            var gender = gdet.ByPatronymic(nominative.Patronymic);
            return gender;
        }
        private static Gender GetGender(CasedSimpleFIO nominative)
        {
            var s = GetSex(nominative);

            switch (s)
            {
                case Sex.Undefined:
                    return Gender.Undefined;
                case Sex.Female:
                    return Gender.Female;
                case Sex.Male:
                    return Gender.Male;
                default:
                    throw new NotImplementedException();
            }
        }

        public CasedFIO Get(string surname, string firstName, Sex gender)
        {
            CasedFIO result = new CasedFIO();

            Capitalize cap = new Capitalize();
            surname = cap.Capitalizer(surname);
            firstName = cap.Capitalizer(firstName);
            var sc = mixedCasers.SurnameCaser;
            var fc = mixedCasers.FirstNameCaser;

            result.Nominative.Surname = sc.GetCase(surname, Sex.Undefined, Case.Nominative);
            result.Nominative.FirstName = fc.GetCase(firstName, Sex.Undefined, Case.Nominative);





            result.Ablative.Surname = sc.GetCase(surname, gender, Case.Ablative);
            result.Dative.Surname = sc.GetCase(surname, gender, Case.Dative);
            result.Genitive.Surname = sc.GetCase(surname, gender, Case.Genitive);
            result.Prepositional.Surname = sc.GetCase(surname, gender, Case.Prepositional);



            result.Ablative.FirstName = fc.GetCase(firstName, gender, Case.Ablative);
            result.Dative.FirstName = fc.GetCase(firstName, gender, Case.Dative);
            result.Genitive.FirstName = fc.GetCase(firstName, gender, Case.Genitive);
            result.Prepositional.FirstName = fc.GetCase(firstName, gender, Case.Prepositional);

            Action<CasedSimpleFIO> fullcalcer = (fio) => fio.Full = fio.Surname + " " + fio.FirstName;
            Action<CasedSimpleFIO> abbrcalcer = (fio) => fio.Abbr = fio.Surname + " " + fio.FirstName[0] + ".";
            Action<CasedSimpleFIO> iocalcer = (fio) => fio.IO = fio.FirstName;
            Action<CasedSimpleFIO> complexcalcer = fio =>
            {
                fullcalcer(fio);
                abbrcalcer(fio);
                iocalcer(fio);
            };
            complexcalcer(result.Genitive);
            complexcalcer(result.Ablative);
            complexcalcer(result.Dative);
            complexcalcer(result.Prepositional);
            complexcalcer(result.Nominative);
            return result;

        }

    }
}
