using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GenPres.Business.Domain.Units;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Allowance
{
    public class PrescriptionPropertyCombinations
    {
        private Prescription _prescription;

        public PrescriptionPropertyCombinations(Prescription prescription)
        {
            _prescription = prescription;
        }

        private List<Expression<Func<UnitValue>>[]> _combinations {
            get{
                var dose = _prescription.Doses[0];
                var drug = _prescription.Drug;
                var substance = _prescription.Drug.Components[0].Substances[0];

                return new List<Expression<Func<UnitValue>>[]>{
                    CreateCombi(() => _prescription.Total, () => _prescription.Quantity, () =>_prescription.Frequency),
                    CreateCombi(() => dose.Total, () => dose.Quantity, () =>_prescription.Frequency),
                };
            }
        }

        private Expression<Func<UnitValue>>[]  CreateCombi(params Expression<Func<UnitValue>>[] combination)
        {
            return combination;
        }

        public void CheckStates()
        {
            for (int i = 0; i < _combinations.Count; i++)
            {
                var combination = _combinations[0];

            }
        }

    }
}
