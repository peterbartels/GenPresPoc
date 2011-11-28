
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

    canBeSet: true,

    state: GenPres.control.states.user,

    value : 0,

    unit : "",

    changedByUser:false,

    setHidden : function (hidden){
        if(hidden) {
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
        me.canBeSet = obj.canBeSet;
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
        me.setHidden(!me.canBeSet);
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
            if(combo==timeCombo){
                debugger;
            }
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
            combo.on("afterrender", setSelected);
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
               canBeSet:me.canBeSet,
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