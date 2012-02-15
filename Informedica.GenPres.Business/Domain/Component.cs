using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
namespace GenPres.Business.Domain
{
    internal class Component : BusinessBase<Component>
    {
        #region Business Methods

        private Ingredient _ingredient;

        internal static PropertyInfo<SubstanceCollection> SubstanceListProperty = RegisterProperty<SubstanceCollection>(p => p.Substances);
        internal SubstanceCollection Substances
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

        internal Substance NewSubstance()
        {
            return Substances.AddNew();
        }

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(Component), new PropertyInfo<int>("Id"));
        internal int Id
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
        internal string Name
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

        internal static PropertyInfo<decimal> ComponentIncrementProperty = RegisterProperty(typeof(Component), new PropertyInfo<decimal>("ComponentIncrement"));
        internal decimal ComponentIncrement
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

        internal static PropertyInfo<decimal> SolutionRelationProperty = RegisterProperty(typeof(Component), new PropertyInfo<decimal>("SolutionRelation"));
        internal decimal SolutionRelation
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
        internal bool IsSolution
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


        public UnitValue Quantity
        {
            get
            {
                return _ingredient.Quantity;
            }
            set
            {
                _ingredient.Quantity = value;
            }
        }

        public UnitValue ContainerConcentration
        {
            get
            {
                return _ingredient.ContainerConcentration;
            }
            set
            {
                _ingredient.ContainerConcentration = value;
            }
        }

        public UnitValue TotalConcentration
        {
            get
            {
                return _ingredient.TotalConcentration;
            }
            set
            {
                _ingredient.TotalConcentration = value;
            }
        }

        public UnitValue DoseQuantity
        {
            get
            {
                return _ingredient.Dose.Quantity;
            }
            set
            {
                _ingredient.Dose.Quantity = value;
            }
        }

        public UnitValue DoseTotal
        {
            get
            {
                return _ingredient.Dose.Total;
            }
            set
            {
                _ingredient.Dose.Total = value;
            }
        }


        public UnitValue DoseRate
        {
            get
            {
                return _ingredient.Dose.Rate;
            }
            set
            {
                _ingredient.Dose.Rate = value;
            }
        }

        #endregion

        #region Factory Methods

        protected override void Child_Create()
        {
            _ingredient = DataPortal.CreateChild<Ingredient>();
            Substances.AddNew();
        }

        public static Component GetComponent(object childData)
        {
            return DataPortal.FetchChild<Component>(childData);
        }

        private Component() { /* Require use of factory methods */ }

        #endregion
    }
}
