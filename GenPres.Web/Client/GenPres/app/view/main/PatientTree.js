Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',

    border:false,

    folderSort: true,
    useArrows: true,

    scroll:'both',
    autoScroll:true,

    initComponent : function(){
        this.store = Ext.create('GenPres.store.patient.PatientStore');
        this.callParent();
    }
});