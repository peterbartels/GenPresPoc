using GenPres.Business.Domain.Prescriptions;
using GenPres.DataAccess.Managers;

namespace GenPres.DataAccess.DAO.Mapper.PrescriptionMapper
{
    public class PrescriptionMapper : DataMapper<IPrescription, Database.Prescription>
    {
        public PrescriptionMapper()
            : base(StructureMap.ObjectFactory.GetInstance<IDataContextManager>())
        {
            
        }

        internal PrescriptionMapper(IDataContextManager context)
            : base(context)
        {
        }

        private void MapChilds(bool toBo, IPrescription _bo, Database.Prescription _dao)
        {
            var childMapper = new ChildMapper<IPrescription, Database.Prescription>(_bo, _dao, ContextManager);
            childMapper.Map(x => x.Drug, y => y.Drug, typeof(DrugMapper), toBo);
        }

        public override Database.Prescription MapFromBoToDao(IPrescription _bo, Database.Prescription _dao)
        {
            _dao.StartDate = _bo.StartDate;
            MapChilds(false, _bo, _dao);
            return _dao;
        }

        public override IPrescription MapFromDaoToBo(Database.Prescription _dao, IPrescription _bo)
        {
            if (_dao.StartDate != null) _bo.StartDate = _dao.StartDate.Value;
            _bo.Id = _dao.Id;
            MapChilds(true, _bo, _dao);
            return _bo;
        }
    }
}
