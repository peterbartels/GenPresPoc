/*
 * Value Field
 * 
 * This field is a combination of fields
 * One field for having values, one or more comboboxes for units
 * Units can be: total, adjust and time  
 */
ValueField = Ext.extend(Ext.Container, {
    layout: 'form',
    initComponent: function() {
      
        this.addEvents(
            'change'
        );


        this.mouseOutOfList = true;
        
        //Load some default values
        this.currentIncrement=0;
        if(this.disableStepping) this.disableList=true;
        var width = 70;
		this.readOnly = false;
        this.incrementDirection = "";
        this.propertyChanged = false;
		this.allowIncrementStep = true;
		this.increments = [];
		this.triggeredBy = null;
		
		this.state = "NotSet";
		if(this.stateless){
			this.state = "User";
		}
		var compositeItems = [];
		
        var stores = Stores.PrescribeMedication.GetInstance();
		
        //Add a store for creating the incrementsteps lists
        this.listStore = new Ext.data.ArrayStore({
		    autoDestroy: true,
		    idIndex: 0,  
		    fields: ['value']
		});
		this.listStore.setDefaultSort("value");
        
        var divider = '<div style="font-size:16px;font-weight:bold;padding-top:1px;">/</div>';
        
        /*
         * If no stepping is wanted, just create a number field else create a spinner field with spinbuttons
         */
        if(this.disableStepping){
	        this.spinner = new Ext.form.NumberField({ 
				decimalPrecision:6, 
				value: 0, 
				width: 60, 
				fieldLabel: '', 
				decimalSeparator: ',',
				selectOnFocus:true,
				minValue : 0 
			});
        }else{
        	this.spinner = new Ext.ux.form.SpinnerField({ 
				decimalPrecision:6, 
				incrementValue: 1, 
				value: 0, 
				width: 60, 
				fieldLabel: '', 
				decimalSeparator: ',',
				selectOnFocus:true,
				minValue : 0 
			});
        }
        
        //Create a unique object name for mapping to backend data
        this.spinner.objName = this.className + this.propertyName;
		
        this.objName = this.className + this.propertyName;
		if(typeof(this.className) != "undefined" && typeof(this.propertyName) != "undefined") this.id = this.objName;

        this.updateFormFunc = this.formContainer.UpdateForm.createDelegate(this.formContainer, [this.spinner], false);
        
        /* If a formContainer is definef, attach the event to the form */
        if (typeof (this.formContainer) != "undefined") {
            
        	var changeFunction = function(){
            	this.fireEvent('change', this)
        	}.createDelegate(this);
        
        
        	/* spin blur event */
            this.spinner.on("blur", function(obj) {
                if(this.readOnly) return;
                if(this.mouseOutOfList == false) return;
                
                this.propertyChanged = true;
				this.list.hide(); 
                this.setUser();
                
                this.formContainer.SetLatestControlChanged(this);
                this.updateFormFunc();
            } .createDelegate(this));
            
            this.spinner.on("blur", changeFunction);
            
            this.doSpin = function(direction){
            	
            	if(this.readOnly) return;
                this.incrementDirection = direction;
                this.propertyChanged = true;
                
                this.setUser();
                
                if(this.increments.length > 0 && this.increments[0] > 0){
                	var currentValue = this.spinner.getValue();
                	
                	if(this.allowIncrementStep){
                		for(var i=0;i<this.listStore.data.items.length;i++){
                			var value = this.listStore.data.items[i].data.value;
                			var incValue = this.roundNumber(value, 4);
                			if(incValue == this.roundNumber(currentValue, 4)){	
                				if(direction == "up") this.spinner.setValue(this.listStore.data.items[i + 1].data.value);
                				if(direction == "down") {
                					if(i==0) 
                					  this.spinner.setValue(0);
                					else
                					  this.spinner.setValue(this.listStore.data.items[i - 1].data.value);
                				}
                				this.formContainer.SetLatestControlChanged(this);
                				this.updateFormFunc();
                				this.updateListItems();
                				return;
                			}
                		}
                        this.updateFormFunc();
                		return;
                	}
                	
                	if(currentValue == 0) {
                		this.spinner.setValue(this.increments[0]);
                		this.formContainer.SetLatestControlChanged(this);
                		this.updateFormFunc();
                		return;
                	}
                	for(var i=0;i<this.increments.length;i++){
                		var diff = 0;
                		var incValue = this.roundNumber(this.increments[i], 4);
                		
                		if(incValue == this.roundNumber(currentValue, 4)){
                			if(i==0 && direction=="down"){
                				this.spinner.setValue(0);
                				this.formContainer.SetLatestControlChanged(this);
                				this.updateFormFunc();
                				return;
                			}
                		    if((i+1) == this.increments.length && direction == "up"){
                				this.spinner.setValue(this.increments[i]);
                				this.formContainer.SetLatestControlChanged(this);
                				this.formContainer.UpdateForm(this.spinner);
                				return;
                			}
                			if(direction == "up") this.spinner.setValue(this.increments[i+1]);
                			if(direction == "down") this.spinner.setValue(this.increments[i-1]);
                			this.formContainer.SetLatestControlChanged(this);
                		}
                	}
                	this.formContainer.SetLatestControlChanged(this);
                	this.fireEvent('change', this);
                }
                this.updateFormFunc();
            }
            
            /*Spin up event */
            this.spinner.on("spinup", function() {
                this.doSpin("up");
            } .createDelegate(this));

            /*Spin down event */
            this.spinner.on("spindown", function() {
                this.doSpin("down");
            } .createDelegate(this));
            
            this.spinner.on("focus", function(obj){
            	
            	if(this.readOnly) return;
            	if(!this.list){
            		this.initList(obj);
            	}
            	if(!this.disableList) this.expandList(obj);
            	this.checkListItemsScroll();
            }.createDelegate(this));
        }
        
        compositeItems.push(this.spinner);
		
		
        if (this.unitStore) {
            this.unitCombo = new Ext.form.ComboBox({
                store: this.unitStore,
                width: 60,
                forceSelection:(this.forceSelection || false),
                emptyText: 'unit',
                value: this.unitValue,
                fieldLabel: ''
            });
            
            if (typeof (this.formContainer) == "object"){
                this.unitCombo.on("select", this.updateFormFunc);
                this.unitCombo.on("blur", this.updateFormFunc);
                this.unitCombo.on("select", changeFunction);
                this.unitCombo.on("blur", changeFunction);
            }

            compositeItems.push(this.unitCombo);
            stores.setSingleValue(this.unitCombo.getStore(), this.unitCombo);
            width = width + 60;
        }

        if (this.totalUnits) {
            this.totalSelect = new Ext.form.ComboBox({ emptyText: 'totaal', store: this.totalUnits, width: 60 });
            if (typeof (this.formContainer) == "object") this.totalSelect.on("select", this.updateFormFunc);
            if (typeof (this.formContainer) == "object") this.totalSelect.on("blur", this.updateFormFunc);
            compositeItems.push({ xtype:'box', html: divider }, this.totalSelect);
            stores.setSingleValue(this.totalSelect.getStore(), this.totalSelect);
            width = width + 85;
        }

        if (this.adjustUnitStore) {
            this.adjustSelect = new Ext.form.ComboBox({ emptyText: 'adjust', store: this.adjustUnitStore, width: 60 });
            
            var clear = function(selectBox){ if(selectBox.getValue() == "-") selectBox.clearValue(); };
            this.adjustSelect.on("select", clear);
            this.adjustSelect.on("blur", clear);
            if (typeof (this.formContainer) == "object") this.adjustSelect.on("select", this.updateFormFunc);
            if (typeof (this.formContainer) == "object") this.adjustSelect.on("blur", this.updateFormFunc);
            compositeItems.push({ xtype:'box', html: divider }, this.adjustSelect);
            width = width + 85;
        }

        if (this.timeUnitStore) {
            this.timeSelect = new Ext.form.ComboBox({ emptyText: 'tijd', store: this.timeUnitStore, width: 60, value: this.timeUnitValue });
            if (typeof (this.formContainer) == "object") this.timeSelect.on("select", this.updateFormFunc);
            if (typeof (this.formContainer) == "object") this.timeSelect.on("blur", this.updateFormFunc);
            compositeItems.push({ xtype:'box', html: divider }, this.timeSelect);
            width = width + 61;
        }
		if(this.width) width = this.width; else this.width = width;
        
		// CompositeField is used to combine the fields
        this.CompositeField = new Ext.form.CompositeField({
            defaultMargins: '0 0 0 0',
            fieldLabel: this.label,
            items: compositeItems
        });
        if(this.label == "") this.CompositeField.hideLabel = true;
		 
        this.items = this.CompositeField;

        ValueField.superclass.initComponent.call(this);
    },
    
    initList : function(spinnerObj){
        var cls = "x-combo-list";
        this.list = new Ext.Layer({
            parentEl: document.body,
            shadow: true,
            cls: cls,
            constrain:false,
            zindex: (12000) + 5
        });
        var lw = 80;
        var lh = 100;
        this.list.setSize(lw, lh);
        this.list.swallowEvent('mousewheel');
        
        this.innerList = this.list.createChild({cls:cls+'-inner'});
        this.innerList.setWidth(lw - this.list.getFrameWidth('lr'));
        this.innerList.setHeight(lh - this.list.getFrameWidth('lr'));
        
        this.mon(this.innerList, 'mouseover', this.onViewOver, this);
        this.mon(this.innerList, 'mouseout', this.onViewOut, this);
        this.mon(this.innerList, 'scroll', this.checkListItemsScroll, this);
        
        var displayField = "value";
        this.tpl = '<tpl for="."><div class="'+cls+'-item">{' + displayField + '}</div></tpl>';
		
        this.view = new Ext.DataView({
            applyTo: this.innerList,
            tpl: this.tpl,
            store:this.listStore,
            singleSelect: true,
            selectedClass: 'x-combo' + '-selected',
            itemSelector: '.' + cls + '-item',
            emptyText: "No possible values",
            deferEmptyText: false
        });
        this.mon(this.view, {
            containerclick : this.onViewClick,
            click : this.onViewClick,
            scope :this
        });
    },
    onViewClick : function(doFocus){
        if(this.spinner.getValue() > 0) this.setUser();
        
        var index = this.view.getSelectedIndexes()[0],
            s = this.listStore,
            r = s.getAt(index);
        if(r){
            this.onSelect(r, index);
        }else {
            this.collapse();
        }
        this.list.hide();
    },
    onSelect : function(record, index){
        this.spinner.setValue(record.data["value"]);
        this.updateFormFunc();
        this.formContainer.SetLatestControlChanged(this);
    },
    onViewOut : function(){
    	this.mouseOutOfList = true;
    },
    onViewOver : function(e, t){
        this.mouseOutOfList = false;
        var item = this.view.findItemFromChild(t);
        if(item){
            var index = this.view.indexOf(item);
            this.select(index, false);
        }	
    },
    updateListItems : function(){
		if(this.allowIncrementStep){
			var currentValue = this.spinner.getValue();
			if(this.roundNumber(currentValue, 6) == 0){
				this.currentIncrement = 0;
				return;
			}
			for(var i=0;i<this.increments.length;i++){
				var value = this.increments[i];
				var incValue = this.roundNumber(currentValue, 6);
				//console.log(this.id, this.increments);
				if(value == 0) continue;
				if(this.roundNumber((incValue / value), 6) == this.roundNumber((incValue / value), 2)){
					this.currentIncrement = this.roundNumber((incValue / value), 0) - 3;
				}
			}
			if(this.currentIncrement < 3) this.currentIncrement = 0;
			if(this.view) this.view.select(5);
			this.updateIncrementSteps();
		}
    },
    checkListItemsScroll : function(a,b,c,d){
    	var top = this.innerList.getScroll().top;
		if(top > 120){
			this.innerList.scrollTo("top",120);
		    this.currentIncrement = this.currentIncrement + 1;
			this.updateIncrementSteps();
		}
		if(top<20 && this.currentIncrement > 0){
			this.innerList.scrollTo("top",20);
			this.currentIncrement = this.currentIncrement - 1;
			this.updateIncrementSteps();
		}
    },
    select : function(index, scrollIntoView){
        this.view.select(index);
    },
    expandList : function(spinnerObj){
        if(this.listStore.data.items.length == 0) return;
		var lw = 80;
        var lh = 100;
        this.list.setSize(lw, lh);
        this.list.alignTo.apply(this.list, [spinnerObj.getEl()].concat(this.listAlign));
    	this.list.show();
    },
    getTotalSelect: function() {
        return this.totalSelect;
    },
    getValue: function() {
        var result = { Value: this.spinner.getValue() };

        if (this.unitStore) {
            result["Unit"] = this.unitCombo.getValue();
        }
        if (this.totalUnits) {
            result["Total"] = this.totalSelect.getValue();
        }
        if (this.adjustUnitStore) {
            result["Adjust"] = this.adjustSelect.getValue();
        }
        
		
		result["UIState"] = this.state;
        
        result["AdjustWeight"] = GenPres.SelectedPatient.Weight;
        result["AdjustLength"] = GenPres.SelectedPatient.Length;
		
        result.PropertyChanged = this.propertyChanged;
        result.IncrementDirection = this.incrementDirection;
        
        if (this.timeUnitStore) {
            result["Time"] = this.timeSelect.getValue();
        }
        return result;
    },
    setValue: function(value) {
        
        value.State = value.UIState;
		
		var stateChanged = false;
		if(this.spinner.getValue() == 0 && value.Value > 0){
			if(this.formContainer.GetLatestControlChanged() != null){
				this.triggeredBy = this.formContainer.GetLatestControlChanged();
				//if(value.State != "User") stateChanged = true;
			}
		}else{
			if(this.spinner.getValue() > 0){
				if(value.State != "User") {
					this.setCalculated();
					stateChanged = true;
				}
			}
		}
		
		this.spinner.setValue(this.roundNumber(value.Value, 6));
		if(value.Value == 0){
			this.triggeredBy = null;
			this.setUser();
			stateChanged = true;
    	}
    	this.updateListItems();
        
        if (this.unitStore) {
            if (value.Unit == "" && this.unitValue != "") value.Unit = this.unitValue;
            this.unitCombo.setValue(value.Unit);
        }
        if (this.totalUnits) {
            this.totalSelect.setValue(value.Total);
        }
        if (this.adjustUnitStore) {
            this.adjustSelect.setValue(value.Adjust);
        }
        if (this.timeUnitStore) {
            if (value.Time == "" && this.timeUnitValue != "") value.Time = this.timeUnitValue;
            this.timeSelect.setValue(value.Time);
        }
        this.incrementDirection = "";
        this.propertyChanged = false;
        this.increments = value.Increments;   
        
        this.allowIncrementStep = value.AllowIncrementStep;


        this.updateIncrementSteps();
        if(this.state != value.State && !stateChanged){
        	this.state = value.State;
        	if(this.state == "User"){
        		this.setUser();
        	}else{
        		this.setCalculated();
        	}
        	//if(this.state == "Calculated") this.setCalculated();
        }
        
    },
	roundNumber : function (num, dec) {
		var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
		return result;
	},
	
	findCurrentIncrementStep : function(val){
		for(var i=0;i<this.increments.length;i++){
			
		}
	},
	
    updateIncrementSteps : function(absVal, val){
    	if(this.increments[0] == 0 || this.increments.length == 0){
    		this.listStore.loadData([]);
    	}
		var nr = this.currentIncrement;
    	if(this.increments[0] > 0 && this.listStore != null){
    		var data = [];
    		if(this.allowIncrementStep){	    		
	    		var maxItems = 20;
	    		while(maxItems > 0){
	    			for(var i=0;i<this.increments.length;i++){
	    				data.push([this.roundNumber(nr*this.increments[i], 6)]);
	    			}
	    			nr++;
	    			maxItems--;
	    		}
    		}else{
    			for(var i=0;i<this.increments.length;i++){
					data.push([this.roundNumber(this.increments[i], 6)]);
    			}
    		}
    		this.listStore.loadData(data);
    	}
    },
    clearClasses: function() {
        this.spinner.el.removeClass("field-calculated");
        this.spinner.el.removeClass("field-user");
        this.spinner.el.removeClass("field-notset");
    },
    setCalculated: function() {
        
        if(this.stateless )return;
        this.clearClasses();
		this.state = "Calculated";
		if(this.formContainer) this.formContainer.setCalculatedState(this.objName);
        
        this.spinner.el.addClass("field-calculated");
    },
    setUser: function() {
        if(this.stateless )return;
		this.state = "User";
		this.clearClasses();
        if(this.formContainer) this.formContainer.setUserState(this.objName);
		this.triggeredBy = null;
        this.spinner.el.addClass("field-user");
    },
    setReadOnly : function(value){
    	this.readOnly = value;
    	this.spinner.setReadOnly(value);
        if (this.unitStore) this.unitCombo.setReadOnly(value);
        if (this.totalUnits) this.totalSelect.setReadOnly(value);
        if (this.adjustUnitStore) this.adjustSelect.setReadOnly(value);
        if (this.timeUnitStore) this.timeSelect.setReadOnly(value);
    },
    setNotSet: function() {
        if(this.stateless )return;
        this.clearClasses();
        this.state = "User";
        this.spinner.el.addClass("field-notset");
    },
    getState: function(){
    	return this.state;
    },
    getText: function() {
        var value = this.getValue();

        if (value.Value == 0) return "";

        result = value.Value;

        if (value.Unit) {
            result += " " + value.Unit;
        }
        if (value.Adjust) {
            result += "/" + value.Adjust;
        }
        if (value.Total) {
            result += "/" + value.Total;
        }
        if (value.Time) {
            result += "/" + value.Time;
        }
        return result;
    },
    show: function() {
		this.getEl().parent().dom.style.visibility = '';
    }
});