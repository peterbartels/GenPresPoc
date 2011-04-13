Ext.namespace("Logic");

Logic.Prescription = function(EventModel) {
    this.generic = "";
    this.frequency = 0;
    this.frequencyUnit = "";
    this.quantity = 0;
    this.quantityUnit = "";
    this.duration = '';
    this.solution = null;
    this.solutionQuantity = null;
    this.solutionQuantityUnit = null;
    this.hasSubComponents = false;

    this.id = 0;

    this.solutionConcentration = null;
    this.solutionConcentrationUnit = null;
    this.solutionConcentrationUnitTotal = null;

    this.dosageQuantity = null;
    this.dosageQuantityUnit = null;
    this.dosageQuantityAdjust = null;

    this.dosageQuantityTotal = null;
    this.dosageQuantityTotalUnit = null;
    this.dosageQuantityTotalAdjust = null;
    this.dosageQuantityTotalTime = null;

    this.dosageQuantityRate = null;
    this.dosageQuantityRateUnit = null;
    this.dosageQuantityRateAdjust = null;
    this.dosageQuantityRateTime = null;

    this.administrationQuantity = null;
    this.administrationQuantityUnit = null;

    this.administrationQuantityTotal = null;
    this.administrationQuantityTotalUnit = null;
    this.administrationQuantityTotalTime = null;

    this.administrationQuantityRate = null;
    this.administrationQuantityRateUnit = null;
    this.administrationQuantityRateTime = null;

    this.infusion = null;
    this.continuous = null;

    var ControlManager = EventModel.ControlMan;
    var dataProvider = new DataProvider();
    var states = Logic.StateManager.states;
    var fieldStateCollection = Logic.StateManager.fields;
    this.scenarios = null;

    this.Save = function() {
        dataProvider.SavePrescription(this);
    }

    this.Clear = function() {
        dataProvider.ClearPrescription(this);
        var baseParams = {
            route: "",
            shape: "",
            unitgroup: "",
            generic: ""
        }
        GenPres.ShapeUnitStore.baseParams = baseParams;
        GenPres.DosageUnitStore.baseParams = baseParams;
        GenPres.GenericStore.baseParams = baseParams;
        GenPres.SolutionStore.baseParams = baseParams;
        GenPres.RouteStore.baseParams = baseParams;

        GenPres.ShapeUnitStore.reload();
        GenPres.DosageUnitStore.reload();
        GenPres.GenericStore.reload();
        GenPres.SolutionStore.reload();
        GenPres.RouteStore.reload();
    }

    this.Calculate = function() {
        dataProvider.CalcPrescription(this);
    }

    this.Retrieve = function(id) {
        dataProvider.GetPrescriptionById(this, id);
    }

    this.DataUpdate = function(data, setUserState, allowZeroValues) {
        for (var name in data) {
            var value = data[name];
            if (name == "total") {
                GenPres.TotalStore.loadData(value)
                continue;
            }
            if (name == "id") {
                this.id = value;
                continue;
            }

            if (HelperFunctions.IsNumeric(value) && value != "") {
                value = parseFloat(parseFloat(value).toFixed(6))
            }
            ControlManager.SetValue(name, value, setUserState, allowZeroValues);
        }
        this.scenarios.start();
    }

    this.GetValue = function(key) {
        if (key == "solConcentrationTotalSelect") {
            //debugger;
        }
        return ControlManager.GetValue(key);
    }

    var UnitGroupPharm = ["stuk"];

    this.getData = function(data) {
        var dataArr = [];
        for (var i = 0; i < data.length; i++) {
            dataArr.push([data[i]]);
        }
        return dataArr;
    }

    this.Set_solution = function() {
        var form = ControlManager.getForm();

        var solutionChoice = form["solution"].getValue();
        GenPres.GenericStore.baseParams.shape = solutionChoice;
        GenPres.GenericStore.reload();

        GenPres.RouteStore.baseParams.shape = solutionChoice;
        GenPres.RouteStore.reload();

        GenPres.ShapeUnitStore.baseParams.shape = solutionChoice;
        GenPres.ShapeUnitStore.reload();

        GenPres.DosageUnitStore.baseParams.shape = solutionChoice;
        GenPres.DosageUnitStore.reload();

        GenPres.ConcentrationKAEs.baseParams.shape = solutionChoice;
        GenPres.ConcentrationKAEs.reload();

        /*var solutionChoice = form["solution"].getValue();
        this.hasSubComponents = false;
        if (solutionChoice == "Zetpil") {

            Ext.get("durationCol").setStyle("display", "none");
        Ext.get("continuousCol").setStyle("display", "none")
        Ext.get("infusionCol").setStyle("display", "none")

            form["solConcentrationTotalSelect"].getStore().loadData(this.getData(UnitGroupPharm), false);
        form["solConcentrationTotalSelect"].setValue(UnitGroupPharm[0], true);

            form["administrationQuantity"].unitfield.setUnitData(this.getData(UnitGroupPharm));
        form["administrationQuantityTotal"].unitfield.setUnitData(this.getData(UnitGroupPharm));

            form["solutionQuantity"].unitfield.setUnitData(this.getData(UnitGroupPharm));

            ControlManager.SetValue("duration", 1, true);
        ControlManager.SetValue("durationUnit", "sec");

            ControlManager.SetValue("quantity", this.generic.GenericQuantity, true);
        ControlManager.SetValue("solutionQuantity", 1, true);

        } else {
        if (solutionChoice != "Infuusvloeistof") {
        this.hasSubComponents = true;
        }
        Ext.get("durationCol").setStyle("display", "");
        Ext.get("continuousCol").setStyle("display", "")
        Ext.get("infusionCol").setStyle("display", "")
        }
        Ext.get("frequencyCol").setStyle("display", "");
        Ext.get("quantityCol").setStyle("display", "");
        Ext.get("solutionCol").setStyle("display", "")
        Ext.get("concentrationCol").setStyle("display", "")
        Ext.get("solutionQuantityCol").setStyle("display", "")
        Ext.get("frequencyCol").setStyle("display", "")
        Ext.get("doseCol").setStyle("display", "")
        Ext.get("administrationCol").setStyle("display", "")
        this.doCalculation();*/
    }

    this.Set_generic = function() {
        var form = ControlManager.getForm();

        var genericChoice = form["generic"].getValue();
        GenPres.SolutionStore.baseParams.generic = genericChoice;
        GenPres.SolutionStore.reload();

        GenPres.RouteStore.baseParams.generic = genericChoice;
        GenPres.RouteStore.reload();

        GenPres.ShapeUnitStore.baseParams.generic = genericChoice;
        GenPres.ShapeUnitStore.reload();

        GenPres.DosageUnitStore.baseParams.generic = genericChoice;
        GenPres.DosageUnitStore.reload();

        GenPres.ConcentrationKAEs.baseParams.generic = genericChoice;
        GenPres.ConcentrationKAEs.reload();
        //var form = ControlManager.getForm();
        //form["solution"].getStore().loadData(this.getData(this.generic.Package), false);
        //Ext.get("solutionCol").setStyle("display", "");
    };

    this.Set_solutionType = function() {
        var form = ControlManager.getForm();

        //alert('f');
        //var genericChoice = form["generic"].getValue();
        //GenPres.SolutionStore.baseParams.generic = genericChoice;
        //GenPres.SolutionStore.reload();
    }
    this.Set_route = function() {
        var form = ControlManager.getForm();
        var routeChoice = form["route"].getValue();

        this.Calculate();

        GenPres.SolutionStore.baseParams.route = routeChoice;
        GenPres.SolutionStore.reload();

        GenPres.GenericStore.baseParams.route = routeChoice;
        GenPres.GenericStore.reload();

        GenPres.ShapeUnitStore.baseParams.route = routeChoice;
        GenPres.ShapeUnitStore.reload();

        GenPres.DosageUnitStore.baseParams.route = routeChoice;
        GenPres.DosageUnitStore.reload();

        GenPres.ConcentrationKAEs.baseParams.route = routeChoice;
        GenPres.ConcentrationKAEs.reload();


        //alert('q');
    }
}
