namespace PATIENT.APPLICATION.Patient.CreatePatient;

public class CreatePatientResponse
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public static CreatePatientResponse ToResponse(PATIENT.DOMAIN.Patient patient)
        => new CreatePatientResponse
        {
            UserId = patient.UserId,
            Name = patient.Name,
            Cpf = patient.Cpf,
            Email = patient.Email,  
        };
}
