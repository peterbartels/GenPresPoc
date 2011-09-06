Ext.define('GenPres.view.prescription.FrequencyDuration', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:2
    },
    alias:'widget.prescriptionformfrequencyduration',
    
    border:false,

    title:'Frequentie en tijdsduur',

    width:600,

    height:53,

    bodyCls: 'presriptionFormCategory',

    bodyPadding:'0 0 0 5',

    initComponent : function(){
        var me = this;

        var frequencyStore = Ext.create('GenPres.store.prescription.LocalUnit', {
            data : [{"Value":"dag", selected: true}]
        });

        var frequency = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Frequentie',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'prescriptionFrequency',
            timeStore: frequencyStore,
            name:'prescriptionFrequency'
        });

        var durationStore = Ext.create('GenPres.store.prescription.LocalUnit', {
            data : [{"Value":"min", selected: false},{"Value":"uur", selected: true}]
        });

        var duration = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Toedienduur',
            unit:'mg',
            labelAlign:'top',
            id:'prescriptionDuration',
            timeStore: durationStore,
            name:'prescriptionDuration'
        });

        me.items = [frequency, duration];
        me.callParent();
    }
});
