using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIOCaseRU.StaticMethods
{
  public  class FIOCaser
    {
        public ICaser SurnameCaser { get; set; }
        public ICaser FirstNameCaser { get; set; }
        public ICaser PatronymicCaser { get; set; }

    }
}
