Ext.define('GenPres.view.prescription.Verbalization', {

    extend: 'Ext.view.View',
    
    colspan:4,

    alias:'widget.verbalization',

    tpl: new Ext.XTemplate('<tpl for=".">&nbsp;{verbalization}</tpl>'),

    store : Ext.create('Ext.data.Store', {
        fields: ['verbalization']
    }),

    initComponent : function(){
        var me=this;
        me.callParent();
    }
});
