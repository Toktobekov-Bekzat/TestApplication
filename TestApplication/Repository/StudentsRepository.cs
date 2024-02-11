using System;
using Microsoft.EntityFrameworkCore;
using TestApplication.Data;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Repository
{
	public class StudentsRepository : IStudentsRepository
	{

		private readonly DataContext _context;

		public StudentsRepository(DataContext context)
		{
			_context = context;
		}

        public Students GetStudentById(int id)
        {
            return _context.Students
                .Include(s => s.Gender)
                .FirstOrDefault(s => s.Id == id);
        }


        public ICollection<Students> GetStudents()
		{
            return _context.Students
				.Include(s => s.Gender)
				.ToList();
        }

		public void AddStudent(Students student)
		{
			_context.Students.Add(student);
			_context.SaveChanges();
		}

		public void DeleteStudentById(int id)
		{
			_context.Students.Remove(GetStudentById(id));
			_context.SaveChanges();
		}
	}
}

