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
}