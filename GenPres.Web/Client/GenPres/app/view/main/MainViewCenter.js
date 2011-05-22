Ext.define('GenPres.view.main.MainViewCenter', {

    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',

    border:false,

    dockedItems: Ext.create('GenPres.view.main.TopToolbar'),

    items:
        Ext.create('GenPres.control.ValueField', {
            labelWidth: 70,
            fieldLabel: 'Default',
            name: 'basic',
            value: 1,
            step:0.1776,
            decimalPrecision:10,
            width:150,
            minValue: 0,
            maxValue: 125
        }
    ),
    
    height: 100,
    split: true,
    margins: '0 5 5 5'
})