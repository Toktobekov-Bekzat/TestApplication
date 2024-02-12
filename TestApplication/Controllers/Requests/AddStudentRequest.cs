using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Controllers.Requests
{
    public class AddStudentRequest : IRequest<GetStudentRequestDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class AddStudentRequestHandler : IRequestHandler<AddStudentRequest, GetStudentRequestDto>
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IGenderRepository _genderRepository;

        public AddStudentRequestHandler(IStudentsRepository studentsRepository, IGenderRepository genderRepository)
        {
            _studentsRepository = studentsRepository;
            _genderRepository = genderRepository;
        }

        public async Task<GetStudentRequestDto> Handle(AddStudentRequest request, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName))
            {
                return null; // Or handle validation error appropriately
            }

            var student = new Students
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                GenderId = request.GenderId,
                BirthDate = request.BirthDate,
            };

            
            student.Gender = _genderRepository.GetGenderByKey(student.GenderId);

            
            _studentsRepository.AddStudent(student);

            
            var studentDto = new GetStudentRequestDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender.Description,
                Age = GetStudentRequestDto.CalculateAge(student.BirthDate)

            };

            return studentDto;
        }
    }
}
