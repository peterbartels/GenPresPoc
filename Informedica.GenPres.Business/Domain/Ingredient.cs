using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    [Serializable]
    internal class Ingredient : BusinessBase<Ingredient>
    {
        internal static PropertyInfo<Dose> DoseProperty = RegisterProperty(typeof(Ingredient), new PropertyInfo<Dose>("Dose"));
        internal Dose Dose
        {
            get
            {
                Dose value = GetProperty(DoseProperty);
                return value;
            }
            set
            {
                SetProperty(DoseProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> QuantityProperty = RegisterProperty(typeof(Ingredient), new PropertyInfo<UnitValue>("Quantity"));
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
                if (Quantity.Id > 0) value.Id = Quantity.Id;
                SetProperty(QuantityProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> ContainerConcentrationProperty = RegisterProperty(typeof(Ingredient), new PropertyInfo<UnitValue>("ContainerConcentration"));
        public UnitValue ContainerConcentration
        {
            get
            {
                UnitValue value = GetProperty(ContainerConcentrationProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(ContainerConcentrationProperty, value);
                }

                return value;
            }
            set
            {
                if (ContainerConcentration.Id > 0) value.Id = ContainerConcentration.Id;
                SetProperty(ContainerConcentrationProperty, value);
            }
        }
        internal static PropertyInfo<UnitValue> TotalConcentrationProperty = RegisterProperty(typeof(Ingredient), new PropertyInfo<UnitValue>("TotalConcentration"));
        public UnitValue TotalConcentration
        {
            get
            {
                UnitValue value = GetProperty(TotalConcentrationProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(TotalConcentrationProperty, value);
                }

                return value;
            }
            set
            {
                if (TotalConcentration.Id > 0) value.Id = TotalConcentration.Id;
                SetProperty(TotalConcentrationProperty, value);
            }
        }

        #region Factory Methods

        protected override void Child_Create()
        {
            Dose = DataPortal.CreateChild<Dose>();
        }

        public static Ingredient NewIngredient()
        {
            return DataPortal.CreateChild<Ingredient>();
        }


        private Ingredient() { /* Require use of factory methods */ }

        #endregion

    }
}
