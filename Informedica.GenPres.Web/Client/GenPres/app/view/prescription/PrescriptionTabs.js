Ext.define('GenPres.view.prescription.PrescriptionTabs', {

    extend: 'Ext.tab.Panel',
    region: 'south',

    border:false,
    
    initComponent : function(){
        var me = this;

        me.items = [
            Ext.create('GenPres.view.prescription.PrescriptionGrid'),
            {
                title: 'Totalen',
                xtype:'box',
                html : 'Under construction'
            },
            {
                title: 'Overzicht',
                xtype:'box',
                html : 'Under construction'
            }
        ];

        me.callParent();
    },

    height: 200,
    split: true,
    margins: '0 5 5 5'
})