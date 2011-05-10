Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',
    
    folderSort: true,
    useArrows: true,

    initComponent : function(){
        this.store = Ext.create('GenPres.store.patient.PatientStore');
        this.callParent();
    }
});