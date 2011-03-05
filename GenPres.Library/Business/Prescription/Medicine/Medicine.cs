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
    /// <summary>
    /// Component class (EditableChild object)
    /// </summary>
    [Serializable]
    public class Medicine : StateBusinessBase<Medicine>, IDataBusinessBase
    {
        #region Business Properties


        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.Medicine();
            return _cachedAccessObject;
        }



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

        #region Data Access
        
        /*
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            Quantity = DataPortal.CreateChild<UnitValue>(); ;
            DoseIncrement = DataPortal.CreateChild<UnitValue>();
            ComponentIncrement = DataPortal.CreateChild<UnitValue>();
            base.Child_Create();
        }
        private void Child_Fetch(Database.Medicine medicineData)
        {
            using (BypassPropertyChecks)
            {
                ApplyMapping(medicineData, this);
                //Quantity = DataPortal.Fetch<UnitValue>(medicineData.Quantity);
                //ComponentIncrement = DataPortal.Fetch<UnitValue>(medicineData.ComponentIncrement);
                //DoseIncrement = DataPortal.Fetch<UnitValue>(medicineData.DoseIncrement);
            }
        }
        private void Child_Insert(Prescription prescription)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                using (BypassPropertyChecks)
                {

                    Database.Medicine medicineData = new Database.Medicine();
                    DataCache.Instance.medicineData = medicineData;

                    ApplyMapping(this, medicineData);

                    Quantity = Quantity.Save();
                    medicineData.Quantity = DataCache.Instance.unitValueData;

                    DoseIncrement = DoseIncrement.Save();
                    medicineData.DoseIncrement = DataCache.Instance.unitValueData;

                    ComponentIncrement = ComponentIncrement.Save();
                    medicineData.ComponentIncrement = DataCache.Instance.unitValueData;

                    FieldManager.UpdateChildren(this);
                    ctx.DataContext.Medicines.InsertOnSubmit(medicineData);
                }
            }
        }

        private void Child_Update(Prescription prescription)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                using (BypassPropertyChecks)
                {
                    Database.Medicine medicineData = ctx.DataContext.Medicines.Single<Database.Medicine>(c => c.Id == this.Id);
                    DataCache.Instance.medicineData = medicineData;
                    ApplyMapping(this, medicineData);
                    Quantity.Save();
                    DoseIncrement.Save();
                    ComponentIncrement.Save();
                    
                    ctx.DataContext.SubmitChanges();
                }
            }
        }
        private void Child_DeleteSelf(object parent)
        {
            // TODO: delete values
        }*/
        #endregion
        
        #region Factory Methods

        [RunLocal]
        public static Medicine NewMedicine()
        {
            return DataPortal.Create<Medicine>();
        }

        public static Medicine GetMedicine(object childData)
        {
            return DataPortal.Fetch<Medicine>(childData);
        }

        public Medicine()
        { /* Require use of factory methods */ }

        #endregion
    }
}
