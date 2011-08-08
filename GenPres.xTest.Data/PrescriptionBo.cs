using GenPres.Business.Domain;

namespace GenPres.xTest.Data
{
    public class PrescriptionBo : ISavable
    {
        public PrescriptionBo()
        {
            //Drug = new DrugBo();
        }
        public DrugBo Drug { get; set; }

        public bool IsNew { get { return (Id == 0); } }
        
        public void OnCreate()
        {
            
        }

        public void OnNew()
        {
            
        }

        public void OnInitExisting()
        {
            
        }

        public void Save()
        {
            
        }

        public int Id
        {
            get; set;
        }
    }
}
