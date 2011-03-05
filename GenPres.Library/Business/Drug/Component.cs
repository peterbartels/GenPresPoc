using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Security;
using GenPres.Database;

namespace GenPres.Business
{
    [Serializable]
    public class Component : StateBusinessBase<Component>, IDataBusinessBase
    {   
        #region Business Methods

        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.Component();
            return _cachedAccessObject;
        }

        internal static PropertyInfo<SubstanceCollection> SubstanceListProperty = RegisterProperty<SubstanceCollection>(p => p.Substances);
        public SubstanceCollection Substances
        {
            get
            {
                if (!(FieldManager.FieldExists(SubstanceListProperty)))
                {
                    LoadProperty(SubstanceListProperty, SubstanceCollection.NewSubstanceCollection());
                }
                return GetProperty(SubstanceListProperty);
            }
            set
            {
                SetProperty(SubstanceListProperty, value);
            }
        }

        public Substance NewSubstance()
        {
            Substances.SetComponent(this);
            Substance s = Substances.AddNew();
            return s;
        }

        internal Drug GetDrug()
        {
            ComponentCollection parentCollection = (ComponentCollection)this.Parent;
            return parentCollection.GetDrug();
        }

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(Component), new PropertyInfo<int>("Id"));
        public int Id
        {
            get
            {
                int value = GetProperty(IdProperty);
                return value;
            }
            set
            {
                SetProperty(IdProperty, value);
            }
        }

        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(Component), new PropertyInfo<string>("Name"));
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

        internal static PropertyInfo<UnitValue> QuantityProperty = RegisterProperty(typeof(Component), new PropertyInfo<UnitValue>("Quantity"));
        public UnitValue Quantity
        {
            get
            {
                UnitValue value = GetProperty(QuantityProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(QuantityProperty, value);
                }
                return value;
            }
            set
            {
                if (Quantity == null) Quantity = DataPortal.CreateChild<UnitValue>();
                if (Quantity.Id > 0) value.Id = Quantity.Id;
                SetProperty(QuantityProperty, value);
            }
        }

        internal static PropertyInfo<decimal> PackageProperty = RegisterProperty(typeof(Component), new PropertyInfo<decimal>("Package"));
        public decimal Package
        {
            get
            {
                decimal value = GetProperty(PackageProperty);
                return value;
            }
            set
            {
                SetProperty(PackageProperty, value);
            }
        }

        internal static PropertyInfo<decimal> ComponentIncrementProperty = RegisterProperty(typeof(Component), new PropertyInfo<decimal>("ComponentIncrement"));
        public decimal ComponentIncrement
        {
            get
            {
                decimal value = GetProperty(ComponentIncrementProperty);
                return value;
            }
            set
            {
                SetProperty(ComponentIncrementProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> DrugConcentrationProperty = RegisterProperty(typeof(Component), new PropertyInfo<UnitValue>("DrugConcentration"));
        public UnitValue DrugConcentration
        {
            get
            {
                UnitValue value = GetProperty(DrugConcentrationProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(DrugConcentrationProperty, value);
                }
                return value;
            }
            set
            {
                if (DrugConcentration.Id > 0) value.Id = DrugConcentration.Id;
                SetProperty(DrugConcentrationProperty, value);
            }
        }

        internal static PropertyInfo<decimal> SolutionRelationProperty = RegisterProperty(typeof(Component), new PropertyInfo<decimal>("SolutionRelation"));
        public decimal SolutionRelation
        {
            get
            {
                decimal value = GetProperty(SolutionRelationProperty);
                return value;
            }
            set
            {
                SetProperty(SolutionRelationProperty, value);
            }
        }

        internal static PropertyInfo<bool> IsSolutionProperty = RegisterProperty(typeof(Component), new PropertyInfo<bool>("IsSolution"));
        public bool IsSolution
        {
            get
            {
                bool value = GetProperty(IsSolutionProperty);
                return value;
            }
            set
            {
                SetProperty(IsSolutionProperty, value);
            }
        } 

        #endregion

        #region Factory Methods

        public static Component NewComponent()
        {
            return DataPortal.CreateChild<Component>();
        }

        public static Component GetComponent(object childData)
        {
            return DataPortal.FetchChild<Component>(childData);
        }

        private Component()
        { /* Require use of factory methods */ }

        #endregion
        
        protected override void Child_Create()
        {
            IsSolution = false;
            base.Child_Create();
        }
    }
}
