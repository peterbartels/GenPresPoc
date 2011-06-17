Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend: 'Ext.app.Controller',

    stores:[],

    models:[],

    views:['main.PatientTree', 'prescription.PrescriptionForm'],

    init: function() {
        this.control({
            'treepanel': {
                itemclick: this.loadPrescriptionForm
            }
        });
    },


    loadPrescriptionForm : function(tree, record){
        var form = Ext.create('GenPres.view.prescription.PrescriptionForm');
        GenPresApplication.MainCenter.items.removeAt(0);
        GenPresApplication.MainCenter.items.removeAll();
        GenPresApplication.MainCenter.items.add(form);
        GenPresApplication.MainCenter.doLayout();
    }
});