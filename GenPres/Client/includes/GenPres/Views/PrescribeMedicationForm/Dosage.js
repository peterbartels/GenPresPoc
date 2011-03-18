Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");

View.PrescribeMedicationForm.Dosage = function(MainForm) {
    var store = Stores.PrescribeMedication.GetInstance();

    ////======================================DOSAGE======================
	//Dose.Quantity   
	this.dosageQuantity = new ValueField({
		className:'Dose',
		propertyName:'Quantity',
		formContainer : MainForm,
		label: 'Per keer',
		unitStore: store.GetDosageUnits(),
		adjustUnitStore: ['-', 'kg', 'm2']
	});
    ////======================================DOSAGE======================

    ////======================================TOTAL======================
	//Dose.Total
    this.dosageQuantityTotal = new ValueField({
		className:'Dose',
		propertyName:'Total',
		formContainer : MainForm,
		label: 'Totaal',
		unitStore: store.GetDosageUnits(),
		adjustUnitStore: ['-', 'kg', 'm2'],
		timeUnitStore: ['dag', 'week'],
		timeUnitValue : "dag"
	});
    ////======================================TOTAL======================

    ////======================================RATE======================
    //Dose.Rate
	this.dosageQuantityRate = new ValueField({
		className:'Dose',
		propertyName:'Rate',
		formContainer : MainForm,
		label: 'Snelheid',
		unitStore: store.GetDosageUnits(),
		adjustUnitStore: ['-', 'kg', 'm2'],
		timeUnitStore: ['min', 'uur', 'dag'],
		timeUnitValue : "uur"
	});
    ////======================================RATE======================


    this.formEl = new Ext.Container({
        layout: 'column',
        width:933,
        id:'dosagePanel',
        title: 'Dosering',
        items: [new Ext.form.FormPanel({
            labelAlign: 'top',
            id: 'dosageQuantity',
            items: [this.dosageQuantity]
        }), { xtype:'box', html: '<div style="width:2px;">&nbsp;</div>' }, new Ext.form.FormPanel({
            labelAlign: 'top',
            id: 'dosageQuantityTotal',
            items: [this.dosageQuantityTotal]
        }), new Ext.form.FormPanel({
            labelAlign: 'top',
            padding:'0 0 0 156',
            id: 'dosageQuantityRate',
            items: [this.dosageQuantityRate]
        })]
    });
    return this;
}
