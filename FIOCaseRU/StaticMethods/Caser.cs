using System;
using System.Runtime.Serialization;

namespace FIOCaseRU.StaticMethods
{

    interface ICaser
    {
        string GetCase(string toCase, Sex gender, Case c);
        bool TryGetCase(string toCase, Sex gender, Case c, out string result);

    }

public  abstract class CaserBase:ICaser
  {
      public abstract string GetCase(string toCase, Sex gender, Case c);
        
        public bool TryGetCase(string toCase, Sex gender, Case c, out string result)
        {
            try
            {
                result = GetCase(toCase, gender, c);
                return true;
            }
            catch (CaserException)
            {
                result = null;
                return false;
            }
        }
    }
    abstract class Caser : CaserBase
    {
        public override string GetCase(string toCase, Sex gender, Case c)
        {
            switch (c)
            {
                case Case.Ablative:
                    return GetAblative(toCase, gender);
                case Case.Accusative:
                    return GetAccusative(toCase, gender);
                case Case.Dative:
                    return GetDative(toCase, gender);
                case Case.Genitive:
                    return GetGenitive(toCase, gender);
                case Case.Nominative:
                    return GetNominative(toCase, gender);
                case Case.Prepositional:
                    return GetPrepositional(toCase, gender);
                default:
                    throw new NotImplementedException();
            }
        }

     

        protected virtual string GetNominative(string toCase, Sex gender)
        {
            return toCase;
        }


        protected abstract string GetGenitive(string toCase, Sex gender);
        protected abstract string GetAblative(string toCase, Sex gender);
        protected abstract string GetDative(string toCase, Sex gender);
        protected abstract string GetPrepositional(string toCase, Sex gender);

        protected virtual string GetAccusative(string toCase, Sex gender)
        {
            return GetGenitive(toCase, gender);
        }

    }

    [Serializable]
    public class CaserException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CaserException()
        {
        }

        public CaserException(string message) : base(message)
        {
        }

        public CaserException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CaserException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}