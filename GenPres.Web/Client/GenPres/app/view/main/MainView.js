Ext.define('GenPres.view.main.MainView', {
    extend: 'Ext.Panel',
    layout:'border',
    items: [
        {
            title: 'South Region is resizable',
            region: 'west',
            xtype: 'panel',
            items:[{html:'west'}],
            width: 250,
            split: true,
            margins: '0 5 5 5'
        },{
            title: 'South Region is resizable',
            region: 'center',
            xtype: 'panel',
            items:[{html:'center'}],
            height: 100,
            split: true,
            margins: '0 5 5 5'
        }
    ],

    constructor : function(config){
        var me = this;
        me.callParent(arguments);
        GenPresApplication.viewport.items.add(me);
        GenPresApplication.viewport.doLayout();
    }
});