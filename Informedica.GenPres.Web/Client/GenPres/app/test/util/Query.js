Ext.define('GenPres.test.util.Query', {

    singleton: true,

    getParentViewQuery: function(parentView){
        if(typeof(parentView) == "undefined"){
            return Ext.ComponentQuery;
        }
        return parentView;
    },

    GetControl : function(name, parentView){
        var result = this.getParentViewQuery(parentView).query('panel[name='+name+']');
        if(result.length == 0){
            result = this.getParentViewQuery().query('field[name='+name+']');
        }
        return result[0];
    },
    SelectFirstComboboxValue : function(name, parentView){
        var combo = this.getParentViewQuery(parentView).query('combobox[name='+name+']')[0];
        combo.select(combo.store.getAt(0).data.Value);
    },
    controlStoreIsLoaded : function(control){
        if(!this.isDefined(control)) return false;
        if(!this.isDefined(control.store)) return false;
        return control.store.getCount() > 0;
    },
    isDefined : function(name){
        return typeof(name) != "undefined";
    }


})