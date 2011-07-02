
Ext.define('GenPres.store.prescription.GenericStore', {
    extend: 'Ext.data.Store',
    alias: 'widget.genericstore',
    proxy : {
        type:'direct',
        directFn : Prescription.GetGenerics,
        paramOrder: ['route', 'shape'],
        extraParams:{
            route: "test1",
            shape : "test2"
        }
    },
    autoLoad:false,
    fields: [
        { name: 'Value', type: 'string' }
    ]
});

Ext.define('GenPres.view.prescription.DrugComposition', {
    extend: 'Ext.Panel',
    xtype: 'panel',
    border:false,
    initComponent : function(){
        var me = this;
        var genericCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            displayField: 'Value',
            labelAlign:'top',
            fieldLabel: 'Generiek'
        });
        me.items = genericCombo;
        me.callParent();
    }
});
Ext.define('GenPres.controller.prescription.PrescriptionController', {
    extend: 'Ext.app.Controller',
    stores: ['prescription.GenericStore'],
    models: [],
    views: ['prescription.DrugComposition']
});


var GenPresApplication;

Ext.application ({
    name: 'GenPres',

    autoCreateViewport: false,
    appFolder: './Client/GenPres/app',

    controllers: [
        'prescription.PrescriptionController'
    ],

    launch: function() {
        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit',
            items : [Ext.create('GenPres.view.prescription.DrugComposition')]
        });
    }
});

