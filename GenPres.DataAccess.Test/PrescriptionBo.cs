using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain;

namespace GenPres.DataAccess.Test
{
    public class PrescriptionBo : ISavable
    {
        public PrescriptionBo()
        {
            //Drug = new DrugBo();
        }
        public DrugBo Drug { get; set; }

        public bool IsNew { get; set; }
        
        public void OnCreate()
        {
            throw new NotImplementedException();
        }

        public void OnNew()
        {
            throw new NotImplementedException();
        }

        public void OnInitExisting()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int Id
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
