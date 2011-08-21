Ext.define('GenPres.view.prescription.DrugComposition', {

    extend: 'Ext.form.Panel',
    region: 'center',

    alias:'widget.drugcomposition',

    border:false,

    title:'Medicament',

    width:600,

    height:141,

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
            unit:'mg',
            width:140,
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

        var drugQuantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: '',
            unit:'mg',
            width:140,
            padding:'26 0 0 0',
            id:'drugQuantity',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'drugQuantity'
        });

        var substanceDrugConcentration = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Concentratie',
            unit:'mg',
            width:240,
            colspan:2,
            margin:'0 0 0 20',
            id:'substanceDrugConcentration',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'substanceDrugConcentration'
        });

        var solutionCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            name:'drugSolution',
            margin:'0 0 0 20',
            width:120,
            id:'drugSolution',
            action:'solution',
            fieldLabel: 'Solution'
        });


        var tablePanel = Ext.create('Ext.Panel', {
            border:false,
            margin:'10 10 10 10',
            layout : {
               type:'table',
                columns:4
            },
            items : [genericCombo, substanceQuantity, solutionCombo, drugQuantity, routeCombo, shapeCombo, substanceDrugConcentration]
        });

        me.items = tablePanel;

        me.callParent();
    }
});