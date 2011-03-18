Ext.namespace("Stores");

//Singleton
Stores.PrescribeMedication = function() {

    var _instance = null;
    var stores = {};
    medicineParams = {};

    var GetPrescriptionStore = function(InvokeMehtod) {
        var store = new Ext.data.DirectStore({
            directFn: Drug[InvokeMehtod],
            idProperty: 'Id',
            baseParams: { unit: "", generic: "", shape: "", route: "", unitgroup: "" },
            fields: ['Name'],
            sortInfo: { field: 'Name', direction: 'ASC' }
        });
        return store;
    }

    var storeFunctions = {
        CreateTPNComponentStore : function(){
        	var store = new Ext.data.DirectStore({
	            directFn: TPN["GetComponentNames"],
	            idProperty: 'Id',
	            baseParams: { selections:"", getBaseSolution:false },
	            fields: ['Name']
	        });
	        return store;	
        },
        GetSolutionTypeStore: function() {
            if (typeof (stores["solutionTypeStore"]) == "undefined") {
                stores["solutionTypeStore"] = GetPrescriptionStore("GetSolutions");
                stores["solutionTypeStore"].reload();
            }
            return stores["solutionTypeStore"];
        },
        GetSolutionStore: function() {
            if (typeof (stores["solutionStore"]) == "undefined") {
                stores["solutionStore"] = GetPrescriptionStore("GetSolutions");
                stores["solutionStore"].reload();
            }
            return stores["solutionStore"];
        },
        GetRouteStore: function() {
            if (typeof (stores["routeStore"]) == "undefined") {
                stores["routeStore"] = GetPrescriptionStore("GetRoutes");
                stores["routeStore"].reload();
            }
            return stores["routeStore"];
        },
        GetSolutionStore: function() {
            if (typeof (stores["solutionStore"]) == "undefined") {
                stores["solutionStore"] = GetPrescriptionStore("GetShapes");
                stores["solutionStore"].reload();
            }
            return stores["solutionStore"];
        },
        GetDosageUnits: function() {
            if (typeof (stores["dosageUnitStore"]) == "undefined") {
                stores["dosageUnitStore"] = GetPrescriptionStore("GetDosageUnits");
                stores["dosageUnitStore"].reload();
            }
            return stores["dosageUnitStore"];
        },
        GetShapeUnits: function() {
            if (typeof (stores["shapeUnitStore"]) == "undefined") {
                stores["shapeUnitStore"] = GetPrescriptionStore("GetShapeUnits");
                stores["shapeUnitStore"].reload();
            }
            return stores["shapeUnitStore"];
        },
        GetGenericStore: function() {
            if (typeof (stores["genericStore"]) == "undefined") {
                stores["genericStore"] = GetPrescriptionStore("GetGenerics");
                stores["genericStore"].reload();
            }
            return stores["genericStore"];
        },
        GetMedicineParams: function() { return medicineParams },
        
        SetMedicineParams: function(params) { medicineParams=params },

        reloadMedicine: function(combo, record, index, params) {
            
            var stores = Stores.PrescribeMedication.GetInstance();
            stores.GetMedicineParams()[params.name] = combo.getValue();
			
            var genericStore = stores.GetGenericStore();
            var routeStore = stores.GetRouteStore();
            var solutionStore = stores.GetSolutionStore();
            if (params.name != "generic") {
                genericStore.baseParams = stores.GetMedicineParams();
                genericStore.reload();
            }
            if (params.name != "route") {
                routeStore.baseParams = stores.GetMedicineParams();
                routeStore.reload();
            }
            if (params.name != "shape") {
                solutionStore.baseParams = stores.GetMedicineParams();
                solutionStore.reload();
            }
            stores.reloadUnitStores(stores.GetMedicineParams());
        },
        reloadUnitStores : function(params){
        	var stores = Stores.PrescribeMedication.GetInstance();
        	var shapeStore = stores.GetShapeUnits();
            var dosageStore = stores.GetDosageUnits();
        	shapeStore.baseParams = params;
            shapeStore.reload();
            dosageStore.baseParams = params;
            dosageStore.reload();
        },
        clear : function(){
        	var baseParams = { unit: "", generic: "", shape: "", route: "", unitgroup: "" };
			
			medicineParams = {};
			
	        var genericStore = this.GetGenericStore();
	        var routeStore = this.GetRouteStore();
	        var solutionStore = this.GetSolutionStore();
	        var shapeStore = this.GetShapeUnits();
	        var dosageStore = this.GetDosageUnits();
	        
			genericStore.baseParams = baseParams;
	        genericStore.reload();
			
	        routeStore.baseParams = baseParams;
	        routeStore.reload();
	        
			solutionStore.baseParams = baseParams;
	        solutionStore.reload();
	        
			dosageStore.baseParams = baseParams;
	        dosageStore.reload();
	        
			shapeStore.baseParams = baseParams;
	        shapeStore.reload();
			
        },
        setSingleValue: function(store, combo) {
            store.addListener("load", function() {
                var stores = Stores.PrescribeMedication.GetInstance();
                if (store.data.items.length == 1) {
                    combo.setValue(store.data.items[0].data.Name);
                }
                if (store.data.items.length == 3) {
                    if (store.data.items[0].data.Name == "g") {
                        if(combo.getValue() != "g" && combo.getValue() != "mg" && combo.getValue() != "microg"){ 
                    		combo.setValue(store.data.items[1].data.Name);
                        }
                    }
                }
            } .createDelegate(this));
        }
    };

    return {
        GetInstance: function() {
            if (_instance === null) {
                _instance = storeFunctions;       //define your instance here
            }
            return _instance;
        }
    };
} ();
