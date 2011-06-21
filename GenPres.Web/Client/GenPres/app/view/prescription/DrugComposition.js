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
            labelAlign:'top',
            fieldLabel: 'Toedieningsweg'
        });

        var shapeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.ShapeStore',
            displayField: 'Value',
            labelAlign:'top',
            fieldLabel: 'Toedieningsvorm'
        });

        var tablePanel = Ext.create('Ext.Panel', {
            border:false,
            layout : {
               type:'table',
                columns:2
            },
            items : [genericCombo, routeCombo, shapeCombo, quantity]
        });

        me.items = tablePanel;

        me.callParent();
    }
});