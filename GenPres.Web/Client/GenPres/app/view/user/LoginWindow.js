Ext.define('GenPres.view.user.LoginWindow', {
    extend: 'Ext.Window',
    alias: 'widget.userlogin',

    bodyPadding: 5,
    closable: false,

    requires : ['GenPres.session.PatientSession', 'GenPres.store.PrescriptionStores'],

    title: 'GenPres Login',
    defaultDatabase: 'Default Database',

    initComponent: function() {
        var me = this;
        me.dockedItems = me.createDockedItems();
        me.items = this.createItems();
        me.callParent(arguments);
    },

    getLoginButton: function () {
        return Ext.ComponentQuery.query('toolbar button[action=login]');
    },

    createDockedItems: function () {
        return [
            {
                xtype: 'toolbar',
                dock: 'bottom',
                items: ['->', { text: 'Login', action: 'login'}]
            }
        ];
    },

    createItems: function () {
        var me = this;
        
        return [
            me.getHtmlImage(),
            me.getLoginForm2()
        ];
    },

    getImagePath: function () {
        return GenPres.application.appFolder + "/style/images/medicalbanner.jpg";
    },

    getHtmlImage: function () {
        var me = this, imagePath = me.getImagePath();
        return { html: '<img src=' + imagePath + ' />', height: 180, xtype: 'box' }
    },


    getLoginForm2: function () {
        var me = this;
        //noinspection JSUnusedGlobalSymbols
        return {
            xtype:'form',
            border: false,
            bodyPadding: 15,
            width: 541,
            defaults: {
                allowBlank: false
            },
            items:[
                { xtype: 'textfield', fieldLabel: 'Gebruikersnaam2', name:'username', margin: '10 0 10 10', value: 'peter' },
                { xtype: 'textfield', inputType: 'password', fieldLabel: 'Wachtwoord', name: 'password', margin: '0 0 10 10',  value: 'Secret' },
                Ext.create('GenPres.view.user.LogicalUnitSelector',{name:'loginLogicalUnitSelector'}),
                me.advancedLoginFieldSet()

            ]
        };
    },

    advancedLoginFieldSet: function () {
        var me = this;
        return {
            xtype: 'fieldset',
            layout: 'hbox',
            collapsible: true,
            collapsed: true,
            margin:'65 0 0 0',
            items: [
                me.createDatabaseCombo(),
                me.createRegisterDatabaseButton()
            ]
        };
    },

    createDatabaseCombo: function () {
        var me = this;
        return {xtype: 'combo', name: 'database', fieldLabel: 'Database', displayField: 'DatabaseName', store: me.getDatabaseStore()};
    },

    getDatabaseStore: function () {
        return Ext.create('GenPres.store.database.Database');
    },

    createRegisterDatabaseButton: function () {
        return {xtype: 'button', text: 'Registreer Database', action: 'registerdatabase'};
    }

});