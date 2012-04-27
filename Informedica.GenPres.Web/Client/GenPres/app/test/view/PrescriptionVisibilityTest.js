Ext.define('GenPres.test.view.PrescriptionVisibilityTest', {

    describe: 'GenPres.view.Prescription.PrescriptionVisibility',

    tests: function () {
        var me = this, instance, win, queryUtil = GenPres.test.util.Query;

        GenPres.application.getController("prescription.PrescriptionController").views =[];

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

        var visibilityItems = ["prescriptionContinuous", "substanceQuantity"];
        for(var i=0;i<visibilityItems.length;i++){
            var visibilitySetHiddenCounter = 0;
            var visibilitySetVisibleCounter = 0;

            it(visibilityItems[i] + ' is default hidden', function () {
                var itemName = visibilityItems[visibilitySetHiddenCounter];
                visibilitySetHiddenCounter++;
                var item = GenPres.test.util.Query.GetControl(itemName, me.getPrescriptionView());
                expect(item.getVisibility()).toBeFalsy();
            });

            it(visibilityItems[i] + ' can be set to visible', function () {
                var itemName = visibilityItems[visibilitySetVisibleCounter];
                visibilitySetVisibleCounter++;
                var item = GenPres.test.util.Query.GetControl(itemName, me.getPrescriptionView());
                item.setVisibility(true);
                expect(item.getVisibility()).toBeTruthy();
            });
        }
/*
        it('when generic, route and shape is selected substance quantity should be visible', function () {

            var genericCombo = queryUtil.GetControl('drugGeneric');
            var routeCombo = queryUtil.GetControl('drugRoute');
            var shapeCombo = queryUtil.GetControl('drugShape');

            genericCombo.store.load();
            routeCombo.store.load();
            shapeCombo.store.load();

            waitsFor(function () {
                if(queryUtil.controlStoreIsLoaded(shapeCombo)){
                    var substanceQuantity = queryUtil.GetControl('substanceQuantity', me.getPrescriptionView());
                    queryUtil.SelectFirstComboboxValue('drugGeneric');
                    queryUtil.SelectFirstComboboxValue('drugRoute');
                    queryUtil.SelectFirstComboboxValue('drugShape');
                    return (!substanceQuantity.isHidden);
                }
                return false;
            }, 'comboboxes to be rendered', 2000);
        });*/
    }
});