
Ext.define('GenPres.control.UnitValueField', {

    extend:'Ext.Panel',

    alias:'widget.unitvaluefield',
    
    width: 500,

    border: false,

    margin:'0 0 0 0',

    isFormField : true,

    getSubmitData : function(includeEmptyText){
        var me = this;
        var result = {};
        
        result[me.name] ={
            value:me.valueField.getValue(),
            unit:me.unitCombo.getValue()
        }
        
        return result;
    },

    getValue : function(){
        return 665;
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
    },
    
    getUnitCombo : function(){
        var me = this;
        return me.unitCombo;
    }
});