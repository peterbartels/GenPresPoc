Ext.define('GenPres.view.prescription.PrescriptionForm', {

    extend: 'Ext.form.Panel',

    alias : 'widget.prescriptionform',

    id: 'card-prescriptionForm',

    border:false,

    width:870,

    bodyPadding: '10',

    constructor : function(){
        var me = this;
        me.callParent(arguments);
    },

    initComponent : function(){
        var me = this;

        me.layout = {
            type:'table',
            columns:2
        }

        me.items = [
            Ext.create('GenPres.view.prescription.DrugComposition'),
            Ext.create('GenPres.view.prescription.Patient'),
            Ext.create('GenPres.view.prescription.FrequencyDuration'),
            Ext.create('GenPres.view.prescription.Options'),
            Ext.create('GenPres.view.prescription.Dose'),
            Ext.create('GenPres.view.prescription.Administration'),
            Ext.create('Ext.form.field.TextArea', {
                width:550,
                height:50,
                action:'save',
                fieldLabel:'Opmerkingen',
                margin:'10 10 10 10'
            }),
            Ext.create('Ext.button.Button', {
                width:100,
                height:50,
                action:'save',
                text:'Opslaan',
                margin:'10 10 10 0'
            })
        ];

        me.dockedItems = Ext.create('GenPres.view.prescription.PrescriptionToolbar');

        me.callParent();

        return me;
    }
});