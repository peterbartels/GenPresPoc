Ext.define('GenPres.view.prescription.Administration', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:3
    },
    alias:'widget.prescriptionformadministration',

    collapsible:true,

    border:false,

    title:'Toediening',

    width:700,

    colspan:2,

    initComponent : function(){
        var me = this;

        var quantity = Ext.create('GenPres.control.UnitValueField', {
            unit:'mg',
            width:200,
            labelAlign:'top',
            fieldLabel:'Per keer',
            id:'adminQuantity',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'adminQuantity'
        });

        var total = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel:'Totaal',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'adminTotal',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'adminTotal'
        });

        var rate = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Snelheid',
            unit:'mg',
            labelAlign:'top',
            id:'adminRate',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'adminRate'
        });
        me.items = [quantity, total, rate];
        me.callParent();

    }

});
