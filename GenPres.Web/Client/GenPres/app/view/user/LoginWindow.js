Ext.define('GenPres.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,

    width: 555,
    height: 350,

     mixins: {
        process: 'GenPres.util.Process'
    },
    
    constructor : function(config){
        var me = this;
        me.mixins.process.constructor.call(me);
        me.callParent(arguments);
    },

    initComponent: function() {
        this.callParent(arguments);
    },

    afterRender : function(){
       this.callParent(arguments);
       this.doProcess('Login');
    },

    dockedItems: [{ 
        xtype: 'toolbar',
        dock: 'bottom',
        items: ['->', { text: 'Login', action: 'login'}]
    }],

    items: [
        { html: '<img src="Client/Application/Images/MedicalBanner.jpg" />', height: 180, xtype: 'box' },
        { xtype: 'panel', border: false, bodyPadding: 15, width: 541,
            items: [
                { xtype: 'form', items: [
                    new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', name: 'username', id:'username', margin: '10 0 10 10' }),
                    new Ext.form.Text({ fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10' })
                ]}
            ]
        }
    ],

    Processes : {
        'Login' : [
            {component:'form', timeout:1200, text:'Use this form to login.'}
        ]
    }
});