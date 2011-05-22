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
