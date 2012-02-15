Ext.require('Ext.form.*');

var GenPres.application;

Ext.application ({
    name: 'GenPres',

    autoCreateViewport: false,
    appFolder: './Client/GenPres/app',

    controllers: [
        'user.LoginController',
        'patient.Patient',
        'prescription.PrescriptionController',
        'prescription.DrugComposition'
    ],
    
    launch: function() {
        var loginWindow;
        
        this.items = [];

        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });
            
        loginWindow = Ext.create('GenPres.view.user.LoginWindow');

        if(typeof(window.dontLoadApplication) == "undefined"){
            loginWindow.show();
        }


        GenPres.application= this;
    }
});

