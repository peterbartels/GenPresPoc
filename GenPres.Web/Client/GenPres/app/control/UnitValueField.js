
GenPres.control = {
    states : {
        user : 1,
        calculated: 2
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



    setHidden : function (hidden){
        if(hidden) {
            this.getEl().dom.style.visibility = "hidden";
        }else{
            this.getEl().dom.style.visibility = "";
        }
    },

    setState : function(state){
        this.state = state;
        this.setInputColor(GenPres.control.stateColors[state]);
    },

    setInputColor : function(color){
        if(typeof(color) != "undefined" && color != ""){
            this.getInputEl().style.border = "solid 1px " + color;
        }else{
            //TODO: find why sometimes color is not set.
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

    getValue : function(){
        var me = this;
        return {
            value : me.valueField.getValue(),
            unit: (!me.unitStore ?  "" : me.unitCombo.getValue()),
            timeUnit: (!me.timeStore ?  "" : me.timeCombo.getValue()),
            totalUnit: (!me.totalStore ?  "" : me.totalCombo.getValue()),
            adjustUnit: (!me.adjustStore ?  "" : me.adjustCombo.getValue())
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
        if(me.unitStore) me.unitCombo.setValue(me.unit);
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
            if(combo.getValue() == "" || combo.getValue() == null){
                for(var i=0;i<store.data.items.length;i++){
                    var val = store.data.items[i].raw;
                    if(val.selected == true){
                        Ext.Function.defer(function(){
                            combo.setValue(val["Value"]);
                        },200);
                    }
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
        
        var items = [me.valueField];

        if(me.unitStore){
            me.unitCombo = new Ext.create('Ext.form.ComboBox',{
                xtype:'combobox',
                isFormField:false,
                store:me.unitStore,
                width:60
            })
            items.push(me.unitCombo);
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
            me.setValue({
               value : me.value,
               unit: me.unit,
               canBeSet:me.canBeSet,
               state:me.state
            });
        })
    },

    createSeperator : function(){
        return Ext.create('Ext.Component', {
            html:'<B>/</B>'
        })
    },

    suspendEvents : function(){
        var me = this;
        //me.valueField.suspendEvents();
        //me.unitCombo.suspendEvents();
        me.callParent();
    },

    resumeEvents : function(){
        var me = this;
        //me.valueField.resumeEvents();
        //me.unitCombo.resumeEvents();
        me.callParent();
    },

    getUnitCombo : function(){
        var me = this;
        return me.unitCombo;
    }
});