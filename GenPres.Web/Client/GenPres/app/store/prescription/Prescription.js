Ext.define('GenPres.store.prescription.Prescription', {

    extend: 'Ext.data.Store',

    alias: 'widget.prescriptionstore',

    autoLoad:true,

    model:'GenPres.model.prescription.Prescription'
});