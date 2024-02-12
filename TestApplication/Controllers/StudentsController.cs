using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.Controllers.Requests;
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
        [ProducesResponseType(200, Type = typeof(GetStudentRequestDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var request = new GetStudentByIdRequest { StudentId = id };
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return BadRequest("Failed to add student.");
            }

            return CreatedAtAction(nameof(GetStudentById), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(GetStudentRequestDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            var request = new DeleteStudentByIdRequest { StudentId = id };
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }



    }
}
