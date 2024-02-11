using System;
using TestApplication.Models;

namespace TestApplication.Interfaces
{
	public interface IStudentsRepository
	{
        Students GetStudentById(int id);
        ICollection<Students> GetStudents();
		void AddStudent(Students student);
		void DeleteStudentById(int id);
    }
}

