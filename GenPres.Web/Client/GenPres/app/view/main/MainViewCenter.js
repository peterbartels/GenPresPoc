Ext.define('GenPres.view.main.MainViewCenter', {

    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',

    border:false,

    initComponent : function(){
        var me = this;

        me.items = [
            {
                html:'<br /><br /><h1>&nbsp;&nbsp;&nbsp;Welkom bij GenPres - Development version</h1>',
                border:false
            }
            /*This is a codesmell: Ext.create('GenPres.control.ValueField', {
                labelWidth: 70,
                fieldLabel: 'Default',
                name: 'basic',
                value: 1,
                step:0.1776,
                decimalPrecision:10,
                width:150,
                minValue: 0,
                maxValue: 125
            })*/
        ];

        me.dockedItems = Ext.create('GenPres.view.main.TopToolbar');
        me.callParent();
        GenPresApplication.MainCenter = this;
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
})