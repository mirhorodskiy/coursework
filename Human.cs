using System;

namespace NetworkLibrary
{
    public abstract class Human
    {
        public string name { get; }
        public string surname { get; }
        public DateTime dateOfBirth { get; }
        protected int age;
        public Human(string name, string surname, DateTime dateOfBirth)
        {
            this.name = name;
            this.surname = surname;
            this.dateOfBirth = dateOfBirth;
            if (dateOfBirth == default)
                age = -1;
            else
            {
                age = DateTime.Now.Year - dateOfBirth.Year;
                if (DateTime.Now.Month < dateOfBirth.Month)
                    age--;
                else if (DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day)
                    age--;
            }
        }
    }
}
