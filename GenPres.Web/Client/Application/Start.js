

Ext.define('GenPres.controller.Login', {
    init: function() {
        console.log('Initialized Users! This happens before the Application launch function is called');
    },
    onLaunch: function() {
        
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
        items: [,'->',{text:'Login'}
        ]
    }],
    items: [
        { html: '<img src="Client/Application/Images/MedicalBanner.jpg" />', height: 180, xtype: 'box' },
        { xtype: 'panel', border: false, bodyPadding: 15, width:541, items: [
            new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', margin: '10 0 10 10' }),
            new Ext.form.Text({ fieldLabel: 'Wachtwoord', margin: '0 0 10 10' })
        ]}
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

