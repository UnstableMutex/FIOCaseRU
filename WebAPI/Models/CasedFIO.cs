using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CasedFIO
    {
        public CasedFIO()
        {
            Nominative = new CasedSimpleFIO();
            Genitive = new CasedSimpleFIO();
            Ablative = new CasedSimpleFIO();
            Dative = new CasedSimpleFIO();
            Prepositional = new CasedSimpleFIO();

        }
        public CasedSimpleFIO Nominative { get; set; }
        public CasedSimpleFIO Genitive { get; set; }
        public CasedSimpleFIO Ablative { get; set; }
        public CasedSimpleFIO Dative { get; set; }
        public CasedSimpleFIO Prepositional { get; set; }


    }
}