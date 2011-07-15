Ext.define('GenPres.controller.patient.PatientController', {
    extend: 'Ext.app.Controller',

    stores:['patient.LogicalUnitStore', 'patient.PatientInfoStore', 'patient.PatientTreeStore'],

    models:['patient.LogicalUnitModel', 'patient.PatientModel'],

    views:['main.PatientTree', 'main.PatientInfo'],

    init: function() {
        this.control({
            'treepanel': {
                itemclick: this.loadPatientData
            }
        });
    },

    onLaunch: function() {
        
    },

    loadPatientData : function(tree, record){
        var infoStore = this.getPatientPatientInfoStoreStore();
        infoStore.loadRecords([record], {addRecords: false});
        GenPres.session.PatientSession.setPatient(record);
        var gridPanel = this.getGridPanel();
        gridPanel.store.proxy.extraParams.PID = GenPres.session.PatientSession.patient.PID;
        gridPanel.store.load();
    },

    getGridPanel : function(){
        return GenPresApplication.MainCenter.query('.prescriptiongrid')[0];
    }

});