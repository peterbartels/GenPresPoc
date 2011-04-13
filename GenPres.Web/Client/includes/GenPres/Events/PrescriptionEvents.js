Ext.namespace("Events");

Events.PrescriptionEvents = function(presribeMedicationForm) {

    //Control manager, setters and getters for controls
    var ControlMan = new ControlManager(presribeMedicationForm);
    this.ControlMan = ControlMan;

    //Obsolete?
    this.setGeneric = function(data) {
        this.controlMan.setFrequency(data.Frequency);
        this.controlMan.setFrequencyUnit(data.FrequencyUnit);
        this.controlMan.setQuantity(data.GenericQuantity);
        this.controlMan.setQuantityUnit(data.GenericQuantityUnit);
    }

    //Make a new prescription
    var Prescription = new Logic.Prescription(this);
    tester = Prescription; /*for easy debugging*/
    
    Prescription.scenarios = presribeMedicationForm.scenarios;

    this.updateUnits = function(unit, controlName) {
        /*switch (controlName) {
        case "dosageQuantity": case "dosageQuantityTotal": case "dosageQuantityRate": case "quantity": case "solutionConcentration":

                //if (this.ControlMan.GetValue("dosageQuantityUnit") == "") {
        //this.ControlMan.setUnitFieldValue("dosageQuantity", unit);
        //Prescription["dosageQuantityUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("dosageQuantityTotalUnit") == "") {
        //this.ControlMan.setUnitFieldValue("dosageQuantityTotal", unit);
        //Prescription["dosageQuantityTotalUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("dosageQuantityRateUnit") == "") {
        //this.ControlMan.setUnitFieldValue("dosageQuantityRate", unit);
        //Prescription["dosageQuantityRateUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("quantityUnit") == "") {
        this.ControlMan.setUnitFieldValue("quantity", unit);
        Prescription["quantityUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("solutionConcentrationUnit") == "") {
        this.ControlMan.setUnitFieldValue("solutionConcentration", unit);
        Prescription["solutionConcentrationUnit"] = unit;
        //}

                break;
        case "administrationQuantity": case "administrationQuantityTotal": case "administrationQuantityRate": case "solutionQuantity": case "solutionConcentrationTotal":

                //if (this.ControlMan.GetValue("administrationQuantityUnit") == "") {
        this.ControlMan.setUnitFieldValue("administrationQuantity", unit);
        Prescription["administrationQuantityUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("administrationQuantityTotalUnit") == "") {
        this.ControlMan.setUnitFieldValue("administrationQuantityTotal", unit);
        Prescription["administrationQuantityTotalUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("administrationQuantityRateUnit") == "") {
        this.ControlMan.setUnitFieldValue("administrationQuantityRate", unit);
        Prescription["administrationQuantityRateUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("solutionQuantityUnit") == "") {
        this.ControlMan.setUnitFieldValue("solutionQuantity", unit);
        Prescription["solutionQuantityUnit"] = unit;
        //}
        //if (this.ControlMan.GetValue("solutionConcentrationTotal") == "") {
        this.ControlMan.setComboBoxValue("solutionConcentrationTotal", unit);
        Prescription["solutionConcentrationTotal"] = unit;
        //}

                break;          
        }*/
        //GenPres.PrescriptionEvents.
    }

    this.SavePrescription = function() {
        Prescription.Save();
    }

    this.ClearPrescription = function() {
        Prescription.Clear();
    }

    this.GetPrescriptionById = function(id) {
        Prescription.Retrieve(id);
    }


    var unitTime = new Logic.UnitTime();
    var unitMass = new Logic.UnitMass();
    //create a shorthand
    var form = presribeMedicationForm;

    //general method for handling events
    this.MonitorChanges = function(field, fieldType, propertyName) {

        //shorthand for states
        var states = Logic.StateManager.states;
        Logic.StateManager.fields[propertyName] = states.NotSet;
        ControlMan.controls[propertyName] = fieldType;

        if (fieldType != ControlMan.ControlTypes.Select && fieldType != ControlMan.ControlTypes.Checkbox) ControlMan.AddStateChanger(propertyName);

        switch (fieldType) {

            //event handling for combobox change                                                    
            case ControlMan.ControlTypes.Select:
                if (typeof (field) == "undefined") {
                    debugger;
                }
                field.on("change", function(combo, newVal, oldVal) {
                    if (propertyName == "generic") {
                        Prescription[propertyName] = newVal;
                    }

                    var func = Prescription["Set_" + propertyName];
                    if (typeof func == "function") Prescription["Set_" + propertyName]();

                    presribeMedicationForm.SetLayout();
                    Prescription.Calculate();
                    //this.updateUnits(newVal, propertyName);
                } .createDelegate(this));
                break;

            //event handling for unitfield type                                                    
            case ControlMan.ControlTypes.UnitField:
                ControlMan.controls[propertyName + "Unit"] = fieldType;

                //onchange event
                var onChange = function(context, field) {

                    var value = ControlMan.GetValue(propertyName);
                    Prescription["latestPropertyChanged"] = propertyName;
                    Prescription[propertyName] = value;

                    if (value != 0) {
                        ControlMan.changeState(propertyName, states.User);
                    } else {
                        ControlMan.changeState(propertyName, states.NotSet);
                    }
                    Prescription.Calculate();
                    presribeMedicationForm.SetLayout();
                }

                //events for spin & textfield
                field.field.on('spin', onChange.createDelegate(this, [field], true));

                //field.field.on('blur', onChange.createDelegate(this, [field], true));

                field.field.el.dom.onblur = onChange.createDelegate(this, [field], true);

                //events for unit change
                field.unitfield.unitCombo.on("change", function(combo, newVal, oldVal) {

                    //I found a bug here, dont know why but sometimes it keeps adding Unit at the end.
                    //So if it already ends with Unit, dont change it ;) What a dirty hack
                    //I removed this (maybe it's already fixed?
                    //if ((propertyName.match(/Unit$/) == null)) propertyName = propertyName + "Unit";

                    //this.updateUnits(newVal, propertyName);
                    Prescription[propertyName] = newVal;
                    Prescription.Calculate();

                    if (typeof (this.checkShapeLoad) == "undefined") {
                        GenPres.ShapeUnitStore.on("load", function() {
                            presribeMedicationForm.SetLayout();
                        } .createDelegate(this));
                        this.checkShapeLoad = true;
                    }

                    Stores.PrescribeMedication.GetInstance().GetDosageUnits().baseParams.unit = newVal;
                    Stores.PrescribeMedication.GetInstance().GetDosageUnits().reload();
                    Stores.PrescribeMedication.GetInstance().GetShapeUnits().baseParams.unit = newVal;
                    Stores.PrescribeMedication.GetInstance().GetShapeUnits().reload();

                } .createDelegate(this));
                break;
            //event handling for checkbox type                                                    
            case ControlMan.ControlTypes.Checkbox:
                field.on("check", function(check, value) {
                    Prescription[propertyName] = value;
                    Prescription.Calculate();
                    presribeMedicationForm.SetLayout();

                } .createDelegate(this));
                presribeMedicationForm.SetLayout();
                break;

        }
    }
    
    var stores = Stores.PrescribeMedication.GetInstance();
    GenPres.RouteStore = stores.GetRouteStore();
    GenPres.SolutionStore = stores.GetSolutionStore();
    GenPres.GenericStore = stores.GetGenericStore();
    GenPres.ShapeUnitStore = stores.GetShapeUnits();
    GenPres.DosageUnitStore = stores.GetDosageUnits();
    GenPres.ConcentrationKAEs = stores.GetDosageUnits(); ;
    /*

    //attach events to controls
    var ControlTypes = ControlMan.ControlTypes;

    form.generic = form.MedicineView.generic;
    form.quantity = form.MedicineView.quantity;
    form.duration = form.PrescriptionView.duration;
    form.frequency = form.PrescriptionView.frequency;

    this.MonitorChanges(form.generic, ControlTypes.Select, "generic");
    this.MonitorChanges(form.duration, ControlTypes.UnitField, "duration");
    this.MonitorChanges(form.quantity, ControlTypes.UnitField, "quantity");
    this.MonitorChanges(form.frequency, ControlTypes.UnitField, "frequency");

    form.solution = form.MedicineView.solution;
    form.solutionQuantity = form.MedicineView.solutionQuantity;
    form.solutionConcentration = form.MedicineView.solutionConcentration;
    form.solConcentrationTotalSelect = form.MedicineView.solConcentrationTotalSelect;

    this.MonitorChanges(form.solution, ControlTypes.Select, "solution");
    this.MonitorChanges(form.solutionQuantity, ControlTypes.UnitField, "solutionQuantity");
    this.MonitorChanges(form.solutionConcentration, ControlTypes.UnitField, "solutionConcentration");
    this.MonitorChanges(form.solConcentrationTotalSelect, ControlTypes.Select, "solConcentrationTotalSelect");

    form.dosageQuantity = form.DosageView.dosageQuantity;
    form.dosageQuantityAdjust = form.DosageView.dosageQuantityAdjust;
    this.MonitorChanges(form.dosageQuantity, ControlTypes.UnitField, "dosageQuantity");
    this.MonitorChanges(form.dosageQuantityAdjust, ControlTypes.Select, "dosageQuantityAdjust");

    form.dosageQuantityTotal = form.DosageView.dosageQuantityTotal;
    form.dosageQuantityTotalAdjust = form.DosageView.dosageQuantityTotalAdjust;
    form.dosageQuantityTotalTime = form.DosageView.dosageQuantityTotalTime;

    this.MonitorChanges(form.dosageQuantityTotal, ControlTypes.UnitField, "dosageQuantityTotal");
    this.MonitorChanges(form.dosageQuantityTotalAdjust, ControlTypes.Select, "dosageQuantityTotalAdjust");
    this.MonitorChanges(form.dosageQuantityTotalTime, ControlTypes.Select, "dosageQuantityTotalTime");

    form.dosageQuantityRate = form.DosageView.dosageQuantityRate;
    form.dosageQuantityRateAdjust = form.DosageView.dosageQuantityRateAdjust;
    form.dosageQuantityRateTime = form.DosageView.dosageQuantityRateTime;
    this.MonitorChanges(form.dosageQuantityRate, ControlTypes.UnitField, "dosageQuantityRate");
    this.MonitorChanges(form.dosageQuantityRateAdjust, ControlTypes.Select, "dosageQuantityRateAdjust");
    this.MonitorChanges(form.dosageQuantityRateTime, ControlTypes.Select, "dosageQuantityRateTime");

    form.administrationQuantity = form.AdministrationView.administrationQuantity;
    form.administrationQuantityTotal = form.AdministrationView.administrationQuantityTotal;
    form.administrationQuantityTotalTime = form.AdministrationView.administrationQuantityTotalTime;

    form.remarks = form.AdministrationView.remarks;


    this.MonitorChanges(form.administrationQuantity, ControlTypes.UnitField, "administrationQuantity");
    this.MonitorChanges(form.administrationQuantityTotal, ControlTypes.UnitField, "administrationQuantityTotal");
    this.MonitorChanges(form.administrationQuantityTotalTime, ControlTypes.Select, "administrationQuantityTotalTime");

    form.administrationQuantityRate = form.AdministrationView.administrationQuantityRate;
    form.administrationQuantityRateTime = form.AdministrationView.administrationQuantityRateTime;
    this.MonitorChanges(form.administrationQuantityRate, ControlTypes.UnitField, "administrationQuantityRate");
    this.MonitorChanges(form.administrationQuantityRateTime, ControlTypes.Select, "administrationQuantityRateTime");

    form.continuous = form.PrescriptionView.continuous;
    form.infusion = form.PrescriptionView.infusion;
    form.onrequest = form.PrescriptionView.onrequest;
    form.issolution = form.PrescriptionView.issolution;
    this.MonitorChanges(form.continuous, ControlTypes.Checkbox, "continuous");
    this.MonitorChanges(form.infusion, ControlTypes.Checkbox, "infusion");
    this.MonitorChanges(form.onrequest, ControlTypes.Checkbox, "onrequest");
    this.MonitorChanges(form.issolution, ControlTypes.Checkbox, "issolution");
    

    form.route = form.MedicineView.route;
    form.solutionType = form.MedicineView.solutionType;
    this.MonitorChanges(form.route, ControlTypes.Select, "route");
    this.MonitorChanges(form.solutionType, ControlTypes.Select, "solutionType");

    form.length = form.MedicineView.length;
    form.weight = form.MedicineView.weight;
    form.bsa = form.MedicineView.bsa;
    this.MonitorChanges(form.length, ControlTypes.UnitField, "length");
    this.MonitorChanges(form.weight, ControlTypes.UnitField, "weight");
    this.MonitorChanges(form.bsa, ControlTypes.UnitField, "bsa");



    */
    this.SyncValues = function() {
        for (var name in ControlMan.controls) {
            Prescription[name] = ControlMan.GetValue(name);
        }
    }

    this.SyncValues();

} 