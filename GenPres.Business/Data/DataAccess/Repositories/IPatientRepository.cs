using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Patient;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IPatientRepository : IRepository<Database.Patient, IPatient>
    {
        IPatient GetByPid(string pid);
    }
}
