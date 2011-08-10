using GenPres.Assembler;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.xTest.Base
{
    public class BaseGenPresTest
    {
        
        public BaseGenPresTest()
        {
            GenPresApplication.Initialize();
            Settings.SettingsManager.Instance.Initialize();
        }

        public IPrescription CreatePrescriptionWithAllPropertiesSet()
        {
            var prescription = ObjectCreator.New<IPrescription>();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";

            prescription.Frequency.Value = 2;
            prescription.Frequency.Time = "dag";

            prescription.Quantity.Unit = "zetp";

            prescription.Total.Unit = "zetp";
            prescription.Total.Time = "dag";
            prescription.Total.Value = 7;

            prescription.Drug.Quantity.Unit = "zetp";
            prescription.Drug.Quantity.Value = 2;

            prescription.Drug.Components[0].Substances[0].Quantity.Value = 0.4m;
            prescription.Drug.Components[0].Substances[0].Quantity.Unit = "mg";
            return prescription;
        }
    }
}
