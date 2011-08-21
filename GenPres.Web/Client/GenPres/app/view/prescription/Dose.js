Ext.define('GenPres.view.prescription.Dose', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:3
    },
    alias:'widget.prescriptionformdose',

    collapsible:true,
    
    border:false,

    title:'Dosering',

    width:700,

    colspan:2,

    initComponent : function(){
        var me = this;

        var quantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel:'Per keer',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'doseQuantity',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'doseQuantity'
        });

        var total = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Totaal',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'doseTotal',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'doseTotal'
        });

        var rate = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Snelheid',
            unit:'mg',
            labelAlign:'top',
            id:'doseRate',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'doseRate'
        });
        me.items = [quantity, total, rate];
        me.callParent();

    }

});
