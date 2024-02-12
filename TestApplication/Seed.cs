using Microsoft.EntityFrameworkCore;
using System;
using System.Linq; // Add this using directive
using TestApplication.Models;

namespace TestApplication.Data
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            // Check if the database already has any students
            if (dataContext.Students.Any())
            {
                // Database has already been seeded
                return;
            }

            //Seed data with Genders
            dataContext.Genders.AddRange(
            new Gender
            {
                Description = "Male"
            },
            new Gender
            {
                Description = "Female"

            },
            new Gender 
            {
                Description = "Other"
            });

            // Seed the database with sample students
            dataContext.Students.AddRange(
                new Students
                {
                    FirstName = "John",
                    LastName = "Doe",
                    GenderId = 1,
                    BirthDate = new DateTime(1990, 5, 15)
                },
                new Students
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    GenderId = 2,
                    BirthDate = new DateTime(1992, 8, 20)
                },
                new Students
                {
                    FirstName = "Michael",
                    LastName = "Johnson",
                    GenderId = 1,
                    BirthDate = new DateTime(1988, 3, 10)
                },
                new Students
                {
                    FirstName = "Emily",
                    LastName = "Brown",
                    GenderId = 2,
                    BirthDate = new DateTime(1995, 11, 28)
                },
                new Students
                {
                    FirstName = "David",
                    LastName = "Wilson",
                    GenderId = 1,
                    BirthDate = new DateTime(1993, 7, 4)
                }
            );

            dataContext.SaveChanges();
        }
    }
}
