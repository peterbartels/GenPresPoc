Ext.define('GenPres.controller.patient.Patient', {
    extend: 'Ext.app.Controller',

    stores:['patient.LogicalUnitStore', 'patient.PatientInfoStore', 'patient.PatientTreeStore'],

    models:['patient.LogicalUnitModel', 'patient.PatientModel'],

    views:['main.PatientTree', 'main.PatientInfo'],

    init: function() {
        this.control({
            'treepanel': {
                beforeitemclick: this.checkRootNode,
                itemclick: this.loadPatientData
            }
        });
    },

    onLaunch: function() {
        
    },
    checkRootNode : function(tree, record, htmlitem, index, event, options){
        if(index==0){
            var infoStore = this.getPatientPatientInfoStoreStore();
            var treePanel = this.getTreePanel();
            if(typeof(infoStore.getAt(0)) != "undefined"){
                treePanel.selModel.select(infoStore.getAt(0));
            }
            return false;
        }
    },
    loadPatientData : function(tree, record, htmlitem, index, event, options){
        var infoStore = this.getPatientPatientInfoStoreStore();
        infoStore.loadRecords([record], {addRecords: false});
        GenPres.session.PatientSession.setPatient(record);
        var gridPanel = this.getGridPanel();
        gridPanel.store.proxy.extraParams.PID = GenPres.session.PatientSession.patient.PID;
        gridPanel.store.load();
    },

    getGridPanel : function(){
        return GenPres.application.MainCenter.query('.prescriptiongrid')[0];
    },
    
    getTreePanel : function(){
        return GenPres.application.viewport.query('.patienttree')[0];
    }

});