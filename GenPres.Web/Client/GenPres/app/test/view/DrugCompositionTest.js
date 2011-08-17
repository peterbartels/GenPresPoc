Ext.define('GenPres.test.view.DrugCompositionTest', {

    describe: 'GenPres.view.Prescription.DrugComposition',

    tests: function () {
        var me = this, instance, win, queryUtil = GenPres.test.util.Query;

        me.getPrescriptionView = function (config) {
            if (!instance) {
                instance = Ext.create('GenPres.view.prescription.DrugComposition', config)
            }
            return instance;
        };

        me.createFormWindow = function(){
            win = Ext.create('Ext.Window', {
                items:me.getPrescriptionView(),
                height:500,
                width:1000
            });
            win.show();
            return win;
        }

        me.getSelect = function(name){
            return Ext.ComponentQuery.query('window combobox[action='+name+']')[0];
        }

        me.getUnitValueField = function(name){
            return Ext.ComponentQuery.query('window unitvaluefield[name='+name+']')[0];
        }

        me.hasSaveCancelToolbar = function (window) {
            return Ext.ComponentQuery.query('window[title=' + window.title + '] toolbar')[0];
        };

        it('can be created', function () {
            expect(me.getPrescriptionView()).toBeDefined();
        });

        it('can be rendered', function () {
            expect(me.createFormWindow()).toBeDefined();
        });

        it('contains a generic field', function () {
            expect(me.getSelect("generic")).toBeDefined();
        });

        it('contains a route field', function () {
            expect(me.getSelect("route")).toBeDefined();
        });

        it('contains a shape field', function () {
            expect(me.getSelect("shape")).toBeDefined();
        });

        it('contains a substance quantity field', function () {
            expect(me.getUnitValueField("substanceQuantity")).toBeDefined();
        });

        it('Substance quantity field has a unit combobox', function () {
            expect(me.getUnitValueField("substanceQuantity").getUnitCombo()).toBeDefined();
        });

        it('When nothing is chosen drugIsChosen returns false ', function () {
            var drugCompController = GenPres.application.getController('prescription.DrugComposition')
            expect(!drugCompController.drugIsChosen()).toBeTruthy();
        });

        it('when generic, route and shape is chosen drugIsChosen returns true', function () {
            waitsFor(function () {

                var genericCombo = queryUtil.GetControl('drugGeneric');
                var routeCombo = queryUtil.GetControl('drugRoute');
                var shapeCombo = queryUtil.GetControl('drugShape');

                if(queryUtil.controlStoreIsLoaded(shapeCombo)){
                    var substanceQuantity = queryUtil.GetControl('substanceQuantity', me.getPrescriptionView());
                    queryUtil.SelectFirstComboboxValue('drugGeneric');
                    queryUtil.SelectFirstComboboxValue('drugRoute');
                    queryUtil.SelectFirstComboboxValue('drugShape');

                    var drugCompController = GenPres.application.getController('prescription.DrugComposition')
                    return (drugCompController.drugIsChosen());
                }
                return false;
            }, 'comboboxes to be rendered', 2000);
        });

        it('View can be destroyed', function () {
            me.getPrescriptionView().destroy();
            expect(me.getPrescriptionView().isDestroyed).toBeTruthy();
            win.close();
        });
    }
});