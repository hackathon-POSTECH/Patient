using MediatR;
using PATIENT.INFRA.Repositories;

namespace PATIENT.APPLICATION.Patient.GetAllPatient;

public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<GetAllPatientResponse>>
{
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<GetAllPatientResponse>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        var Patients = await _patientRepository.GetAllAsync(x => x.UserId != null);
        return GetAllPatientResponse.ToResponse(Patients);
    }
}
