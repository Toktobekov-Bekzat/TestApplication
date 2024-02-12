using MediatR;
using TestApplication.Interfaces;

public class GetStudentByIdRequest : IRequest<GetStudentRequestDto>
{
    public int StudentId { get; set; }
}


public class GetStudentByIdRequestHandler : IRequestHandler<GetStudentByIdRequest, GetStudentRequestDto>
{
    private readonly IStudentsRepository _studentsRepository;
    private readonly IGenderRepository _genderRepository;

    public GetStudentByIdRequestHandler(IStudentsRepository studentsRepository, IGenderRepository genderRepository)
    {
        _studentsRepository = studentsRepository;
        _genderRepository = genderRepository;
    }

    public async Task<GetStudentRequestDto> Handle(GetStudentByIdRequest request, CancellationToken cancellationToken)
    {
        var student = _studentsRepository.GetStudentById(request.StudentId);

        if (student == null)
        {
            return null; // Or handle the case when student is not found
        }

        var studentWithAgeAndGender = new GetStudentRequestDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Gender = student.Gender.Description,
            Age = GetStudentRequestDto.CalculateAge(student.BirthDate)
        };

        return studentWithAgeAndGender;
    }
}
