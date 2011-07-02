
Ext.define('GenPres.view.prescription.PrescriptionGrid', {

    extend:'Ext.Panel',

    border:false,
    
    initComponent : function(){
        var me = this;
        me.items = [
            Ext.create('Ext.button.Button', {
                text:'Nieuw',
                action:'new'
            })
        ]
        me.callParent();
    }
})
