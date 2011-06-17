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

        me.items = [genericCombo, routeCombo, shapeCombo];

        me.callParent();
    }
});