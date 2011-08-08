Ext.require('Ext.form.*');

Ext.define('GenPres.control.ValueField', {

    extend:'Ext.form.field.Number',

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
        Ext.Function.defer(focusEl, 200);
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
