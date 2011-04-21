

Ext.define('GenPres.controller.Login', {
    extend: 'Ext.app.Controller',
    
    init: function() {
        this.control({
            'toolbar button[action=login]': {
                click: this.validateLogin
            }
        });
    },
    
    onLaunch: function() {

    },
    validateLogin: function(button) {
        var win = button.up('window');
        var form = win.down('form');
        var record = form.getRecord();
        var vals = form.getValues();
        
        User.Login(vals.username, vals.password, function(result) {
            debugger;
            if (result.success) {
                Ext.MessageBox.alert('Login succesvol!');
            } else {
                Ext.MessageBox.alert('Login geweigerd!');
            }
        });
    }
});


Ext.define('GenPres.view.user.Login', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,

    
    width: 555,
    height: 350,

    initComponent: function() {
        this.callParent(arguments);
    },
    dockedItems: [{
        xtype: 'toolbar',
        dock: 'bottom',
        items: ['->', { text: 'Login', action: 'login'}]
    }],
    items: [
        { html: '<img src="Client/Application/Images/MedicalBanner.jpg" />', height: 180, xtype: 'box' },
        { xtype: 'panel', border: false, bodyPadding: 15, width:541, 
            items: [
                {xtype:'form', items:[
                    new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', name:'username', margin: '10 0 10 10' }),
                    new Ext.form.Text({ fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10' })
                ]}
            ]
        }
    ]
});

Ext.create('Ext.app.Application', {
    name: 'GenPres',

    autoCreateViewport: false,

    controllers: [
        'Login'
    ],
    views: [
        'user.Login'
    ],

    launch: function() {
        Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });
        var win = new GenPres.view.user.Login();
        win.show();
    }
});

