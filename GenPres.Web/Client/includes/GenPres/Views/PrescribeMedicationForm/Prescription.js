Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");

View.PrescribeMedicationForm.Prescription = function(MainForm) {
    var stores = Stores.PrescribeMedication.GetInstance();

    //Prescription.Frequency
    this.Frequency = new ValueField({
        label: 'Frequentie',
        className: 'Prescription',
        propertyName: 'Frequency',
        formContainer : MainForm,
        timeUnitValue: 'dag',
        timeUnitStore: ['dag', 'week']
    });
    

    //Prescription.Time
    this.Duration = new ValueField({
        label: 'Toedienduur',
        className: 'Prescription',
        propertyName: 'Time',
        disableStepping:true,
        formContainer : MainForm,
        unitValue: 'uur',
        unitStore: ['dag', 'uur', 'min']
    });

    //Prescription.Continuous
    this.Continuous = new Ext.form.Checkbox({
        fieldLabel: 'Continu',
        name: 'continuous',
        className: 'Prescription',
        propertyName: 'Continuous',
        id: 'continuous'
    });
    this.Continuous.on("check", MainForm.UpdateForm);

    //Prescription.Infusion
    this.Infusion = new Ext.form.Checkbox({
        fieldLabel: 'Inlooptijd',
        name: 'infusion',
        className: 'Prescription',
        propertyName: 'Infusion',
        id: 'infusion'
    });
    this.Infusion.on("check", MainForm.UpdateForm);
    
    //Prescription.issolution
    this.Issolution = new Ext.form.Checkbox({   
        fieldLabel: 'Oplossing',
        name: 'issolution',
        className: 'Prescription',
        propertyName: 'Solution',
        id: 'issolution'
    });
    this.Issolution.on("check", MainForm.UpdateForm);

    //Prescription.onrequest
    this.Onrequest = new Ext.form.Checkbox({
        fieldLabel: 'Indien nodig',
        name: 'onrequest',
        className: 'Prescription',
        propertyName: 'Onrequest',
        id: 'onrequest'
    });
    this.Onrequest.on("check", MainForm.UpdateForm);
    
    //this.StartDate = new Ext.form.TimeField(width:50);
    
    //The panel
    this.formEl = new Ext.form.FormPanel({
        border: false,
        items: [{
            layout: 'column',
            columnWidth:1,
            items: [{

                //----  Prescription
                layout: 'column',
                padding:"6 6 6 6",
                width: 650,
                items: [{
                    layout: 'form',
                    labelAlign: 'top',
                    id: 'frequency',
                    padding:'10 0 0 0',
                    items: [this.Frequency]
                }, {html:'<div style="width:120px;">&nbsp;</div>'}, {
                    layout: 'form',
                    labelAlign: 'top',
                    padding:'10 0 0 170',
                    id: 'duration',
                    items: [this.Duration]
				}]
            },{html:'&nbsp;',width:2},{
            //---- Options
            layout: 'column',
            title: 'Opties',
            /*id: 'PrescribeMedicationForm_Opties',*/
            id: 'optionsPanel',
            width:280,
            items: [{
                layout: 'form',
                labelAlign: 'top',
                labelSeparator: '&nbsp;',
                items: [this.Continuous]
            }, {
                layout: 'form',
                labelAlign: 'top',
                labelSeparator: '&nbsp;',
                items: [this.Infusion]
            }, {
                layout: 'form',
                labelAlign: 'top',
                labelSeparator: '&nbsp;',
                items: [this.Onrequest]
            }, {
                layout: 'form',
                labelAlign: 'top',
                labelSeparator: '&nbsp;',
                items: [this.Issolution]
			}]
		}]
        }]
    });
    return this;
}
