using PATIENT.DOMAIN;
using PATIENT.INFRA.Repositories.Common;

namespace PATIENT.INFRA.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
}
