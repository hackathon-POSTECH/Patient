using MediatR;

namespace PATIENT.APPLICATION.Patient.GetById;

public record GetByIdQuery(Guid Id) : IRequest<GetByIdResponse>;

