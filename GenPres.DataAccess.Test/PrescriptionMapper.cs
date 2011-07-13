using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using GenPres.DataAccess.DataMapper;

namespace GenPres.DataAccess.Test
{
    public class PrescriptionMapper : DataMapper<PrescriptionBo, Prescription>
    {
        protected PrescriptionBo _bo;
        protected Prescription _dao;

        public PrescriptionMapper(PrescriptionBo rootBo, Prescription dao)
            : base(rootBo, dao)
        {
            _bo = rootBo;
            _dao = dao;
        }

        public override void  InitChildMappings()
        {
            
        }
        public void MapChilds(bool toBo)
        {
            var childMapper = new ChildMapper<PrescriptionBo, Prescription>(_bo, _dao);
            childMapper.Map(x => x.Drug, y => y.Drug, typeof(DrugMapper), toBo);
        }

        public override void MapFromBoToDao()
        {
            MapChilds(false);
        }

        public override void MapFromDaoToBo()
        {
            MapChilds(true);
        }
    }
    
    public class DrugBo : ISavable
    {
        public string Generic { get; set; }
        public List<ComponentBo> Components { get; set; }

        public bool IsNew
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

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

        public int Id { get; set; }
    }

    public class ComponentBo : ISavable
    {

        public string Name;

        public bool IsNew
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

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

        public int Id { get; set; }
    }
    public class ComponentMapper : DataMapper<ComponentBo, Component>
    {
        protected ComponentBo _bo;
        protected Component _dao;

        public ComponentMapper(ComponentBo rootBo, Component dao)
            : base(rootBo, dao)
        {
            _bo = rootBo;
            _dao = dao;
        }

        public override void InitChildMappings()
        {
            throw new NotImplementedException();
        }

        public override void MapFromBoToDao()
        {
            _dao.ComponentName =_bo.Name;
        }

        public override void MapFromDaoToBo()
        {
            _bo.Name = _dao.ComponentName;
        }
    }
    public class DrugMapper : DataMapper<DrugBo, Drug>
    {
        protected DrugBo _bo;
        protected Drug _dao;

        public DrugMapper(DrugBo rootBo, Drug dao)
            : base(rootBo, dao)
        {
            _bo = rootBo;
            _dao = dao;
        }

        public override void InitChildMappings()
        {
            throw new NotImplementedException();
        }

        public override void MapFromBoToDao()
        {
            _dao.Name = _bo.Generic;
            MapChilds(false);
        }


        public override void MapFromDaoToBo()
        {
            _bo.Generic = _dao.Name;
            MapChilds(true);
            
        }
        private void MapChilds(bool toBo)
        {
            var childMapper = new ChildMapper<DrugBo, Drug>(_bo, _dao);
            childMapper.MapCollection(x => x.Components, y => y.Components, typeof(ComponentMapper), toBo);
        }
    }
}
