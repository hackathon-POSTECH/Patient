using PATIENT.DOMAIN;
using PATIENT.INFRA.context;

namespace PATIENT.INFRA.Repositories.Common;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(PATIENTCONTEXT context) : base(context) { }
}
