using MediatR;

namespace PATIENT.APPLICATION.Patient.CreatePatient;

public record CreatePatientCommand(
    Guid UserId,
    string Name,
    string Email,
    string Cpf) : IRequest<CreatePatientResponse>;
