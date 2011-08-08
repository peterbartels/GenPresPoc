Ext.define('GenPres.test.view.PrescriptionFormTest', {

    describe: 'GenPres.view.Prescription.PrescriptionForm',

    tests: function () {
        var me = this, instance;

        me.getPrescriptionView = function (config) {
            if (!instance) {
                instance = Ext.create('GenPres.view.prescription.PrescriptionForm', config)
            }
            return instance;
        };

        me.createFormWindow = function(){
            var win = Ext.create('Ext.Window', {
                items:me.getPrescriptionView(),
                height:520,
                width:900
            });
            win.show();
            return win;
        };

        it('can be created', function () {
            expect(me.getPrescriptionView()).toBeDefined();
        });

        it('can be rendered', function () {
            expect(me.createFormWindow()).toBeDefined();
        });

        it('contains a drugcomposition view', function () {
            var form = Ext.ComponentQuery.query('window prescriptionform')[0];
            var drugCompositionView = form.query("drugcomposition")[0];
            expect(drugCompositionView).toBeDefined();
        });
    }
});