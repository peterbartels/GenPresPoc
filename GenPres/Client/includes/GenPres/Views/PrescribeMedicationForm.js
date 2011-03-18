Ext.namespace("View");

View.PrescribeMedicationForm = function(parentView) {

    this.hasForm = false;
    this.parentView = parentView;
    this.FieldData = {};
    this.Controls = {};
	this.Increment = 0;
	this.Id = 0;
	this.IsTemplate = false;
	this.UsesTemplate = false;
	this.dataRetrieved = {};
	
    this.Combinations = [];
    this.Combinations.push(["Dose.Total", "Dose.Quantity", "Prescription.Frequency"]);
    this.Combinations.push(["Prescription.Total", "Prescription.Quantity", "Prescription.Frequency"]);
    
    this.Combinations.push(["Prescription.Time", "Prescription.Rate", "Prescription.Quantity"]);
    this.Combinations.push(["Prescription.Time", "Dose.Rate", "Dose.Quantity"]);
    
    this.Combinations.push(["Substance.DrugConcentration", "Substance.Quantity", "Drug.Quantity"]);
    
    this.Combinations.push(["Dose.Rate", "Prescription.Rate", "Substance.DrugConcentration"]);
    this.Combinations.push(["Dose.Total", "Prescription.Total", "Substance.DrugConcentration"]);
    this.Combinations.push(["Dose.Quantity", "Prescription.Quantity", "Substance.DrugConcentration"]);
    
    this.Combinations.push(["Dose.Total", "Prescription.Total", "Substance.Quantity", "Drug.Quantity"]);
    this.Combinations.push(["Dose.Rate", "Prescription.Rate", "Substance.Quantity", "Drug.Quantity"]);
    this.Combinations.push(["Dose.Quantity", "Prescription.Quantity", "Substance.Quantity", "Drug.Quantity"]);
    
    this.Combinations.push(["Dose.Total", "Prescription.Quantity", "Substance.DrugConcentration", "Prescription.Frequency"]);
    this.Combinations.push(["Prescription.Total", "Dose.Quantity", "Substance.DrugConcentration", "Prescription.Frequency"]);

    this.Combinations.push(["Dose.Total", "Prescription.Quantity", "Substance.Quantity", "Drug.Quantity", "Prescription.Frequency"]);
    this.Combinations.push(["Prescription.Total", "Dose.Quantity", "Substance.Quantity", "Drug.Quantity", "Prescription.Frequency"]);
    
	
	this.latestChangedControl = null;
		
    this.StateManager = new Logic.StateManager();

    this.scenarios = new View.PrescribeMedicationForm.Scenarios(this);
    this.DataProvider = new DataProvider();
    this.latestChangedProperty = "";
    
    this.SubstanceIncrements = [];
    this.ComponentIncrement = 0;
	
	this.hasCustomMedicine = false;
	
    this.NewMedicine = function() {
    	this.clear();
    	var newMedicineWindow = new View.PrescribeMedicationForm.NewMedicine(this);
    }
    
    this.newTemplate = function(){
    	GenPres.SelectedPatient = {
                Unit: "",
                Bed: "",
                FirstName: "TEMPLATE (berekeningen worden niet uitgevoerd)!",
                LastName: "LET OP",
                Birthdate: "",
                Age: "",
                WeightGuess: "",
                Weight:0,
                Length: 0,
                WeightMedication: "",
                WeightActual: "",
                WeightAddmission: "",
                WeightBirth: "",
                LengthGuessed: "",
                LengthActual: "",
                AddmissionDate: "",
                CurrentDate: "",
                Days: "",
                PID: "0"
            }
    	
    	this.parentView.UpdatePatient("0", "TEMPLATE");
    	this.IsTemplate = true;
    	this.clear();
    }
    
    var save = function() {
    	this.UpdateData(true);
    }
    this.save = save;
    
    this.clear = function(skipStoreClear) {
        this.DataProvider.ClearPrescription(this.UpdateControls.createDelegate(this));
        var stores = Stores.PrescribeMedication.GetInstance();
        if(skipStoreClear) stores.clear();
        this.FieldData.Component = {};
        this.CustomSubstanceUnit = "";
        this.CustomComponentUnit = "";
        this.Id = 0;
    }
    
    this.UpdatePatientData = function(weightUnitValue, lengthUnitValue){
    	this.DataProvider.UpdatePatientData(weightUnitValue, lengthUnitValue);
    }
    
    this.IncrementField = function(field, direction){
    	this.UpdateData(field, direction);	
    }

    this.GetPrescriptionById = function(id) {
        this.DataProvider.GetPrescriptionById(id, this.UpdateControlsAfterPrescriptionLoad.createDelegate(this));
    }
    
    this.UpdateControlsAfterPrescriptionLoad = function(data) {
        this.UpdateControls(data);
        var params = {};
        params["generic"] = this.getField("Drug.Name").getValue();
        params["shape"] = this.getField("Drug.Shape").getValue();
        params["route"] = this.getField("Drug.Route").getValue();
        Stores.PrescribeMedication.GetInstance().reloadUnitStores(params);
    }
    
    this.StopPrescription = function(id, status){
    	this.DataProvider.StopPrescription(id, status);
    }
	
    this.getTextByName = function(objName) {
        return this.Controls[objName].getValue();
    }

    this.addProperty = function(className, propertyName, item) {
        var value = item.getValue();
        var objName = className + "." + propertyName;
        if (!this.FieldData[className]) this.FieldData[className] = {};

        this.Controls[objName] = item;

        if (typeof (value.Value) != "undefined") {
			
			if(this.StateManager.GetState(objName) != this.StateManager.state.User){
        		value.Value = 0;
        	}
        }
        
        this.FieldData[className][propertyName] = value;
    }

    this.collectFormData = function(items) {
        for (var i = 0; i < items.length; i++) {
        	var item = items.get(i);
        	if (typeof(item.className) != "undefined" && typeof(item.propertyName) != "undefined") 
        		this.addProperty(item.className, item.propertyName, item);
                    
            if (item.items) this.collectFormData(item.items);
            if (item.form) if(item.form.items) this.collectFormData(item.form.items);
        }
    }

    this.getControllerObj = function(className) {
        var data = this.dataRetrieved;
        switch (className) {
            case "Prescription":
                return data;
            case "Dose":
                return data.PrescriptionDoses[0];
            case "Component":
                return data.Drug.Components[0];
            case "Substance":
                return data.Drug.Components[0].Substances[0];
            case "Drug":
                return data.Drug;
                break;
        }
        return "";
    }
	
	this.SetLatestControlChanged = function(control){
		this.latestChangedControl = control;
	}
	this.GetLatestControlChanged = function(){
		return this.latestChangedControl;
	}
	
    this.UpdateControls = function(data) {
        
    	if(this.DataProvider.busy){
    		return;
    	}
    	this.dataRetrieved = data;
    	
    	this.UsesTemplate = data.UsesTemplate;
    	
    	if(data.Totals.data) GenPres.TotalStore.loadData(data.Totals);
    	this.Id = data.Id;
    	
    	
    	var objInvoke = this.getControllerObj.createDelegate(this);
    	
        if(objInvoke("Substance").CustomIncrement.Unit != "" && objInvoke("Component").CustomIncrement.Unit != ""){
	        this.SubstanceIncrements = [objInvoke("Substance").CustomIncrement.Value];
	        this.CustomSubstanceUnit = objInvoke("Substance").CustomIncrement.Unit;
	        this.ComponentIncrement = objInvoke("Component").CustomIncrement.Value;
	        this.CustomComponentUnit = objInvoke("Component").CustomIncrement.Unit;
        }
        
        for (var c in this.FieldData) {
            var cl = this.FieldData[c];
            for (var p in cl) {
                var property = p;
                var objName = c + "." + property;
				
				if(typeof(this.Controls[objName]) == "undefined") continue;
				
                if (typeof (this.Controls[objName].setValue) == "function") {
                    var controllerObj = this.getControllerObj(c);
                    //if(controllerObj == "") continue;
                    var value = controllerObj[p];
                    this.Controls[objName].suspendEvents(false);
                    if(objName == "Prescription.StartDate" || objName == "Prescription.EndDate"){
                    	if(value=="") {
                    		if(objName == "Prescription.StartDate") value = new Date();
                    	}else{
                    		value = new Date(value);	
                    	}
                    }
                    this.Controls[objName].setValue(value);
                    this.Controls[objName].resumeEvents();
                }
            }
        }
        if(this.Controls["Prescription.AdjustWeight"].spinner.getValue() == 0)
			this.Controls["Prescription.AdjustWeight"].spinner.setValue(GenPres.SelectedPatient.Weight / 1000);
        
        this.scenarios.start(this);
    }
	
    this.checkCombinations = function(controlName){
    	if(this.Controls[controlName].state == "user") return;
    	var combinations = this.Combinations;
    	
    	var stateCount = 0;
    	var foundStateCounts = [];
    	var foundCombinations = [];
    	
		for(var i=0;i<combinations.length;i++){
			stateCount = 0;
			var containsFieldName = false;
			for(var c=0;c<combinations[i].length;c++){
			    if (this.Controls[combinations[i][c]].state == "User" && this.Controls[combinations[i][c]].spinner.getValue() > 0) stateCount++;
				if(combinations[i][c] == controlName){
					containsFieldName = true;
					foundCombinations.push(combinations[i]);
				}
			}
			if(containsFieldName) {
				foundStateCounts.push(stateCount);
			}
		}
		
		for(var i=0;i<foundCombinations.length;i++){
			var foundCombination = foundCombinations[i]; 
			if(foundStateCounts[i] == foundCombination.length){
				for(var c=0;c<foundCombination.length;c++){
					if(foundCombination[c] != controlName){
						this.Controls[foundCombination[c]].setCalculated();
						break;
					}
				}
			}
		}
    }
    
	this.UpdateGrid = function(data){
		this.Id = data.Id;
		GenPres.GridStore.reload();
		this.UpdateControls(data);
		this.scenarios.start(this);
	}
    this.UpdateData = function(obj) {
        this.DataProvider.busy=true;
        
        var items = this.prescribeMedicationPanel.items;
        this.collectFormData(items);
        
        if (obj.objName) {
            this.latestChangedProperty = obj.objName;
            this.checkCombinations(this.latestChangedProperty);
            this.collectFormData(items);
        }

        if (obj == true) save = true; else save = false;
        
        var returnFunc = this.UpdateControls.createDelegate(this);
        if(save){
        	returnFunc = this.UpdateGrid.createDelegate(this);
        }

        if(this.CustomSubstanceUnit != "" && this.CustomComponentUnit != ""){
	        this.FieldData.Substance.CustomIncrement = {};
	        this.FieldData.Component.CustomIncrement = {};
	        
	        this.FieldData.Substance.CustomIncrement.Value = this.SubstanceIncrements[0];
	        this.FieldData.Substance.CustomIncrement.Unit = this.CustomSubstanceUnit;
	        this.FieldData.Component.CustomIncrement.Value = this.ComponentIncrement;
	        this.FieldData.Component.CustomIncrement.Unit = this.CustomComponentUnit;
        }
        
        this.FieldData.Prescription.Id = this.Id;
        this.FieldData.Prescription.IsTemplate = this.IsTemplate;
        this.FieldData.Prescription.UsesTemplate = this.UsesTemplate;
        
        this.DataProvider.UpdatePrescription(
            this.FieldData.Prescription,
            this.FieldData.Dose,
            this.FieldData.Drug,
            this.FieldData.Component,
            this.FieldData.Substance,
            save,
            returnFunc
        );
    } .createDelegate(this);


    this.getField = function(objName) {
        return this.Controls[objName];
    }
	
    this.GoHome = function(){
    	this.prescribeMedicationPanel.getLayout().setActiveItem(0)
    }
    
    this.GetPanel = function() {
        if (typeof this.prescribeMedicationPanel != "undefined") return this.prescribeMedicationPanel;
        this.prescribeMedicationPanel = new Ext.Panel({
            layout:'card',
            region: 'west',
            resizable: true,
            padding: "4 4 4 4",
            id : 'prescribeMedicationPanel', 
            split: true,
            width:940,
            activeItem:0,
            buttonAlign:"left",
            items: [noPatientSelected]
        });

        return this.prescribeMedicationPanel;
    }
    this.SetPatient = function(pid, name) {
        
		
        this.IsTemplate = false;
        this.UsesTemplate = false;
        
        var el = this.prescribeMedicationPanel.getEl();
        var loader = new Ext.LoadMask(el, { msg: 'Bezig met laden...', removeMask: true });
        loader.show();
        
        var func = function(){
	        //reduce memory by just using 1 form
	        if (!this.hasForm) {
	            this.MainForm = new View.PrescribeMedicationForm.MainForm(this);
	            this.prescribeMedicationPanel.add(this.MainForm.formEl);
	            this.prescribeMedicationPanel.getLayout().setActiveItem(1)
	            //this.prescribeMedicationPanel.doLayout();
	            this.hasForm = true;
	            //this.prescribeMedicationPanel.render();
	            this.clear(true);
	        }else{
	        	this.prescribeMedicationPanel.getLayout().setActiveItem(1)
	        	this.clear(false);
	        }
			
			window.setTimeout(function(){
	        	loader.hide();
	        	delete loader;
			}, 30);
			//debugger;
	        var items = this.MainForm.formEl.items;
	        this.collectFormData(items);
	        this.scenarios.start(this);
        }.createDelegate(this);
        window.setTimeout(func, 20);
    }
    return this;
}

