Ext.define('GenPres.store.prescription.ComponentUnit', {
    extend: 'GenPres.store.prescription.ValueStore',
    id: 'componentunit',
    proxy : {
        type:'direct',
        directFn : Prescription.GetComponentUnits,
        autoLoad:true,
        extraParams:{
            generic:"",
            route: "",
            shape:""
        },
        paramOrder : ['generic', 'route', 'shape']
    }
});