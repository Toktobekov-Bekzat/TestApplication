using System;

namespace TestApplication.Models
{
    public class Students
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GenderId { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

    }

    //public class Position
    //{
    //    public int Id { get; set; }
    //    public string Test { get; set; }
    //}

    //public class PositionEmployee
    //{
    //    public int id { get; set; }
    //    public Position Position { get; set; }
    //    public PositionEmployee positionEmployee { get; set; }
    //    public DateTime Start { get; set; }
    //    public DateTime End { get; set; }
    //}
}
/*1) Clean
2) CQRS
3) Нормальные формы бД(нормализация)
4) ACID
5) связи в ЬД(one to one etc)
6) DataBaseFirst EF CORE*/
