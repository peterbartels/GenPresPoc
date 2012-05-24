using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenPres.Business.Domain.Prescriptions.Scenarios
{
    public interface IScenario
    {
        bool AppliesTo(Prescription prescription);
    }
}
