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
    /// Substance class (EditableChild object)
    /// </summary>
    [Serializable]
    public class Substance : StateBusinessBase<Substance>
    {
        /*[NonSerialized]
        public Database.Substance SubstanceData = new GenPres.Database.Substance();*/

        #region Business Methods

        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.Substance();
            return _cachedAccessObject;
        }

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

        internal static PropertyInfo<string> SubstanceNameProperty = RegisterProperty(typeof(Substance), new PropertyInfo<string>("SubstanceName"));
        public string SubstanceName
        {
            get
            {
                string value = GetProperty(SubstanceNameProperty);
                return value;
            }
            set
            {
                SetProperty(SubstanceNameProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> QuantityProperty = RegisterProperty(typeof(Substance), new PropertyInfo<UnitValue>("Quantity"));
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

        internal static PropertyInfo<UnitValue> ComponentConcentrationProperty = RegisterProperty(typeof(Substance), new PropertyInfo<UnitValue>("ComponentConcentration"));
        public UnitValue ComponentConcentration
        {
            get
            {
                UnitValue value = GetProperty(ComponentConcentrationProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(ComponentConcentrationProperty, value);
                }
                return value;
            }
            set
            {
                if (ComponentConcentration.Id > 0) value.Id = ComponentConcentration.Id;
                SetProperty(ComponentConcentrationProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> DrugConcentrationProperty = RegisterProperty(typeof(Substance), new PropertyInfo<UnitValue>("DrugConcentration"));
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

        internal static PropertyInfo<Component> ComponentProperty = RegisterProperty(typeof(Substance), new PropertyInfo<Component>("Component"));
        public Component Component
        {
            get
            {
                Component value = GetProperty(ComponentProperty);
                return value;
            }
            set
            {
                SetProperty(ComponentProperty, value);
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
                if(!value.Contains<decimal>(0) && value.Length > 0)_substanceIncrements = value;
            }
        }


        internal void SetComponent(Component c)
        {
            //this.Component = ((SubstanceCollection)this.Parent).GetComponent();
        }

        internal Component GetComponent()
        {
            return ((SubstanceCollection)this.Parent).GetComponent(); ;
        }
        #endregion
        
        #region Data Access

        /*
        protected override void Child_Create()
        {
            //ComponentConcentration = UnitValue.NewUnitValue();
            //DrugConcentration = UnitValue.NewUnitValue();
            // omit this override if you have no defaults to set
            base.Child_Create();
            Quantity._Factor = new Factor(QuantityProperty);
        }
        
        private void Child_Fetch(Database.Substance substanceData)
        {
            using (BypassPropertyChecks)
            {
                DrugConcentration = DataPortal.Fetch<UnitValue>(substanceData.DrugConcentration);
                ComponentConcentration = DataPortal.Fetch<UnitValue>(substanceData.ComponentConcentration);
                Quantity = DataPortal.Fetch<UnitValue>(substanceData.Quantity);
                Id = substanceData.Id;
                SubstanceName = substanceData.SubstanceName;
            }
        }

        protected void Child_Insert(Component component)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                Database.Substance substanceData = new Database.Substance();
                DataCache.Instance.substanceData = substanceData;

                substanceData.SubstanceName = SubstanceName;
                
                Quantity = Quantity.Save();
                substanceData.Quantity = DataCache.Instance.unitValueData;

                ComponentConcentration = ComponentConcentration.Save();
                substanceData.ComponentConcentration = DataCache.Instance.unitValueData;

                DrugConcentration = DrugConcentration.Save();
                substanceData.DrugConcentration = DataCache.Instance.unitValueData;

                substanceData.Component = DataCache.Instance.componentData;

                FieldManager.UpdateChildren(this);
                ctx.DataContext.Substances.InsertOnSubmit(substanceData);
            }
        }
        private void Child_Update(Component component)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                Database.Substance substanceData = ctx.DataContext.Substances.Single<Database.Substance>(c => c.Id == this.Id);
                substanceData.Component = DataCache.Instance.componentData;
                Quantity = Quantity.Save();
                ComponentConcentration = ComponentConcentration.Save();
                DrugConcentration = DrugConcentration.Save();
                substanceData.SubstanceName = SubstanceName;
                FieldManager.UpdateChildren(this);
                ctx.DataContext.SubmitChanges();
            }
        }
        protected void Child_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Substance, int>(this.Id));
        }*/
        #endregion
        
        #region Factory Methods

        [RunLocal]
        internal static Substance NewSubstance()
        {
            
            return DataPortal.CreateChild<Substance>();
        }

        public Substance()
        { /* Require use of factory methods */ }

        #endregion
    }
}
