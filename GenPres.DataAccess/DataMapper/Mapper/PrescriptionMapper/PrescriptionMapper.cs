using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.DataAccess.DataMapper.Mapper.PrescriptionMapper
{
    public class PrescriptionMapper : DataMapper<Prescription, Database.Prescription>
    {
        public PrescriptionMapper()
            : base(new GenPresDataContextFactory())
        {
            
        }

        internal PrescriptionMapper(IDataContextFactory context)
            : base(context)
        {
        }

        private void MapChilds(bool toBo, Prescription _bo, Database.Prescription _dao)
        {
            var childMapper = new ChildMapper<Prescription, Database.Prescription>(_bo, _dao, _contextFactory);
            childMapper.Map(x => x.Drug, y => y.Drug, typeof(DrugMapper), toBo);
        }

        public override Database.Prescription MapFromBoToDao(Prescription _bo, Database.Prescription _dao)
        {
            _dao.StartDate = _bo.StartDate;
            MapChilds(false, _bo, _dao);
            return _dao;
        }

        public override Prescription MapFromDaoToBo(Database.Prescription _dao, Prescription _bo)
        {
            if (_dao.StartDate != null) _bo.StartDate = _dao.StartDate.Value;
            _bo.Id = _dao.Id;
            MapChilds(true, _bo, _dao);
            return _bo;
        }
    }
}
