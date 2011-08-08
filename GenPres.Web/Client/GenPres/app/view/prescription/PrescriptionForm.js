Ext.define('GenPres.view.prescription.PrescriptionForm', {

    extend: 'Ext.form.Panel',

    alias : 'widget.prescriptionform',

    id: 'card-prescriptionForm',

    border:false,

    width:800,

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
            Ext.create('Ext.button.Button', {
                width:100,
                height:50,
                action:'save',
                text:'Opslaan',
                margin:'10 10 10 10'
            })
        ];

        me.dockedItems = Ext.create('GenPres.view.prescription.PrescriptionToolbar');

        me.callParent();

        return me;
    }
});