Ext.define('GenPres.view.prescription.DrugComposition', {

    extend: 'Ext.form.Panel',
    region: 'center',

    alias:'widget.drugcomposition',

    border:false,

    title:'Medicament',

    width:600,

    height:111,

    bodyPadding:'0 0 0 5',

    bodyCls: 'presriptionFormCategory',

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
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
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
            store: 'prescription.ShapeStore',
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
            unitStore: GenPres.store.PrescriptionStores.getComponentUnitStore(),
            name:'drugQuantity'
        });

        var substanceDrugConcentration = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Concentratie',
            unit:'mg',
            width:240,
            colspan:2,
            margin:'0 0 0 20',
            id:'substanceDrugConcentration',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            totalStore: GenPres.store.PrescriptionStores.getComponentUnitStore(),
            name:'substanceDrugConcentration'
        });

        var solutionCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            name:'drugSolution',
            margin:'0 0 0 20',
            width:120,
            id:'drugSolution',
            action:'solution',
            style:{visibility:'hidden'},
            fieldLabel: 'Oplossing'
        });

        var tablePanel = Ext.create('Ext.Container', {
            border:false,
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