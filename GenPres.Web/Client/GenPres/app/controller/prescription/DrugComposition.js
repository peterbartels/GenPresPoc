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
            'combobox' : {
                select : this.changeSelection
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

    changeSelection : function(combo){

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
        
        var extraParams = {
            generic:this.generic,
            route:this.route,
            shape:this.shape
        };
        
        var subststanceUnitStore = GenPres.store.PrescriptionStores.getSubstanceUnitStore();
        subststanceUnitStore.proxy.extraParams = extraParams;
        GenPres.ASyncEventManager.registerEventListener(subststanceUnitStore, "load", []);
        //subststanceUnitStore.load();

        var componentUnitStore = GenPres.store.PrescriptionStores.getComponentUnitStore();
        componentUnitStore.proxy.extraParams = extraParams;
        GenPres.ASyncEventManager.registerEventListener(componentUnitStore, "load", []);

        GenPres.ASyncEventManager.execute();
        Ext.Function.defer(this.updatePrescription, 0, this);
    },

    drugIsChosen : function(){
        if(this.getComboBox('generic').getValue() != "" && this.getComboBox('route').getValue() && this.getComboBox('shape').getValue()){
            return true;
        }
        return false;
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