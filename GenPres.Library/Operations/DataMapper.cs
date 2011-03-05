using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Csla;
using Csla.Data;
using Csla.Properties;
using Csla.Reflection;


namespace GenPres.Business.Operations
{
    public static class DataMapper 
    {
        public static void Map(object source, object target)
        {
            foreach (string propertyName in DataMapper.GetPropertyNames(source.GetType()))
            {
               try
                {
                    object sourceValue = MethodCaller.CallPropertyGetter(source, propertyName);
                    object targetValue = MethodCaller.CallPropertyGetter(target, propertyName);

                    if (sourceValue is Csla.Core.IEditableCollection)
                    {
                        System.Collections.IList sourceList = (System.Collections.IList)sourceValue;
                        System.Collections.IList targetList = (System.Collections.IList)targetValue;
                        for (int i = 0; i < sourceList.Count; i++)
                        {
                            if (i < targetList.Count)
                            {
                                Map(sourceList[i], targetList[i]);
                            }
                            else
                            {
                                targetList.Add(sourceList[i]);
                            }
                        }
                    }
                    else if (targetValue == null)
                        Csla.Data.DataMapper.SetPropertyValue(target, propertyName, sourceValue);
                    else if (sourceValue is Csla.Core.ISavable)
                    {
                        Map(sourceValue, targetValue);
                    }
                    else
                    {
                        Csla.Data.DataMapper.SetPropertyValue(target, propertyName, sourceValue);
                    }
                }
                catch
                {
                    /*For now we do nothing, alternatively we could throw an exception here if a property cannot be mapped*/
                    /*Like Csla, a ignoreList could be added to provide type checking*/
                }
            }
        }


        private static IList<string> GetPropertyNames(Type sourceType)
        {
            List<string> result = new List<string>();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(sourceType);
            foreach (PropertyDescriptor item in props)
                if (item.IsBrowsable)
                    result.Add(item.Name);
            return result;
        }
    }
}
