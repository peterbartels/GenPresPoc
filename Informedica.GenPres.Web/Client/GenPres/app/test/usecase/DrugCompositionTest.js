


Ext.define('GenPres.test.usecase.DrugCompositionTest', {
    describe: 'DrugComposition tests that',

    tests: function () {
        var me = this;
        me.getDrugCompositionController = function () {
            return GenPres.application.getController('prescription.DrugComposition');
        };

        me.getPrescriptionController = function () {
            return GenPres.application.getController('prescription.PrescriptionController');
        };
        me.getComboBox = function(action){
            return  Ext.ComponentQuery.query('combobox[action='+action+']')[0];
        }
        it('There should be a prescription controller', function () {
            expect(me.getPrescriptionController()).toBeDefined();
        });

        it('There should be a drug composition controller', function () {
            expect(me.getDrugCompositionController()).toBeDefined();
        });

        it('There should be a generic combobox', function () {
            expect(me.getComboBox('generic').inputEl.dom).toBeDefined();
        });

        it('There should be a route combobox', function () {
            expect(me.getComboBox('route').inputEl.dom).toBeDefined();
        });


        me.comboIsSet = function(component, globalvarname){
            if(component.store.getCount() > 0 && !component.store.loading){
                component.select(component.store.getAt(0).data.Value);
                globalvars[globalvarname] = (component.getValue() != "");
            }
        }

        it('There should be a shape combobox', function () {
            expect(me.getComboBox('shape').inputEl.dom).toBeDefined();
        });
        
        it('A generic can be set', function () {
            var component = me.getComboBox('generic');
            globalvars["checkGenericSet"]=false;
            waitsFor(createBindFunction(me.comboIsSet, me, [component, "checkGenericSet"], "checkGenericSet", 200), 'waiting for generic to be selected', 3000);
        });

        it('A route can be set', function () {
            var component = me.getComboBox('route');
            globalvars["checkRouteSet"]=false;
            waitsFor(createBindFunction(me.comboIsSet, me, [component, "checkRouteSet"], "checkRouteSet", 200), 'waiting for route to be selected', 3000);
        });

        it('A shape can be set', function () {
            component = me.getComboBox('shape');
            globalvars["checkShapeSet"]=false;
            waitsFor(createBindFunction(me.comboIsSet, me, [component, "checkShapeSet"], "checkShapeSet", 500), 'waiting for shape to be selected', 3000);
        });
        
        //prescriptionTest = Ext.create('GenPres.test.usecase.PrescriptionTest');
        //describe(prescriptionTest.describe, prescriptionTest.tests);
        //jasmine.getEnv().execute();
    }
});