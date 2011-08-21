Ext.define('GenPres.view.prescription.Options', {

    extend: 'Ext.form.Panel',
    
    layout:{
        type:'table',
        columns:4
    },
    alias:'widget.prescriptionformoptions',

    collapsible:true,
    
    border:false,

    title:'Opties',

    width:270,

    height:73,

    initComponent : function(){
        var me = this;

        var continuous = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Continu',
            labelAlign:'top',
            labelSeparator:'',
            style: { textAlign: 'center'},
            width:60,
            name:'prescriptionContinuous'
        });

        var infusion = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Inlooptijd',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:70,
            labelSeparator:'',
            name:'prescriptionInfusion'
        });

        var onRequest = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Indien nodig',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:74,
            labelSeparator:'',
            name:'prescriptionOnRequest'
        });

        solution = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Oplossing',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:60,
            labelSeparator:'',
            name:'prescriptionSolution'
        });



        me.items = [continuous, infusion, onRequest, solution];
        me.callParent();

    }

});
