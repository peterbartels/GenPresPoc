Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");


/*
 *  Class which shows a NewMedicine Form
 */
View.PrescribeMedicationForm.NewMedicine = function(prescribeMedicationForm) {
    this.prescribeMedicationForm = prescribeMedicationForm;

    this.LoadWindow = function(){
        var stores = Stores.PrescribeMedication.GetInstance();

        this.generic = new Ext.form.ComboBox({
            store: stores.GetStoreInstance("GetGenerics"),
            name: 'generic',
            width: 140,
            typeAhead: false,
            forceSelection:false,
            fieldLabel: 'Generiek'
        });

        this.route = new Ext.form.ComboBox({
            store: stores.GetStoreInstance("GetRoutes"),
            width: 140,
            typeAhead: false,
            forceSelection:false,
            fieldLabel: 'Toedieningsweg'
        });

        this.shape = new Ext.form.ComboBox({
            store: stores.GetStoreInstance("GetShapes"),
            width: 140,
            fieldLabel: 'Toedienvorm',
            typeAhead: false,
            forceSelection:false
        });

        //---------DOSE UNIT--------------
        this.doseUnitCombo = new Ext.form.ComboBox({
            store: stores.GetStoreInstance("GetDosageUnits"),
            fieldLabel:'Doseereenheid',
            typeAhead: false,
            forceSelection:false,
            width: 100
        });

        ////======================================Quantity======================
        this.quantity = new ValueField({
            formContainer : this,
            label: 'Hoeveelheid',
            forceSelection:false,
            width:300,
            unitStore: stores.GetStoreInstance("GetDosageUnits")
        });
        ////======================================Quantity======================


        ////======================================DosageIncrement======================
        this.dosageIncrement = new ValueField({
            formContainer : this,
            label: 'Doseer-KAE',
            forceSelection:false,
            width:300,
            unitStore: stores.GetStoreInstance("GetDosageUnits")
        });
        ////======================================DosageIncrement======================


        ////======================================AdminIncrement======================
        this.adminIncrement = new ValueField({
            formContainer : this,
            label: 'Toedien-KAE',
            forceSelection:false,
            width:300,
            unitStore: stores.GetStoreInstance("GetShapeUnits")
        });
        ////======================================AdminIncrement======================

        //var doseSmallestPossibleValue = new Ext.form.NumberField({minValue : 0,decimalSeparator: ',',width:50,value:1    });

        //--- Form for presenting controls
        var form = new Ext.Panel({
            layout:'form',
            labelWidth:100,
            padding: "20 20 20 20",
            items:[
                this.generic,
                this.shape,
                this.route,
                this.quantity,
                this.dosageIncrement,
                this.adminIncrement
            ]
        });


        //--- Method to reset the store with the selected Value 
        this.setStoreValue = function(store,value){
            if(value.trim() == "") return;
            store.removeAll();
            store.loadData([{Name:value}]);
        }

        //--- Load values from the prescribeMedicationForm and use them to fill these fields
        this.loadDefaultValues = function(){

            //Substance and drug quantity for determining incrementSizes and Units
            var substanceQty = this.prescribeMedicationForm.getField("Substance.Quantity");
            var substanceQtyValue = substanceQty.getValue();

            var drugQty = this.prescribeMedicationForm.getField("Drug.Quantity");
            var drugQtyValue = drugQty.getValue();

            //Generic, route en shape values
            var generic = this.prescribeMedicationForm.getField("Drug.Name").getValue();
            var route = this.prescribeMedicationForm.getField("Drug.Route").getValue();
            var shape = this.prescribeMedicationForm.getField("Drug.Shape").getValue();
            this.generic.setValue(generic);
            this.route.setValue(route);
            this.shape.setValue(shape);

            //Set Units
            this.quantity.unitCombo.setValue(substanceQty.unitCombo.getValue());
            this.dosageIncrement.unitCombo.setValue(substanceQtyValue.Unit);
            this.adminIncrement.unitCombo.setValue(drugQtyValue.Unit);

            //Set the quantity incrementSizes
            this.quantity.increments = substanceQty.increments;
            this.quantity.allowIncrementStep = false;
            this.quantity.updateIncrementSteps();
        }

        //--- Event that is triggered when saving the form
        //--- Checks if all fields are filled in, resets the prescription stores
        //--- and sets the controls in the prescriptionForm
        var handleForm = function(){
            var error = "";
            if(this.generic.getValue() == "") error += "Geen generiek gekozen. <br />";
            if(this.shape.getValue() == "") error += "Geen toedienvorm gekozen. <br />";
            if(this.route.getValue() == "") error += "Geen toedieningsweg gekozen. <br />";
            if(this.dosageIncrement.unitCombo.getValue() == "") error += "Geen doseereenheid gekozen. <br />";
            if(this.adminIncrement.unitCombo.getValue() == "") error += "Geen toedieningseenheid gekozen. <br />";
            if(this.quantity.getValue().Value == 0) error += "Geen hoeveelheid ingevuld. <br />";
            if(this.dosageIncrement.getValue().Value == 0) error += "Geen doseer KEA ingevuld. <br />";
            if(this.adminIncrement.getValue().Value == 0) error += "Geen toediening KAE ingevuld. <br />";

            if(error!=""){
                Ext.Msg.alert('Niet alle velden zijn ingevuld', error);
                return;
            }
            this.setStoreValue(stores.GetGenericStore(), this.generic.getValue());
            this.setStoreValue(stores.GetSolutionStore(), this.shape.getValue());
            this.setStoreValue(stores.GetRouteStore(), this.route.getValue());
            this.setStoreValue(stores.GetShapeUnits(), this.adminIncrement.unitCombo.getValue());
            this.setStoreValue(stores.GetDosageUnits(), this.quantity.unitCombo.getValue());

            this.prescribeMedicationForm.SetMedcine({
                DoseIncrement : this.dosageIncrement.getValue(),
                ComponentIncrement : this.adminIncrement.getValue(),
                Quantity : this.quantity.getValue(),
                Generic: this.generic.getValue(),
                Shape : this.shape.getValue(),
                Route: this.route.getValue()
            });
            
            this.prescribeMedicationForm.UpdateData({});
            this.win.hide();

        }

        //Small window presenting the form in the middle of the prescribeMedicationPanel
        this.win = new Ext.Window({
            layout:'fit',
            width:300,
            height:300,
            modal:true,
            closeAction:'hide',
            items: form,
            renderTo: prescribeMedicationForm.prescribeMedicationPanel.getEl(),
            buttons: [{
                text:'Opslaan',
                handler: handleForm.createDelegate(this)
            },{
                text: 'Annuleren',
                handler: function(){
                    this.win.hide();
                }.createDelegate(this)
            }]
        });

        this.win.show();
        this.loadDefaultValues();
    }


    //----------Events triggered from all UnitValues in this class ------
        this.SetLatestControlChanged = function(){};
        
        //triggered when a user changes a UnitValue control
        this.UpdateForm = function(control){
            this.checkCombinations(control);
            
            //Make sure quantity and dosageIncrement use the same unit
            if(control == this.quantity.spinner)
                this.dosageIncrement.unitCombo.setValue(this.quantity.unitCombo.getValue());

            if(control == this.dosageIncrement.spinner)
                this.quantity.unitCombo.setValue(this.dosageIncrement.unitCombo.getValue());

            var returnFunction = function(medicine){
                this.dosageIncrement.setValue(medicine.DoseIncrement);
                this.adminIncrement.setValue(medicine.ComponentIncrement);
                this.quantity.setValue(medicine.Quantity);
            }.createDelegate(this);
            
            Medicine.CalculateMedicine({
                DoseIncrement : this.dosageIncrement.getValue(),
                ComponentIncrement : this.adminIncrement.getValue(),
                Quantity : this.quantity.getValue(),
                Generic: this.generic.getValue(),
                Shape : this.shape.getValue(),
                Route: this.route.getValue()
            },returnFunction);
            
        }

        this.checkCombinations = function(control){
            var stateCount = 0;

            var combination = [this.dosageIncrement, this.adminIncrement, this.quantity];
            for(var c=0;c<combination.length;c++){
			    if (combination[c].getState() == "User" && combination[c].spinner.getValue() > 0) stateCount++;
			}
            if(stateCount == 3){
                for(var c=0;c<combination.length;c++){
                    console.log(combination[c]);
                    if(combination[c].spinner != control)
                        return combination[c].setCalculated();
                }
            }
        }
        this.GetLatestControlChanged = function(){};
        this.setCalculatedState = function(){};
        this.setUserState = function(){};
    //----------Events triggered from all unitValues in this class ------
}