using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business
{
    [Serializable]
    public class Department : BusinessBase<Department>
    {
        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(Department), new PropertyInfo<string>("Name"));
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
