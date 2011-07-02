
Ext.define('GenPres.model.prescription.Prescription', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    autoLoad : true,

    fields: [
        { name: 'id', type: 'float' },
        { name: 'drugGeneric', type: 'string' },
        { name: 'drugRoute', type: 'string' },
        { name: 'drugShape', type: 'string' },
        { name: 'StartDate', type: 'string' }
    ],
    proxy : {
        type:'direct',
        directFn : Prescription.GetPrescriptions
    }
});
