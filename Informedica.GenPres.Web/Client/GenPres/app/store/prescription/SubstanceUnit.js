Ext.define('GenPres.store.prescription.SubstanceUnit', {
    extend: 'GenPres.store.prescription.ValueStore',
    id: 'substanceunit',
    autoLoad:false,
    proxy : {
        type:'direct',
        directFn : Prescription.GetSubstanceUnits,
        extraParams:{
            generic:"",
            route: "",
            shape:""
        },
        paramOrder : ['generic', 'route', 'shape']
    }
});