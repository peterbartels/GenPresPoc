var GenPresApplication;

Ext.app.config = {
    name: 'GenPres',

    autoCreateViewport: false,

    controllers: [
        'user.LoginController',
        'patient.PatientController',
        'prescription.PrescriptionController',
        'prescription.DrugComposition'
    ],

    launch: function () {
        var loginWindow;

        this.items = [];

        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });

        loginWindow = Ext.create('GenPres.view.user.LoginWindow');

        if (typeof (window.dontLoadApplication) == "undefined") {
            loginWindow.show();
        }


        GenPresApplication = this;
    }
};

