Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',

    border:false,

    folderSort: true,
    useArrows: true,

    scroll:'both',
    autoScroll:true,

    store: 'patient.PatientTreeStore',

    constructor : function(){
        var me = this;
        me.callParent();
        
        me.store.model.proxy.extraParams.logicalUnitId = GenPres.session.PatientSession.getLogicalUnitId();
        me.store.load();

    },

    initComponent : function(){
        var me = this;
        me.callParent();

    }
});