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
        paramOrder:['node', 'logicalUnitId'],
        directFn : Patient.GetPatientsByLogicalUnit,
        extraParams:{
            logicalUnitId : GenPres.session.PatientSession.getLogicalUnitId()
        }
    },

    autoLoad:true,

    model:'GenPres.model.patient.PatientModel'
});