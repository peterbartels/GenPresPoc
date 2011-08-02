using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.DataAccess.DataMapper.Mapper.PrescriptionMapper
{
    public class PrescriptionMapper : DataMapper<IPrescription, Database.Prescription>
    {
        public PrescriptionMapper()
            : base(new GenPresDataContextManager())
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
