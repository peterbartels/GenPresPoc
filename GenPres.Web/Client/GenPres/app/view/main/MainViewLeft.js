Ext.define('GenPres.view.main.MainViewLeft', {
    extend: 'Ext.Panel',
    layout:'vbox',
    region: 'west',
    xtype: 'panel',
    border:false,

    autoScroll:true,

    layout: {
        type: 'vbox',
        align: 'stretch'
    },

    width: 200,
    split: true,
    margins: '0 5 5 5',

    initComponent : function(){
        var me = this;
        me.items = [
            {
                xtype:'box',
                border:false,
                html:'<img src="Client/GenPres/style/images/logo.png" style="margin-top:22px;" />',
                height: 82
            },
            Ext.create('GenPres.view.main.PatientTree', {
                name:'mainPatientTree'
            })
        ];
        me.callParent();
    }
});