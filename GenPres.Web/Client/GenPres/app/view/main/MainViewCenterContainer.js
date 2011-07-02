Ext.define('GenPres.view.main.MainViewCenterContainer', {

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
        GenPresApplication.MainCenterContainer = this;
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
})