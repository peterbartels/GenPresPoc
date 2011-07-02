Ext.define('GenPres.view.prescription.PrescriptionTabs', {

    extend: 'Ext.tab.Panel',
    region: 'south',

    border:false,
    
    initComponent : function(){
        var me = this;

        me.items = [
            {
                title: 'Voorschriften',
                items : Ext.create('GenPres.view.prescription.PrescriptionGrid')
            },
            {
                title: 'Totalen',
                html : 'Under construction'
            },
            {
                title: 'Overzicht',
                html : 'Under construction'
            }
        ];

        me.callParent();
    },

    height: 200,
    split: true,
    margins: '0 5 5 5'
})