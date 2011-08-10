Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true
});

Ext.require([
    'Ext.direct.*',
    'Ext.container.Viewport',
    'Ext.grid.plugin.RowEditing',
    'Ext.form.FieldSet',
    'Ext.form.field.ComboBox',
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



    Ext.app.config.appFolder = 'Client/GenPres/app';
    //noinspection JSUnusedGlobalSymbols
    Ext.app.config.launch = function() {
        var me = this, test,
            testList = Ext.create('GenPres.test.TestList'),
            testLoader = Ext.create('GenPres.test.TestLoader');

        GenPres.application = me;

        me.setDefaults();

        //var query = Ext.create('GenPres.test.util.Query');

        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });
        
        testLoader.loadTests(testList);

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
