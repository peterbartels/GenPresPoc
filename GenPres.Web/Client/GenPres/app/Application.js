Ext.require('Ext.form.*');

var GenPresApplication;

Ext.application ({
    name: 'GenPres',

    autoCreateViewport: false,
    appFolder: './Client/GenPres/app',

    controllers: [
        'user.LoginController',
        'patient.PatientController'
    ],
    
    launch: function() {
        var loginWindow;
        
        this.items = [];

        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });
            
        loginWindow = Ext.create('GenPres.view.user.LoginWindow');
        loginWindow.show();

        GenPresApplication = this;
        this.items.push(loginWindow);
    }
});

