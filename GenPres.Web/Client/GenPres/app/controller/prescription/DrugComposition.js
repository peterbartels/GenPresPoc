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
            options.comboBox.select(options.comboBox.store.data.getAt(0))
        }
    },

    addStoreListeners : function(combo){
        combo.store.on("load", this.checkValues, this, {comboBox:combo});
        combo.store.on("load", this.checkUpdatePrescription, this, {comboBox:combo});
        combo.store.on("change", this.checkUpdatePrescription, this, {comboBox:combo});
    },

    checkUpdatePrescription : function(){
        if(this.getDrugCompositionController().drugIsChosen()){
            if(!this.prescriptionIsLoading) this.updatePrescription();
        }
    },

    changeSelection : function(combo){

        if(this.panel == null) {
            this.panel = combo.up('panel');
            this.addStoreListeners(this.getComboBox('generic'));
            this.addStoreListeners(this.getComboBox('route'));
            this.addStoreListeners(this.getComboBox('shape'));
        }

        if(combo.action == "generic"){
            this.generic = combo.getValue();
            this.setExtraParams('route', 'generic', this.generic);
            this.setExtraParams('shape', 'generic', this.generic);
            this.getComboBox('route').store.load();
            this.getComboBox('shape').store.load();
        }
        if(combo.action == "route"){
            this.route = combo.getValue();
            this.setExtraParams('generic', 'route', this.route);
            this.setExtraParams('shape', 'route', this.route);
            this.getComboBox('generic').store.load();
            this.getComboBox('shape').store.load();
        }
        if(combo.action == "shape"){
            this.shape = combo.getValue();
            this.setExtraParams('generic', 'shape', this.shape);
            this.setExtraParams('route', 'shape', this.shape);
            this.getComboBox('generic').store.load();
            this.getComboBox('route').store.load();
        }

        var extraParams = {
            generic:this.generic,
            route:this.route,
            shape:this.shape
        };

        GenPres.store.PrescriptionStores.getSubstanceUnitStore().proxy.extraParams = extraParams;
        GenPres.store.PrescriptionStores.getSubstanceUnitStore().load();

        GenPres.store.PrescriptionStores.getComponentUnitStore().proxy.extraParams = extraParams;
        GenPres.store.PrescriptionStores.getComponentUnitStore().load();
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

        this.getComboBox('generic').store.load();
        this.getComboBox('route').store.load();
        this.getComboBox('shape').store.load();

    }
});