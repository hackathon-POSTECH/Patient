using PATIENT.DOMAIN.common;

namespace PATIENT.DOMAIN;

public class Patient : AggregateRoot
{
    public string Name { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public Guid UserId { get; private set; }

    public static Patient CreatePatient(string name, string cpf, string email, Guid userId)
    {
        return new Patient
        {
            Cpf = cpf,
            Email = email,
            UserId = userId,
            Name = name
        };
    }


}
