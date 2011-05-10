Ext.define('GenPres.model.patient.LogicalUnitModel', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    fields: [
        { name: 'id', type: 'float' },
        { name: 'text', type: 'string' },
        { name: 'leaf', type: 'boolean' },
            
        { name: 'PID', type: 'boolean' },
        { name: 'FirstName', type: 'boolean' },
        { name: 'LastName', type: 'boolean' }
    ],

    proxy : {
        type:'direct',
        directFn : Patient.GetPatientsByLogicalUnit,
        extraParams : {
            logicalUnitId : 1
        }
    }
});
