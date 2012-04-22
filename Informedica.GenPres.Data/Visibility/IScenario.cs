using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Data.Visibility
{
    public interface IScenario
    {
        void SetVisibilities(Prescription prescription, PrescriptionDto _prescriptionDto);
    }
}
