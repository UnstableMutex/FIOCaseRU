using FIOCaseRU.StaticMethods;

namespace FIOCaseRU
{
    public class CaseSettings
    {
        Capitalize c = new Capitalize();
        public CaseSettings()
        {
            
        }
        public virtual string Capitalizer(string str)
        {
          return  c.Capitalizer(str);
        }
       
        public virtual bool SpaceBetweenAbbr()
        {
            return true;
        }
    }
}
