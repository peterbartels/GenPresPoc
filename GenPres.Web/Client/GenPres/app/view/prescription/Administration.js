Ext.define('GenPres.view.prescription.Administration', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:3
    },
    alias:'widget.prescriptionformadministration',

    title:'toediening',

    border:false,

    width:870,

    colspan:2,

    bodyPadding:'0 0 0 5',

    bodyCls: 'presriptionFormCategory',

    initComponent : function(){
        var me = this;

        var quantity = Ext.create('GenPres.control.UnitValueField', {
            unit:'mg',
            width:260,
            labelAlign:'top',
            fieldLabel:'Per keer',
            id:'adminQuantity',
            unitStore: Ext.create('GenPres.store.prescription.ComponentUnit'),
            name:'adminQuantity'
        });

        var total = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel:'Totaal',
            unit:'mg',
            width:260,
            labelAlign:'top',
            id:'adminTotal',
            unitStore: Ext.create('GenPres.store.prescription.ComponentUnit'),
            totalStore: Ext.create('GenPres.store.prescription.TotalTimeUnit'),
            name:'adminTotal'
        });

        var rate = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Snelheid',
            unit:'mg',
            labelAlign:'top',
            id:'adminRate',
            unitStore: Ext.create('GenPres.store.prescription.ComponentUnit'),
            timeStore: Ext.create('GenPres.store.prescription.RateUnit'),
            name:'adminRate'
        });

        me.items = [quantity, total, rate];
        me.callParent();

    }

});
