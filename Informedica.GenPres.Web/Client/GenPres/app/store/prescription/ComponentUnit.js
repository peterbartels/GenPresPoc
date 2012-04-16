Ext.define('GenPres.store.prescription.ComponentUnit', {
    extend: 'GenPres.store.prescription.ValueStore',
    id: 'componentunit',
    autoLoad:false,
    proxy : {
        type:'direct',
        directFn : Prescription.GetComponentUnits,
        extraParams:{
            generic:"",
            route: "",
            shape:""
        },
        paramOrder : ['generic', 'route', 'shape']
    }
});