
Ext.override(Ext.Panel, {
    border:false,
    defaultType:'container'
});

Ext.override(Ext.form.Field, {
	setFieldLabel : function(text) {
		if (this.rendered) {
			this.el.up('.x-form-item', 10, true).child('.x-form-item-label').update(text);
		}
		this.fieldLabel = text;
	}
});
Ext.override(Ext.form.ComboBox, {
    displayField: 'Name',
    typeAhead: true,
    mode: 'local',
    width: 140,
    typeAheadDelay:1,
    queryDelay:1,
    validateOnBlur:false,
    validationDelay : 0,
    forceSelection: true,
    triggerAction: 'all',
    selectOnFocus: true
});

Ext.BLANK_IMAGE_URL = 'Client/resources/images/default/s.gif';

//Define Class GenPres (Facade)
GenPres = {
    SelectedPatient: {
        Unit: '',
        Bed:'',
        FirstName : '',
        LastName : '',
        Birthdate : '',
        Age: '',
        Weight : 0,
        WeightGuess: '',
        WeightMedication: '',
        WeightActual:'',
        WeightAdmission: '',
        WeightBirth:'',
        LengthGuessed:'',
        LengthActual: '',
        Length : 0,
        AddmissionDate: '',
        CurrentDate:'',
        Days:'',
        PID:''
    },
	User : 
	{
	    Login:'pbartels',
	    FirstName:'Peter',
	    LastName:'Bartels',
	    Function:'Voorschrijver'
	},
	GenPresView : null,
	PrescriptionEvents : null
};

Ext.namespace('App', 'App.form', 'App.data');

//When DOM Ready start main sequence
Ext.onReady(function() {
    //for showing information messages
    Ext.QuickTips.init();


    var genPresView = new GenPresView();
    GenPres.GenPresView = genPresView;
    Stores.PrescribeMedication.GetInstance().GetGenericStore();
    Stores.PrescribeMedication.GetInstance().GetRouteStore();
    Stores.PrescribeMedication.GetInstance().GetSolutionStore();
    Stores.PrescribeMedication.GetInstance().GetSolutionTypeStore();
    Stores.PrescribeMedication.GetInstance().GetShapeUnits();
    Stores.PrescribeMedication.GetInstance().GetDosageUnits();
    
});