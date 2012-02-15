
Ext.onReady(function () {

    var tablePanel = Ext.create('Ext.form.Panel', {
        width:400,
        height:400,
        layout : {
           type:'table',
           columns:1
        },
        items : [
            Ext.create('GenPres.control.ValueField',{
                fieldLabel:'test',
                margin:'50 50 50 50',
                step:0.1,
                value:0
            })
        ]
    });

    var win = Ext.create('Ext.Window', {
        layout: 'fit',
        width:500,
        height:500,
        items: [tablePanel]
    });
    win.show();
})