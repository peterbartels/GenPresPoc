namespace GenPres.Business.Domain.PrescriptionDomain
{
    public class Drug : IDrug
    {
        #region Private Fields
        
        private string _generic;

        private string _route;

        private string _shape;
        
        #endregion

        #region Public Properties

        public string Generic
        {
            get { return _generic; }
            set { _generic = value; }
        }

        public string Route
        {
            get { return _route; }
            set { _route = value; }
        }

        public string Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }
        
        #endregion

        public static Drug NewDrug()
        {
            return ObjectFactory.New<Drug>();
        }


        public bool IsNew { get; set; }

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

        public void Save(string patientId)
        {
            
        }

        public int Id { get; set; }
    }
}
