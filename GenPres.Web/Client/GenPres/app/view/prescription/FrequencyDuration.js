Ext.define('GenPres.view.prescription.FrequencyDuration', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:2
    },
    alias:'widget.prescriptionformfrequencyduration',

    collapsible:true,
    
    border:false,

    title:'Frequentie en tijdsduur',

    width:600,

    initComponent : function(){
        var me = this;

        var frequency = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Frequentie',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'prescriptionFrequency',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'prescriptionFrequency'
        });

        var duration = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Toedienduur',
            unit:'mg',
            labelAlign:'top',
            id:'prescriptionDuration',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'prescriptionDuration'
        });

        me.items = [frequency, duration];
        me.callParent();

    }

});
