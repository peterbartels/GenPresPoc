Ext.define('GenPres.test.extjs.LoaderTests', {
    describe: 'Ext.Loader',

    tests: function () {
        var me = this;

        me.createLoginModel = function () {
            return Ext.create('GenPres.model.user.Login');
        };

        me.createValidationRuleModel = function () {
            return Ext.create('GenPres.model.validation.ValidationRule');
        };

        it('should be enabled', function () {
            expect(Ext.Loader.config.enabled).toBe(true);
        });

        it('should have a GenPres path', function () {
            expect(Ext.Loader.config.paths.GenPres).toBeDefined();
        });

        it('GenPres.model.user.LoginModel can be created', function () {
            expect(me.createLoginModel()).toBeDefined();
        });
    }
});