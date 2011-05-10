Ext.define('GenPres.store.patient.PatientStore', {

    extend: 'Ext.data.TreeStore',

    alias: 'widget.patientstore',

    root: {
            text: 'Patienten',
            id: 'src',
            expanded: true
    },
    
    proxy : {
        type:'direct',
        directFn : Patient.GetPatientsByLogicalUnit
    },

    autoLoad:true,

    model:'GenPres.model.patient.PatientModel'
});