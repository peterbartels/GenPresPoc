Ext.define('GenPres.session.PatientSession', {

    currentLogicalUnitId:0,

    currentLogicalUnitName:'',

    singleton: true,

    setLogicalUnit : function(id, name){
        this.currentLogicalUnitId = id;
        this.currentLogicalUnitName = name;
    },

    getLogicalUnitId : function(){
        return this.currentLogicalUnitId;
    },
    
    getLogicalUnitName : function(){
        return this.currentLogicalUnitName;
    }
})