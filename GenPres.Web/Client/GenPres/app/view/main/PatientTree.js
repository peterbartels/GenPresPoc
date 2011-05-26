Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',

    border:false,

    folderSort: true,
    useArrows: true,

    scroll:'both',
    autoScroll:true,

    store: 'GenPres.app.patient.PatientTreeStore',

    constructor : function(){
        var me = this;
        me.callParent();
    },

    initComponent : function(){
        var me = this;
        var hackfunc = function(){
            //me.store.model.proxy.extraParams.logicalUnitId = GenPres.session.PatientSession.getLogicalUnitId();
            //me.store.load();
        }
        //Ext.Function.defer(hackfunc,2000,this);
        me.callParent();
    }
});