
Ext.onReady(function(){

    Ext.define('GenPres.store.prescription.GenericStore', {
        extend: 'Ext.data.Store',
        alias: 'widget.genericstore',
        proxy : {
            type : 'direct',
            directFn : Prescription.GetGenerics,
            extraParams:{
                route:'rect',
                shape:'zetp'
            },
            paramOrder:['route','shape']
        },
        fields: [
            { name: 'Value', type: 'string' }
        ],
        autoLoad:true
    });

    var win = Ext.create('Ext.Window', {
        layout: 'fit',
        items : [{
            xtype:'combo',
            value:'paracetamol',
            displayField: 'Value',
            queryMode:'local',
            fieldLabel:'Test',
            store:Ext.create('GenPres.store.prescription.GenericStore')
        }]
    });
    win.show();
})