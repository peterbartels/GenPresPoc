
Ext.onReady(function(){

    console.log(Prescription.GetGenerics);

    var store = Ext.create('Ext.data.DirectStore', {
        directFn : Prescription.GetGenerics,
        fields: [
            { name: 'Value', type: 'string' }
        ],
        extraParams:{
            route:'rect',
            shape:'zetp'
        },
        paramOrder:['route','shape'],
        autoLoad:true
    });

    var win = Ext.create('Ext.Window', {
        layout: 'fit',
        items : [{
            xtype:'combo',
            displayField: 'Value',
            fieldLabel:'Test',
            store:store
        }]
    });
    win.show();
})