using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using GenPres.DataAccess.DataMapper;

namespace GenPres.DataAccess.Test
{
    public class PrescriptionMapper : DataMapper<PrescriptionBo, Prescription>
    {
        public PrescriptionMapper()
            : base(new TestDataContextFactory())
        {
            
        }

        public void MapChilds(bool toBo, PrescriptionBo _bo, Prescription _dao)
        {
            var childMapper = new ChildMapper<PrescriptionBo, Prescription>(_bo, _dao, _contextFactory);
            childMapper.Map(x => x.Drug, y => y.Drug, typeof(DrugMapper), toBo);
        }

        public override Prescription MapFromBoToDao(PrescriptionBo rootBo, Prescription dao)
        {
            MapChilds(false, rootBo, dao);
            return dao;
        }

        public override PrescriptionBo MapFromDaoToBo(Prescription dao, PrescriptionBo rootBo)
        {
            MapChilds(true, rootBo, dao);
            return rootBo;
        }
    }
    
    public class DrugBo : ISavable
    {
        public string Generic { get; set; }
        public List<ComponentBo> Components { get; set; }

        public bool IsNew {get; set; }

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

        public int Id { get; set; }
    }

    public class ComponentBo : ISavable
    {

        public string Name;

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

        public int Id { get; set; }
    }
    public class ComponentMapper : DataMapper<ComponentBo, Component>
    {
        public ComponentMapper()
            : base(new TestDataContextFactory())
        {
            
        }

        public override Component MapFromBoToDao(ComponentBo _bo, Component _dao)
        {
            _dao.ComponentName = _bo.Name;
            return _dao;
        }

        public override ComponentBo MapFromDaoToBo(Component _dao, ComponentBo _bo)
        {
            _bo.Name = _dao.ComponentName;
            return _bo;
        }
    }
    public class DrugMapper : DataMapper<DrugBo, Drug>
    {
        public DrugMapper()
            : base(new TestDataContextFactory())
        {
            
        }

        private void MapChilds(bool toBo, DrugBo _bo, Drug _dao)
        {
            var childMapper = new ChildMapper<DrugBo, Drug>(_bo, _dao, _contextFactory);
            childMapper.MapCollection(x => x.Components, y => y.Components, typeof(ComponentMapper), toBo);
        }

        public override Drug MapFromBoToDao(DrugBo _bo, Drug _dao)
        {
            _dao.Name = _bo.Generic;
            MapChilds(false, _bo, _dao);
            return _dao;
        }

        public override DrugBo MapFromDaoToBo(Drug _dao, DrugBo _bo)
        {
            _bo.Generic = _dao.Name;
            MapChilds(true, _bo, _dao);
            return _bo;
        }
    }
}
