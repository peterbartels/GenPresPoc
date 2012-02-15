Ext.define('GenPres.view.prescription.Dose', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:3
    },
    alias:'widget.prescriptionformdose',
    
    border:false,

    title:'Dosering',

    width:870,

    colspan:2,

    bodyPadding:'0 0 0 5',

    bodyCls: 'presriptionFormCategory',

    initComponent : function(){
        var me = this;

        var quantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel:'Per keer',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'doseQuantity',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            adjustStore: Ext.create('GenPres.store.prescription.AdjustUnit'),
            name:'doseQuantity'
        });

        var total = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Totaal',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'doseTotal',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            adjustStore: Ext.create('GenPres.store.prescription.AdjustUnit'),
            timeStore: Ext.create('GenPres.store.prescription.TotalTimeUnit'),
            name:'doseTotal'
        });

        var rate = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Snelheid',
            unit:'mg',
            labelAlign:'top',
            id:'doseRate',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            adjustStore: Ext.create('GenPres.store.prescription.AdjustUnit'),
            timeStore: Ext.create('GenPres.store.prescription.RateUnit'),
            name:'doseRate'
        });
        me.items = [quantity, total, rate];
        me.callParent();

    }

});
