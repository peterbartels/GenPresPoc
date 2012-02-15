using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.xTest.Acceptance
{
    public class PrescriptionCalculate : PrescriptionCalculateProperties
    {
       
        public PrescriptionCalculate()
        {
            
        }

        public void CalculatePrescription()
        {
            var calculator = new PrescriptionCalculator(_prescription);
            calculator.Calculate();
        }

        public void CreatePrescription()
        {
            _prescription = Prescription.NewPrescription();
        }

        public void PrescriptionCanCalculate()
        {
            
        }

        public void execute()
        {
            CalculatePrescription();
        }

        public void reset()
        {
            CreatePrescription();
        }
    }
}
