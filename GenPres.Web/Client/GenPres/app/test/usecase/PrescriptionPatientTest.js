Ext.define('GenPres.test.usecase.PrescriptionPatientTest', {
    describe: 'Prescription tests that',

    tests: function () {
        var me = this;

        me.getGridCount = function(){
            return me.getGrid().store.getCount();
        }

        me.getGrid = function(){
            var prescriptiongrid = GenPres.application.MainCenter.query('prescriptiongrid')[0];
            return prescriptiongrid;
        }
        me.getPatientComponent = function(name){
            var prescriptiongpatientview = GenPres.application.MainCenter.query('prescriptionpatient')[0];
            var patientcomp = prescriptiongpatientview.down('unitvaluefield[name='+name+']');
            return patientcomp;
        }
        me.checkGridCount = function(oldCount){
            if(me.getGridCount() == oldCount + 1){
                globalvars["checkGridCount"]=true;
            }
        }
        it('Patient weight can be set', function () {
            var prController = GenPres.application.getController('patient.Patient');
            prController.setPatientWeight(10000, 'gram');
            var weightComponent = me.getPatientComponent('patientWeight');
            expect(weightComponent.getValue().value).toBe(10000);
            expect(weightComponent.getValue().unit).toBe("gram");
        });
        it('Patient length can be set', function () {
            var prController = GenPres.application.getController('patient.Patient');
            prController.setPatientHeight(120, 'cm');
            var weightComponent = me.getPatientComponent('patientHeight');
            expect(weightComponent.getValue().value).toBe(120);
            expect(weightComponent.getValue().unit).toBe("cm");
        });
    }
});