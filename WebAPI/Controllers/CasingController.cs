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
            DeterminyGender gdet = new DeterminyGender();
            var gender = gdet.ByPatronymic(patronymic);
            var sc = mixedCasers.SurnameCaser;
            result.Ablative.Surname = sc.GetCase(surname, gender, Case.Ablative);
            result.Dative.Surname = sc.GetCase(surname, gender, Case.Dative);
            result.Genitive.Surname = sc.GetCase(surname, gender, Case.Genitive);
            result.Prepositional.Surname = sc.GetCase(surname, gender, Case.Prepositional);
            var fc = mixedCasers.FirstNameCaser;
            result.Ablative.FirstName = fc.GetCase(firstName, gender, Case.Ablative);
            result.Dative.FirstName = fc.GetCase(firstName, gender, Case.Dative);
            result.Genitive.FirstName = fc.GetCase(firstName, gender, Case.Genitive);
            result.Prepositional.FirstName = fc.GetCase(firstName, gender, Case.Prepositional);
            var pc = mixedCasers.PatronymicCaser;
            result.Ablative.Patronymic = pc.GetCase(patronymic, gender, Case.Ablative);
            result.Dative.Patronymic = pc.GetCase(patronymic, gender, Case.Dative);
            result.Genitive.Patronymic = pc.GetCase(patronymic, gender, Case.Genitive);
            result.Prepositional.Patronymic = pc.GetCase(patronymic, gender, Case.Prepositional);

            Action<CasedSimpleFIO> fullcalcer =
                (fio) => fio.Full = fio.Surname + " " + fio.FirstName + " " + fio.Patronymic;
            Action<CasedSimpleFIO> abbrcalcer =
                (fio) => fio.Abbr = fio.Surname + " " + fio.FirstName[0] + "." + fio.Patronymic[0]+".";

            Action<CasedSimpleFIO> iocalcer = (fio) => fio.IO = fio.FirstName + " "+fio.Patronymic;

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
        public CasedFIO Get(string surname, string firstName, Sex gender)
        {
            CasedFIO result = new CasedFIO();
            var sc = mixedCasers.SurnameCaser;

            result.Ablative.Surname = sc.GetCase(surname, gender, Case.Ablative);
            result.Dative.Surname = sc.GetCase(surname, gender, Case.Dative);
            result.Genitive.Surname = sc.GetCase(surname, gender, Case.Genitive);
            result.Prepositional.Surname = sc.GetCase(surname, gender, Case.Prepositional);


            var fc = mixedCasers.FirstNameCaser;
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
