using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Determination
{
    interface IDetermination
    {
        bool Determine(Prescription p);
    }
}
