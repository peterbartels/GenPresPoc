
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
        this.getInputEl().style.border = "solid 1px " + color;
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
            unit : me.unitCombo.getValue()
        };
    },

    setValue : function(obj){
        var me = this;
        me.value = obj.value;
        me.unit = obj.unit;
        me.state = obj.state;
        me.canBeSet = obj.canBeSet;
        me.processValues();
    },

    processValues : function(){
        var me = this;
        me.valueField.setValue(me.value);
        me.unitCombo.setValue(me.unit);
        me.setHidden(!me.canBeSet);
        me.setState(me.state);
    },

    isValid : function(){
        return true;
    },

    isDirty : function(){
        return true;
    },
    
    initComponent : function(){
        var me = this;

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
        }

        me.items = [{
            xtype: 'fieldcontainer',
            labelAlign:me.labelAlign,
            fieldLabel: me.fieldLabel,
            margin:'0 0 0 0',
            width: 500,
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
    
    getUnitCombo : function(){
        var me = this;
        return me.unitCombo;
    }
});