
Ext.define('GenPres.control.UnitValueField', {

    extend:'Ext.form.Panel',

    width: 200,

    border: false,
    
    items: [{
        xtype: 'fieldcontainer',
        labelAlign:'top',
        fieldLabel: 'Hoeveelheid',
        labelWidth: 100,
        layout: 'hbox',
        items: [Ext.create('GenPres.control.ValueField')]
    }]
});