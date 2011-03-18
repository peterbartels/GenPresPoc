Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");

View.PrescribeMedicationForm.Medicine = function(MainForm) {
    var stores = Stores.PrescribeMedication.GetInstance();
	
    //Drug.Name
    this.generic = new Ext.form.ComboBox({
        store: stores.GetGenericStore(),
        name: 'generic',
        className: 'Drug',
        lazyInit:false,
        propertyName: 'Name',
        emptyText: 'Selecteer generiek'
    });
    var params = [{ name: 'generic'}];
    
    stores.setSingleValue(stores.GetGenericStore(), this.generic);
    
    this.generic.addListener("change", MainForm.UpdateForm);
    this.generic.addListener("change", stores.reloadMedicine.createDelegate(this, params, true));
    
    //Substance.Quantity
    this.quantity = new ValueField({
        className: 'Substance',
        propertyName: 'Quantity',
        formContainer : MainForm,
        unitStore: stores.GetDosageUnits()
    });

    //Drug.Quantity
    this.solutionQuantity = new ValueField({
        unitValue: '',
        className: 'Drug',
        propertyName: 'Quantity',
        formContainer : MainForm,
        unitStore: stores.GetShapeUnits()
    });

    //Patient.Weight
    this.weight = new ValueField({
        label: 'Gewicht',
        unitValue: 'kg',
        width:225,
        disableToggle:true,
        stateless:true,
        className: 'Prescription',
        propertyName: 'AdjustWeight',
        formContainer: MainForm,
        unitStore: ["gram", "kg"]
    });
    
    var updatePatientFunc = function(){
    	MainForm.UpdatePatientData(this.weight.getValue(), this.length.getValue());
    }.createDelegate(this);
    
    this.weight.spinner.setValue(GenPres.SelectedPatient.Weight / 1000);
    this.weight.sate = "User";
    this.weight.on("change", updatePatientFunc);

    //Patient.Length
    this.length = new ValueField({
        label: 'Lengte',
        unitValue: 'cm',
        width:225,
        disableToggle:true,
        stateless:true,
        className: 'Prescription',
        formContainer: MainForm,
        propertyName: 'AdjustLength',
        unitStore: ["cm"]
    });
	this.length.spinner.setValue(GenPres.SelectedPatient.Length * 100);
    this.length.sate = "User";
    this.length.on("change", updatePatientFunc);
    
    //Patient.BSA
    this.bsa = new ValueField({
        label: 'BSA',
        unitValue: 'm2',
        width:225,
        disableToggle:true,
        /*onUpdate: function(el) { GenPres.SelectedPatient.Weight = el.getValue(); },*/
        /*stateless:true,*/
        formContainer: MainForm,
        className: 'Prescription',
        propertyName: 'AdjustBSA',
        unitStore: ["m2"]
    });

    //Drug.Route
    this.route = new Ext.form.ComboBox({
        store: stores.GetRouteStore(),
        width: 140,
        className: 'Drug',
        propertyName: 'Route',
        fieldLabel: 'Toedieningsweg'
    });
    var params = [{ name: 'route'}]
    this.route.addListener("change", stores.reloadMedicine.createDelegate(this, params, true));
    stores.setSingleValue(stores.GetRouteStore(), this.route);

    //Drug.Shape
    this.solution = new Ext.form.ComboBox({
        store: stores.GetSolutionStore(),
        width: 121,
        fieldLabel: 'Toedienvorm',
        className: 'Drug',
        propertyName: 'Shape'
    });
    var params = [{ name: 'shape'}]
    this.solution.addListener("change", stores.reloadMedicine.createDelegate(this, params, true));
    stores.setSingleValue(stores.GetSolutionStore(), this.solution);
	
    stores.GetDosageUnits().addListener("load", function(){
    	window.setTimeout(function(){
	    	if(this.generic.getValue() != "" && this.solution.getValue() != "" && this.route.getValue() != ""){
				MainForm.UpdateForm(this.generic);
	    	}
    	}.createDelegate(this), 200);
	}.createDelegate(this));
    
    //Drug.SolutionType
    this.solutionType = new Ext.form.ComboBox({
        store: stores.GetSolutionTypeStore(),
        width: 122,
        emptyText: '',
        className: 'Drug',
        propertyName: 'SolutionType',
        fieldLabel: 'Oplossing'
    });

    //Substance.DrugConcentration
    this.solutionConcentration = new ValueField({
        label: 'Concentratie',
        totalUnits: stores.GetShapeUnits(),
        unitValue: '',
        disableStepping:true,
        unitStore: stores.GetDosageUnits(),
        className: 'Substance',
        propertyName: 'DrugConcentration',
        formContainer : MainForm
    });

    this.formEl = new Ext.form.FormPanel({
        border: false,
        items: [{
            layout: 'column',
            items: [{
                layout: 'fit',
                width: 650,
                id:'medicinePanel',
                title:'medicament',
                items: [
                {
                    layout: 'column',
                    height: 40,
                    columnWidth: '1',
                    items: [
                    {
                        layout: 'form',
                        labelAlign: 'top',
                        items: [this.generic]
                    }, {
                        layout: 'form',
                        labelAlign: 'top',
                        id: 'quantity',
                        items: [this.quantity]
                    }, { xtype:'box', html: '<div style="width:30px;">&nbsp;</div>' }, {
                        layout: 'form',
                        labelAlign: 'top',
                        id: 'solutionType',
                        items: [this.solutionType]
                    }, {
                        layout: 'form',
                        labelAlign: 'top',
                        id: 'solutionQuantity',
                        items: [this.solutionQuantity]
					}]
                }, {
                    layout: 'column',
                    columnWidth: '1',
                    items: [{
	                        layout: 'form',
	                        labelAlign: 'top',
	                        id: 'route',
	                        items: [this.route]
	                    }, {
	                        layout: 'form',
	                        labelAlign: 'top',
	                        id: 'solution',
	                        items: [this.solution]
	                    }, { xtype:'box', html: '<div style="width:40px;">&nbsp;</div>' }, {
	                        layout: 'form',
	                        labelAlign: 'top',
	                        id: 'solutionConcentration',
	                        items: [this.solutionConcentration]
	                    }, { xtype:'box', html: '<div style="clear:both;">&nbsp;</div>' }
                	]}
            	]
        	},{html:'&nbsp;',width:2},{
                title: 'Patient',
                id:'patientPanel',
                width:280,
                items: [{
                    layout: 'form',
                    width:225,
                    items: [this.weight]
                }, {
                    layout: 'form',
                    width:225,
                    items: [this.length]
                }, {
                    layout: 'form',
                    width:225,
                    items: [this.bsa]
				}]
			}]
		}]
    });
	return this;
}
