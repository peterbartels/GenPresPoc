using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    internal class Substance : BusinessBase<Substance>, IIngredient
    {
        private Ingredient _ingredient;

        #region Business Methods

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(Substance), new PropertyInfo<int>("Id"));
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

        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(Substance), new PropertyInfo<string>("Name"));
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


        private decimal[] _substanceIncrements = new decimal[] { };
        public decimal[] SubstanceIncrements
        {
            get
            {
                return _substanceIncrements;
            }
            set
            {
                if (!value.Contains<decimal>(0) && value.Length > 0) _substanceIncrements = value;
            }
        }
        #endregion

        #region Factory Methods

        protected override void Child_Create()
        {
            _ingredient = DataPortal.CreateChild<Ingredient>();
        }

        private Substance(){ /* Require use of factory methods */ }

        #endregion
    }
}
