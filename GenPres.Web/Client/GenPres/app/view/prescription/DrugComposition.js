Ext.define('GenPres.view.prescription.DrugComposition', {

    extend: 'Ext.form.Panel',
    region: 'center',

    border:false,
    
    initComponent : function(){
        var me = this;

        var genericCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            displayField: 'Value',
            name:'drugGeneric',
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
            name:'drugRoute',
            displayField: 'Value',
            action:'route',
            labelAlign:'top',
            fieldLabel: 'Toedieningsweg'
        });

        var shapeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.ShapeStore',
            displayField: 'Value',
            name:'drugShape',
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