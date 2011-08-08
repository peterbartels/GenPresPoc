Ext.define('GenPres.test.view.DrugCompositionTest', {

    describe: 'GenPres.view.Prescription.DrugComposition',

    tests: function () {
        var me = this, instance;

        me.getPrescriptionView = function (config) {
            if (!instance) {
                instance = Ext.create('GenPres.view.prescription.DrugComposition', config)
            }
            return instance;
        };

        me.createFormWindow = function(){
            var win = Ext.create('Ext.Window', {
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
    }
});