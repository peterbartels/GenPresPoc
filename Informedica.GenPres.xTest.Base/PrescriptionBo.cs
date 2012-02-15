using System;
using System.Collections.Generic;
using GenPres.Business.Domain;

namespace GenPres.xTest.Data
{
    public class PrescriptionBo
    {
        public PrescriptionBo()
        {
            //Drug = new DrugBo();
        }
        public virtual DrugBo Drug { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual bool IsNew { get { return (Id.ToString() != ""); } }

        public virtual Guid Id
        {
            get; set;
        }
    }

    public class DrugBo : ISavable
    {
        public string Generic { get; set; }
        public List<ComponentBo> Components { get; set; }

        public virtual bool IsNew { get { return (Id == 0); } }

        public virtual int Id { get; set; }
    }

    public class ComponentBo : ISavable
    {
        public virtual string Name { get; set; }

        public virtual bool IsNew { get { return (Id == 0); } }

        public virtual int Id { get; set; }
    }
}
