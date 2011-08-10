Ext.define('GenPres.test.view.PrescriptionVisibilityTest', {

    describe: 'GenPres.view.Prescription.DrugComposition',

    tests: function () {
        var me = this, instance, win, queryUtil = GenPres.test.util.Query;;

        me.getPrescriptionView = function (config) {
            if (!instance) {
                instance = Ext.create('GenPres.view.prescription.PrescriptionForm', config)
            }
            return instance;
        };

        me.createFormWindow = function(){
            win = Ext.create('Ext.Window', {
                items:me.getPrescriptionView(),
                height:520,
                width:900
            });
            win.show();
            return win;
        };

        it('can create a prescription form', function () {
            me.createFormWindow();
            expect(instance).toBeDefined();
        });

        it('has set substanceQuantity hidden', function () {
            expect(instance).toBeDefined();
        });

        it('new prescriptionform should hide unnecessary fields', function () {
            var substanceQuantity = GenPres.test.util.Query.GetControl('substanceQuantity', me.getPrescriptionView());
            expect(substanceQuantity.isHidden).toBeTruthy();
        });

        it('when generic, route and shape is selected substance quantity should be visible', function () {
            waitsFor(function () {

                var genericCombo = queryUtil.GetControl('drugGeneric');
                var routeCombo = queryUtil.GetControl('drugRoute');
                var shapeCombo = queryUtil.GetControl('drugShape');

                if(queryUtil.controlStoreIsLoaded(shapeCombo)){
                    var substanceQuantity = queryUtil.GetControl('substanceQuantity', me.getPrescriptionView());
                    queryUtil.SelectFirstComboboxValue('drugGeneric');
                    queryUtil.SelectFirstComboboxValue('drugRoute');
                    queryUtil.SelectFirstComboboxValue('drugShape');
                    return (!substanceQuantity.isHidden);
                }
                return false;
            }, 'comboboxes to be rendered', 2000);
        });

    }
});