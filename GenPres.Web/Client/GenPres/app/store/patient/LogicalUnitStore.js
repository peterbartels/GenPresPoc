Ext.define('GenPres.store.patient.LogicalUnitStore', {

    extend: 'Ext.data.Store',

    alias: 'widget.logicalunitstore',
    
    root: {
            text: 'Patienten',
            id: 'src',
            expanded: true
    },

    autoLoad:true,

    model:'GenPres.model.patient.LogicalUnitModel'
});