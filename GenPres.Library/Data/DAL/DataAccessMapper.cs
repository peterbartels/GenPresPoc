using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Csla.Reflection;
using Csla.Core;
using AutoMapper;
using System.Data.Linq;

namespace GenPres.Business.Data
{
    public class DataAccessMapper
    {
        private DataAccessManager _dataAccessManager = DataAccessGateway.Instance.DataAccessManager;

        public static void MapKey<TSource, TDestination>(Expression<Func<TSource, object>> sourceMember, Expression<Func<TDestination, object>> destinationMember)
        {
            DataAccessManager dataAccessManager = DataAccessGateway.Instance.DataAccessManager;
            dataAccessManager.Cache.AddKey(
                typeof(TSource), 
                sourceMember.Body, 
                destinationMember.Body
            );
        }
        public static List<DataKeyMap> GetKeys(string type)
        {
            return DataAccessGateway.Instance.DataAccessManager.Cache.GetKeys(type);
        }

        #region Map To Business

        public void MapToBusiness(object dataObject, object businessObject)
        {
            Mapper.Map(dataObject, businessObject, dataObject.GetType(), businessObject.GetType());
            _mapToBusinessObject(dataObject, businessObject);
        }

        private void _mapToBusinessObject(object dataObject, object businessObject)
        {
            List<DataKeyMap> keys = _dataAccessManager.Cache.GetKeys(businessObject.GetType().Name);
            if (keys == null) return;
            foreach (DataKeyMap dataKey in keys)
            {
                PropertyInfo dataProp = dataKey.DataKeyProperty;
                PropertyInfo businessProp = dataKey.ObjectKeyProperty;

                if (businessProp.PropertyType.GetInterfaces().Contains<Type>(typeof(IDataCollection)))
                {
                    _mapToBusinessChildCollection(dataProp, businessProp, dataObject, businessObject);
                }
                else
                {
                    _mapToBusinessChildObject(dataProp, businessProp, dataObject, businessObject);
                }
            }
        }
        private void _mapToBusinessChildObject(PropertyInfo dataProp, PropertyInfo businessProp, object dataObject, object businessObject)
        {
            Csla.Server.ChildDataPortal portal = new Csla.Server.ChildDataPortal();
            object dataValue = dataProp.GetValue(dataObject, null);
            if (dataValue != null)
            {
                object savableObject = portal.Fetch(
                    businessProp.PropertyType,
                    MethodCaller.CallPropertyGetter(dataObject, dataProp.Name)
                );
                MethodCaller.CallPropertySetter(businessObject, businessProp.Name, savableObject);
            }
        }
        private void _mapToBusinessChildCollection(PropertyInfo dataProp, PropertyInfo businessProp, object dataObject, object businessObject)
        {
            Csla.Server.ChildDataPortal portal = new Csla.Server.ChildDataPortal();
            object savableObject = portal.Fetch(businessProp.PropertyType);

            IList dataList = (IList)MethodCaller.CallPropertyGetter(dataObject, dataProp.Name);

            foreach (object item in dataList)
            {
                object collectionDataOject = portal.Fetch(
                    MethodCaller.GetProperty(savableObject.GetType(), "Item").PropertyType,
                    item
                );
                ((IList)savableObject).Add(collectionDataOject);
            }
            MethodCaller.CallPropertySetter(businessObject, businessProp.Name, savableObject);
        }
        #endregion

        #region Map To Data
        public object MapToData(object businessObject, object dataObject)
        {
            this._mapToDataObject(businessObject, dataObject);
            return dataObject;
        }

        public void MapToDataCollection(object parentData, object businessObject, object dataObject, IList listObj, int iterationNr)
        {
            dataObject = _mapToDataObject(businessObject, dataObject);
            if (!listObj.Contains(dataObject))
            {
                listObj.Add(dataObject);
                Type pType = dataObject.GetType();
                ITable iTable =  this._dataAccessManager.DataContext.GetTable(pType);
                iTable.InsertOnSubmit(dataObject);
            }
        }
        private object _mapToDataObject(object businessObject, object dataObject)
        {
            Csla.Data.DataMapper.Map(businessObject, dataObject, true);

            List<DataKeyMap> keys = _dataAccessManager.Cache.GetKeys(businessObject.GetType().Name);
            if (keys == null) return dataObject;

            foreach (DataKeyMap propertyMappingKey in keys)
            {
                PropertyInfo dataProp = propertyMappingKey.DataKeyProperty;
                PropertyInfo businessProp = propertyMappingKey.ObjectKeyProperty;

                //object savableObject = GetCslaProperty(businessProp.Name);
                object CslaObject = MethodCaller.CallPropertyGetter(businessObject, businessProp.Name);

                if (CslaObject == null)
                    continue; //throw exception here? This means the propertyInfo object is non-exisiting in the Business Object

                PropertyInfo dataKey = (from i in keys where i.DataKeyProperty.Name == dataProp.Name select i.DataKeyProperty).SingleOrDefault<PropertyInfo>();
                if (CslaObject is IEditableCollection)
                {
                    IList newListObj = (IList)MethodCaller.CreateInstance(dataProp.PropertyType);

                    if (!((IEditableBusinessObject)businessObject).IsNew)
                    {
                        newListObj = (IList)dataProp.GetValue(dataObject, null);
                    }

                    for (int i = 0; i < ((IList)CslaObject).Count; i++)
                    {
                        /*if (((IEditableBusinessObject)((IList)CslaObject)[i]).IsDirty == false)
                        {
                            continue;
                        }*/
                        Csla.DataPortal.UpdateChild(CslaObject, new object[] { dataObject, newListObj, i });
                    }
                    if (!((IEditableBusinessObject)businessObject).IsNew)
                    {
                        this._dataAccessManager.DataContext.SubmitChanges();
                    }
                    else
                    {
                        if (newListObj.Count > 0) dataProp.SetValue(dataObject, newListObj, null);
                    }
                }
                if (CslaObject is IEditableBusinessObject)
                {
                    Csla.DataPortal.UpdateChild(CslaObject, new object[] { dataObject, dataKey.Name });
                }
            }
            return dataObject;
        }
        #endregion

        public void UpdateDataKey(object parentData, object dataObject, string keyName)
        {
            List<DataKeyMap> keys = _dataAccessManager.Cache.GetKeys(parentData.GetType().Name);
            PropertyInfo dataKey = (from i in keys where i.DataKeyProperty.Name == keyName select i.DataKeyProperty).SingleOrDefault<PropertyInfo>();
            if (dataKey != null)
            {
                MethodCaller.CallPropertySetter(parentData, dataKey.Name, dataObject);
            }
        }
    }
}
