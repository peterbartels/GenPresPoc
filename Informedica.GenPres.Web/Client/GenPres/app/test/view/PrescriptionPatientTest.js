Ext.define('GenPres.test.view.PrescriptionPatientTest', {

    describe: 'GenPres.view.Prescription.Patient',

    tests: function () {
        var me = this, instance, win;

        me.getPrescriptionPatientView = function (config) {
            if (!instance) {
                instance = Ext.create('GenPres.view.prescription.Patient', config)
            }
            return instance;
        };

        me.createWindow = function(){
            win = Ext.create('Ext.Window', {
                items:me.getPrescriptionPatientView(),
                height:520,
                width:900
            });
            win.show();
            return win;
        };

        it('can be created', function () {
            expect(me.getPrescriptionPatientView()).toBeDefined();
        });

        it('can be rendered', function () {
            expect(me.createWindow()).toBeDefined();
        });

        it('contains a weight component', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientWeight]")[0]
            expect(component).toBeDefined();
        });

        it('contains a height component', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientHeight]")[0]
            expect(component).toBeDefined();
        });

        it('contains a bsa component', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientBSA]")[0]
            expect(component).toBeDefined();
        });

        it('patient weight should have unit kg', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientWeight]")[0]
            expect(component.getValue().unit).toBe("kg");
        });

        it('patient weight should be 0', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientWeight]")[0]
            expect(component.getValue().value).toBe(0);
        });

        it('patient height should have unit cm', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientHeight]")[0]
            expect(component.getValue().unit).toBe("cm");
        });

        it('patient height should be 0', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientHeight]")[0]
            expect(component.getValue().value).toBe(0);
        });

        it('patient BSA should have unit m2', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientBSA]")[0]
            expect(component.getValue().unit).toBe("m2");
        });

        it('patient BSA should be 0', function () {
            var component = Ext.ComponentQuery.query("window prescriptionpatient unitvaluefield[name=patientBSA]")[0]
            expect(component.getValue().value).toBe(0);
        });
        
        it('View can be destroyed', function () {
            me.getPrescriptionPatientView().destroy();
            expect(me.getPrescriptionPatientView().isDestroyed).toBeTruthy();
            win.close();
        });
    }
});