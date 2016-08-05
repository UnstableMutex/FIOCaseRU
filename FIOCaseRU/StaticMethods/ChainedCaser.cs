using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIOCaseRU.StaticMethods
{
    class ChainedCaser : CaserBase
    {
        private readonly LinkedList<ICaser> _list;

        public ChainedCaser(LinkedList<ICaser> list)
        {
            _list = list;
        }

    
        public override string GetCase(string toCase, Sex gender, Case c)
        {
            var f = _list.First;
            string res;
            while (!f.Value.TryGetCase(toCase, gender, c, out res))
            {
                f = f.Next;
            }
            return res;

        }
    }
}
