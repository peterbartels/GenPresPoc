Ext.define('GenPres.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,

    width: 560,
    height: 400,

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
       //this.doProcess('Login');
    },

    dockedItems: [{ 
        xtype: 'toolbar',
        dock: 'bottom',
        items: ['->', { text: 'Login', action: 'login'}]
    }],

    items: [
        { html: '<img src="Client/Application/Images/MedicalBanner.jpg" />', height: 180, xtype: 'box' },
        { xtype: 'panel', border: false, bodyPadding: 12, width: 542,
            items: [
                { xtype: 'form', items: [
                    new Ext.form.Text({ fieldLabel: 'Gebruikersnaam', name: 'username', id:'username', margin: '10 0 10 10', value:'test' }),
                    new Ext.form.Text({ fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10', value:'Test' })
                ]}
            ]
        },
        Ext.create('Ext.view.View', {
            margin:'20 0 0 16',
            itemSelector: 'div.thumb-wrap',
            emptyText: 'No images available',
            store: Ext.create('GenPres.store.patient.LogicalUnitStore'),
            tpl: new Ext.XTemplate(
                    '<tpl for=".">',
                        '<div class="thumb-wrap">',
                            '{text}',
                        '</div>',
                    '</tpl>')
        })
    ],

    Processes : {
        'Login' : [
            {component:'form', text:'Dit formulier kan gebruikt worden om in te loggen.'},
            {component:'form textfield[name=username]', text:'Vul uw gebruikersnaam in.'},
            {component:'form textfield[name=password]', text:'Vul uw wachtwoord in.'},
            {component:'toolbar button[action=login]', text:'Klik op de login button om de applicatie te starten.'}
        ]
    }
});