using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Patients;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IPatientRepository : IRepository<IPatient>
    {
        IPatient GetByPid(string pid);
    }
}
