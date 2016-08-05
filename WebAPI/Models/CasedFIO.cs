using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{




    public class CasedSimpleFIO
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Gender Gender { get; set; }
        public string Abbr { get; set; }
        public string IO { get; set; }
        public string Full { get; set; }
    }

    public enum Gender:byte
    {
        Undefined=0,Male=1,Female=2
    }
    public class CasedFIO
    {
        public CasedSimpleFIO Nominative { get; set; }
        public CasedSimpleFIO Genitive { get; set; }
        public CasedSimpleFIO Ablative { get; set; }
        public CasedSimpleFIO Dative { get; set; }
        public CasedSimpleFIO Prepositional { get; set; }


    }
}