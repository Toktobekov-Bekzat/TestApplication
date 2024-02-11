using MediatR;
using TestApplication.Interfaces;

public class GetStudentRequest : IRequest<List<GetStudentRequestDto>>
{
    
}

public class GetStudentRequestHandler : IRequestHandler<GetStudentRequest, List<GetStudentRequestDto>>
{
    private readonly IStudentsRepository studentsRepository;
    private readonly IGenderRepository genderRepository;

    public GetStudentRequestHandler(IStudentsRepository studentsRepository, IGenderRepository genderRepository)
    {
        this.studentsRepository = studentsRepository;
        this.genderRepository = genderRepository;
    }

    public Task<List<GetStudentRequestDto>> Handle(GetStudentRequest request, CancellationToken cancellationToken)
    {
         var studentsWithGender = studentsRepository.GetStudents();

         var studentsWithAgeAndGender = studentsWithGender.Select(s => new GetStudentRequestDto
         {
             Id = s.Id,
             FirstName = s.FirstName,
             LastName = s.LastName,
             Gender = s.Gender.Description,
             Age = GetStudentRequestDto.CalculateAge(s.BirthDate)
         }).ToList();

        return Task.FromResult(studentsWithAgeAndGender);
    }
}