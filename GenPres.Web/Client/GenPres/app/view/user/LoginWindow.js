Ext.define('GenPres.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,
    
    width: 560,
    height: 400,

    requires : 'GenPres.session.PatientSession',
    
    mixins: {
        process: 'GenPres.util.Process'
    },
    
    constructor : function(config){
        var me = this;
        me.mixins.process.constructor.call(me);
        me.callParent(arguments);
    },

    initComponent: function() {
        this.items = [
            { html: '<img src="Client/Application/Images/MedicalBanner.jpg" />', height: 180, xtype: 'box'},
            { xtype: 'panel', border: false, bodyPadding: 12, width: 542,
                items: [
                    { xtype: 'form', border:false, items: [
                        new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', name: 'username', id:'username', margin: '10 0 10 10', value:'test' }),
                        new Ext.form.Text({ fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10', value:'Test' })
                    ]}
                ]
            },
            Ext.create('GenPres.view.user.LogicalUnitSelector')
        ];


        this.callParent(arguments);
    },

    afterRender : function(){
       this.callParent(arguments);
       //this.doProcess('Login');
    },

    dockedItems: [{ 
        xtype: 'toolbar',
        dock: 'bottom',
        items: ['->', { text: 'Login', action: 'login'}]
    }],


    Processes : {
        'Login' : [
            {component:'form', text:'Dit formulier kan gebruikt worden om in te loggen.'},
            {component:'form textfield[name=username]', text:'Vul uw gebruikersnaam in.'},
            {component:'form textfield[name=password]', text:'Vul uw wachtwoord in.'},
            {component:'toolbar button[action=login]', text:'Klik op de login button om de applicatie te starten.'}
        ]
    }
});