using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    internal class Medicine : BusinessBase<Medicine>
    {
        #region Business Properties

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(Medicine), new PropertyInfo<int>("Id"));
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

        internal static PropertyInfo<string> GenericProperty = RegisterProperty(typeof(Medicine), new PropertyInfo<string>("Generic"));
        public string Generic
        {
            get
            {
                string value = GetProperty(GenericProperty);
                return value;
            }
            set
            {
                SetProperty(GenericProperty, value);
            }
        }
        internal static PropertyInfo<string> ShapeProperty = RegisterProperty(typeof(Medicine), new PropertyInfo<string>("Shape"));
        public string Shape
        {
            get
            {
                string value = GetProperty(ShapeProperty);
                return value;
            }
            set
            {
                SetProperty(ShapeProperty, value);
            }
        }
        internal static PropertyInfo<string> RouteProperty = RegisterProperty(typeof(Medicine), new PropertyInfo<string>("Route"));
        public string Route
        {
            get
            {
                string value = GetProperty(RouteProperty);
                return value;
            }
            set
            {
                SetProperty(RouteProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> QuantityProperty = RegisterProperty(typeof(Medicine), new PropertyInfo<UnitValue>("Quantity"));
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


        internal static PropertyInfo<UnitValue> DoseIncrementProperty = RegisterProperty(typeof(Medicine), new PropertyInfo<UnitValue>("DoseIncrement"));
        public UnitValue DoseIncrement
        {
            get
            {
                UnitValue value = GetProperty(DoseIncrementProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(DoseIncrementProperty, value);
                }
                return value;
            }
            set
            {
                if (DoseIncrement.Id > 0) value.Id = DoseIncrement.Id;
                SetProperty(DoseIncrementProperty, value);
            }
        }


        internal static PropertyInfo<UnitValue> ComponentIncrementProperty = RegisterProperty(typeof(Medicine), new PropertyInfo<UnitValue>("ComponentIncrement"));
        public UnitValue ComponentIncrement
        {
            get
            {
                UnitValue value = GetProperty(ComponentIncrementProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(ComponentIncrementProperty, value);
                }
                return value;
            }
            set
            {
                if (ComponentIncrement.Id > 0) value.Id = ComponentIncrement.Id;
                SetProperty(ComponentIncrementProperty, value);
            }
        }

        #endregion
    }
}
