using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Determination
{
    class PrescriptionDetermination
    {
        private Prescription prescription;
        public PrescriptionDetermination(Prescription p)
        {
            prescription = p;
        }

        /*
         * Executes all determination rules
         */
        public void Determine(List<IDetermination> determinationTypes)
        {
            for (int i = 0; i < determinationTypes.Count; i++)
            {
                determinationTypes[i].Determine(prescription);
            }
        }
    }
}
