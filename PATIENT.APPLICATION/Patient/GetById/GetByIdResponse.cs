namespace PATIENT.APPLICATION.Patient.GetById;

public class GetByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public static GetByIdResponse ToResponse(DOMAIN.Patient patient)
        => new GetByIdResponse
        {
            Email = patient.Email,
            Id = patient.Id,
            Name = patient.Name,
            Phone = "123-456"
        };

}
