
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
    }
})

Ext.define('GenPres.view.prescription.PrescriptionGrid', {

    extend:'Ext.grid.Panel',

    border:false,

    alias: 'widget.prescriptiongrid',

    xtype:'gridpanel',

    columns: [
        {header: 'StartDate',  dataIndex: 'StartDate'},
        {header: 'Generiek',  dataIndex: 'drugGeneric'}
    ],

    initComponent : function(){
        var me = this;
        me.callParent();
    }
});


var store = Ext.create('Ext.data.TreeStore', {
    root: {
        expanded: true,
        children: [
            { text: "detention", leaf: true },
            { text: "homework", expanded: true, children: [
                { text: "book report", leaf: true },
                { text: "alegrbra", leaf: true}
            ] },
            { text: "buy lottery tickets", leaf: true }
        ]
    }
});

Ext.define('GenPres.view.main.MainViewCenterContainer', {
    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',
    activeItem: 0,

    border:false,
    layout: 'card',

    initComponent : function(){
        var me = this;
        me.items = [
            {
                id: 'card-0',
                xtype:'box',
                html:'<br /><br /><h1>&nbsp;&nbsp;&nbsp;Welkom bij GenPres - Development version</h1>',
                border:false
            }
        ];
        me.callParent();
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
})

Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',
    xtype:'treepanel',
    border:false,
    folderSort: true,
    useArrows: true,
    flex: 1,
    scroll:'both',
    autoScroll:true,
    store:store,

    constructor : function(){
        var me = this;
        me.callParent();
    },

    initComponent : function(){
        var me = this;
        me.callParent();
        me.expandAll();
    }
});

Ext.define('GenPres.view.main.MainViewCenter', {
    extend: 'Ext.Panel',
    region: 'center',
    xtype: 'panel',

    border:false,
    layout: 'border',
    initComponent : function(){
        var me = this;
        me.items = [
            Ext.create('GenPres.view.main.MainViewCenterContainer'),
            Ext.create('GenPres.view.prescription.PrescriptionTabs')
        ];
        me.callParent();
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
});

Ext.define('GenPres.view.main.MainViewLeft', {
    extend: 'Ext.Panel',
    layout:'vbox',
    region: 'west',
    xtype: 'panel',
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    width: 200,
    initComponent : function(){
        var me = this;
        me.items = [
            Ext.create('GenPres.view.main.PatientTree')
        ];
        me.callParent();
    }
});

Ext.define('GenPres.view.main.MainView', {

    extend: 'Ext.Panel',
    layout:'border',

    initComponent : function(){
        var me = this;

        me.items = [
            Ext.create('GenPres.view.main.MainViewLeft'),
            Ext.create('GenPres.view.main.MainViewCenter')
        ];
        me.callParent();

        GenPres.application.viewport.items.add(me);
        GenPres.application.viewport.doLayout();

        return me;
    }
});

Ext.onReady(function(){
    GenPres = {};
    GenPres.application = {};
    this.viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit'
    });
    GenPres.application.viewport = this.viewport;

    Ext.create('GenPres.view.main.MainView', {logicalUnitId:"1"});

})