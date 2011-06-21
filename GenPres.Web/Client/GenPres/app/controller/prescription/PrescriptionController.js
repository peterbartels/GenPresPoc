Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend: 'Ext.app.Controller',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore'],

    models:[],

    views:['prescription.DrugComposition', 'main.PatientTree', 'prescription.PrescriptionForm', 'main.TopToolbar'],

    init: function() {
        this.control({
            'treepanel': {
                itemclick: this.loadPrescriptionForm
            },
            'button[action=home]': {
                click : this.loadHome
            }
        });
    },


    loadPrescriptionForm : function(tree, record){
        var form = Ext.create('GenPres.view.prescription.PrescriptionForm');
        GenPresApplication.MainCenter.items.add(form);
        GenPresApplication.MainCenter.doLayout();
        GenPresApplication.MainCenter.layout.setActiveItem(1);
    },

    loadHome : function(){
        GenPresApplication.MainCenter.layout.setActiveItem(0);
    }
});