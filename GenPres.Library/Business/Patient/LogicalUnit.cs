using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business
{
    [Serializable]
    public class LogicalUnit : BusinessBase<LogicalUnit>
    {

        internal static PropertyInfo<string> IdProperty = RegisterProperty(typeof(LogicalUnit), new PropertyInfo<string>("Id"));
        public string Id
        {
            get
            {
                string value = GetProperty(IdProperty);
                return value;
            }
            set
            {
                SetProperty(IdProperty, value);
            }
        }

        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(LogicalUnit), new PropertyInfo<string>("Name"));
        public string Name
        {
            get
            {
                string value = GetProperty(NameProperty);
                return value;
            }
            set
            {
                SetProperty(NameProperty, value);
            }
        }

    }
}
