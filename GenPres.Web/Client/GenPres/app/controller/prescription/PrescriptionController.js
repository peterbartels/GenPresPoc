Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend:'Ext.app.Controller',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore', 'prescription.Prescription'],

    models:['prescription.Prescription'],

    views:['prescription.PrescriptionToolbar', 'prescription.DrugComposition', 'main.PatientTree', 'prescription.PrescriptionForm', 'main.TopToolbar', 'prescription.PrescriptionGrid'],

    substanceUnitStore:null,

    prescriptionIsLoading: false,

    init: function() {

        var me = this;

        me.control({
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
            'unitvaluefield' :{
                userChange : function(){
                    GenPres.lib.Prescription.UserStateCheck.checkStates(me.getControls());
                }
            },
            'checkboxfield' :{
                change : this.updatePrescription
            }
        });
    },

    getControls: function(){
        var me = this;
        var form = me.getForm();
        return form.query('unitvaluefield');
    },

    updatePrescription: function(){
        var me = this;

        var PID = GenPres.session.PatientSession.patient.PID;
        me.prescriptionIsLoading = true;
        
        var returnFunc = function(newValues){
            me.setValues(newValues);
            me.prescriptionIsLoading = false;
        };

        GenPres.ASyncEventManager.registerDirectEvent(Prescription.UpdatePrescription, [PID, Ext.Function.bind(me.getValues, me), returnFunc]);
        GenPres.ASyncEventManager.execute();
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
        var resultFunc = function(result){
            this.setValues(record.data);
            var drugController = GenPres.application.getController('prescription.DrugComposition');
            drugController.changeSelection(drugController.getComboBox("generic"));
            drugController.changeSelection(drugController.getComboBox("route"));
            drugController.changeSelection(drugController.getComboBox("shape"));
        };
        //GenPres.ASyncEventManager.registerDirectEvent(Prescription.GetPrescriptionById, [record.data.Id, resultFunc]);
        //GenPres.ASyncEventManager.execute();
    },

    setValues: function(data){
        var form = this.getForm();

        Ext.Object.each(data, function(key, value){
            var components = form.query('#'+ key);
            if(components.length > 0){
                var component = components[0];
                component.suspendEvents();
                component.setValue(value);
                component.resumeEvents();
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
        this.getDrugCompositionController().clear();
        var me = this;
        GenPres.ASyncEventManager.registerDirectEvent(Prescription.ClearPrescription, [function(newValues){
            me.setValues(newValues);
        }]);
        GenPres.ASyncEventManager.execute();
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

        return vals;
    }
});