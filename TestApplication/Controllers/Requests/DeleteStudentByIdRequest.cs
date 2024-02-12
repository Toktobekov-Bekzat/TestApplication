using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Controllers.Requests
{
    public class DeleteStudentByIdRequest : IRequest<GetStudentRequestDto>
    {
        public int StudentId { get; set; }
    }

    public class DeleteStudentByIdRequestHandler : IRequestHandler<DeleteStudentByIdRequest, GetStudentRequestDto>
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IGenderRepository _genderRepository;

        public DeleteStudentByIdRequestHandler(IStudentsRepository studentsRepository, IGenderRepository genderRepository)
        {
            _studentsRepository = studentsRepository;
            _genderRepository = genderRepository;
        }

        public async Task<GetStudentRequestDto> Handle(DeleteStudentByIdRequest request, CancellationToken cancellationToken)
        {
            

            var student = _studentsRepository.GetStudentById(request.StudentId);
            
            var studentDto = new GetStudentRequestDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender.Description,
                Age = GetStudentRequestDto.CalculateAge(student.BirthDate)

            };

            _studentsRepository.DeleteStudentById(request.StudentId);

            return studentDto;
        }
    }
}
