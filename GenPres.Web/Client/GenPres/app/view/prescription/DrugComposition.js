Ext.define('GenPres.view.prescription.DrugComposition', {

    extend: 'Ext.form.Panel',
    region: 'center',

    alias:'widget.drugcomposition',

    border:false,

    title:'Medicament',

    width:400,

    initComponent : function(){
        var me = this;

        var genericCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            name:'drugGeneric',
            id:'drugGeneric',
            action:'generic',
            fieldLabel: 'Generiek'
        });

        var substanceQuantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Hoeveelheid',
            labelAlign:'top',
            id:'substanceQuantity',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'substanceQuantity'
        });
        
        var routeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.RouteStore',
            id:'drugRoute',
            name:'drugRoute',
            action:'route',
            fieldLabel: 'Toedieningsweg'
        });

        var shapeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: Ext.create('GenPres.store.prescription.ShapeStore'),
            id:'drugShape',
            name:'drugShape',
            action:'shape',
            width:140,
            fieldLabel: 'Toedieningsvorm'
        });

        var tablePanel = Ext.create('Ext.Panel', {
            border:false,
            margin:'10 10 10 10',
            layout : {
               type:'table',
               columns:2
            },
            items : [genericCombo, substanceQuantity, routeCombo, shapeCombo]
        });

        me.items = tablePanel;

        me.callParent();
    }
});