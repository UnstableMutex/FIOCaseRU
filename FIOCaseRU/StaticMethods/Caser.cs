using System;

namespace FIOCaseRU.StaticMethods
{
    abstract class Caser
    {
        public string GetCase(string toCase, Sex gender, Case c)
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
}