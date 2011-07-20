Ext.define('GenPres.test.store.ProductSubstanceStoreTests', {

    describe: 'GenPres.store.product.ProductSubstance',

    tests: function () {
        var createProductSubstanceStore, getProductSubstanceStore;

        createProductSubstanceStore = function () {
            return Ext.create('GenPres.store.product.ProductSubstance');
        };

        getProductSubstanceStore = function () {
            return Ext.getStore('productsubstancestore');
        };

        it('GenPres.store.product.ProductSubstanceStore should be created', function () {
            expect(createProductSubstanceStore()).toBeDefined();
        });

        it('GenPres.store.product.ProductSubstanceStore should be defined', function () {
            expect(getProductSubstanceStore()).toBeDefined();
        });

    }
});