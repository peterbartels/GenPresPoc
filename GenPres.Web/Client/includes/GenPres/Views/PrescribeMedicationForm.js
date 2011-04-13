Ext.namespace("View");

View.PrescribeMedicationForm = function(parentView) {

    this.hasForm = false;
    this.parentView = parentView;
    
    this.FieldData = {};
    this.Controls = {};
	this.Id = 0;
	this.IsTemplate = false;
	this.UsesTemplate = false;
	this.dataRetrieved = {};
	
    this.Combinations = [];
    this.Combinations.push(["DoseTotal", "DoseQuantity", "PrescriptionFrequency"]);
    this.Combinations.push(["AdminTotal", "AdminQuantity", "PrescriptionFrequency"]);
    
    this.Combinations.push(["PrescriptionTime", "AdminRate", "AdminQuantity"]);
    this.Combinations.push(["PrescriptionTime", "DoseRate", "DoseQuantity"]);
    
    this.Combinations.push(["SubstanceDrugConcentration", "SubstanceQuantity", "DrugQuantity"]);
    
    this.Combinations.push(["DoseRate", "AdminRate", "SubstanceDrugConcentration"]);
    this.Combinations.push(["DoseTotal", "AdminTotal", "SubstanceDrugConcentration"]);
    this.Combinations.push(["DoseQuantity", "AdminQuantity", "SubstanceDrugConcentration"]);
    
    this.Combinations.push(["DoseTotal", "AdminTotal", "SubstanceQuantity", "DrugQuantity"]);
    this.Combinations.push(["DoseRate", "AdminRate", "SubstanceQuantity", "DrugQuantity"]);
    this.Combinations.push(["DoseQuantity", "AdminQuantity", "SubstanceQuantity", "DrugQuantity"]);

    this.Combinations.push(["DoseTotal", "AdminQuantity", "SubstanceDrugConcentration", "PrescriptionFrequency"]);
    this.Combinations.push(["AdminTotal", "DoseQuantity", "SubstanceDrugConcentration", "PrescriptionFrequency"]);

    this.Combinations.push(["DoseTotal", "AdminQuantity", "SubstanceQuantity", "DrugQuantity", "PrescriptionFrequency"]);
    this.Combinations.push(["AdminTotal", "DoseQuantity", "SubstanceQuantity", "DrugQuantity", "PrescriptionFrequency"]);
	
	this.latestChangedControl = null;
		
    this.StateManager = new Logic.StateManager();

    this.scenarios = new View.PrescribeMedicationForm.Scenarios(this);
    this.DataProvider = new DataProvider();
    this.latestChangedProperty = "";
    

    this.NewMedicine = function() {
    	var newMedicineWindow = new View.PrescribeMedicationForm.NewMedicine(this);
        newMedicineWindow.LoadWindow();
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
        this.FieldData.Medicine = {};
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
        params["generic"] = this.getField("DrugName").getValue();
        params["shape"] = this.getField("DrugShape").getValue();
        params["route"] = this.getField("DrugRoute").getValue();
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
        var objName = className + propertyName;
        if (!this.FieldData[objName]) this.FieldData[objName] = {};

        this.Controls[objName] = item;

        if (typeof (value.Value) != "undefined") {
			
			if(this.StateManager.GetState(objName) != this.StateManager.state.User){
        		value.Value = 0;
        	}
        }
        
        this.FieldData[objName] = value;
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
        return data;
    }
	
	this.SetLatestControlChanged = function(control){
		this.latestChangedControl = control;
	}
	this.GetLatestControlChanged = function(){
		return this.latestChangedControl;
	}

    this.SetMedcine = function(medicineObj){
        this.FieldData.Medicine = medicineObj;
    }
    
    this.UpdateControls = function(data) {
    	if(this.DataProvider.busy){
    		return;
    	}
    	this.dataRetrieved = data;
    	
    	this.UsesTemplate = data.PrescriptionUsesTemplate;
    	
    	if(data.Totals.data) GenPres.TotalStore.loadData(data.Totals);
    	this.Id = data.PrescriptionId;
    	
    	var objInvoke = this.getControllerObj.createDelegate(this);
        
        for (var c in this.FieldData) {
            var objName = c;

            if(typeof(this.Controls[objName]) == "undefined") continue;

            if (typeof (this.Controls[objName].setValue) == "function") {

                var controllerObj = this.getControllerObj(c);
                //if(controllerObj == "") continue;
                var value = controllerObj[c];
                this.Controls[objName].suspendEvents(false);
                if(objName == "PrescriptionStartDate" || objName == "PrescriptionEndDate"){
                    if(value=="") {
                        //if(objName == "PrescriptionStartDate") value = new Date();
                    }else{
                        //value = new Date(value);
                    }
                }
                this.Controls[objName].setValue(value);
                this.Controls[objName].resumeEvents();
            }
        }
        
        if(this.Controls["PrescriptionAdjustWeight"].spinner.getValue() == 0)
			this.Controls["PrescriptionAdjustWeight"].spinner.setValue(GenPres.SelectedPatient.Weight / 1000);
        
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
		//TEMPWEG this.Id = data.Id;
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
        
        this.FieldData.PrescriptionId = this.Id;
        //TEMPWEG this.FieldData.Prescription.PrescriptionIsTemplate = this.IsTemplate;
        //TEMPWEG this.FieldData.Prescription.PrescriptionUsesTemplate = this.UsesTemplate;

        this.DataProvider.UpdatePrescription(
            this.FieldData,
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

