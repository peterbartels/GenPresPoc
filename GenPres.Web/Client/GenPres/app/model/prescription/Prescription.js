
Ext.define('GenPres.model.prescription.Prescription', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    fields: [
        { name: 'id', type: 'float' },
        { name: 'drugGeneric', type: 'string' },
        { name: 'drugRoute', type: 'string' },
        { name: 'drugShape', type: 'string' }
    ],
    proxy : {
        type:'direct',
        directFn : Prescription.GetPrescriptions
    }
});
