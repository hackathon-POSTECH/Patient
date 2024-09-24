using Microsoft.EntityFrameworkCore;
using PATIENT.DOMAIN;

namespace PATIENT.INFRA.context;

public class PATIENTCONTEXT : DbContext
{
    public PATIENTCONTEXT(DbContextOptions<PATIENTCONTEXT> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }

}
