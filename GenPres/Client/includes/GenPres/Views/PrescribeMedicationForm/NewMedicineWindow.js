Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");

View.PrescribeMedicationForm.NewMedicine = function(prescribeMedicationForm) {
	var stores = Stores.PrescribeMedication.GetInstance();
		
	var generic = new Ext.form.ComboBox({
        store: stores.GetGenericStore(),
        name: 'generic',
        width: 140,
        typeAhead: false,
        forceSelection:false,
        fieldLabel: 'Generiek'
    });
    
    var route = new Ext.form.ComboBox({
        store: stores.GetRouteStore(),
        width: 140,
        typeAhead: false,
        forceSelection:false,
        fieldLabel: 'Toedieningsweg'
    });
    
    var shape = new Ext.form.ComboBox({
        store: stores.GetSolutionStore(),
        width: 140,
        fieldLabel: 'Toedienvorm',
        typeAhead: false,
        forceSelection:false
    });
    
    //---------DOSE UNIT--------------
    var doseUnitCombo = new Ext.form.ComboBox({
        store: stores.GetDosageUnits(),
        fieldLabel:'Doseereenheid',
        typeAhead: false,
        forceSelection:false,
        width: 100
    });
    
    var doseSmallestPossibleValue = new Ext.form.NumberField({
    	minValue : 0,
    	decimalSeparator: ',',
    	width:50,
    	value:1
    })
    var doseComposite = new Ext.form.CompositeField({
        defaultMargins: '0 10 0 0',
        items:[doseUnitCombo, {html:'Kleinste aflever eenheid'},doseSmallestPossibleValue]
    });
    //------------------------------
    
    //---------ADMIN UNIT--------------
    var adminUnitCombo = new Ext.form.ComboBox({
        store: stores.GetShapeUnits(),
        fieldLabel:'Toedieneenheid',
        typeAhead: false,
        forceSelection:false,
        width: 100
    });
    
    var adminSmallestPossibleValue = new Ext.form.NumberField({
    	minValue : 0,
    	decimalSeparator: ',',
    	width:50,
    	value:1
    })
    var adminComposite = new Ext.form.CompositeField({
        defaultMargins: '0 10 0 0',
        items:[adminUnitCombo, {html:'Kleinste aflever eenheid'},adminSmallestPossibleValue]
    });
    //------------------------------
	var form = new Ext.Panel({
		layout:'form',
		items:[
			generic,shape,route,doseComposite,adminComposite
		]
	});
	
	var setStoreValue = function(store,value){
		if(value.trim() == "") return;
		store.removeAll();
		store.loadData([{Name:value}]);
	}
	var win = new Ext.Window({
        layout:'fit',
        width:500,
        height:300,
        closeAction:'hide',
        items: form,
        buttons: [{
            text:'Opslaan',
            handler: function(){
                var error = "";
                if(generic.getValue() == "") error += "Geen generiek gekozen. <br />";
                if(shape.getValue() == "") error += "Geen toedienvorm gekozen. <br />";
                if(route.getValue() == "") error += "Geen toedieningsweg gekozen. <br />";
                if(adminUnitCombo.getValue() == "") error += "Geen oseereenheid gekozen. <br />";
                if(doseUnitCombo.getValue() == "") error += "Geen toedieneenheid gekozen. <br />";
                if(doseSmallestPossibleValue.getValue() == "") error += "Geen dosering kleinste aflevereenheid gekozen. <br />";
                if(adminSmallestPossibleValue.getValue() == "") error += "Geen toediening kleinste aflevereenheid gekozen. <br />";
                if(error!=""){
					Ext.Msg.alert('Niet alle velden zijn ingevuld', error);
                	return;
                }
                setStoreValue(stores.GetGenericStore(), generic.getValue());
                setStoreValue(stores.GetSolutionStore(), shape.getValue());
                setStoreValue(stores.GetRouteStore(), route.getValue());
                setStoreValue(stores.GetShapeUnits(), adminUnitCombo.getValue());
                setStoreValue(stores.GetDosageUnits(), doseUnitCombo.getValue());
                
                if(doseSmallestPossibleValue.getValue() != "") 
                	prescribeMedicationForm.SubstanceIncrements = [doseSmallestPossibleValue.getValue()];
                	
                if(adminSmallestPossibleValue.getValue() != "") 
                	prescribeMedicationForm.ComponentIncrement = adminSmallestPossibleValue.getValue();
                	
                prescribeMedicationForm.CustomComponentUnit = adminUnitCombo.getValue();
                prescribeMedicationForm.CustomSubstanceUnit = doseUnitCombo.getValue();
                
                prescribeMedicationForm.UpdateData({});
                
                win.hide();
            }
        },{
            text: 'Annuleren',
            handler: function(){
                win.hide();
            }
        }]
    });
    
    win.show(this);
}