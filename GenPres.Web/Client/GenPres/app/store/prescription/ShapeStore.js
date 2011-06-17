Ext.define('GenPres.store.prescription.ShapeStore', {
    extend: 'GenPres.store.prescription.ValueStore',
    alias: 'widget.shapestore',
    proxy : {
        type:'direct',
        directFn : Prescription.GetShapes
    }
});