using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.Interfaces;
using TestApplication.Models;
using TestApplication.Repository;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository studentsRepository;
        private readonly IGenderRepository genderRepository;
        private readonly IMediator _mediator;

        public StudentsController(IStudentsRepository studentRepository, IGenderRepository genderRepository, IMediator mediator)
        {
            this.studentsRepository = studentRepository;
            this.genderRepository = genderRepository;
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Students>))]
        public async Task<IActionResult> GetStudents()
        {
            var response = await _mediator.Send(new GetStudentRequest());
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(object))]
        public IActionResult GetStudentByID(int id)
        {
            var student = studentsRepository.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            var studentWithAge = new
            {
                student.Id,
                student.FirstName,
                student.LastName,
                student.Gender.Description,
                Age = CalculateAge(student.BirthDate)
            };

            return Ok(studentWithAge);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AddStudent([FromBody] Students student)
        {
            if (student == null)
            {
                return BadRequest("Student data is null.");
            }

            try
            {
                // Fetch the gender information based on the GenderId provided
                student.Gender = genderRepository.GetGenderByKey(student.GenderId);

                studentsRepository.AddStudent(student);
                return CreatedAtAction(nameof(GetStudentByID), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudentById(int id)
        {
            var student = studentsRepository.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                studentsRepository.DeleteStudentById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;

            //helper for the time
        }
    }
}
