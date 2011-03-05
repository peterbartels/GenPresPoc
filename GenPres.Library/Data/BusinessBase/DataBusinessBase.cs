using System;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections;
using Csla;
using Csla.Data;
using Csla.Core;
using GenPres.Business;
using System.Reflection;
using DB=GenPres.Database;
using AutoMapper;
using GenPres.Business.Data;
using Csla.Reflection;

namespace GenPres.Business
{
    [Serializable()]
    public abstract class DataBusinessBase<T> : Csla.BusinessBase<T>, IDataBusinessBase
     where T : DataBusinessBase<T>
    {
        [NonSerialized]
        protected object _cachedAccessObject;

        public abstract object GetDataAccessObject();

        private DataAccessManager _dataAccessManager = DataAccessGateway.Instance.DataAccessManager;

        #region Fetch Overrides

        protected override void DataPortal_Fetch(object dataObject)
        {
            using (BypassPropertyChecks)
            {
                _cachedAccessObject = dataObject;
                _dataAccessManager.Mapper.MapToBusiness(dataObject, this);
            }
        }

        protected void Child_Fetch(object dataObject)
        {
            using (BypassPropertyChecks)
            {
                //Mapper.Map(data, this, data.GetType(), this.GetType());
                //FetchChildren(data);
                _cachedAccessObject = dataObject;
                _dataAccessManager.Mapper.MapToBusiness(dataObject, this);
            }
        }
        #endregion

        #region Update Overrides
        protected void Child_Update(object parentData, string keyName)
        {
            DataAccessGateway.Instance.DataAccessManager.Mapper.MapToData(this, this.GetDataAccessObject());
        }

        protected void Child_Update(object parentData, IList listObj, int iterationNr)
        {
            DataAccessGateway.Instance.DataAccessManager.Mapper.MapToDataCollection(parentData, this, this.GetDataAccessObject(), listObj, iterationNr);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            DataAccessGateway.Instance.DataAccessManager.Mapper.MapToData(this, this.GetDataAccessObject());
        }
        #endregion

        #region Insert Overrides
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (BypassPropertyChecks)
            {
                object dataObject = DataAccessGateway.Instance.DataAccessManager.Mapper.MapToData(this, this.GetDataAccessObject());
                Type pType = dataObject.GetType();
                ITable iTable = DataAccessGateway.Instance.DataAccessManager.DataContext.GetTable(pType);
                iTable.InsertOnSubmit(dataObject);
            }
        }
        protected override object GetClone()
        {
            object clone = ObjectCloner.Clone(this);
            if (clone is DataBusinessBase<T>)
            {
                ((DataBusinessBase<T>)clone)._cachedAccessObject = this._cachedAccessObject;
            }
            return clone;
        }

        protected void Child_Insert(object parentData, string keyName)
        {
            DataAccessGateway.Instance.DataAccessManager.Mapper.MapToData(this, this.GetDataAccessObject());
            DataAccessGateway.Instance.DataAccessManager.Mapper.UpdateDataKey(parentData, this.GetDataAccessObject(), keyName);
        }

        protected void Child_Insert(object parentData, IList listObj, int iterationNr)
        {
            DataAccessGateway.Instance.DataAccessManager.Mapper.MapToDataCollection(parentData, this, this.GetDataAccessObject(), listObj, iterationNr);
        }
        #endregion
    }
}

