Ext.define('GenPres.store.patient.PatientTreeStore', {

    extend: 'Ext.data.TreeStore',

    alias: 'widget.patienttreestore',

    root: {
        text: 'Patienten',
        id: 'src',
        expanded: false
    },

    idProperty : 'id',

    fields: [
        { name: 'id', type: 'float' },
        { name: 'text', type: 'string' },
        { name: 'leaf', type: 'boolean' }
    ]
});

﻿Ext.define('GenPres.controller.patient.PatientController', {
    extend: 'Ext.app.Controller',

    stores:['patient.PatientTreeStore']
});

Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',

    border:false,

    folderSort: true,
    useArrows: true,

    scroll:'both',
    autoScroll:true,
    flex: 1,
    store: 'patient.PatientTreeStore',

    constructor : function(){
        var me = this;
        me.callParent();
    },

    initComponent : function(){
        var me = this;
        me.callParent();
    }
});

var GenPresApplication;

Ext.application ({
    name: 'GenPres',

    autoCreateViewport: false,
    appFolder: './Client/GenPres/app',

    controllers: [
        'patient.PatientController'
    ],

    launch: function() {

        var patientTree = Ext.create('GenPres.view.main.PatientTree');

        var panel = new Ext.Panel({
            width: 200,
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

    }
});

