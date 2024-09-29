using MediatR;

namespace PATIENT.APPLICATION.Patient.GetAllPatient;

public record GetAllPatientQuery() : IRequest<IEnumerable<GetAllPatientResponse>>;
