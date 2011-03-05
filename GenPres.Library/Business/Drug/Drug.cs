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
    public class Drug : StateBusinessBase<Drug>, IDataBusinessBase
    {
        
        #region Business Methods

        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.Drug();
            return _cachedAccessObject;
        }

        internal static PropertyInfo<ComponentCollection> ComponentListProperty = RegisterProperty<ComponentCollection>(p => p.Components);
        public ComponentCollection Components
        {
            get
            {
                if (!(FieldManager.FieldExists(ComponentListProperty)))
                {
                    LoadProperty(ComponentListProperty, ComponentCollection.NewComponentCollection());
                }
                return GetProperty(ComponentListProperty);
            }
            set
            {
                SetProperty(ComponentListProperty, value);
            }
        }

        public Component NewComponent()
        {
            Components.SetDrug(this);
            return Components.AddNew();;
        }

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(Drug), new PropertyInfo<int>("Id"));
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

        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(Drug), new PropertyInfo<string>("Name"));
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


        internal static PropertyInfo<string> SolutionTypeProperty = RegisterProperty(typeof(Drug), new PropertyInfo<string>("SolutionType"));
        public string SolutionType
        {
            get
            {
                string value = GetProperty(SolutionTypeProperty);
                return value;
            }
            set
            {
                SetProperty(SolutionTypeProperty, value);
            }
        }


        internal static PropertyInfo<string> ShapeProperty = RegisterProperty(typeof(Drug), new PropertyInfo<string>("Shape"));
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

        internal static PropertyInfo<string> RouteProperty = RegisterProperty(typeof(Drug), new PropertyInfo<string>("Route"));
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
        /*
        internal static PropertyInfo<UnitValue> ComponentKAEProperty = RegisterProperty(typeof(Drug), new PropertyInfo<UnitValue>("ComponentKAE"));
        public UnitValue ComponentKAE
        {
            get
            {
                UnitValue value = GetProperty(ComponentKAEProperty);
                if (value == null)
                {
                    value = UnitValue.EmptyUnitValue();
                    LoadProperty(ComponentKAEProperty, value);
                }
                return value;
            }
            set
            {
                if (ComponentKAE.Id > 0) value.Id = ComponentKAE.Id;
                SetProperty(ComponentKAEProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> ConcentrationKAEProperty = RegisterProperty(typeof(Drug), new PropertyInfo<UnitValue>("ConcentrationKAE"));
        public UnitValue ConcentrationKAE
        {
            get
            {
                return UnitValue.EmptyUnitValue();
            }
        }*/

        internal static PropertyInfo<UnitValue> QuantityProperty = RegisterProperty(typeof(Drug), new PropertyInfo<UnitValue>("Quantity"));
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
        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //ValidationRules.AddRule(RuleMethod, "");
        }

        #endregion

        #region Authorization Rules

        protected override void AddAuthorizationRules()
        {
            //AuthorizationRules.AllowCreate(typeof(Prescription), GenPresApp.ADMIN_ROLE);
            //AuthorizationRules.AllowEdit(typeof(Prescription), GenPresApp.ADMIN_ROLE);
        }

        internal static void AddObjectAuthorizationRules()
        {
            //AuthorizationRules.AllowCreate(typeof(Prescription), GenPresApp.ADMIN_ROLE);
            //AuthorizationRules.AllowEdit(typeof(Prescription), GenPresApp.ADMIN_ROLE);
        }

        #endregion

        #region Factory Methods

        public static Drug NewDrug()
        {
            return DataPortal.Create<Drug>();
        }

        public static Drug GetDrug(object childData)
        {
            return DataPortal.Fetch<Drug>(childData);
        }

        public Drug()
        { /* Require use of factory methods */ }

        #endregion
        
        #region Data Access


        protected override void DataPortal_Create()
        {
            base.Child_Create();
        }/*
        private void Child_Fetch(Database.Drug drugData)
        {
            using (BypassPropertyChecks)
            {
                ApplyMapping(drugData, this);
                Quantity = DataPortal.Fetch<UnitValue>(drugData.Quantity);
                Id = drugData.Id;
                this.Components.SetDrug(this);
                for (int i = 0; i < drugData.Components.Count; i++)
                {
                    Component c = DataPortal.FetchChild<Component>(drugData.Components[i]);
                    this.Components.Add(c);
                }
            }
        }*/
        
        private void Child_Insert(Prescription prescription)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                using (BypassPropertyChecks)
                {
                    //Database.Drug drugData = new Database.Drug();
                    //DataCache.Instance.drugData = drugData;
                    //ApplyMapping(this, drugData);

                    //Quantity = Quantity.Save();
                    //drugData.Quantity = DataCache.Instance.unitValueData;
                    
                    //FieldManager.UpdateChildren(this);
                    //ctx.DataContext.Drugs.InsertOnSubmit(drugData);
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
                    Database.Drug data = ctx.DataContext.Drugs.Single<Database.Drug>(c => c.Id == this.Id);
                    //DataCache.Instance.drugData = data;
                    //ApplyMapping(this, data);
                    Quantity = Quantity.Save();
                    FieldManager.UpdateChildren(this);
                    ctx.DataContext.SubmitChanges();
                }
            }
        }
        private void Child_DeleteSelf(object parent)
        {
            // TODO: delete values
        }
         
        #endregion
    }
}
