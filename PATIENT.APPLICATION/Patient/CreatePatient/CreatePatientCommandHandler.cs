using MediatR;
using PATIENT.INFRA.Repositories;

namespace PATIENT.APPLICATION.Patient.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, CreatePatientResponse>
{
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<CreatePatientResponse> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = PATIENT.DOMAIN.Patient.CreatePatient(request.Name, request.Cpf, request.Email, request.UserId);

        await _patientRepository.AddAsync(patient);
        _patientRepository.SaveChangesAsync();

        return CreatePatientResponse.ToResponse(patient);
    }
}
