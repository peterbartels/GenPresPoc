using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain.Prescription
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

        public static IDrug NewDrug()
        {
            return new Drug();
        }
    }
}
