Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true
});

Ext.require([
    'Ext.direct.*',
    'Ext.container.Viewport',
    'Ext.grid.plugin.RowEditing',
    'Ext.form.FieldSet',
    'Ext.tab.Panel',
    'Ext.form.field.HtmlEditor'
]);

Ext.onReady(function () {
    var classTests, componentQueryTests, loaderTests, modelTests,
        storeTests,
        loginModelTests,
        saveCancelWindowTests, loginControllerTests,
        databaseRegistrationWindowTests;

    Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

    Ext.app.config.appFolder = '../Client/GenPres/app';
    //noinspection JSUnusedGlobalSymbols
    Ext.app.config.launch = function() {
        var me = this;
        GenPres.application = me;

        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });

        classTests = Ext.create('GenPres.test.extjs.ClassTests');
        describe(classTests.describe, classTests.tests);

        componentQueryTests = Ext.create('GenPres.test.extjs.ComponentQueryTests');
        describe(componentQueryTests.describe, componentQueryTests.tests);

        loaderTests = Ext.create('GenPres.test.extjs.LoaderTests');
        describe(loaderTests.describe, loaderTests.tests);
/*
        modelTests = Ext.create('GenPres.test.extjs.ModelTests');
        describe(modelTests.describe, modelTests.tests);

        storeTests = Ext.create('GenPres.test.extjs.StoreTests');
        describe(storeTests.describe, storeTests.tests);
        */

        loginControllerTests = Ext.create('GenPres.test.controller.LoginControllerTests');
        describe(loginControllerTests.describe, loginControllerTests.tests);

        loginModelTests = Ext.create('GenPres.test.model.LoginModelTests');
        describe(loginModelTests.describe, loginModelTests.tests);

        saveCancelWindowTests = Ext.create('GenPres.test.view.SaveCancelWindowTests');
        describe(saveCancelWindowTests.describe, saveCancelWindowTests.tests);

        databaseRegistrationWindowTests = Ext.create('GenPres.test.view.DatabaseRegistrationWindowTests');
        describe(databaseRegistrationWindowTests.describe, databaseRegistrationWindowTests.tests);

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
