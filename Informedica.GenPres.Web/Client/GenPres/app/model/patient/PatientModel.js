
Ext.define('GenPres.model.patient.PatientModel', {
    extend: 'Ext.data.Model',

    idProperty : 'PID',
    fields: [
        { name: 'text', type: 'string' },
        { name: 'leaf', type: 'boolean' },

        { name: 'PID', type: 'string' },
        { name: 'FirstName', type: 'string' },
        { name: 'LastName', type: 'string' },

        { name: 'Unit', type: 'string' },
        { name: 'Bed', type: 'string' },
        { name: 'RegisterDate', type: 'string' }
    ],
    proxy : {
        type:'direct',
        paramOrder:['node', 'logicalUnitId'],
        directFn : Patient.GetPatientsByLogicalUnit,
        extraParams:{
            logicalUnitId : 1
        }
    }
});
