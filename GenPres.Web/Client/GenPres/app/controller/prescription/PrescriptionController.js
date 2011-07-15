Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend:'Ext.app.Controller',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore', 'prescription.Prescription'],

    models:['prescription.Prescription'],

    views:['prescription.PrescriptionToolbar', 'prescription.DrugComposition', 'main.PatientTree', 'prescription.PrescriptionForm', 'main.TopToolbar', 'prescription.PrescriptionGrid'],

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
            }
        });
    },

    loadPrescription : function(view, record, htmlItem, index, event, options){
        Prescription.GetPrescriptionById(record.data.Id, function(result){
            this.setValues(record);
        }, this);
    },

    setValues: function(record){
        var forms = this.getForms();
        for(var i=0; i<forms.length; i++){
            Ext.Object.each(record.data, function(key, value){
                var components = forms[i].query('#'+ key);
                if(components.length > 0){
                    var component = components[i];
                    component.setValue(value);
                }
            }, this);
        }
    },

    loadPrescriptionForm : function(tree, record){
        if(GenPresApplication.MainCenterContainer.items.length == 1){
            var form = Ext.create('GenPres.view.prescription.PrescriptionForm');
            GenPresApplication.MainCenterContainer.items.add(form);
            GenPresApplication.MainCenterContainer.doLayout();
        }
        GenPresApplication.MainCenterContainer.layout.setActiveItem(1);
    },

    loadHome : function(){
        GenPresApplication.MainCenterContainer.layout.setActiveItem(0);
    },

    clearPrescription : function(){
        var drugCompositionController = GenPresApplication.getController('prescription.DrugComposition');
        drugCompositionController.clear();
    },

    getForms : function(){
        var prescriptionform = GenPresApplication.MainCenter.query('prescriptionform')[0];
        return prescriptionform.query('form');
    },
    
    savePrescription:function(){
        var vals = {};
        var forms = this.getForms();

        for(var i=0; i<forms.length; i++)
            vals = forms[i].getValues();

        var prescriptiongrid = GenPresApplication.MainCenter.query('prescriptiongrid')[0];
        var PID = GenPres.session.PatientSession.patient.PID;
        Prescription.SavePrescription(PID, vals, function(newValues){
            prescriptiongrid.store.load();
        })
    }

});