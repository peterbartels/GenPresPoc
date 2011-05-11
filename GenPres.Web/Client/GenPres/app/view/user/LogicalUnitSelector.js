Ext.define('GenPres.view.user.LogicalUnitSelector', {
    extend: 'Ext.view.View',
    alias: 'widget.logicalunitselector',
    margin:'20 0 0 16',
    itemSelector: 'div.LogicalUnitDataViewItem',
    emptyText: 'No images available',
    singleSelect: true,
    listeners : {
        itemclick : function(view, record, item, index, event){
            var id = record.id;
            console.log(id);
        }
    },
    store: Ext.create('GenPres.store.patient.LogicalUnitStore'),
    tpl: new Ext.XTemplate(
            '<tpl for="."><div class="LogicalUnitDataView">',
                '<div class="LogicalUnitDataViewItem" style="float:left;margin-right:20px;color:blue;">',
                    '{text}',
                '</div>',
            '</div></tpl>')
});