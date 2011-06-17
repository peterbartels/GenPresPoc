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

        /*me.items = [
            Ext.create('GenPres.view.main.MainViewLeft'),
            Ext.create('GenPres.view.main.MainViewCenter')
        ];*/

        me.items = [
            Ext.create('GenPres.view.prescription.DrugComposition')
        ];

        me.callParent();

        return me;
    }
});