Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend:'Ext.app.Controller',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore', 'prescription.Prescription'],

    models:['prescription.Prescription'],

    views:['prescription.PrescriptionToolbar', 'prescription.DrugComposition', 'main.PatientTree', 'prescription.PrescriptionForm', 'main.TopToolbar', 'prescription.PrescriptionGrid'],

    substanceUnitStore:null,

    init: function() {
        this.control({
            'gridpanel' : {
                itemdblclick: this.loadPrescription
            },
            'treepanel': {
                itemclick: this.loadPrescriptionForm
            },
            'button[action=home]': {
                click : this.loadHome
            },
            'button[action=new]': {
                click : this.clearPrescription
            },
            'button[action=save]': {
                click : this.savePrescription
            },
            'valuefield' : {
                blur : this.updatePrescription
            },
            'combobox[isFormField=false]' :{
                change : this.updatePrescription
            }
        });
    },


    updatePrescription: function(){
        var me = this;
        if(this.getDrugCompositionController().drugIsChosen()){
            var PID = GenPres.session.PatientSession.patient.PID;
            Prescription.UpdatePrescription(PID, this.getValues(), function(newValues){
                me.setValues(newValues);
            });
        }
    },

    getDrugCompositionController : function(){
        return GenPres.application.getController('prescription.DrugComposition');
    },

    getSubstanceUnitStore : function(){
        if(this.substanceUnitStore == null){
            this.substanceUnitStore = Ext.create('');
        }
        return this.substanceUnitStore;
    },

    loadPrescription : function(view, record, htmlItem, index, event, options){
        Prescription.GetPrescriptionById(record.data.Id, function(result){
            this.setValues(record.data);
            var drugController = GenPres.application.getController('prescription.DrugComposition');
            drugController.changeSelection(drugController.getComboBox("generic"));
            drugController.changeSelection(drugController.getComboBox("route"));
            drugController.changeSelection(drugController.getComboBox("shape"));
        }, this);
    },

    setValues: function(data){
        var form = this.getForm();

        Ext.Object.each(data, function(key, value){
            var components = form.query('#'+ key);
            if(components.length > 0){
                var component = components[0];
                component.setValue(value);
            }
        }, this);

    },

    loadPrescriptionForm : function(tree, record){
        var me = this;
        if(GenPres.application.MainCenterContainer.items.length == 1){
            var form = Ext.create('GenPres.view.prescription.PrescriptionForm');
            GenPres.application.MainCenterContainer.items.add(form);
            GenPres.application.MainCenterContainer.doLayout();
        }
        GenPres.application.MainCenterContainer.layout.setActiveItem(1);
        me.clearPrescription();
    },

    loadHome : function(){
        GenPres.application.MainCenterContainer.layout.setActiveItem(0);
    },

    clearPrescription : function(){
        //this.getDrugCompositionController().clear();
        var me = this;
        Prescription.ClearPrescription(function(newValues){
            me.setValues(newValues);
        });
    },

    getForm : function(){
        return GenPres.application.MainCenter.query('prescriptionform')[0];
    },
    savePrescription:function(){
        var prescriptiongrid = GenPres.application.MainCenter.query('prescriptiongrid')[0];
        var PID = GenPres.session.PatientSession.patient.PID;
        Prescription.SavePrescription(PID, this.getValues(), function(newValues){
            prescriptiongrid.store.load();
        })
    },
    getValues:function(){
        var vals = {};
        var form = this.getForm();

        Ext.Object.each(form.getValues(), function(key, value, myself) {
            vals[key] = value;
        });
        console.log(vals);
        return vals;
    }
});