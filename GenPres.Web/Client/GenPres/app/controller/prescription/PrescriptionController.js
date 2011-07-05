Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend:'Ext.app.Controller',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore', 'prescription.Prescription'],

    models:['prescription.Prescription'],

    views:['prescription.PrescriptionToolbar', 'prescription.DrugComposition', 'main.PatientTree', 'prescription.PrescriptionForm', 'main.TopToolbar', 'prescription.PrescriptionGrid'],

    init: function() {
        this.control({
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
    
    savePrescription:function(){
        var prescriptionform = GenPresApplication.MainCenter.query('prescriptionform')[0];
        var prescriptiongrid = GenPresApplication.MainCenter.query('prescriptiongrid')[0];
        var vals = {};
        var forms = [];
        forms = prescriptionform.query('form');
        for(var i=0; i<forms.length; i++){
            vals = forms[i].getValues();
        }
        Prescription.SavePrescription(vals, function(newValues){
            prescriptiongrid.store.load();
        })
    }

});