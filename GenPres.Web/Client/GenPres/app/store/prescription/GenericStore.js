Ext.define('GenPres.store.prescription.GenericStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.genericstore',
    autoLoad:false,
    proxy : {
        type:'direct',
        directFn : Prescription.GetGenerics,
        extraParams:{
            route: "",
            shape:""
        },
        paramOrder : ['route', 'shape']
    }

});