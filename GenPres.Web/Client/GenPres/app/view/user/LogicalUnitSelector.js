Ext.define('GenPres.view.user.LogicalUnitSelector', {
    extend: 'Ext.view.View',

    alias: 'widget.logicalunitselector',

    margin:'20 0 0 16',

    itemSelector: 'div.LogicalUnitDataViewItem',

    emptyText: 'No images available',

    singleSelect: true,
    
    store: Ext.create('GenPres.store.patient.LogicalUnitStore'),

    tpl: new Ext.XTemplate(
            '<tpl for="."><div class="LogicalUnitDataView">',
                '<div class="LogicalUnitDataViewItem">',
                    '{text}',
                '</div>',
            '</div></tpl>')
});