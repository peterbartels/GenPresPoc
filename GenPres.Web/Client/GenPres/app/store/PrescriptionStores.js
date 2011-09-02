Ext.define('GenPres.store.PrescriptionStores', {

    substanceUnitStore:null,

    componentUnitStore:null,

    singleton:true,

    getSubstanceUnitStore : function(id, name){
        if(!this.substanceUnitStore){
            this.substanceUnitStore = Ext.create('GenPres.store.prescription.SubstanceUnit')
        }
        return this.substanceUnitStore;
    },

    getComponentUnitStore : function(id, name){
        if(!this.componentUnitStore){
            this.componentUnitStore = Ext.create('GenPres.store.prescription.ComponentUnit')
        }
        return this.componentUnitStore;
    }
})