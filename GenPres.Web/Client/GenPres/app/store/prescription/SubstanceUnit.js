Ext.define('GenPres.store.prescription.SubstanceUnit', {
    extend: 'GenPres.store.prescription.ValueStore',
    id: 'substanceunit',
    proxy : {
        type:'direct',
        directFn : Prescription.GetSubstanceUnits,
        autoLoad:true,
        extraParams:{
            generic:"",
            route: "",
            shape:""
        },
        paramOrder : ['generic', 'route', 'shape']
    }

});