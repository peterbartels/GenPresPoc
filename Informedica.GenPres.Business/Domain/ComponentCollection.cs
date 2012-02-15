using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    internal class ComponentCollection : BusinessListBase<ComponentCollection, Component>
    {
        protected override void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }

        protected override object AddNewCore()
        {
            Component item = DataPortal.CreateChild<Component>();
            this.Add(item);
            return item;
        }

        public object GetParent()
        {
            return this.Parent;
        }

        #region Factory Methods

        internal static ComponentCollection NewComponentCollection()
        {
            return DataPortal.CreateChild<ComponentCollection>();
        }

        private ComponentCollection()
        { }
        #endregion
    }
}
