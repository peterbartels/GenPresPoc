/*
GenPres (c) 2011
*/
Ext.define('GenPres.lib.util.QueryHelper', {

    getFormTextField: function (container, fieldname) {
        var me = this;
        return me.getFormField(container, fieldname, 'textfield');
    },

    getFormNumberField: function (container, fieldname) {
        var me = this;
        return me.getFormField(container, fieldname, 'numberfield');
    },

    getFormComboBox: function (container, fieldname) {
        var me = this;
        return me.getFormField(container, fieldname, 'combobox');
    },

    getFormField: function (container, fieldname, type) {
        return Ext.ComponentQuery.query(container + ' ' + type + '[name=' + fieldname + ']')[0];
    },

    setFormField: function (formfield, value) {
        formfield.inputEl.dom.value = value;
        formfield.value = value;
        return true;
    },

    getButton: function (container, buttontext) {
        return Ext.ComponentQuery.query(container + ' button[text=' + buttontext + ']')[0];
    },

    clickButton: function (button) {
        button.btnEl.dom.click();
    },

    getWindow: function (windowname) {
        return Ext.ComponentQuery.query(windowname)[0];
    }

});
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

             if(oldItemIdx == 2){
                this.fireEvent("upreached");
             }else{
                me.highlightAt(newItemIdx);
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
                if(oldItemIdx == 2){
                    this.fireEvent("downreached");
                }else{
                    me.highlightAt(newItemIdx);
                }
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

GenPres.control = {
    states : {
        user : 'user',
        calculated: 'calculated'
    }
}
GenPres.control.stateColors = {}
GenPres.control.stateColors[GenPres.control.states.user] = "lime";
GenPres.control.stateColors[GenPres.control.states.calculated] = "orange";

Ext.define('GenPres.control.UnitValueField', {

    extend:'Ext.Panel',

    alias:'widget.unitvaluefield',
    
    width: 500,

    border: false,

    margin:'0 0 0 0',

    isFormField : true,

    visible: false,

    state: GenPres.control.states.user,

    value : 0,

    unit : "",

    changedByUser:false,

    setVisibile : function (visible){
        this.visible = visible;
        if(!this.visible) {
            this.getEl().dom.style.visibility = "hidden";
        }else{
            this.getEl().dom.style.visibility = "";
        }
    },

    setState : function(state){
        if(state != null && state!="") this.state = state;
        this.setInputColor(GenPres.control.stateColors[state]);
    },

    getState : function(){
        return this.state;
    },

    setInputColor : function(color){
        if(typeof(color) != "undefined" && color != ""){
            this.getInputEl().style.border = "solid 1px " + color;
        }else{
            console.log("Color not defined.");
        }
    },

    getInputEl : function(){
        return this.valueField.inputEl.dom;
    },

    getSubmitData : function(includeEmptyText){
        var me = this;
        var result = {};
        result[me.name] =me.getValue();
        return result;
    },

    getInputValue : function(){
        var me = this;
        return me.valueField.getValue();
    },

    getValue : function(){
        var me = this;
        
        return {
            value : (me.getState() == GenPres.control.states.user ? me.valueField.getValue() : 0),
            unit: (!me.unitStore ?  "" : me.unitCombo.getValue()),
            timeUnit: (!me.timeStore ?  "" : me.timeCombo.getValue()),
            totalUnit: (!me.totalStore ?  "" : me.totalCombo.getValue()),
            adjustUnit: (!me.adjustStore ?  "" : me.adjustCombo.getValue()),
            changedByUser:me.changedByUser,
            state:me.getState()
        };
    },

    setValue : function(obj){
        var me = this;
        me.value = obj.value;
        me.timeUnit = obj.timeUnit;
        me.totalUnit = obj.totalUnit;
        me.adjustUnit = obj.adjustUnit;
        me.unit = obj.unit;
        me.state = obj.state;
        me.visible = obj.visible;
        me.processValues();
    },

    processValues : function(){
        var me = this;
        me.valueField.setValue(me.value);
        if(me.unitStore) {
            me.unitCombo.setValue(me.unit);
        }
        if(me.timeStore) me.timeCombo.setValue(me.timeUnit);
        if(me.adjustStore) me.adjustCombo.setValue(me.adjustUnit);
        if(me.totalStore) me.totalCombo.setValue(me.totalUnit);
        me.setVisibile(me.visible);
        me.setState(me.state);
    },

    isValid : function(){
        return true;
    },

    isDirty : function(){
        return true;
    },

    setDefaultComboValue : function(combo, store){
        var me = this;
        var store = combo.store;
        var setSelected = function(){
            for(var i=0;i<store.data.items.length;i++){
                var val = store.data.items[i].raw;
                if(val.selected == true){
                    combo.suspendEvents();
                    combo.setValue(val["Value"]);
                    combo.resumeEvents();
                }
            }
        };

        if(store.proxy.type != "memory"){
            store.on("load", setSelected);
        }else{
            me.on("afterrender", Ext.Function.createBuffered(setSelected, 200, me));
        }
    },

    initComponent : function(){
        var me = this;

        //me.width = 500;

        me.valueField = Ext.create('GenPres.control.ValueField', {
            step:0.1,
            isFormField:false,
            width:80
        });
        
        me.valueField.on("blur", function(){
            me.fireEvent('inputfieldChange', me);
        });

        var items = [me.valueField];

        if(me.unitStore){
            me.unitCombo = new Ext.create('Ext.form.ComboBox',{
                xtype:'combobox',
                isFormField:false,
                store:me.unitStore,
                width:60
            })
            items.push(me.unitCombo);

            me.unitCombo.on("change", function(){
                me.fireEvent('inputfieldChange', me);
            });
            
            me.setDefaultComboValue(me.unitCombo, me.unitStore);
        }


        if(me.adjustStore){
            me.adjustCombo = new Ext.create('Ext.form.ComboBox',{
                xtype:'combobox',
                isFormField:false,
                store:me.adjustStore,
                width:60
            });
            me.width = me.width + 60;
            if(items.length > 0) items.push(me.createSeperator());
            items.push(me.adjustCombo);
            me.adjustCombo.on("change", function(){me.fireEvent('comboChange', me);});
            me.setDefaultComboValue(me.adjustCombo, me.adjustStore);
        }
        
        if(me.totalStore){
            me.totalCombo = new Ext.create('Ext.form.ComboBox',{
                xtype:'combobox',
                isFormField:false,
                store:me.totalStore,
                width:60
            });
            me.width = me.width + 60;
            if(items.length > 0) items.push(me.createSeperator());
            items.push(me.totalCombo);
            me.totalCombo.on("change", function(){me.fireEvent('comboChange', me);});
            me.setDefaultComboValue(me.totalCombo, me.totalStore);
        }

        if(me.timeStore){

            me.timeCombo = new Ext.create('Ext.form.ComboBox',{
                xtype:'combobox',
                isFormField:false,
                id:me.id+"_timecombo",
                store:me.timeStore,
                width:60
            });
            me.width = me.width + 60;
            if(items.length > 0) items.push(me.createSeperator());
            items.push(me.timeCombo);
            me.timeCombo.on("change", function(){me.fireEvent('comboChange', me);});

            me.setDefaultComboValue(me.timeCombo, me.timeStore);
        }


        me.items = [{
            xtype: 'fieldcontainer',
            labelAlign:me.labelAlign,
            fieldLabel: me.fieldLabel,
            margin:'0 0 0 0',
            width: me.width,
            labelWidth: 100,
            layout: 'hbox',
            items: items
        }];
        
        me.callParent();
        
        me.on("afterrender", function(){
            me.suspendEvents();
            me.setValue({
               value : me.value,
               unit: me.unit,
               visible:me.visible,
               state:me.state
            });
            me.resumeEvents();
        })
    },

    createSeperator : function(){
        return Ext.create('Ext.Component', {
            html:'<B>/</B>'
        })
    },

    suspendEvents : function(){
        var me = this;
        me.valueField.suspendEvents();
        if(me.unitCombo) me.unitCombo.suspendEvents();
        if(me.totalCombo) me.totalCombo.suspendEvents();
        if(me.adjustCombo) me.adjustCombo.suspendEvents();
        if(me.timeCombo) me.timeCombo.suspendEvents();
        me.callParent();
    },

    resumeEvents : function(){
        var me = this;
        me.valueField.resumeEvents();
        if(me.unitCombo) me.unitCombo.resumeEvents();
        if(me.totalCombo) me.totalCombo.resumeEvents();
        if(me.adjustCombo) me.adjustCombo.resumeEvents();
        if(me.timeCombo) me.timeCombo.resumeEvents();
        me.callParent();
    },

    getUnitCombo : function(){
        var me = this;
        return me.unitCombo;
    }
});
Ext.require('Ext.form.*');

Ext.define('GenPres.control.ValueField', {

    extend:'Ext.form.field.Number',

    alias : 'widget.valuefield',

    mixins: {
        picker: 'Ext.form.field.Picker'
    },

    requires: ['Ext.form.Picker', 'Ext.data.ArrayStore', 'Ext.util.DelayedTask', 'Ext.EventObject', 'Ext.view.BoundList', 'Ext.view.BoundListKeyNav', 'Ext.data.StoreMgr'],

    keyNavEnabled : false,

    enableKeyEvents:true,

    isFormField:false,
    
    initComponent: function() {
    	var me = this;
        
    	me.Picker = new Ext.form.Picker({
    		height:500
    	});

    	me.callParent();
        me.isFormField = false;
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
    	var me = this;
    	me.callParent();
    },
    spinDown : function(){
    	this.inputEl.focus();
    	var me = this, picker = me.getPicker();
    	me.callParent();
    	this.inputEl.focus();
    },
    getCurrentStep:function(){
        var me = this;
        var value = this.getValue();
        return value / me.step;
    },
    updateNumberStore :  function(){
        var me = this;

        var store = me.store;
        var data = [];
        
        var step = me.getCurrentStep();
        var start = 0;

        if(step == 0) start = 0;

    	for(var i = start; i<start+5; i++){
    		data.push({number:me.fixPrecision(i*me.step)})
    	}

        store.loadData(data, false);
    },
    onFocus: function() {
    	var me = this;
        me.mixins.picker.expand.call(me);
        me.callParent();
        var focusEl = function(){
            me.inputEl.dom.focus();
            me.inputEl.dom.select();
            me.findRecord();
        }
        Ext.Function.defer(focusEl, 50);
    },
    onKeyUp : function(event){
        var me = this;
        if(!event.isNavKeyPress()){
            me.updateNumberStore();
            me.findRecord();
        }
    },
    findRecord : function(){
        var me = this;
        var value = this.getValue();
        foundRecord = {};
        var minVal = null;
        var i = 0;
        var recordFound = false;
        me.store.each(function(record){
        	if(record.data.number == value){
                me.listKeyNav.highlightAt(i);
                recordFound = true;
            }
            i++;
		}, this);
        if(recordFound == false){
            me.updateNumberStore();
        }
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

        me.inputEl.dom.value = data[2].data.number;
    },


    updateStoreUp : function(){
        var me = this;

    	var store = me.store;

        var firstValue = store.getAt(0).data.number;

        if(firstValue == me.step) {
            me.listKeyNav.highlightAt(0);
            return;
        }

    	store.clearData();

    	var start = (firstValue-me.step) / me.step;
        var data = [];

    	for(var i = start; i < start + 5; i++){
    		data.push({number:me.fixPrecision(i*me.step)})
    	}
        store.loadData(data, false);

        me.listKeyNav.highlightAt(2);

        me.inputEl.dom.value = data[2].data.number;
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
            keyNav.on("upreached", me.updateStoreUp, me);
        }
        Ext.defer(keyNav.enable, 15, keyNav); //wait a bit so it doesn't react to the down arrow opening the picker
/*
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
		}, this);*/
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

    patient : {
        PID:""
    },

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
        this.patient.PID = record.data.PID;
    }
})
Ext.define('GenPres.store.PrescriptionStores', {

    substanceUnitStore:null,

    componentUnitStore:null,

    singleton:true,

    getSubstanceUnitStore : function(id, name){
        if(!this.substanceUnitStore){
            this.substanceUnitStore = Ext.create('GenPres.store.prescription.SubstanceUnit')
        }
        return this.substanceUnitStore;
    },

    getComponentUnitStore : function(id, name){
        if(!this.componentUnitStore){
            this.componentUnitStore = Ext.create('GenPres.store.prescription.ComponentUnit')
        }
        return this.componentUnitStore;
    }
})
Ext.define('GenPres.model.database.Database', {
    extend: 'Ext.data.Model',

    fields: [
        {name: 'DatabaseName', type: 'string'}
    ]
});

Ext.define('GenPres.model.prescription.Prescription', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    autoLoad : true,

    fields: [
        { name: 'Id', type: 'string' },
        { name: 'drugGeneric', type: 'string' },
        { name: 'drugRoute', type: 'string' },
        { name: 'drugShape', type: 'string' },
        { name: 'StartDate', type: 'string' }
    ],
    proxy : {
        type:'direct',
        directFn : Prescription.GetPrescriptions,
        extraParams:{
            PID : ""
        },
        paramOrder:["PID"]
    }
});

Ext.define('GenPres.model.patient.LogicalUnitModel', {
    extend: 'Ext.data.Model',

    idProperty : 'id',

    fields: [
        { name: 'id', type: 'float' },
        { name: 'text', type: 'string' },
        { name: 'leaf', type: 'boolean' }
    ]
});


Ext.define('GenPres.model.patient.PatientModel', {
    extend: 'Ext.data.Model',

    idProperty : 'PID',
    fields: [
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

Ext.define('GenPres.store.database.Database', {
    extend: 'Ext.data.Store',
    alias: 'widget.databasestore',
    storeId: 'databasestore',
    // This requires is necessary when Ext.Loader is enabled
    requires: ['GenPres.model.database.Database'],

    model: 'GenPres.model.database.Database',
    autoLoad: true,

    proxy: {
        type: 'direct',
        directFn: Database.GetDatabases
    }
});
Ext.define('GenPres.store.patient.LogicalUnitStore', {

    extend: 'Ext.data.Store',

    alias: 'widget.logicalunitstore',

    root: {
            text: 'Patienten',
            id: 'src',
            expanded: true
    },

    autoLoad:true,
    proxy : {
        type:'direct',
        directFn : Patient.GetLogicalUnits
    },
    
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

    autoLoad:false,

    model:'GenPres.model.prescription.Prescription'
});
Ext.define('GenPres.store.prescription.ValueStore', {

    extend: 'Ext.data.Store',

    alias: 'widget.valuestore',

    autoLoad:true,

    fields: [
        { name: 'Value', type: 'string' }
    ],

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
Ext.define('GenPres.store.prescription.GenericStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.genericstore',
    autoLoad:false,
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
    autoLoad:false,
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
Ext.define('GenPres.store.prescription.SubstanceUnit', {
    extend: 'GenPres.store.prescription.ValueStore',
    id: 'substanceunit',
    autoLoad:false,
    proxy : {
        type:'direct',
        directFn : Prescription.GetSubstanceUnits,
        extraParams:{
            generic:"",
            route: "",
            shape:""
        },
        paramOrder : ['generic', 'route', 'shape']
    }
});
Ext.define('GenPres.store.prescription.ShapeStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.shapestore',
    autoLoad:false,
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
Ext.define('GenPres.store.prescription.ComponentUnit', {
    extend: 'GenPres.store.prescription.ValueStore',
    id: 'componentunit',
    autoLoad:false,
    proxy : {
        type:'direct',
        directFn : Prescription.GetComponentUnits,
        extraParams:{
            generic:"",
            route: "",
            shape:""
        },
        paramOrder : ['generic', 'route', 'shape']
    }
});
Ext.define('GenPres.store.prescription.LocalUnit', {
    extend: 'Ext.data.Store',
    id: 'localUnit',
    fields: ['Value', 'selected']
});
Ext.define('GenPres.store.prescription.AdjustUnit', {
    extend:'GenPres.store.prescription.LocalUnit',
    data : [
        {"Value":"kg", selected: false},
        {"Value":"m2", selected: false}
    ]
});
Ext.define('GenPres.store.prescription.TotalTimeUnit', {
    extend:'GenPres.store.prescription.LocalUnit',
    data : [
        {"Value":"dag", selected: true},
        {"Value":"week", selected: false}
    ]
});
Ext.define('GenPres.store.prescription.RateUnit', {
    extend:'GenPres.store.prescription.LocalUnit',
    data : [
        {"Value":"min", selected: false},
        {"Value":"uur", selected: true}
    ]
});
Ext.define('GenPres.lib.view.component.SaveCancelToolbar', {
    extend: 'Ext.toolbar.Toolbar',
    alias: 'widget.savecanceltoolbar',

    items: [
            { text: 'Opslaan', action: 'save'},
            { text: 'Cancel', action: 'cancel'}
    ]
    
});
/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/19/11
 * Time: 10:06 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenPres.lib.view.window.SaveCancelWindow', {
    extend: 'Ext.window.Window',

    requires: [
        'GenPres.lib.view.component.SaveCancelToolbar'
    ],

    constructor: function (config) {
        var me = this;
    
        me = me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        me.dockedItems = me.createSaveCancelToolbar();

        me.callParent(arguments);
    },

    createSaveCancelToolbar: function () {
        return Ext.create('GenPres.lib.view.component.SaveCancelToolbar', { dock: 'bottom'});
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

        GenPres.application.viewport.items.add(me);
        GenPres.application.viewport.doLayout();

        return me;
    }
});
Ext.define('GenPres.view.main.PatientInfo', {

    extend: 'Ext.view.View',

    colspan:4,

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
});

﻿Ext.define('GenPres.view.main.PatientTree', {
    extend: 'Ext.tree.Panel',
    alias: 'widget.patienttree',
    xtype:'treepanel',
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
        GenPres.application.MainCenter = this;
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
                xtype:'box',
                html:'<br /><br /><h1>&nbsp;&nbsp;&nbsp;Welkom bij GenPres - Development version</h1>',
                border:false
            }
        ];

        me.dockedItems = Ext.create('GenPres.view.main.TopToolbar');
        me.callParent();
        GenPres.application.MainCenterContainer = this;
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
            Ext.create('GenPres.view.main.PatientTree', {
                name:'mainPatientTree'
            })
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

        GenPres.application.viewport.items.add(me);
        GenPres.application.viewport.doLayout();
        
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

    extend: 'Ext.form.Panel',

    alias : 'widget.prescriptionform',

    id: 'card-prescriptionForm',

    border:false,

    width:870,

    bodyPadding: '10',

    constructor : function(){
        var me = this;
        me.callParent(arguments);
    },

    initComponent : function(){
        var me = this;

        me.layout = {
            type:'table',
            columns:2
        }

        me.items = [
            Ext.create('GenPres.view.prescription.Verbalization'),
            Ext.create('GenPres.view.prescription.DrugComposition'),
            Ext.create('GenPres.view.prescription.Patient'),
            Ext.create('GenPres.view.prescription.FrequencyDuration'),
            Ext.create('GenPres.view.prescription.Options'),
            Ext.create('GenPres.view.prescription.Dose'),
            Ext.create('GenPres.view.prescription.Administration'),
            Ext.create('Ext.form.field.TextArea', {
                width:550,
                height:50,
                action:'save',
                fieldLabel:'Opmerkingen',
                margin:'10 10 10 10'
            }),
            Ext.create('Ext.button.Button', {
                width:100,
                height:50,
                action:'save',
                text:'Opslaan',
                margin:'10 10 10 0'
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

    alias:'widget.drugcomposition',

    border:false,

    title:'Medicament',

    width:600,

    height:115,

    bodyPadding:'0 0 0 2',

    bodyCls: 'presriptionFormCategory',

    initComponent : function(){
        var me = this;

        var genericCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            name:'drugGeneric',
            id:'drugGeneric',
            action:'generic',
            fieldLabel: 'Generiek'
        });

        var substanceQuantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Hoeveelheid',
            unit:'mg',
            width:140,
            labelAlign:'top',
            id:'substanceQuantity',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            name:'substanceQuantity'
        });
        
        var routeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.RouteStore',
            id:'drugRoute',
            name:'drugRoute',
            action:'route',
            fieldLabel: 'Toedieningsweg'
        });

        var shapeCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.ShapeStore',
            id:'drugShape',
            name:'drugShape',
            action:'shape',
            width:140,
            fieldLabel: 'Toedieningsvorm'
        });

        var drugQuantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: '',
            unit:'mg',
            width:140,
            padding:'26 0 0 0',
            id:'drugQuantity',
            unitStore: GenPres.store.PrescriptionStores.getComponentUnitStore(),
            name:'drugQuantity'
        });

        var substanceDrugConcentration = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Concentratie',
            width:240,
            colspan:2,
            margin:'0 0 0 20',
            id:'substanceDrugConcentration',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            totalStore: GenPres.store.PrescriptionStores.getComponentUnitStore(),
            name:'substanceDrugConcentration'
        });

        var solutionCombo = Ext.create('Ext.form.field.ComboBox', {
            store: 'prescription.GenericStore',
            name:'drugSolution',
            margin:'0 0 0 20',
            width:120,
            id:'drugSolution',
            action:'solution',
            style:{visibility:'hidden'},
            fieldLabel: 'Oplossing'
        });

        var tablePanel = Ext.create('Ext.Container', {
            border:false,
            layout : {
               type:'table',
                columns:4
            },
            items : [genericCombo, substanceQuantity, solutionCombo, drugQuantity, routeCombo, shapeCombo, substanceDrugConcentration]
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
    },

    height: 200,
    split: true,
    margins: '0 5 5 5'
})
Ext.define('GenPres.view.prescription.Options', {

    extend: 'Ext.form.Panel',
    
    layout:{
        type:'table',
        columns:4
    },
    alias:'widget.prescriptionformoptions',
    
    border:false,

    title:'&nbsp;',

    width:270,

    height:53,

    initComponent : function(){
        var me = this;

        var continuous = Ext.create('GenPres.control.Option',{
            fieldLabel : 'Continu',
            labelAlign:'top',
            labelSeparator:'',
            style: { textAlign: 'center'},
            width:60,
            fieldStyle :'visibility:hidden;',
            name:'prescriptionContinuous'
        });

        var infusion = Ext.create('GenPres.control.Option',{
            fieldLabel : 'Inlooptijd',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:70,
            labelSeparator:'',
            fieldStyle :'visibility:hidden;',
            name:'prescriptionInfusion'
        });

        var onRequest = Ext.create('GenPres.control.Option',{
            fieldLabel : 'Indien nodig',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:74,
            labelSeparator:'',
            name:'prescriptionOnRequest'
        });

        var solution = Ext.create('GenPres.control.Option',{
            fieldLabel : 'Oplossing',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:60,
            labelSeparator:'',
            name:'prescriptionSolution'
        });
        
        solution.on("change", function(field, newValue, oldValue, eOpts){
            var solution = Ext.ComponentQuery.query("#drugSolution")[0];
            if(newValue == true){
                //continuous.setFieldStyle("visibility:visible;");
                //infusion.setFieldStyle("visibility:visible;");
                solution.el.dom.style.visibility = "visible"
            }else{
                //continuous.setFieldStyle("visibility:hidden;");
                //infusion.setFieldStyle("visibility:hidden;");
                //continuous.setValue(false);
                //infusion.setValue(false);
                //solution.el.dom.style.visibility = "hidden"
                //solution.setValue("");
            }
        })


        me.items = [continuous, infusion, onRequest, solution];
        me.callParent();

    }

});

Ext.define('GenPres.view.prescription.Dose', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:3
    },
    alias:'widget.prescriptionformdose',
    
    border:false,

    title:'Dosering',

    width:870,

    colspan:2,

    bodyPadding:'0 0 0 5',

    bodyCls: 'presriptionFormCategory',

    initComponent : function(){
        var me = this;

        var quantity = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel:'Per keer',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'doseQuantity',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            adjustStore: Ext.create('GenPres.store.prescription.AdjustUnit'),
            name:'doseQuantity'
        });

        var total = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Totaal',
            unit:'mg',
            width:200,
            labelAlign:'top',
            id:'doseTotal',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            adjustStore: Ext.create('GenPres.store.prescription.AdjustUnit'),
            timeStore: Ext.create('GenPres.store.prescription.TotalTimeUnit'),
            name:'doseTotal'
        });

        var rate = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Snelheid',
            unit:'mg',
            labelAlign:'top',
            id:'doseRate',
            unitStore: GenPres.store.PrescriptionStores.getSubstanceUnitStore(),
            adjustStore: Ext.create('GenPres.store.prescription.AdjustUnit'),
            timeStore: Ext.create('GenPres.store.prescription.RateUnit'),
            name:'doseRate'
        });
        me.items = [quantity, total, rate];
        me.callParent();

    }

});

Ext.define('GenPres.view.prescription.Administration', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:3
    },
    alias:'widget.prescriptionformadministration',

    title:'toediening',

    border:false,

    width:870,

    colspan:2,

    bodyPadding:'0 0 0 5',

    bodyCls: 'presriptionFormCategory',

    initComponent : function(){
        var me = this;

        var quantity = Ext.create('GenPres.control.UnitValueField', {
            unit:'mg',
            width:260,
            labelAlign:'top',
            fieldLabel:'Per keer',
            id:'adminQuantity',
            unitStore: GenPres.store.PrescriptionStores.getComponentUnitStore(),
            name:'adminQuantity'
        });

        var total = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel:'Totaal',
            unit:'mg',
            width:260,
            labelAlign:'top',
            id:'adminTotal',
            unitStore: GenPres.store.PrescriptionStores.getComponentUnitStore(),
            timeStore: Ext.create('GenPres.store.prescription.TotalTimeUnit'),
            name:'adminTotal'
        });

        var rate = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Snelheid',
            unit:'mg',
            labelAlign:'top',
            id:'adminRate',
            unitStore: GenPres.store.PrescriptionStores.getComponentUnitStore(),
            timeStore: Ext.create('GenPres.store.prescription.RateUnit'),
            name:'adminRate'
        });

        me.items = [quantity, total, rate];
        me.callParent();

    }

});

Ext.define('GenPres.view.prescription.FrequencyDuration', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:2
    },
    alias:'widget.prescriptionformfrequencyduration',
    
    border:false,

    title:'Frequentie en tijdsduur',

    width:600,

    height:53,

    bodyCls: 'presriptionFormCategory',

    bodyPadding:'0 0 0 5',

    initComponent : function(){
        var me = this;

        var frequencyStore = Ext.create('GenPres.store.prescription.LocalUnit', {
            data : [{"Value":"dag", selected: true}]
        });

        var frequency = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Frequentie',
            width:200,
            labelAlign:'top',
            id:'prescriptionFrequency',
            timeStore: frequencyStore,
            name:'prescriptionFrequency'
        });

        var durationStore = Ext.create('GenPres.store.prescription.LocalUnit', {
            data : [{"Value":"min", selected: false},{"Value":"uur", selected: true}]
        });

        var duration = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Toedienduur',
            unit:'mg',
            labelAlign:'top',
            id:'prescriptionDuration',
            timeStore: durationStore,
            name:'prescriptionDuration'
        });

        me.items = [frequency, duration];
        me.callParent();
    }
});

Ext.define('GenPres.view.prescription.Patient', {

    extend: 'Ext.form.Panel',
    
    region: 'center',

    alias:'widget.prescriptionpatient',

    border:false,

    width:270,

    height:115,

    bodyPadding:'25 0 0 2',

    bodyCls: 'presriptionFormCategory',

    bodyStyle:"margin-left:1px;",

    title:'Patient',

    initComponent : function(){
        var me = this;

        var patientWeight = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Gewicht',
            labelAlign:'left',
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['gram'],['kg']]
            }),
            unit:'kg',
            id:'patientWeight',
            name:'patientWeight'
        });

        var patientLength = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Lengte',
            labelAlign:'left',
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['cm']]
            }),
            unit:'cm',
            id:'patientLength',
            name:'patientLength'
        });

        var patientBSA = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'BSA',
            labelAlign:'left',
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['m2']]
            }),
            unit:'m2',
            id:'patientBSA',
            name:'patientBSA'
        });

        me.items = [patientWeight, patientLength, patientBSA];

        me.callParent();
    }
});
Ext.define('GenPres.view.prescription.FrequencyDuration', {

    extend: 'Ext.form.Panel',
    region: 'center',
    layout:{
        type:'table',
        columns:2
    },
    alias:'widget.prescriptionformfrequencyduration',
    
    border:false,

    title:'Frequentie en tijdsduur',

    width:600,

    height:53,

    bodyCls: 'presriptionFormCategory',

    bodyPadding:'0 0 0 5',

    initComponent : function(){
        var me = this;

        var frequencyStore = Ext.create('GenPres.store.prescription.LocalUnit', {
            data : [{"Value":"dag", selected: true}]
        });

        var frequency = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Frequentie',
            width:200,
            labelAlign:'top',
            id:'prescriptionFrequency',
            timeStore: frequencyStore,
            name:'prescriptionFrequency'
        });

        var durationStore = Ext.create('GenPres.store.prescription.LocalUnit', {
            data : [{"Value":"min", selected: false},{"Value":"uur", selected: true}]
        });

        var duration = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Toedienduur',
            unit:'mg',
            labelAlign:'top',
            id:'prescriptionDuration',
            timeStore: durationStore,
            name:'prescriptionDuration'
        });

        me.items = [frequency, duration];
        me.callParent();
    }
});


Ext.define('GenPres.view.prescription.PrescriptionGrid', {

    extend:'Ext.grid.Panel',

    border:false,

    alias: 'widget.prescriptiongrid',

    store: 'prescription.Prescription',

    title:'voorscrhiften',

    columns: [
        {header: 'StartDate',  dataIndex: 'StartDate'},
        {header: 'Generiek',  dataIndex: 'drugGeneric'}
    ],
    
    initComponent : function(){
        var me = this;
        me.callParent();
    }
});

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
Ext.define('GenPres.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,
    closable: false,

    requires : ['GenPres.session.PatientSession', 'GenPres.store.PrescriptionStores'],

    title: 'GenPres Login',
    defaultDatabase: 'Default Database',

    initComponent: function() {
        var me = this;
        me.dockedItems = me.createDockedItems();
        me.items = this.createItems();
        me.callParent(arguments);
    },

    getLoginButton: function () {
        return Ext.ComponentQuery.query('toolbar button[action=login]');
    },

    createDockedItems: function () {
        return [
            {
                xtype: 'toolbar',
                dock: 'bottom',
                items: ['->', { text: 'Login', action: 'login'}]
            }
        ];
    },

    createItems: function () {
        var me = this;
        
        return [
            me.getHtmlImage(),
            me.getLoginForm2()
        ];
    },

    getImagePath: function () {
        return GenPres.application.appFolder + "/style/images/medicalbanner.jpg";
    },

    getHtmlImage: function () {
        var me = this, imagePath = me.getImagePath();
        return { html: '<img src=' + imagePath + ' />', height: 180, xtype: 'box' }
    },


    getLoginForm2: function () {
        var me = this;
        //noinspection JSUnusedGlobalSymbols
        return {
            xtype:'form',
            border: false,
            bodyPadding: 15,
            width: 541,
            defaults: {
                allowBlank: false
            },
            items:[
                { xtype: 'textfield', fieldLabel: 'Gebruikersnaam', name:'username', margin: '10 0 10 10', value: 'peter' },
                { xtype: 'textfield', inputType: 'password', fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10',  value: 'Secret' },
                Ext.create('GenPres.view.user.LogicalUnitSelector',{name:'loginLogicalUnitSelector'}),
                me.advancedLoginFieldSet()

            ]
        };
    },

    advancedLoginFieldSet: function () {
        var me = this;
        return {
            xtype: 'fieldset',
            layout: 'hbox',
            collapsible: true,
            collapsed: true,
            margin:'65 0 0 0',
            items: [
                me.createDatabaseCombo(),
                me.createRegisterDatabaseButton()
            ]
        };
    },

    createDatabaseCombo: function () {
        var me = this;
        return {xtype: 'combo', name: 'database', fieldLabel: 'Database', displayField: 'DatabaseName', store: me.getDatabaseStore()};
    },

    getDatabaseStore: function () {
        return Ext.create('GenPres.store.database.Database');
    },

    createRegisterDatabaseButton: function () {
        return {xtype: 'button', text: 'Registreer Database', action: 'registerdatabase'};
    }

});
Ext.define('GenPres.view.database.RegisterDatabaseWindow', {
    extend: 'GenPres.lib.view.window.SaveCancelWindow',
    title: 'Registreer Database',

    layout: 'fit',

    width: 800,
    heigth: 300,

    initComponent: function () {
        var me = this;

        me.items = me.getFormItem();
        me.callParent();
    },

    getFormItem: function () {
        var me = this,
            defaults = {
                width: 700,
                labelWidth: 120,
                labelAlign: 'left'
            };
        return { xtype: 'form', bodyPadding: 10, fieldDefaults: defaults, items: me.getFormItems()}
    },

    getFormItems: function () {
        return [
            { xtype: 'textfield', name: 'databasename', fieldLabel: 'Database Naam'},
            { xtype: 'textfield', name: 'machinename', fieldLabel: 'Machine Naam'},
            { xtype: 'textfield', name: 'genpresconnectionstring', fieldLabel: 'GenPres Connection String'},
            { xtype: 'textfield', name: 'patientdbconnectionstring', fieldLabel: 'PDMS Connection String'},
            { xtype: 'textfield', name: 'genformwebservice', fieldLabel: 'GenForm Webservice'}
        ]
    },

    getDatabaseName: function () {
        var me = this;
        return me.getDatabaseNameField().value;
    },

    getDatabaseNameField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[0];
    },

    getMachineName: function () {
        var me = this;
        return me.getMachineNameField().value;
    },

    getMachineNameField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[1];
    },

    getGenPresConnectionString: function () {
        var me = this;
        return me.getGenPresConnectionStringField().value;
    },

    getGenPresConnectionStringField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[2];
    },

    getPDMSConnectionString: function () {
        var me = this;
        return me.getPDMSConnectionStringField().value;
    },

    getPDMSConnectionStringField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[3];
    },

    getGenFormWebservice: function () {
        var me = this;
        return me.getGenFormWebserviceField().value;
    },

    getGenFormWebserviceField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[4];
    },

    getDatabaseRegistrationForm: function () {
        var me = this;
        return me.items.items[0];
    }

});

﻿Ext.define('GenPres.controller.patient.Patient', {
    extend: 'Ext.app.Controller',

    stores:['patient.LogicalUnitStore', 'patient.PatientInfoStore', 'patient.PatientTreeStore'],

    models:['patient.LogicalUnitModel', 'patient.PatientModel'],

    views:['main.PatientTree', 'main.PatientInfo'],

    init: function() {
        this.control({
            'treepanel': {
                beforeitemclick: this.checkRootNode,
                itemclick: this.loadPatientData
            }
        });
    },

    onLaunch: function() {
        
    },
    checkRootNode : function(tree, record, htmlitem, index, event, options){
        if(index==0){
            var infoStore = this.getPatientPatientInfoStoreStore();
            var treePanel = this.getTreePanel();
            if(typeof(infoStore.getAt(0)) != "undefined"){
                treePanel.selModel.select(infoStore.getAt(0));
            }
            return false;
        }
    },
    loadPatientData : function(tree, record, htmlitem, index, event, options){

        var me =this,
            infoStore = this.getPatientPatientInfoStoreStore();

        infoStore.loadRecords([record], {addRecords: false});
        GenPres.session.PatientSession.setPatient(record);

        var gridPanel = this.getGridPanel();
        gridPanel.store.proxy.extraParams.PID = GenPres.session.PatientSession.patient.PID;
        gridPanel.store.load();

        Patient.SelectPatient(GenPres.session.PatientSession.patient.PID, function(patientDto){
            var prescriptionController = GenPres.application.getController('prescription.PrescriptionController');
            prescriptionController.loadPrescriptionForm();
            prescriptionController.clearPrescription();
            me.setPatientWeight(patientDto.Weight);
            me.setPatientLength(patientDto.Height);
            prescriptionController.updatePrescription();
        });
    },

    setPatientWeight : function(weight){
        var prescriptionPatientComp = this.getPrescriptionPatientComponent();
        prescriptionPatientComp.down('unitvaluefield[name=patientWeight]').setValue(weight);
    },

    setPatientLength : function(height, unit){
        var prescriptionPatientComp = this.getPrescriptionPatientComponent();
        prescriptionPatientComp.down('unitvaluefield[name=patientLength]').setValue(height);
    },

    getPrescriptionPatientComponent : function(){
        return GenPres.application.MainCenter.query('prescriptionpatient')[0];
    },

    getGridPanel : function(){
        return GenPres.application.MainCenter.query('.prescriptiongrid')[0];
    },
    
    getTreePanel : function(){
        return GenPres.application.viewport.query('.patienttree')[0];
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

    models:['prescription.Prescription'],

    views:['prescription.PrescriptionToolbar', 'prescription.DrugComposition', 'main.PatientTree', 'prescription.PrescriptionForm', 'main.TopToolbar', 'prescription.PrescriptionGrid'],

    substanceUnitStore:null,

    prescriptionIsLoading: false,

    init: function() {

        var me = this;

        var updatePrescription = function(){
            //GenPres.lib.Prescription.UserStateCheck.checkStates(me.getControls());
            me.updatePrescription();
        };
        
        me.control({
            'gridpanel' : {
                itemdblclick: me.loadPrescription
            },
            'button[action=home]': {
                click : me.loadHome
            },
            'button[action=new]': {
                click : function(){
                    me.clearPrescription();
                    me.updatePrescription();
                }
            },
            'button[action=save]': {
                click : me.savePrescription
            },
            'valuefield' : {
                blur : Ext.Function.bind(updatePrescription, me)
            },
            'unitvaluefield' :{
                comboChange :me.updatePrescription
            },
            'unitvaluefield' :{
                inputfieldChange : me.resetLatestChangedUnitValueField
            },
            'checkboxfield' :{
                change : me.updatePrescription
            }
        });
    },

    resetLatestChangedUnitValueField:function(unitValueField){
        var latestChangedUnitValueFields = GenPres.application.MainCenter.query('unitvaluefield[changedByUser=true]');
        for(var i=0;i<latestChangedUnitValueFields.length;i++){
            latestChangedUnitValueFields[i].changedByUser = false;
        }
        unitValueField.changedByUser = true;
    },

    getControls: function(){
        var me = this;
        var form = me.getForm();
        return form.query('unitvaluefield');
    },

    updatePrescription: function(){
        var me = this;
        
        var PID = GenPres.session.PatientSession.patient.PID;
        me.prescriptionIsLoading = true;
        
        var returnFunc = function(newValues){
            me.setValues(newValues);
            me.prescriptionIsLoading = false;
        };
        
        GenPres.ASyncEventManager.registerDirectEvent(Prescription.UpdatePrescription, [PID, Ext.Function.bind(me.getValues, me), returnFunc]);
        GenPres.ASyncEventManager.execute();
    },

    getDrugCompositionController : function(){
        return GenPres.application.getController('prescription.DrugComposition');
    },

    getSubstanceUnitStore : function(){
        if(this.substanceUnitStore == null){
            this.substanceUnitStore = Ext.create('');
        }
        return this.substanceUnitStore;
    },

    loadPrescription : function(view, record, htmlItem, index, event, options){
        var me = this;
        var resultFunc = function(result){
            me.setValues(record.data);
            var drugController = GenPres.application.getController('prescription.DrugComposition');
            drugController.updateStores(drugController.getComboBox("generic"));
            drugController.updateStores(drugController.getComboBox("route"));
            drugController.updateStores(drugController.getComboBox("shape"));
        };
        GenPres.ASyncEventManager.registerDirectEvent(Prescription.GetPrescriptionById, [record.data.Id, resultFunc]);
        GenPres.ASyncEventManager.execute();
    },

    setValues: function(data){
        var form = this.getForm();

        Ext.Object.each(data, function(key, value){
            var components = form.query('#'+ key);
            
            if(components.length > 0){
                var component = components[0];

                component.suspendEvents();
                component.setValue(value);
                component.resumeEvents();
            }
        }, this);
        var verbalization = GenPres.application.MainCenter.query('verbalization')[0];
        verbalization.store.loadData([data], false);
    },

    loadPrescriptionForm : function(tree, record){
        var me = this;
        if(GenPres.application.MainCenterContainer.items.length == 1){
            var form = Ext.create('GenPres.view.prescription.PrescriptionForm');
            GenPres.application.MainCenterContainer.items.add(form);
            GenPres.application.MainCenterContainer.doLayout();
        }
        GenPres.application.MainCenterContainer.layout.setActiveItem(1);
    },

    loadHome : function(){
        GenPres.application.MainCenterContainer.layout.setActiveItem(0);
    },

    clearPrescription : function(){
        this.getDrugCompositionController().clear();
    },

    getForm : function(){
        return GenPres.application.MainCenter.query('prescriptionform')[0];
    },
    savePrescription:function(){

        var prescriptiongrid = GenPres.application.MainCenter.query('prescriptiongrid')[0];
        var PID = GenPres.session.PatientSession.patient.PID;

        Prescription.SavePrescription(PID, this.getValues(), function(newValues){
            prescriptiongrid.store.load();
        })
    },
    getValues:function(){
        var vals = {};
        var form = this.getForm();
        
        Ext.Object.each(form.getValues(), function(key, value, myself) {
            vals[key] = value;
        });

        return vals;
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

    panel:null,

    init: function() {
        this.control({
            'combobox[isFormField=true]' : {
                select : this.updateStores
            }
        });
    },

    checkValues : function(store, records, succesful, options){
        if(store.getCount() == 1){
            options.comboBox.setValue(options.comboBox.store.data.getAt(0).data.Value);
        }
    },

    addStoreListeners : function(combo){
        combo.store.on("load", this.checkValues, this, {comboBox:combo});
    },

    updateStores : function(combo){
        var me = this;
        if(combo.action == "generic"){
            this.generic = combo.getValue();
            this.setExtraParams('route', 'generic', this.generic);
            this.setExtraParams('shape', 'generic', this.generic);
            GenPres.ASyncEventManager.registerEventListener(this.getComboBox('route').store, "load", []);
            GenPres.ASyncEventManager.registerEventListener(this.getComboBox('shape').store, "load", []);
        }

        if(combo.action == "route"){
            this.route = combo.getValue();
            this.setExtraParams('generic', 'route', this.route);
            this.setExtraParams('shape', 'route', this.route);
            GenPres.ASyncEventManager.registerEventListener(this.getComboBox('generic').store, "load", []);
            GenPres.ASyncEventManager.registerEventListener(this.getComboBox('shape').store, "load", []);
        }
        if(combo.action == "shape"){
            this.shape = combo.getValue();
            this.setExtraParams('generic', 'shape', this.shape);
            this.setExtraParams('route', 'shape', this.shape);
            GenPres.ASyncEventManager.registerEventListener(this.getComboBox('generic').store, "load", []);
            GenPres.ASyncEventManager.registerEventListener(this.getComboBox('route').store, "load", []);
        }
        
        GenPres.ASyncEventManager.execute();

        GenPres.ASyncEventManager.registerFunction(Ext.Function.bind(me.reloadUnitStores, me), [function(){}])

        GenPres.ASyncEventManager.execute();
    },

    drugIsChosen : function(){
        if(this.getComboBox('generic').getValue() != "" && this.getComboBox('route').getValue() && this.getComboBox('shape').getValue()){
            return true;
        }
        return false;
    },

    reloadUnitStores: function(){
        var extraParams = {
            generic:this.getComboBox('generic').getValue(),
            route:this.getComboBox('route').getValue(),
            shape:this.getComboBox('shape').getValue()
        };

        var me = this;

        var subststanceUnitStore = GenPres.store.PrescriptionStores.getSubstanceUnitStore();
        subststanceUnitStore.proxy.extraParams = extraParams;
        GenPres.ASyncEventManager.registerEventListener(subststanceUnitStore, "load", []);

        var componentUnitStore = GenPres.store.PrescriptionStores.getComponentUnitStore();
        componentUnitStore.proxy.extraParams = extraParams;
        GenPres.ASyncEventManager.registerEventListener(componentUnitStore, "load", []);

        GenPres.ASyncEventManager.execute();

        me.updatePrescription();
    },

    getComboBox : function(name){
        return Ext.ComponentQuery.query('combobox[action='+name+']')[0];
    },

    setExtraParams:function(comboName, paramName, paramValue){
        var combobox = this.getComboBox(comboName);
        if(paramValue == "") combobox.clearValue();
        combobox.store.proxy.extraParams[paramName] = paramValue;
    },

    clear : function(){

        this.getComboBox('generic').setValue("");
        this.getComboBox('route').setValue("");
        this.getComboBox('shape').setValue("");
        
        this.setExtraParams('generic', 'route', '');
        this.setExtraParams('generic', 'shape', '');
        this.setExtraParams('route', 'generic', '');
        this.setExtraParams('route', 'shape', '');
        this.setExtraParams('shape', 'generic', '');
        this.setExtraParams('shape', 'route', '');

        if(this.panel == null) {
            this.panel = this.getComboBox('generic').up('panel');
            this.addStoreListeners(this.getComboBox('generic'));
            this.addStoreListeners(this.getComboBox('route'));
            this.addStoreListeners(this.getComboBox('shape'));
        }
        
        GenPres.ASyncEventManager.registerEventListener(this.getComboBox('generic').store, "load", []);
        GenPres.ASyncEventManager.registerEventListener(this.getComboBox('route').store, "load", []);
        GenPres.ASyncEventManager.registerEventListener(this.getComboBox('shape').store, "load", []);
        GenPres.ASyncEventManager.execute();

    }
});


