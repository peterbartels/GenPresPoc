Ext.define('GenPres.model.patient.LogicalUnitModel', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    fields: [
        { name: 'id', type: 'float' },
        { name: 'text', type: 'string' },
        { name: 'leaf', type: 'boolean' }
    ],

    proxy : {
        type:'direct',
        directFn : Patient.GetLogicalUnits
    }
});
