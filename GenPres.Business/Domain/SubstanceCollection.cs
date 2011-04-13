using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    internal class SubstanceCollection : BusinessListBase<SubstanceCollection, Substance>
    {
        public object GetParent()
        {
            return this.Parent;
        }

        protected override object AddNewCore()
        {
            Substance item = DataPortal.CreateChild<Substance>();
            this.Add(item);
            return item;
        }

        #region Factory Methods

        internal static SubstanceCollection NewSubstanceCollection()
        {
            return DataPortal.Create<SubstanceCollection>();
        }

        internal static SubstanceCollection GetSubstanceCollection(object childData)
        {
            return DataPortal.Fetch<SubstanceCollection>(childData);
        }

        private SubstanceCollection()
        { }
        #endregion
    }
}
