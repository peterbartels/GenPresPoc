
Ext.require('Ext.form.*');


Ext.define('GenPres.controls.NumericBoundListKeyNav', {
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
                
                //me.fireEvent('select', me, {
            	if(oldItemIdx == 0){
            		if(me.boundList.store.getAt(0).data.number != 0){
            			
            		}
            	}else{
            		if(oldItemIdx == 2){
            			this.fireEvent("upreached");
            		}else{
            			me.highlightAt(newItemIdx);
            		}
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
        }
    }
});

Ext.define('GenPres.controls.ComboSpinner', {
    extend:'Ext.form.Number',
    
    requires: ['Ext.form.Picker', 'Ext.data.ArrayStore', 'Ext.util.DelayedTask', 'Ext.EventObject', 'Ext.view.BoundList', 'Ext.view.BoundListKeyNav', 'Ext.data.StoreMgr'],
    
    keyNavEnabled : false,
    
    initComponent: function() {
    	var me = this;
    	me.Picker = new Ext.form.Picker({
    		height:500
    	});
    	
    	me.callParent();
    },
    
    getInnerTpl: function(displayField) {
        return '{' + displayField + '}';
    },
    onBlur: function() {
    	var me = this;
    	Ext.form.ComboSpinner.superclass.onBlur.call(me);
    },
    spinUp : function(){
    	//this.inputEl.focus();
    	
    	var me = this;
    	Ext.form.ComboSpinner.superclass.spinUp.call(me);
    	//this.inputEl.focus();
    	//this.onFocus();
    },
    spinDown : function(){
    	this.inputEl.focus();
    	var me = this,
    		picker = me.getPicker();
    	Ext.form.ComboSpinner.superclass.spinDown.call(me);
    	this.inputEl.focus();
    },
    updateNumberStore :  function(){
    	var me = this;
    	var store = me.store;
    	store.clearData();
    	for(var i = 0; i<5; i++){
    		store.add({number:me.fixPrecision(i*me.step)})
    	}
    },
    onFocus: function() {
    	var me = this;
		Ext.form.ComboSpinner.superclass.onFocus.call(me);
		alert('f');
		me.expand();
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
                maxHeight: 200,
                loadingText: "d",
                emptyText: "e"
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
    	
    	for(var i = start; i< start + 5; i++){
    		store.add({number:me.fixPrecision(i*me.step)});
    		me.listKeyNav.highlightAt(2);
    	}
    	//var start = picker.
    	
    },
    
    onExpand: function() {
       
    	var me = this,
            keyNav = me.listKeyNav,
            picker = me.getPicker(),
            lastSelected = picker.getSelectionModel().lastSelected,
            itemNode;
		me.selectOnTab= true;
        if (!keyNav) {
            keyNav = me.listKeyNav = new GenPres.controls.NumericBoundListKeyNav(this.inputEl, {
                boundList: picker,
                selectOnTab: me.selectOnTab,
                forceKeyDown: true
            });
            keyNav.on("downreached", me.updateStoreDown, me);
        }
        Ext.defer(keyNav.enable, 1, keyNav); //wait a bit so it doesn't react to the down arrow opening the picker
			
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
		
        picker.highlightItem(picker.getNode(foundRecord));

        me.inputEl.focus();
    },
    
    
    onListSelectionChange: function(list, selectedRecords) {
        var me = this;
        if (!me.inSetValue && me.isExpanded) {
            if (!me.multiSelect) {
                Ext.defer(me.collapse, 1, me);
            }
            if(selectedRecords.length > 0){
            	me.setValue(selectedRecords[0].data.number, false);
            }
            //me.fireEvent('select', me, selectedRecords);
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

Ext.define('GenPres.Views.Viewport', {

    extend: 'Ext.container.Viewport',

    layout: 'fit',

    initComponent: function() {
        var me = this;
        
        var comboSpinner = new GenPres.controls.ComboSpinner({
            labelWidth: 70,
            fieldLabel: 'Default',
            name: 'basic',
            value: 1,
            step:0.1776,
            decimalPrecision:10,
            width:150,
            minValue: 1,
            maxValue: 125
        });
        /*
        var composite = new Ext.form.FieldContainer({
        	layout: 'hbox',
        	defaults: {
	            height: 50,
	            flex: 1
        	},
        	 items: [{
            	xtype: 'panel',
            	title: 'Panel 1'
        	}
        })*/
			            
        var tree = new GenPres.Views.PatientSelectorTree();
        me.items = [{
            xtype: 'container',
            layout: {
                type: 'hbox',
                align: 'stretch'
            },
            items: [
                {
                    xtype: 'container',
                    
                    width: 200
                },{
                    xtype:'panel',
                    flex:1,
                    layout : {
                        type:'vbox',
                        align: 'stretch'
                    },
                    items : [{
                        xtype: 'panel',
                        layout: { type: 'fit' },
                        html:'header',
                        height:200
                    }, {
                        
                        items:[comboSpinner],
                        flex:1
                    }]
                }
            ]
        }]
        me.callParent(arguments);
    }
});