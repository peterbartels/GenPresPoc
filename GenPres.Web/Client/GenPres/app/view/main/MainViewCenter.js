Ext.define('GenPres.view.main.MainViewCenter', {

    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',

    activeItem: 0,
    
    border:false,

    layout: 'card',

    initComponent : function(){
        var me = this;

        me.items = [
            {
                id: 'card-0',
                html:'<br /><br /><h1>&nbsp;&nbsp;&nbsp;Welkom bij GenPres - Development version</h1>',
                border:false
            }
        ];

        me.dockedItems = Ext.create('GenPres.view.main.TopToolbar');
        me.callParent();
        GenPresApplication.MainCenter = this;
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
})