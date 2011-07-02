Ext.define('GenPres.view.prescription.DrugComposition', {

    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',

    border:false,
    
    initComponent : function(){
        var me = this;

        var genericCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            displayField: 'Value',
            action:'generic',
            labelAlign:'top',
            fieldLabel: 'Generiek'
        });

        var quantity = Ext.create('Ext.form.field.Number', {
            fieldLabel: 'Quantity',
            labelAlign:'top'
        });
        
        var routeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.RouteStore',
            displayField: 'Value',
            action:'route',
            labelAlign:'top',
            fieldLabel: 'Toedieningsweg'
        });

        var shapeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.ShapeStore',
            displayField: 'Value',
            action:'shape',
            labelAlign:'top',
            fieldLabel: 'Toedieningsvorm'
        });

        var tablePanel = Ext.create('Ext.Panel', {
            border:false,
            margin:'10 10 10 10',
            layout : {
               type:'table',
                columns:2
            },
            items : [genericCombo, quantity, routeCombo, shapeCombo]
        });

        me.items = tablePanel;

        me.callParent();
    }
});