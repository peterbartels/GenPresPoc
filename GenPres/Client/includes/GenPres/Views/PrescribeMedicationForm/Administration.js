Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");

View.PrescribeMedicationForm.Administration = function(MainForm) {
    var store = Stores.PrescribeMedication.GetInstance();
    
    ////======================================DOSAGE======================   
    //Prescription.Quantity
	this.administrationQuantity = new ValueField({
		label: 'Per keer',
		className:'Prescription',
		propertyName:'Quantity',
		className:'Prescription',
		propertyName : 'Quantity',
		formContainer : MainForm,
		unitStore: store.GetShapeUnits()
	});
    ////======================================DOSAGE======================

    ////======================================TOTAL======================
	//Prescription.Total
	this.administrationQuantityTotal = new ValueField({
		label: 'Totaal',
		className:'Prescription',
		propertyName:'Total',
		unitStore: store.GetShapeUnits(),
		className:'Prescription',
		propertyName : 'Total',
		formContainer : MainForm,
		timeUnitStore: ['dag', 'week'],
		timeUnitValue : "dag"
	});
    ////======================================TOTAL======================

    ////======================================RATE======================
	//Prescription.Rate
	this.administrationQuantityRate = new ValueField({
		label: 'Snelheid',
		className:'Prescription',
		propertyName:'Rate',
		formContainer : MainForm,
		unitStore: store.GetShapeUnits(),
		className:'Prescription',
		propertyName : 'Rate',
		timeUnitStore: ['uur'],
		timeUnitValue : "uur"
	});
    ////======================================RATE======================
	
	this.verbalization = {
		xtype:'box',
		height:14,
		id:"Verbalization",
		html:''
	};
    this.remarks = new Ext.form.TextArea({
        width: 650,
        name : 'remarks',
        id:'remarks',
        hideLabel:true,
		className:'Prescription',
		propertyName : 'Remarks',
		enableKeyEvents: true,
		emptyClass: 'remarksEmtpy',
		emptyText:'Opmerkingen',
        height:70
    });
        
    this.StartDate = new Ext.form.CompositeField({
    	fieldLabel:'Startdatum',
    	width:170,
    	items: [
    		new Ext.form.DateField({
    			width:100, format:'d-m-Y',
    			value:new Date().format("d-m-Y"),
    			className:'Prescription',propertyName : 'StartDate'
			}),
    		new Ext.form.TimeField({width:60,format:'H:i',increment:5,className:'Prescription',propertyName : 'StartTime',value:new Date().format("H:i")})
		]
    });
    
    this.EndDate = new Ext.form.CompositeField({
    	fieldLabel:'Einddatum',
    	width:170,
    	items: [
    		new Ext.form.DateField({width:100,format:'d-m-Y',className:'Prescription',propertyName : 'EndDate'}),
    		new Ext.form.TimeField({width:60,format:'H:i',increment:5,className:'Prescription',propertyName : 'EndTime'})
		]
    });
    
    this.formEl = new Ext.Container({
        layout: 'column',
        items : [{
	        layout: 'column',
	        width:933,
	        id:'adminPanel',
	        title: 'Toediening',
	        items: [new Ext.form.FormPanel({
	            labelAlign: 'top',
	            id: 'administrationQuantity',
	            items: [this.administrationQuantity]
	        }), { xtype:'box', html: '<div style="width:2px;">&nbsp;</div>' }, new Ext.form.FormPanel({
	            labelAlign: 'top',
	            id: 'administrationQuantityTotal',
	            padding:'0 0 0 88',
	            items: [this.administrationQuantityTotal]
	        }), new Ext.form.FormPanel({
	            labelAlign: 'top',
	            padding:'0 0 0 238',
	            id: 'administrationQuantityRate',
	            items: [this.administrationQuantityRate]
	        })]
    	},{
            layout: 'column',
            title: '',
            width:933,
            items : [{
                labelAlign: 'top',
                layout: 'form',
                id: 'Opmerkingen',
                height:100,
                items: [this.verbalization,this.remarks]
            },{
                labelAlign: 'top',
                layout: 'form',
                padding:'0 0 0 2',
                id: 'StartDatum',
                items: [this.StartDate, this.EndDate]
            },{xtype:"button", text:"Opslaan", id:"prescribeMedicationFormSaveButton", handler:MainForm.save}]
        }]
    });
    
    /*
    this.formEl = new Ext.form.FormPanel({
        border: false,
        layout: 'column',
        items: [{
            layout: 'column',
        	width:933,
            title: 'Toediening',
            id:'adminPanel',
            items: [new Ext.form.FormPanel({
	            labelAlign: 'top',
	            layout: 'column',
	            id: 'administrationQuantity',
	            items: this.administrationQuantity
	        }), new Ext.form.FormPanel({
	            labelAlign: 'top',
	            layout: 'column',
	            id: 'administrationQuantityTotal',
	            padding:'0 0 0 88',
	            items: this.administrationQuantityTotal
	        }), new Ext.form.FormPanel({
	            labelAlign: 'top',
	            layout: 'column',
	            padding:'0 0 0 238',
	            id: 'administrationQuantityRate',
	            items: this.administrationQuantityRate
	        })]
        },{
            layout: 'column',
            title: '',
            items : [{
                labelAlign: 'top',
                layout: 'form',
                id: 'Opmerkingen',
                height:100,
                items: [this.remarks]
            },{
                labelAlign: 'top',
                layout: 'form',
                padding:'0 0 0 2',
                id: 'StartDatum',
                items: [this.StartDate, this.EndDate]
            }]
        }]
    });
    */
    return this;
}
