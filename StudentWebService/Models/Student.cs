using System;

namespace StudentWebService.Models
{
    public class Student
    {
        public long Index { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }

        public Student()
        {

        }

        public Student(long index, string name, string surname, DateTime birthdate)
        {
            Index = index;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
        }

        public string GetText(Student student)
        {
            return "Imie: " + student.Name + "\r\n"
                   + "Nazwisko: " + student.Surname + "\r\n"
                   + "Data usrodzenia: " + student.Birthdate.ToString() + "\r\n";
        }
    }
}