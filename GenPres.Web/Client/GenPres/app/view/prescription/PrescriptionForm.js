Ext.define('GenPres.view.prescription.PrescriptionForm', {

    extend: 'Ext.Panel',

    region:'center',

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

        me.items = [{html:'test'}];

        me.callParent();

        return me;
    }
});