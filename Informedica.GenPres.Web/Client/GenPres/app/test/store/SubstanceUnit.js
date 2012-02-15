Ext.define('GenPres.test.store.SubstanceUnit', {

    describe: 'GenPres.store.prescription.SubstanceUnit',

    tests: function () {
        var me = this;

        me.createSubstanceUnitStore = function () {
            return Ext.create('GenPres.store.prescription.SubstanceUnit');
        };

        me.getSubstanceUnitStore = function () {
            return Ext.getStore('substanceunit');
        };

        it('GenPres.store.prescription.SubstanceUnit can be created', function () {
            expect(me.createSubstanceUnitStore()).toBeDefined();
        });

        it('substanceunit alias can be called', function () {
            expect(me.getSubstanceUnitStore()).toBeDefined();
        });

        it('GenPres.store.prescription.SubstanceUnit contains extraparam generic', function () {
            expect(me.getSubstanceUnitStore().proxy.extraParams.generic).toBeDefined();
        });
        it('GenPres.store.prescription.SubstanceUnit contains extraparam route', function () {
            expect(me.getSubstanceUnitStore().proxy.extraParams.route).toBeDefined();
        });
        it('GenPres.store.prescription.SubstanceUnit contains extraparam shape', function () {
            expect(me.getSubstanceUnitStore().proxy.extraParams.shape).toBeDefined();
        });
        
    }
});