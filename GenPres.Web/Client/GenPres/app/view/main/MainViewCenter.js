Ext.define('GenPres.view.main.MainViewCenter', {

    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',

    border:false,

    layout: 'border',

    initComponent : function(){
        var me = this;

        me.items = [
            Ext.create('GenPres.view.main.MainViewCenterContainer'),
            Ext.create('GenPres.view.prescription.PrescriptionTabs')
        ];
        GenPresApplication.MainCenter = this;
        me.callParent();
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
})