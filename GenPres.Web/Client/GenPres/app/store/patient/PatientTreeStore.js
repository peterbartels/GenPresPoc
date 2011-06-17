Ext.define('GenPres.store.patient.PatientTreeStore', {

    extend: 'Ext.data.TreeStore',

    alias: 'widget.patienttreestore',

    root: {
            text: 'Patienten',
            id: 'src',
            expanded: true
    },

    model:'GenPres.model.patient.PatientModel'
});