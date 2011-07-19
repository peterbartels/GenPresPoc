Ext.define('GenPres.view.prescription.DrugComposition', {

    extend: 'Ext.form.Panel',
    region: 'center',

    border:false,
    
    initComponent : function(){
        var me = this;

        var genericCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            displayField: 'Value',
            id:'drugGeneric',
            action:'generic',
            labelAlign:'top',
            value:'paracetamol',
            fieldLabel: 'Generiek'
        });

        var quantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Quantity',
            labelAlign:'top'
        });
        
        var routeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.RouteStore',
            id:'drugRoute',
            displayField: 'Value',
            action:'route',
            labelAlign:'top',
            fieldLabel: 'Toedieningsweg'
        });

        var shapeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: Ext.create('GenPres.store.prescription.ShapeStore'),
            displayField: 'Value',
            id:'drugShape',
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