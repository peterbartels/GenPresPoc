Ext.define('GenPres.view.main.MainView', {
    extend: 'Ext.Panel',
    layout:'border',
    items: [
        Ext.create('GenPres.view.main.MainViewLeft'),
        Ext.create('GenPres.view.main.MainViewCenter')
    ],
    
    constructor : function(){
        var me = this;
        me.callParent(arguments);
        GenPresApplication.viewport.items.add(me);
        GenPresApplication.viewport.doLayout();
    }
});