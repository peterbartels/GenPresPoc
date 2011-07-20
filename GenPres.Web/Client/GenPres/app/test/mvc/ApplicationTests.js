Ext.define('GenPres.test.mvc.ApplicationTests', {

    describe: 'Application Tests',

    tests: function () {
        var app;

        beforeEach(function () {
           if (!app) app = GenPres.tests;
        });

        it('TestApplication should be defined', function () {
            expect(GenPres.tests).toBeDefined();
        });

        it('TestApplication should have a path to /Client/GenPres/tests/mvc', function () {
            expect(app.appFolder).toBe('../Client/GenPres/tests/mvc');
        });

        it('TestApplication should have a list of controllers', function () {
            expect(app.controllers).toBeDefined();
        });

        it('TestApplication should have a list of models', function () {
           expect(app.models).toBeDefined();
        });

        it('TestApplication can have a sub module that is an application?', function () {
           expect(app.getController('module1.Module1')).toBeDefined();
        });

    }
});