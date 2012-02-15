
Ext.define('GenPres.view.prescription.PrescriptionToolbar', {
    extend:'Ext.container.ButtonGroup',
    dock: 'top',

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
