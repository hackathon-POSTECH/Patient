namespace PATIENT.APPLICATION.Patient.GetAllPatient;

public class GetAllPatientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }

    public static IEnumerable<GetAllPatientResponse> ToResponse(IEnumerable<PATIENT.DOMAIN.Patient> patients)
        => patients.Select(x => new GetAllPatientResponse
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            Cpf = x.Cpf,
        });

}
