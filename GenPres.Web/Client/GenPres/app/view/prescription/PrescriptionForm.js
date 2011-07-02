Ext.define('GenPres.view.prescription.PrescriptionForm', {

    extend: 'Ext.Panel',

    id: 'card-prescriptionForm',

    border:false,
    
    constructor : function(){
        var me = this;
        me.callParent(arguments);
    },

    initComponent : function(){
        var me = this;
        
        me.items = [

            Ext.create('GenPres.view.prescription.DrugComposition'),
            Ext.create('Ext.button.Button', {
                width:100,
                height:50,
                text:'Opslaan',
                margin:'10 10 10 10'
            })
        ];

        me.dockedItems = Ext.create('GenPres.view.prescription.PrescriptionToolbar');

        me.callParent();

        return me;
    }
});