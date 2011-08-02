
Ext.onReady(function () {

    var tablePanel = Ext.create('Ext.form.Panel', {
        width:400,
        height:400,
        layout : {
           type:'table',
           columns:1
        },
        items : [{
            xtype: 'fieldcontainer',
            labelAlign:'top',
            fieldLabel: 'Test',
            labelWidth: 100,
            layout: 'hbox',
            items: [Ext.create('Ext.form.field.Number', {
                value:99,
                flex:1
            })]
        }]
    });

    var win = Ext.create('Ext.Window', {
        layout: 'fit',
        width:500,
        height:500,
        items: [tablePanel]
    });
    win.show();
})