/*
GenPres (c) 2011
*/
Ext.define('GenPres.util.Process', {

    currentProcessNr : 0,

    constructor: function(config) {
        var me = this;

        Ext.apply(me, config);
        
        me.spot = Ext.create('GenPres.util.Spotlight', {
            easing: 'easeOut',
            duration: 300
        });

    },

    doProcess:function(name){
        var me = this;

        me.name = name;
        
        var component;

        var process = me.Processes[name];
        
        var action = process[me.currentProcessNr];

        var queryResult = me.query(action.component);

        if(queryResult.length == 1){
            component = queryResult[0];
        }

        if(component.el){
           me.spot.show(component.el);
        }

        component.on('click', function(){
            if(me.currentProcessNr == (process.length - 1)) {
                me.spot.hide();
                me.tooltip.destroy();
            }
        });

        var config = {
            target: component.el,
            anchor: 'left',
            title: name,
            html:action.text,
            dockedItems: [{
                xtype: 'toolbar',
                baseCls:'none',
                dock: 'bottom',
                items: ['->', new Ext.Button({
                    text: 'Volgende',
                    hidden:(me.currentProcessNr == (process.length - 1) ),
                    handler:me.processNext,
                    scope:me
                })]
            }],
            minWidth:100,
            minHeight:100,
            autoHide : false,
            closable : true
        }
        
        me.tooltip = Ext.create('Ext.tip.ToolTip', config);

        var setZIndex = Ext.Function.createDelayed(function(){
            me.tooltip.el.dom.style.zIndex = 222001;
        }, 50);

        me.tooltip.show();

        me.tooltip.zIndexManager.setBase().zseed = 222001;
        
        setZIndex();
    },

    processNext : function(){
        var me = this;
        me.tooltip.destroy();
        me.currentProcessNr++;
        me.doProcess(me.name);
    }
});
/**
 * @class Ext.ux.Spotlight
 * UX used to provide a spotlight around a specified component/element.
 */
Ext.define('GenPres.util.Spotlight', {
    extend: 'Object',

    mixins:{observable:'Ext.util.Observable'},
    /**
     * @private
     * The baseCls for the spotlight elements
     */
    baseCls: 'x-spotlight',

    /**
     * @cfg animate {Boolean} True to animate the spotlight change
     * (defaults to true)
     */
    animate: true,

    /**
     * @cfg duration {Integer} The duration of the animation, in milliseconds
     * (defaults to 250)
     */
    duration: 250,

    /**
     * @cfg easing {String} The type of easing for the spotlight animatation
     * (defaults to null)
     */
    easing: null,

    /**
     * @private
     * True if the spotlight is active on the element
     */
    active: false,

    constructor:function(config){
        var me = this;
        me.mixins.observable.constructor.call(me);
    },

    /**
     * Create all the elements for the spotlight
     */
    createElements: function() {
        var body = Ext.getBody();

        this.right = body.createChild({
            cls: this.baseCls
        });
        this.left = body.createChild({
            cls: this.baseCls
        });
        this.top = body.createChild({
            cls: this.baseCls
        });
        this.bottom = body.createChild({
            cls: this.baseCls
        });

        this.all = Ext.create('Ext.CompositeElement', [this.right, this.left, this.top, this.bottom]);
    },

    /**
     * Show the spotlight
     */
    show: function(el, callback, scope) {
        //get the target element
        this.el = Ext.get(el);
        
        //create the elements if they don't already exist
        if (!this.right) {
            this.createElements();
        }

        if (!this.active) {
            //if the spotlight is not active, show it
            this.all.setDisplayed('');
            this.active = true;
            Ext.EventManager.onWindowResize(this.syncSize, this);
            this.applyBounds(this.animate, false);
        } else {
            //if the spotlight is currently active, just move it
            this.applyBounds(false, false);
        }
    },

    /**
     * Hide the spotlight
     */
    hide: function(callback, scope) {
        Ext.EventManager.removeResizeListener(this.syncSize, this);

        this.applyBounds(this.animate, true);
    },

    /**
     * Resizes the spotlight with the window size.
     */
    syncSize: function() {
        this.applyBounds(false, false);
    },

    /**
     * Resizes the spotlight depending on the arguments
     * @param {Boolean} animate True to animate the changing of the bounds
     * @param {Boolean} animate True to reverse the animation
     */
    applyBounds: function(animate, reverse) {
        var me = this,
            box = me.el.getBox();

        //get the current view width and height
        var viewWidth = Ext.core.Element.getViewWidth(true);
        var viewHeight = Ext.core.Element.getViewHeight(true);

        var i = 0,
            config = false,
            from, to;

        //where the element should start (if animation)
        from = {
            right: {
                x: box.right,
                y: viewHeight,
                width: (viewWidth - box.right),
                height: 0
            },
            left: {
                x: 0,
                y: 0,
                width: box.x,
                height: 0
            },
            top: {
                x: viewWidth,
                y: 0,
                width: 0,
                height: box.y
            },
            bottom: {
                x: 0,
                y: (box.y + box.height),
                width: 0,
                height: (viewHeight - (box.y + box.height)) + 'px'
            }
        };

        //where the element needs to finish
        to = {
            right: {
                x: box.right,
                y: box.y,
                width: (viewWidth - box.right) + 'px',
                height: (viewHeight - box.y) + 'px'
            },
            left: {
                x: 0,
                y: 0,
                width: box.x + 'px',
                height: (box.y + box.height) + 'px'
            },
            top: {
                x: box.x,
                y: 0,
                width: (viewWidth - box.x) + 'px',
                height: box.y + 'px'
            },
            bottom: {
                x: 0,
                y: (box.y + box.height),
                width: (box.x + box.width) + 'px',
                height: (viewHeight - (box.y + box.height)) + 'px'
            }
        };

        //reverse the objects
        if (reverse) {
            var clone = Ext.clone(from);
            from = to;
            to = clone;

            delete clone;
        }

        if (animate) {
            Ext.each(['right', 'left', 'top', 'bottom'], function(side) {
                me[side].setBox(from[side]);
                me[side].animate({
                    duration: me.duration,
                    easing: me.easing,
                    to: to[side]
                });

            },
            this);
        } else {
            Ext.each(['right', 'left', 'top', 'bottom'], function(side) {
                me[side].setBox(Ext.apply(from[side], to[side]));
            },
            this);
        }
    },

    /**
     * Removes all the elements for the spotlight
     */
    destroy: function() {
        Ext.destroy(this.right, this.left, this.top, this.bottom);
        delete this.el;
        delete this.all;
    }
});

Ext.define('GenPres.control.NumericBoundListKeyNav', {
    extend: 'Ext.view.BoundListKeyNav',
    mixins: {
        observable: 'Ext.util.Observable'
    },
    constructor: function(el, config) {
        var me = this;
        me.callParent([el, config]);
        me.addEvents('upreached');
        me.addEvents('downreaced');
    },
    defaultHandlers : {
        up: function() {

            var me = this,
            boundList = me.boundList,
            allItems = boundList.all,
            oldItem = boundList.highlightedItem,
            oldItemIdx = oldItem ? boundList.indexOf(oldItem) : -1,
            newItemIdx = oldItemIdx > 0 ? oldItemIdx - 1 : allItems.getCount() - 1; //wraps around

             if(oldItemIdx == 0){
             if(me.boundList.store.getAt(0).data.number != 0){

             }
             }else{
                 //if(oldItemIdx == 2){
                    this.fireEvent("upreached");
                 //}else{
                   // me.highlightAt(newItemIdx);
                 //}
             }
        },

        down: function() {
            var me = this,
                boundList = me.boundList,
                allItems = boundList.all,
                oldItem = boundList.highlightedItem,
                oldItemIdx = oldItem ? boundList.indexOf(oldItem) : -1,
                newItemIdx = oldItemIdx < allItems.getCount() - 1 ? oldItemIdx + 1 : 0; //wraps around
            if(oldItemIdx == allItems.getCount() - 1){
                //this.fireEvent("downreached");
            }else{
                //if(oldItemIdx == 2){
                    this.fireEvent("downreached");
                //}else{
                    //me.highlightAt(newItemIdx);
                //}
            }
        },

        pageup: function() {
            //TODO
        },

        pagedown: function() {
            //TODO
        },

        home: function() {

        },

        end: function() {

        },

        enter: function(e) {
            this.selectHighlighted(e);
        }
    }
});
Ext.require('Ext.form.*');

Ext.define('GenPres.control.ValueField', {

    extend:'Ext.form.field.Number',

    mixins: {
        picker: 'Ext.form.field.Picker'
    },

    requires: ['Ext.form.Picker', 'Ext.data.ArrayStore', 'Ext.util.DelayedTask', 'Ext.EventObject', 'Ext.view.BoundList', 'Ext.view.BoundListKeyNav', 'Ext.data.StoreMgr'],

    keyNavEnabled : false,

    initComponent: function() {
    	var me = this;
        
    	me.Picker = new Ext.form.Picker({
    		height:500
    	});

    	me.callParent();
        me.mixins.picker.initComponent.call(me);
    },

    getInnerTpl: function(displayField) {
        return '{' + displayField + '}';
    },
    onBlur: function() {
    	var me = this;
    	me.callParent();
    },
    spinUp : function(){
    	//this.inputEl.focus();
    	var me = this;
    	me.callParent();
    	//this.inputEl.focus();
    	//this.onFocus();
    },
    spinDown : function(){
    	this.inputEl.focus();
    	var me = this, picker = me.getPicker();
    	me.callParent();
    	this.inputEl.focus();
    },
    updateNumberStore :  function(){
        var me = this;
    	var store = me.store;

        var data = [];
        
    	for(var i = 0; i<5; i++){
    		data.push({number:me.fixPrecision(i*me.step)})
    	}
        store.loadData(data, false);
    },
    onFocus: function() {
    	var me = this;

		//me.expand();
        
        me.mixins.picker.expand.call(me);
        me.listKeyNav.highlightAt(2);
        
        me.callParent();
    },
    createPicker: function() {

        var store = Ext.create('Ext.data.ArrayStore', {
	    	fields: [ 'number' ],
	    	data: [['1'],['2'],['3']],
	    	sorters: [
	        {
	            property : 'number',
	            direction: 'ASC'
	        }]
	    });

        var me = this,
            picker,
            opts = {
                selModel: {
                    mode: 'SINGLE'
                },
                floating: true,
                hidden: true,
                ownerCt: this.ownerCt,
                renderTo: document.body,
                store: store,
                displayField: 'number',
                width: 100,
                maxHeight: 200
            };

        opts.getInnerTpl = me.getInnerTpl;

        me.store = store;

        picker = new Ext.view.BoundList(opts);

        this.updateNumberStore();

        me.mon(picker.getSelectionModel(), {
            selectionChange: me.onListSelectionChange,
            scope: me
        });


        return picker;
    },

    updateStoreDown : function(){
    	var me = this;
    	var store = me.store;
    	var lastRecord = store.getAt(1);
    	store.clearData();

    	var start = lastRecord.data.number / me.step;

        var data = [];

    	for(var i = start; i < start + 5; i++){
    		data.push({number:me.fixPrecision(i*me.step)})
    	}
        store.loadData(data, false);

        me.listKeyNav.highlightAt(2);
    },

    onExpand: function() {
        
    	var me = this,
            keyNav = me.listKeyNav,
            picker = me.mixins.picker.getPicker.call(me);
            lastSelected = picker.getSelectionModel().lastSelected;

		me.selectOnTab= true;

        if (!keyNav) {

            keyNav = me.listKeyNav = Ext.create('GenPres.control.NumericBoundListKeyNav',this.inputEl, {
                boundList: picker,
                selectOnTab: me.selectOnTab,
                forceKeyDown: true
            });
            keyNav.on("downreached", me.updateStoreDown, me);
        }
        Ext.defer(keyNav.enable, 5, keyNav); //wait a bit so it doesn't react to the down arrow opening the picker

        var minVal = null;
        foundRecord = {};
        me.store.each(function(record){
        	var diff = record.data.number - this.getValue();
        	if(minVal == null) {
        		foundRecord = record;
        		minVal = Math.abs(diff);
        	}

			if(minVal != null && Math.abs(diff) < minVal){
				minVal = Math.abs(diff);
				foundRecord = record;
			}
		}, this);

        //picker.highlightItem(picker.getNode(foundRecord));
        me.listKeyNav.highlightAt(2);
        me.inputEl.focus();
    },


    onListSelectionChange: function(list, selectedRecords) {
        var me = this;
        if (!me.inSetValue && me.isExpanded) {
            if (!me.multiSelect) {
                //Ext.defer(me.collapse, 1, me);
            }
            if(selectedRecords.length > 0){
            	me.setValue(selectedRecords[0].data.number, false);
            }
            me.fireEvent('select', me, selectedRecords);
            me.inputEl.focus();
        }
    },

    onCollapse: function() {
        var keyNav = this.listKeyNav;
        if (keyNav) {
            keyNav.disable();
        }
    }
});

Ext.define('GenPres.session.PatientSession', {

    currentLogicalUnitId:0,

    currentLogicalUnitName:'',

    singleton: true,

    setLogicalUnit : function(id, name){
        this.currentLogicalUnitId = id;
        this.currentLogicalUnitName = name;
    },

    getLogicalUnitId : function(){
        return this.currentLogicalUnitId;
    },
    
    getLogicalUnitName : function(){
        return this.currentLogicalUnitName;
    },

    setPatient : function(record){
        
    }
})
Ext.define('GenPres.store.patient.LogicalUnitStore', {

    extend: 'Ext.data.Store',

    alias: 'widget.logicalunitstore',
    
    root: {
            text: 'Patienten',
            id: 'src',
            expanded: true
    },

    autoLoad:true,

    model:'GenPres.model.patient.LogicalUnitModel'
});
Ext.define('GenPres.store.patient.PatientInfoStore', {

    extend: 'Ext.data.Store',

    alias: 'widget.patientstore',

    model:'GenPres.model.patient.PatientModel'
});
Ext.define('GenPres.store.patient.PatientTreeStore', {

    extend: 'Ext.data.TreeStore',

    alias: 'widget.patienttreestore',

    root: {
            text: 'Patienten',
            id: 'src',
            expanded: false
    },

    model:'GenPres.model.patient.PatientModel'
});
Ext.define('GenPres.store.prescription.Prescription', {

    extend: 'Ext.data.Store',

    alias: 'widget.prescriptionstore',

    autoLoad:true,

    model:'GenPres.model.prescription.Prescription'
});
Ext.define('GenPres.store.prescription.ValueStore', {

    extend: 'Ext.data.Store',

    alias: 'widget.valuestore',

    autoLoad:false,

    fields: [
        { name: 'Value', type: 'string' }
    ]
});
Ext.define('GenPres.store.prescription.GenericStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.genericstore',
    proxy : {
        type:'direct',
        directFn : Prescription.GetGenerics,
        extraParams:{
            route: "",
            shape:""
        },
        paramOrder : ['route', 'shape']
    }
});
Ext.define('GenPres.store.prescription.RouteStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.routestore',
    proxy : {
        type:'direct',
        directFn : Prescription.GetRoutes,
        extraParams:{
            generic: "",
            shape:""
        },
        paramOrder : ['generic', 'shape']
    }
});
Ext.define('GenPres.store.prescription.ShapeStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.shapestore',
    proxy : {
        type:'direct',
        directFn : Prescription.GetShapes,
        extraParams:{
            generic: "",
            route : ""
        },
        paramOrder : ['generic', 'route']
    }
});

Ext.define('GenPres.model.prescription.Prescription', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    autoLoad : true,

    fields: [
        { name: 'id', type: 'float' },
        { name: 'drugGeneric', type: 'string' },
        { name: 'drugRoute', type: 'string' },
        { name: 'drugShape', type: 'string' },
        { name: 'StartDate', type: 'string' }
    ],
    proxy : {
        type:'direct',
        directFn : Prescription.GetPrescriptions
    }
});

Ext.define('GenPres.model.patient.LogicalUnitModel', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    fields: [
        { name: 'id', type: 'float' },
        { name: 'text', type: 'string' },
        { name: 'leaf', type: 'boolean' }
    ],

    proxy : {
        type:'direct',
        directFn : Patient.GetLogicalUnits
    }
});


Ext.define('GenPres.model.patient.PatientModel', {
    extend: 'Ext.data.Model',

    idProperty : 'id',
    fields: [
        { name: 'id', type: 'float' },
        { name: 'text', type: 'string' },
        { name: 'leaf', type: 'boolean' },

        { name: 'PID', type: 'string' },
        { name: 'FirstName', type: 'string' },
        { name: 'LastName', type: 'string' },

        { name: 'Unit', type: 'string' },
        { name: 'Bed', type: 'string' },
        { name: 'RegisterDate', type: 'string' }
    ],
    proxy : {
        type:'direct',
        paramOrder:['node', 'logicalUnitId'],
        directFn : Patient.GetPatientsByLogicalUnit,
        extraParams:{
            logicalUnitId : 1
        }
    }
});

Ext.define('GenPres.view.main.MainView', {

    extend: 'Ext.Panel',
    
    layout:'border',
    
    constructor : function(){
        var me = this;
        me.callParent(arguments);
    },

    initComponent : function(){
        var me = this;

        me.items = [
            Ext.create('GenPres.view.main.MainViewLeft'),
            Ext.create('GenPres.view.main.MainViewCenter')
        ];
        me.callParent();

        GenPresApplication.viewport.items.add(me);
        GenPresApplication.viewport.doLayout();

        return me;
    }
});
Ext.define('GenPres.view.main.PatientInfo', {

    extend: 'Ext.view.View',

    alias : 'widget.patientinfo',

    itemSelector : 'patientInfo',

    tpl: new Ext.XTemplate('<tpl for=".">',
            '<div class="patientIcon"><div class="PatientInfoPid">{PID}</div></div>' +
                '<div class="patientNameInfo">',
                    '<b><span style="font-size:12px;">{LastName}, {FirstName}</span></b><br />',
                    '<div class="patientInfoValue">',
                        '<div class="patientInfoHeader"><b>Afdeling/bed:</b> {Unit} - {Bed}</div><br />',
                        '<div class="patientInfoHeader"><b>Opname: {RegisterDate}</b></div><br />',
                        '<div class="patientInfoHeader"><b>Ligdag: {Days}</b></div>',
                    '</div>',
                '</div>',
            '</tpl>'),

    store : 'patient.PatientInfoStore',

    initComponent : function(){
        var me=this;
        me.callParent();
    }
});


Ext.define('GenPres.view.main.ToolbarButton', {
    extend:'Ext.button.Button',
    text: '',
    scale:'large',
    location: 'Client/GenPres/style/images/TopToolbar/',
    iconAlign:'top',
    disabled: false,
    width:60,
    initComponent:function(){
        var me = this;
        me.icon = me.location + me.icon;
        me.callParent();
    }
});

Ext.define('GenPres.view.main.TopToolbar', {
    extend:'Ext.container.ButtonGroup',
    dock: 'top',

    initComponent : function(){
        var me = this;
        me.items = [
            {
                xtype: 'buttongroup',
                columns: 5,
                height:86,
                title: 'Algemeen',
                items : [
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'Home_32.png', text:'Home', action:'home'}),
                    {xtype: 'tbseparator',height:20},
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'Prescription_32.png', text:'Voorschriften', width:80}),
                    {xtype: 'tbseparator',height:20},
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'TPN_32.png', text:'TPN'})
                ]
            },
            {xtype: 'tbseparator',height:20},
            {
                xtype: 'buttongroup',
                columns: 3,
                height:86,
                title: 'Opties',
                items : [
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'Template_32.png', text:'Sjablonen'}),
                    {xtype: 'tbseparator',height:20},
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'NewMedicine_32.png', text:'Nieuw medicament', width:105})
                ]
            },
            {
                xtype: 'buttongroup',
                columns: 1,
                title: 'Patient informatie',
                id:'PatientInfoView',
                width:350,
                height:86,
                items : Ext.create('GenPres.view.main.PatientInfo')
            },
            {
                xtype: 'buttongroup',
                columns: 1,
                height:86,
                title: 'Medewerker',
                items : [
                    {html:' ', height:57, width:200}
                ]
            }
        ]
        me.callParent();
    }
})

﻿Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',

    border:false,

    folderSort: true,
    useArrows: true,

    flex: 1,

    scroll:'both',
    autoScroll:true,

    store: 'patient.PatientTreeStore',

    constructor : function(){
        var me = this;
        me.callParent();
    },

    initComponent : function(){
        var me = this;
        me.callParent();
        me.store.model.proxy.extraParams.logicalUnitId = GenPres.session.PatientSession.getLogicalUnitId();
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
        GenPresApplication.MainCenter = this;
        me.callParent();
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
})
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
                html:'<br /><br /><h1>&nbsp;&nbsp;&nbsp;Welkom bij GenPres - Development version</h1>',
                border:false
            }
        ];

        me.dockedItems = Ext.create('GenPres.view.main.TopToolbar');
        me.callParent();
        GenPresApplication.MainCenterContainer = this;
    },

    height: 100,
    split: true,
    margins: '0 5 5 5'
})
Ext.define('GenPres.view.main.MainViewLeft', {
    extend: 'Ext.Panel',
    layout:'vbox',
    region: 'west',
    xtype: 'panel',
    border:false,

    autoScroll:true,

    layout: {
        type: 'vbox',
        align: 'stretch'
    },

    width: 200,
    split: true,
    margins: '0 5 5 5',

    initComponent : function(){
        var me = this;
        me.items = [
            {
                xtype:'box',
                border:false,
                html:'<img src="Client/GenPres/style/images/logo.png" style="margin-top:22px;" />',
                height: 82
            },
            Ext.create('GenPres.view.main.PatientTree')
        ];
        me.callParent();
    }
});
Ext.define('GenPres.view.main.MainView', {

    extend: 'Ext.Panel',
    layout:'border',
    
    constructor : function(){
        var me = this;
        me.callParent(arguments);
    },

    initComponent : function(){
        var me = this;
        
        me.items = [
            Ext.create('GenPres.view.main.MainViewLeft'),
            Ext.create('GenPres.view.main.MainViewCenter')
        ];
        me.callParent();

        GenPresApplication.viewport.items.add(me);
        GenPresApplication.viewport.doLayout();
        
        return me;
    }
});

Ext.define('GenPres.view.prescription.PrescriptionToolbar', {
    extend:'Ext.container.ButtonGroup',
    dock: 'top',

    initComponent : function(){
        var me = this;
        me.items = [
            Ext.create('Ext.button.Button', {
                text:'Nieuw',
                action:'new'
            })
        ]
        me.callParent();
    }
})

Ext.define('GenPres.view.prescription.PrescriptionForm', {

    extend: 'Ext.Panel',

    alias : 'widget.prescriptionform',

    id: 'card-prescriptionForm',

    border:false,
    
    constructor : function(){
        var me = this;
        me.callParent(arguments);
    },

    initComponent : function(){
        var me = this;
        
        me.items = [

            Ext.create('GenPres.view.prescription.DrugComposition'),
            Ext.create('Ext.button.Button', {
                width:100,
                height:50,
                action:'save',
                text:'Opslaan',
                margin:'10 10 10 10'
            })
        ];

        me.dockedItems = Ext.create('GenPres.view.prescription.PrescriptionToolbar');

        me.callParent();

        return me;
    }
});
Ext.define('GenPres.view.prescription.DrugComposition', {

    extend: 'Ext.form.Panel',
    region: 'center',

    border:false,
    
    initComponent : function(){
        var me = this;

        var genericCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            displayField: 'Value',
            name:'drugGeneric',
            action:'generic',
            labelAlign:'top',
            fieldLabel: 'Generiek'
        });

        var quantity = Ext.create('Ext.form.field.Number', {
            fieldLabel: 'Quantity',
            labelAlign:'top'
        });
        
        var routeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.RouteStore',
            name:'drugRoute',
            displayField: 'Value',
            action:'route',
            labelAlign:'top',
            fieldLabel: 'Toedieningsweg'
        });

        var shapeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.ShapeStore',
            displayField: 'Value',
            name:'drugShape',
            action:'shape',
            labelAlign:'top',
            fieldLabel: 'Toedieningsvorm'
        });

        qqq=shapeCombo;

        var tablePanel = Ext.create('Ext.Panel', {
            border:false,
            margin:'10 10 10 10',
            layout : {
               type:'table',
                columns:2
            },
            items : [genericCombo, quantity, routeCombo, shapeCombo]
        });

        me.items = tablePanel;

        me.callParent();
    }
});
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

Ext.define('GenPres.view.prescription.PrescriptionGrid', {

    extend:'Ext.grid.Panel',

    border:false,

    alias: 'widget.prescriptiongrid',

    store: 'prescription.Prescription',

    columns: [
        {header: 'StartDate',  dataIndex: 'StartDate'},
        {header: 'Generiek',  dataIndex: 'drugGeneric'}
    ],
    
    initComponent : function(){
        var me = this;
        me.callParent();
    }
})

Ext.define('GenPres.view.user.LogicalUnitSelector', {
    extend: 'Ext.view.View',

    alias: 'widget.logicalunitselector',

    margin:'20 0 0 16',

    itemSelector: 'div.LogicalUnitDataViewItem',

    emptyText: 'No images available',

    singleSelect: true,

    store : 'patient.LogicalUnitStore',

    tpl: new Ext.XTemplate(
            '<tpl for="."><div class="LogicalUnitDataView">',
                '<div class="LogicalUnitDataViewItem">',
                    '{text}',
                '</div>',
            '</div></tpl>'),

    initComponent : function(){
        var me = this;
        me.callParent();
    }
});
﻿Ext.define('GenPres.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,
    
    width: 560,
    height: 400,

    requires : 'GenPres.session.PatientSession',
    
    mixins: {
        process: 'GenPres.util.Process'
    },
    
    constructor : function(config){
        var me = this;
        me.mixins.process.constructor.call(me);
        me.callParent(arguments);
    },

    initComponent: function() {
        this.items = [
            { html: '<img src=" Client/Application/Images/MedicalBanner.jpg" />', height: 180, xtype: 'box'},
            { xtype: 'panel', border: false, bodyPadding: 12, width: 542,
                items: [
                    { xtype: 'form', border:false, items: [
                        new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', name: 'username', id:'username', margin: '10 0 10 10', value:'test' }),
                        new Ext.form.Text({ fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10', value:'Test' })
                    ]}
                ]
            },
            Ext.create('GenPres.view.user.LogicalUnitSelector')
        ];


        this.callParent(arguments);
    },

    afterRender : function(){
       this.callParent(arguments);
       //this.doProcess('Login');
    },

    dockedItems: [{ 
        xtype: 'toolbar',
        dock: 'bottom',
        items: ['->', { text: 'Login', action: 'login'}]
    }],


    Processes : {
        'Login' : [
            {component:'form', text:'Dit formulier kan gebruikt worden om in te loggen.'},
            {component:'form textfield[name=username]', text:'Vul uw gebruikersnaam in.'},
            {component:'form textfield[name=password]', text:'Vul uw wachtwoord in.'},
            {component:'toolbar button[action=login]', text:'Klik op de login button om de applicatie te starten.'}
        ]
    }
});

﻿Ext.define('GenPres.controller.patient.PatientController', {
    extend: 'Ext.app.Controller',

    stores:['patient.LogicalUnitStore', 'patient.PatientInfoStore', 'patient.PatientTreeStore'],

    models:['patient.LogicalUnitModel', 'patient.PatientModel'],

    views:['main.PatientTree', 'main.PatientInfo'],

    init: function() {
        this.control({
            'treepanel': {
                itemclick: this.loadPatientData
            }
        });
    },

    onLaunch: function() {
        
    },

    loadPatientData : function(tree, record){
        var infoStore = this.getPatientPatientInfoStoreStore();
        infoStore.loadRecords([record], {addRecords: false});

        var gridPanel = this.getGridPanel();
        //Load grid data
    },

    getGridPanel : function(){
        var prescriptiongrid = GenPresApplication.MainCenter.query('prescriptiongrid')[0];
    }

});
﻿

Ext.define('GenPres.controller.user.LoginController', {
    extend: 'Ext.app.Controller',

    loggedIn: false,
    loginWindow: null,

    init: function() {
        this.control({
            'toolbar button[action=login]': {
                click: this.loginClickEvent
            },
            'dataview' : {
                itemclick : function(view, record, item, index, event){
                    GenPres.session.PatientSession.setLogicalUnit(
                        record.data.id,
                        record.data.text
                    )
                }
            }
        });
    },

    onLaunch: function() {
        
    },

    loginClickEvent : function(button){
        var win, form, vals;
        controller3 = this;
        win = button.up('window');
        this.loginWindow = win;
        form = win.down('form');
        vals = form.getValues();
        this.validateLogin(vals);
    },

    validateLogin: function(vals) {
        if(this.validateLoginForm(vals)){
            User.Login(vals.username, vals.password, this.loginCallBackFunction, this);
        }
    },

    validateLoginForm : function(vals){
        var error = '';
        
        if(vals.username == ''){
            error += 'Selecteer aub een gebruikersnaam<br />';
        }

        if(vals.password == ''){
            error += 'Selecteer aub een wachtwoord<br />';
        }

        if(
            GenPres.session.PatientSession.getLogicalUnitId() == ''
            ||
            GenPres.session.PatientSession.getLogicalUnitName() == ''
        ){
            error += 'Selecteer aub een afdeling\n';
        }
        if(error != ''){
            Ext.MessageBox.alert('GenPres 2011 Login Error', error);
        }
        
        return error == '';
    },

    loginCallBackFunction: function(result) {
        this.loggedIn = result.success;

        if (result.success) {
            this.closeLoginWindow();
        } else {
            Ext.MessageBox.alert('GenPres 2011 Login Error', 'Login geweigerd');
        }
    },

    closeLoginWindow: function() {
        this.loginWindow.close();
        Ext.create('GenPres.view.main.MainView', {logicalUnitId:this.logicalUnitId});
    }
});
Ext.define('GenPres.controller.prescription.PrescriptionController', {

    extend:'Ext.app.Controller',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore', 'prescription.Prescription'],

    models:[],

    views:['prescription.PrescriptionToolbar', 'prescription.DrugComposition', 'main.PatientTree', 'prescription.PrescriptionForm', 'main.TopToolbar', 'prescription.PrescriptionGrid'],

    init: function() {
        this.control({
            'treepanel': {
                itemclick: this.loadPrescriptionForm
            },
            'button[action=home]': {
                click : this.loadHome
            },
            'button[action=new]': {
                click : this.clearPrescription
            },
            'button[action=save]': {
                click : this.savePrescription
            }
        });
    },

    loadPrescriptionForm : function(tree, record){
        if(GenPresApplication.MainCenterContainer.items.length == 1){
            var form = Ext.create('GenPres.view.prescription.PrescriptionForm');
            GenPresApplication.MainCenterContainer.items.add(form);
            GenPresApplication.MainCenterContainer.doLayout();
        }
        GenPresApplication.MainCenterContainer.layout.setActiveItem(1);
    },

    loadHome : function(){
        GenPresApplication.MainCenterContainer.layout.setActiveItem(0);
    },

    clearPrescription : function(){
        var drugCompositionController = GenPresApplication.getController('prescription.DrugComposition');
        drugCompositionController.clear();
    },
    
    savePrescription:function(){
        var prescriptionform = GenPresApplication.MainCenter.query('prescriptionform')[0];
        var prescriptiongrid = GenPresApplication.MainCenter.query('prescriptiongrid')[0];
        var vals = {};
        var forms = [];
        forms = prescriptionform.query('form');
        for(var i=0; i<forms.length; i++){
            vals = forms[i].getValues();
        }
        Prescription.SavePrescription(vals, function(newValues){
            prescriptiongrid.store.load();
        })
    }

});
Ext.define('GenPres.controller.prescription.DrugComposition', {

    extend:'GenPres.controller.prescription.PrescriptionController',

    stores:['prescription.GenericStore', 'prescription.RouteStore', 'prescription.ShapeStore'],

    models:[],

    views:['prescription.DrugComposition'],

    generic:"",
    shape:"",
    route:"",

    panel : null,

    init: function() {
        this.control({
            'combobox' : {
                select : this.changeSelection
            }
        });
    },

    checkValues : function(store, records, succesful, options){
        if(store.getCount() == 1){
            options.comboBox.select(options.comboBox.store.data.getAt(0))
        }
    },

    addStoreListeners : function(combo){
        combo.store.on("load", this.checkValues, this, {comboBox:combo})
    },

    changeSelection : function(combo){

        if(this.panel == null) {
            this.panel = combo.up('panel');
            this.addStoreListeners(this.getComboBox('generic'));
            this.addStoreListeners(this.getComboBox('shape'));
            this.addStoreListeners(this.getComboBox('route'));
        }

        if(combo.action == "generic"){
            this.generic = combo.getValue();
            this.setExtraParams('route', 'generic', this.generic);
            this.setExtraParams('shape', 'generic', this.generic);
            this.getComboBox('route').store.load();
            this.getComboBox('shape').store.load();
        }
        if(combo.action == "route"){
            this.route = combo.getValue();
            this.setExtraParams('generic', 'route', this.route);
            this.setExtraParams('shape', 'route', this.route);
            this.getComboBox('generic').store.load();
            this.getComboBox('shape').store.load();
        }
        if(combo.action == "shape"){
            this.shape = combo.getValue();
            this.setExtraParams('generic', 'shape', this.shape);
            this.setExtraParams('route', 'shape', this.shape);
            this.getComboBox('generic').store.load();
            this.getComboBox('route').store.load();
        }
        qqq = this.getComboBox('route');
    },

    getComboBox : function(name){
        return this.panel.down('combobox[action='+name+']');
    },

    setExtraParams:function(comboName, paramName, paramValue){
        var combobox = this.getComboBox(comboName);
        if(paramValue == "") combobox.clearValue();
        combobox.store.proxy.extraParams[paramName] = paramValue;
    },

    clear : function(){
        if(this.panel == null) return;
        this.setExtraParams('generic', 'route', '');
        this.setExtraParams('generic', 'shape', '');
        this.setExtraParams('route', 'generic', '');
        this.setExtraParams('route', 'shape', '');
        this.setExtraParams('shape', 'generic', '');
        this.setExtraParams('shape', 'route', '');

        this.getComboBox('generic').store.load();
        this.getComboBox('route').store.load();
        this.getComboBox('shape').store.load();
    }
});


