Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',

    border:false,
    folderSort: true,
    useArrows: true,
    scroll:'both',
    autoScroll:true,
    flex: 1,
    store: Ext.create('Ext.data.TreeStore', {
        root: {
            expanded: true,
            children: [
                { text: "detention", leaf: true },
                { text: "homework", expanded: true, children: [
                    { text: "book report", leaf: true },
                    { text: "alegrbra", leaf: true}
                ] }
            ]
        }
    })
});

var GenPresApplication;

Ext.onReady(function(){
    var patientTree = Ext.create('GenPres.view.main.PatientTree');

    var panel = new Ext.Panel({
        width: 500,
        split: false,
        border:false,
        margins: '0 5 5 5',
        layout: {
            type: 'vbox',
            align: 'stretch'
        },
        items:[
            {border:false, html:'test'},
            patientTree
        ]
    });

    this.viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit',
        items:panel
    });
});

