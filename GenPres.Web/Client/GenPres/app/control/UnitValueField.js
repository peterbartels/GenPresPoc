
Ext.define('GenPres.control.UnitValueField', {

    extend:'Ext.form.Panel',

    width: 200,

    border: false,
    
    items: [{
        xtype: 'fieldcontainer',
        labelAlign:'top',
        fieldLabel: 'Last Three Jobs',
        labelWidth: 100,
        layout: 'hbox',
        items: [Ext.create('GenPres.control.ValueField')]
    }]
});