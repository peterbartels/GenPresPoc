Ext.namespace("View");
Ext.namespace("Factory");

View.Component = function(Factory, infusionId, ExistingPrescription){
	var stores = Stores.PrescribeMedication.GetInstance();
	this.index = null;
	this.infusionId = infusionId;
	this.store = stores.CreateTPNComponentStore();
	this.TPNForm = Factory.TPNForm;
	this.factory = Factory;
	this.value = "";
	this.deleteIcon = null;
	this.existingPrescription = ExistingPrescription;
	this.customStore = false;
	this.Quantity = null;
	this.componentSelector = null;
	this.isSolution = false;
	this.viewEl = false;
	this.reloadStore = function(allVals){
		var params = "";
		for(var i=0;i<allVals.length;i++){
			if(allVals[i] != this.value){
				params = params + allVals[i] + ";";
			}
		}
		
		var baseParams = { selections: params };
		if(this.isSolution){
			baseParams = { OnlySolutionComponents:true, selections: params };
		}
		this.store.baseParams = baseParams;
		this.store.reload();
	}
	
	this.GetComponentRow = function(name){
		var label = name;
    	this.Quantity = new ValueField({
			label: '',
			formContainer : this.TPNForm,
			unitStore: stores.GetShapeUnits(),
			className:this.infusionId+'TPNPrescription' + this.index,
			unitValue : "ml",
			width:200
		});
		this.Quantity.increments = [0.1];
		
		if(this.customStore) {
			label = this.label;
			this.store.baseParams.customParam = this.customParam;
		}
		if(!this.isSolution) this.store.load();
    	this.componentSelector = new Ext.form.ComboBox({
	        store: this.store,
	        width: 240,
	        emptyText: '',
	        className: this.infusionId+'TPNDrug' + this.index,
	        columnWidth:1,
	        fieldLabel: label
	    });
	    var changeFunction = function(select,event,index){
	    	if(select.getValue() != ""){
	    		if(this.customStore == false) this.TPNForm.addRow.createDelegate(this.TPNForm, [this.index], true)();
	    		if(!this.existingPrescription) this.deleteIcon.setDisabled(false);
	    		this.value = select.getValue();
	    		this.TPNForm.UpdateForm({}, false);
	    		this.factory.reloadStores(this.index);
	    	}else{
	    		this.factory.deleteComponent(this.index);
	    	}
	    	
	    }.createDelegate(this);
	    
	    this.componentSelector.on("select", changeFunction);
	    this.componentSelector.on("change", changeFunction);
	    
    	var componentSelectorForm = {layout:"form",items:[this.componentSelector]};
    	var quantityForm = {hideLabel:true,layout:"form",items:[this.Quantity],width:120};
    	this.deleteIcon = new Ext.Button({text:'Verwijder',disabled:true});
    	
    	var componentViewItems = [componentSelectorForm,quantityForm];
    	if(this.isSolution == false) componentViewItems.push(this.deleteIcon);
    	this.viewEl = new Ext.Container({
    		layout:"column",
    		items:componentViewItems
    	});
    	
    	this.deleteIcon.on("click", function(){
    		//newItem.destroy(true);
    		this.factory.deleteComponent(this.index);
    	}.createDelegate(this))
    	return this.viewEl;
	},
	
	this.CreateComponentView = function(index/*, customStore*/){
		this.index = index;
		var label = 'Component ' + (this.index);
		return this.GetComponentRow(label);
	}
	this.GetQuantityValue = function(){
		return this.Quantity.getValue();
	}
	this.GetQuantityState = function(){
		return this.Quantity.getState();
	}
	this.GetName = function(){
		return this.componentSelector.getValue();
	}
	this.setIndex = function(index){
		this.index = index;
		this.Quantity.className = this.infusionId+'TPNDrug' + this.index;
		this.Quantity.el.id = this.Quantity.className;
		this.componentSelector.className = this.infusionId+'TPNDrug' + this.index;
		this.componentSelector.setFieldLabel("Component "+this.index)
		
	}
}
Factory.Component = function(TPNForm) {
	this.TPNForm = TPNForm;
	this.components = [];
	this.changedProperties = [];
	
	this.deleteComponent = function(index){
		
		var components = this.components.splice(index,1);
		components[0].viewEl.destroy(true);
		
		for(var i=1; i<this.components.length;i++){
			this.components[i].setIndex(i);
		}
		
		if(this.components.length == 1){
			this.TPNForm.addRow(1);
		} 
		else 
			this.reloadStores(-1);
		
		this.TPNForm.UpdateForm({}, false);		
		
	}
	this.GetComponent = function(id, existingPrescription){
		var componentView = new View.Component(this, id, existingPrescription);
		return componentView;
	}
	this.getAllComponentNames = function(){
		var allVals = [];
		var start = 0;
		if(this.TPNForm != null) start = 1;
		for(var i=start; i<this.components.length;i++){
			if(this.components[i].value != ""){
				allVals[allVals.length] = this.components[i].value;
			}
		}
		return allVals;
	}
	this.reloadStores = function(index){
		var allVals = this.getAllComponentNames();
		for(var i=0; i<this.components.length;i++){
			if((index) != i) this.components[i].reloadStore(allVals, false);
		}
	}
	this.NewComponentView = function(id, existingPrescription){
		var componentView = new View.Component(this, id, existingPrescription);
		this.components.push(componentView)	
    	return componentView.CreateComponentView(this.components.length - 1);
	}
	
	this.GetComponents = function(){
		var components = [];
		for(var i=0; i<this.components.length;i++){
			var component = this.GetComponentValue(this.components[i]);
			if(component != null) components[components.length] = component;
			
		}
		return components;
	}
	this.GetComponentValue = function(component){
		var name = component.GetName();
		if(name == "") return null;
		var qtyVal = component.GetQuantityValue();
		if(qtyVal.State != "User") qtyVal.Value = 0;
		return  {
			Name:name,
			Quantity:qtyVal
		}
	}
	this.setChangedProperty = function(changedProperty){
		for(var i=0; i<this.components.length;i++){
			if(this.components[i].Quantity == changedProperty){
				this.changedProperties.push(changedProperty);
				if(this.changedProperties.length > 2){
					this.changedProperties.shift();
				}
				return;
			}
		}
	}
	
	this.CheckStates = function(latestChangedProperty){
		var setCount = 0;
		var totalCount = 0;
		var hasLatestChangesProperty = false;
		for(var i=0; i<this.components.length;i++){
			var name = this.components[i].GetName();
			if(name == "") continue;
			var qtyVal = this.components[i].GetQuantityValue();
			var qtyState = this.components[i].GetQuantityState();
			if(qtyVal.Value > 0 && qtyState == "User") setCount++;
			if(qtyVal.Value < 0){
				this.TPNForm.Quantity.setCalculated();
				return true;
			}
			if(this.components[i].Quantity == this.changedProperties[0]) hasLatestChangesProperty = true;
			totalCount++;
		}
		if(setCount == totalCount && hasLatestChangesProperty){
			if(hasLatestChangesProperty) this.changedProperties[0].setCalculated();
		}else{
			return true;
		}
		return hasLatestChangesProperty;
	}
	
	this.setComponentValue = function(index, value){
		if(value.Increments[0] == 0) value.Increments = [0.1];
		this.components[index].Quantity.setValue(value);
	}
}
View.TPNForm = function(parentView) {
	this.infusions = [];
	this.added = false;
	
	this.Show = function(medicationPanel){
	    
	    if(this.added){
			this.form.destroy(true);
			this.infusions = [];
		}
		
		this.form = new Ext.Container({
	        padding: "4 4 4 4",
	        id : 'TPNForm',
	        layout:'column',
	    	autoScroll :true,
	        width:700
    	});
		
	    medicationPanel.add(this.form);
	    medicationPanel.getLayout().setActiveItem(2);
	    this.retrieveData();
	    this.added = true;
	}
	this.loadData = function(prescriptionCollection){
		
		if(prescriptionCollection.length > 0) {
			for(var p=0;p<prescriptionCollection.length;p++){
				this.Add(false, prescriptionCollection[p]);
				this.infusions[this.infusions.length-1].NextInfusionAdded = true;
			}
			this.Add(true, null);
		}else{
			this.Add(false, null);
		}
	}
	this.retrieveData = function(){
		this.DataProvider = new DataProvider();
		this.DataProvider.GetTPNPrescriptions(this.loadData.createDelegate(this));
		this.DataProvider = null;
		delete this.DataProvider;
		//this.Add(false);
	}
	this.Add = function(collapsed, prescription){
	    var infusion = new View.TPNInfusion(this, collapsed, "Infuus "+(this.infusions.length + 1));
	    this.infusions.push(infusion);
	    var form = infusion.GetView(this, prescription);
	    
	    this.form.add(form);
	    
        this.form.doLayout();
        this.form.render();
        if(!collapsed) {
        	infusion.AddItems();
        	infusion.init();
        }else{
        	var addInfusionListener = function(){
        		infusion.AddItems();
        		infusion.init();
        		form.events.expand.clearListeners()
        	};
        	form.addListener("expand", addInfusionListener, {scope:this});
        }
	}
}

View.TPNInfusion = function(parentView, collapsed, id) {
	this.id = id;
	this.prescriptionId = 0;
    this.componentFactory = new Factory.Component(this);
    this.added = false;
    this.DataProvider = new DataProvider();
    this.StateManager = new Logic.StateManager();
    this.NextInfusionAdded = false;
    this.latestControlChanged = null;
    this.parentView = parentView;
    this.hasSolutionView = false;
    this.solution = null;
    this.collapsed = collapsed;
    this.prescription = null;
    this.existingPrescription = false;
    
    this.updateRowsByComponents = function(components){
    	var counter = 0;
    	var no = 0;
    	for(var i=0;i<components.length;i++){
			no = i;
			if(components[0].IsSolution == false) no = i+1;
			if(typeof(this.componentFactory.components[no]) == "undefined") this.addRow(this.componentFactory.components.length);
			this.componentFactory.components[no].Quantity.setValue(components[i].Quantity)
			this.componentFactory.components[no].Quantity.setReadOnly(true);
			this.componentFactory.components[no].componentSelector.setValue(components[i].Name)
			this.componentFactory.components[no].componentSelector.setReadOnly(true);
    		counter++;
    	}
    }
    this.addRow = function(index){
    	
    	if(index < (this.componentFactory.components.length - 1)) return;
    	if(index == 0) this.addSolution();
    	
    	var componentRow = this.componentFactory.NewComponentView(this.id, this.existingPrescription);
    	if(this.hasSolutionView)
    		this.form.insert(this.form.items.length - 2,componentRow);
		else
			this.form.insert(this.form.items.length - 1,componentRow);
			
    	this.updateFormLayout();
    	
    }
    
    this.updateFormLayout = function(comp){
    	this.form.doLayout();
    	this.form.render();
    }
    this.addSolution = function(){
    	this.hasSolutionView = true;
    	this.solution = this.componentFactory.GetComponent(this.id, this.existingPrescription);
    	this.componentFactory.components.push(this.solution);
    	this.solution.isSolution = true;
    	this.form.insert(this.form.items.length - 1, this.solution.GetComponentRow("Oplossing"));
    	this.updateFormLayout();
    }
    
    this.makeLabel = function(text, width){
    	return {width:width,html:'<div class="x-form-item" style="padding-top:3px;">'+text+'</div>'};	    
    }
    this.AddItems = function(){
    	
        var stores = Stores.PrescribeMedication.GetInstance();
        this.Total = new ValueField({
			label: '',
			className:this.id+'TPNPrescriptionTotal',
			unitStore: ["ml"],
			formContainer : this,
			timeUnitStore: ['dag'],
			timeUnitValue : "dag",
			unitValue : "ml"
		});
		
		
    	this.Rate = new ValueField({
			label: '',
			formContainer : this,
			className:this.id+'TPNPrescriptionRate',
			unitStore: stores.GetShapeUnits(),
			timeUnitStore: ['uur'],
			unitValue : "ml",
			timeUnitValue : "uur",
			width:200
		});
		this.Header = {columnWidth:1,layout:'column',items:[this.makeLabel('Infuus snelheid:',105),this.Rate,this.makeLabel('totaal',40),this.Total]};
    	
		var seperator = new Ext.FormPanel({ 
			columnWidth:1,
			labelSeparator: ' ',
			items : [{html:'<hr />'}]
		})
		
		this.Quantity = new ValueField({
			label: '',
			formContainer : this,
			unitStore: stores.GetShapeUnits(),
			className:this.id+'TPNDrugQuantity',
			unitValue : "ml",
			width:200
		});
		
    	var qtyTotal = {layout:"column",items:[
    		this.makeLabel('totaal',345),this.Quantity
    	]};
    	this.form.add(this.Header);
    	this.form.add(qtyTotal);
    	this.updateFormLayout();
    	if(this.prescription != null){
			this.Rate.setValue(this.prescription.Rate);
			this.Quantity.setValue(this.prescription.Drug.Quantity);
			this.Quantity.setReadOnly(true);
			this.Total.setValue(this.prescription.Total); 	
		}
    }
    this.save = function(){
    	this.UpdateForm({}, true);
    }
    this.GetView = function(parentView, prescription) {
        
        this.prescription = prescription;
        
        if(prescription!=null) {
        	this.existingPrescription = true;
        	this.prescriptionId = prescription.Id;
        }
		this.form = new Ext.Panel({
            padding: "4 4 4 4",
            layout:'form',
            id:this.id+'infusionBox',
            width:575,
            buttonAlign:"left"
        });
        
        var saveButton = new Ext.Button({text:"Opslaan",height:30,handler:this.save.createDelegate(this)});
        var deleteButton = new Ext.Button({text:"Afsluiten"});
        
        var saveColumn = {
	    	items: {items:[saveButton, deleteButton],style: {marginTop: '20px'},defaults:{width:100,style:{height:22,marginTop:2}},width:100}
	    }
	    
        this.view = new Ext.Panel({
    		width:700,
    		layout:"column",
    		cls:'infusionBox',
            bwrapCssClass : 'infusionBody',
            titleCollapse:true,
            collapsed:this.collapsed,
            forceLayout:true,
            collapseFirst:false,
            title:this.id,
            collapsible:true,
            animCollapse:false,
            items:[this.form,saveColumn]
    	})
        this.added = true;
        return this.view;
    }
    this.init = function(){
    	this.addRow(0);
    	if(this.prescription != null) this.updateRowsByComponents(this.prescription.Drug.Components);
    }
    this.CheckStates = function(){
    	if((this.Total.getValue().Value > 0 && this.Total.getState() == "User") && (this.Total.getValue().Value > 0 && this.Rate.getState() == "User")){
    		if(this.latestControlChanged == this.Total) {
    			this.Rate.setCalculated();
    			return;
    		}
    		if(this.latestControlChanged == this.Rate) {
    			this.Total.setCalculated();
    			return;
    		}
    		this.Total.setCalculated();
    	}
    	this.componentFactory.setChangedProperty(this.latestControlChanged);
    	if(this.Quantity.getState() == "User" && this.Quantity.getValue().Value > 0){
    		if(!this.componentFactory.CheckStates(this.latestControlChanged)){
    			this.Quantity.setCalculated();
    		}
    	}
    }
    this.setUserState = function (field){}
    this.setCalculatedState = function (field){}
    this.SetLatestControlChanged = function (control){
    	
    	this.latestControlChanged = control;
    	this.CheckStates();
    }
    this.GetLatestControlChanged = function (){}
    this.GetStatedValue = function(val){
    	if(val.State != "User") val.Value = 0;
    	return val;
    }
    this.SetComponents = function(components){
    	for(var i=0;i<components.length;i++){
    		var index = i;
    		if(this.solution.GetName() == "") index = i + 1;
    		if(index >= this.componentFactory.components.length) continue;
    		this.componentFactory.setComponentValue(index, components[i].Quantity);
    	}
    }
    this.UpdateForm = function(obj, save){
        
        if(typeof(save) == "undefined") save = false;
        
        var returnFunc = function(prescription){
        	if(prescription.Totals.data) GenPres.TotalStore.loadData(prescription.Totals);
        	if(prescription.Drug.Quantity.Increments[0] == 0) prescription.Drug.Quantity.Increments = [0.1];
        	if(prescription.Rate.Increments[0] == 0) prescription.Rate.Increments = [0.1];
        	this.Quantity.setValue(prescription.Drug.Quantity);
        	this.Rate.setValue(prescription.Rate);
        	this.Total.setValue(prescription.Total);
        	this.SetComponents(prescription.Drug.Components);
        	if(prescription.Id > 0 ) this.prescriptionId = prescription.Id;
        	
        }.createDelegate(this);
        
        var components = this.componentFactory.GetComponents();
        if(this.solution.GetName() != "") components[0].IsSolution = true;
        	
        if(components.length > 0 && !this.NextInfusionAdded){
        	this.NextInfusionAdded = true;
        	parentView.Add(true, null);
        }
        
        this.DataProvider.UpdateTPN(
        	{Id:this.prescriptionId,Rate:this.GetStatedValue(this.Rate.getValue()),Total:this.GetStatedValue(this.Total.getValue())},
        	{Quantity:this.GetStatedValue(this.Quantity.getValue())},
        	components,
        	save,
            returnFunc
        );
    }   
}