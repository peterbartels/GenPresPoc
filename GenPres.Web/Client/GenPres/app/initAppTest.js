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

globalvars = {};

var createBindFunction = function(func, scope, args, gobalvarname, waitcount){
    //var bindFunc = Ext.Function.bind(func, scope, args);
    var fn = Ext.Function.bind(func, scope, args);
    var deferFunc = function(){
        if (waitcount > 0) {
            setTimeout(fn, waitcount);
        }else{
            fn();
        }
        return globalvars[gobalvarname];
    }
    return deferFunc;
}

Ext.onReady(function () {
    var loginTest, advancedLoginTest, selectPatientTest, drugCompositionTest, prescriptionTest;

    Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

    Ext.app.config.appFolder = '../Client/GenPres/app';
    Ext.app.config.launch = function() {
        var me = this;
        GenPres.application = me;


        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });
        
        me.showLoginWindow();

        advancedLoginTest = Ext.create('GenPres.test.usecase.AdvancedLoginTest');
        describe(advancedLoginTest.describe, advancedLoginTest.tests);

        loginTest = Ext.create('GenPres.test.usecase.LoginTest');
        describe(loginTest.describe, loginTest.tests);

        selectPatientTest = Ext.create('GenPres.test.usecase.SelectPatientTest');
        describe(selectPatientTest.describe, selectPatientTest.tests);

        drugCompositionTest = Ext.create('GenPres.test.usecase.DrugCompositionTest');
        describe(drugCompositionTest.describe, drugCompositionTest.tests);

        prescriptionTest = Ext.create('GenPres.test.usecase.PrescriptionTest');
        describe(prescriptionTest.describe, prescriptionTest.tests);


        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
