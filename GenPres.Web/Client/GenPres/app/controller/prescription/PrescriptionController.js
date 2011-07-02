Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend:'Ext.app.Controller',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore', 'prescription.Prescription'],

    models:[],

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
            }
        });
    },

    loadPrescriptionForm : function(tree, record){
        if(GenPresApplication.MainCenter.items.length == 1){
            var form = Ext.create('GenPres.view.prescription.PrescriptionForm');
            GenPresApplication.MainCenter.items.add(form);
            GenPresApplication.MainCenter.doLayout();
        }
        GenPresApplication.MainCenter.layout.setActiveItem(1);
    },

    loadHome : function(){
        GenPresApplication.MainCenter.layout.setActiveItem(0);
    },

    clearPrescription : function(){
        var drugCompositionController = GenPresApplication.getController('prescription.DrugComposition');
        drugCompositionController.clear();
    }

});