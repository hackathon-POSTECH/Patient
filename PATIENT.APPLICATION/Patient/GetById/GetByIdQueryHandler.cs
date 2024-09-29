using MediatR;
using PATIENT.INFRA.Repositories;

namespace PATIENT.APPLICATION.Patient.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, GetByIdResponse>
{
    private readonly IPatientRepository _patientRepository;

    public GetByIdQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<GetByIdResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return GetByIdResponse.ToResponse(await _patientRepository.GetByIdAsync(request.Id));
    }
}
