Ext.define('GenPres.store.prescription.Prescription', {

    extend: 'Ext.data.Store',

    alias: 'widget.prescriptionstore',

    autoLoad:false,

    model:'GenPres.model.prescription.Prescription'
});