Ext.define('GenPres.view.main.MainView', {

    extend: 'Ext.Panel',
    layout:'border',
    
    constructor : function(){
        var me = this;
        me.callParent(arguments);
    },

    initComponent : function(){
        var me = this;
        
        me.items = [
            Ext.create('GenPres.view.main.MainViewLeft'),
            Ext.create('GenPres.view.main.MainViewCenter')
        ];
        me.callParent();

        GenPresApplication.viewport.items.add(me);
        GenPresApplication.viewport.doLayout();
        
        return me;
    }
});