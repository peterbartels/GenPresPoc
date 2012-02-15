using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Allowance
{
    public class PropertyStateAllowance
    {
        private Prescription _prescription;

        public PropertyStateAllowance(Prescription prescription)
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
