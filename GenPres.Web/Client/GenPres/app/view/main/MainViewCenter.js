Ext.define('GenPres.view.main.MainViewCenter', {
    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',
    dockedItems: Ext.create('GenPres.view.main.TopToolbar'),
    items:[{html:'<br /><br /><h1>&nbsp;&nbsp;&nbsp;Welkom bij GenPres - Development version</h1>'}],
    height: 100,
    split: true,
    margins: '0 5 5 5'
})