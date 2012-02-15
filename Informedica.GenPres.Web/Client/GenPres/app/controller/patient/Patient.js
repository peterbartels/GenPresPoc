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

        var me =this,
            infoStore = this.getPatientPatientInfoStoreStore();

        infoStore.loadRecords([record], {addRecords: false});
        GenPres.session.PatientSession.setPatient(record);

        var gridPanel = this.getGridPanel();
        gridPanel.store.proxy.extraParams.PID = GenPres.session.PatientSession.patient.PID;
        gridPanel.store.load();

        Patient.SelectPatient(GenPres.session.PatientSession.patient.PID, function(patientDto){
            var prescriptionController = GenPres.application.getController('prescription.PrescriptionController');
            prescriptionController.loadPrescriptionForm();
            prescriptionController.clearPrescription();
            me.setPatientWeight(patientDto.Weight);
            me.setPatientLength(patientDto.Height);
            prescriptionController.updatePrescription();
        });
    },

    setPatientWeight : function(weight){
        var prescriptionPatientComp = this.getPrescriptionPatientComponent();
        prescriptionPatientComp.down('unitvaluefield[name=patientWeight]').setValue(weight);
    },

    setPatientLength : function(height, unit){
        var prescriptionPatientComp = this.getPrescriptionPatientComponent();
        prescriptionPatientComp.down('unitvaluefield[name=patientLength]').setValue(height);
    },

    getPrescriptionPatientComponent : function(){
        return GenPres.application.MainCenter.query('prescriptionpatient')[0];
    },

    getGridPanel : function(){
        return GenPres.application.MainCenter.query('.prescriptiongrid')[0];
    },
    
    getTreePanel : function(){
        return GenPres.application.viewport.query('.patienttree')[0];
    }

});