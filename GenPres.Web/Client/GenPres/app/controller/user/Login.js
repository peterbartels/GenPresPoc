Ext.define('GenPres.controller.user.Login', {
    extend: 'Ext.app.Controller',
    alias: 'widget.logincontroller',

    views: [
        'user.LoginWindow',
        'database.RegisterDatabaseWindow'
    ],

    loggedIn: false,
    loginWindow: null,

    init: function() {
        var me = this;

        this.control({
            'toolbar button[action=login]': {
                click: me.onClickValidateLogin
            },
            'button[action=registerdatabase]': {
                click: me.showRegisterDatabaseWindow
            },
            'window[title="Registreer Database"] button[action=save]': {
                click: me.onClickSaveDatabaseRegistration
            },
            'dataview' : {
                itemclick : function(view, record, item, index, event){
                    GenPres.session.PatientSession.setLogicalUnit(
                        record.data.id,
                        record.data.text
                    )
                }
            }
        });
    },

    getLoginWindow: function () {
        var me = this, window;
        window = me.getUserLoginWindowView().create();
        return window;
    },

    setDefaultDatabase: function (window) {
        var combo, queryHelper = Ext.create('GenPres.lib.util.QueryHelper');
        combo = Ext.ComponentQuery.query('window[title=' + window.title + '] combobox[name=database]')[0];
        var fieldset = combo.up('fieldset');
        fieldset.expand();
        queryHelper.setFormField(combo, 'Default Database');
        fieldset.collapse();
    },

    onClickValidateLogin: function(button) {
        var win, form, vals;
        controller3 = this;
        win = button.up('window');
        this.loginWindow = win;
        form = win.down('form');
        vals = form.getValues();
        this.validateLogin(vals);
    },

    validateLogin: function(vals) {
        if(this.validateLoginForm(vals)){
            User.Login(vals.username, vals.password, this.loginCallBackFunction, this);
        }
    },

    validateLoginForm : function(vals){
        var error = '';

        if(vals.username == ''){
            error += 'Selecteer aub een gebruikersnaam<br />';
        }

        if(vals.password == ''){
            error += 'Selecteer aub een wachtwoord<br />';
        }

        if(
            GenPres.session.PatientSession.getLogicalUnitId() == ''
            ||
            GenPres.session.PatientSession.getLogicalUnitName() == ''
        ){
            error += 'Selecteer aub een afdeling\n';
        }
        if(error != ''){
            Ext.MessageBox.alert('GenPres 2011 Login Error', error);
        }

        return error == '';
    },

    loginCallBackFunction: function (result) {
        var me = this;
        me.loggedIn = result.success;

        if (result.success) {
            Ext.MessageBox.alert('GenPres 2011 Login', 'Login succesvol', me.closeLoginWindow, me);7
            Ext.create('GenPres.view.main.MainView', {logicalUnitId:GenPres.session.PatientSession.getLogicalUnitId()});
        }else{
            Ext.MessageBox.alert('GenPres 2011 Login', 'Login geweigerd');
        }
    },

    closeLoginWindow: function () {
        var me = this;
        me.loginWindow.close();
    },

    showRegisterDatabaseWindow: function () {
        var me = this;
        me.createRegisterDatabaseWindow().show();
    },

    createRegisterDatabaseWindow: function () {
        var me = this;
        return me.getDatabaseRegisterDatabaseWindowView().create();
    },

    onClickSaveDatabaseRegistration: function (button) {
        var me = this;
        Database.SaveDatabaseRegistration(me.getWindowFromButton(button).getDatabaseName(),
                                          me.getWindowFromButton(button).getMachineName(),
                                          me.getWindowFromButton(button).getGenPresConnectionString(),
                                          me.getWindowFromButton(button).getPDMSConnectionString(),
                                          me.getWindowFromButton(button).getGenFormWebservice(),
                                          me.onDatabaseRegistrationSaved);
        me.getWindowFromButton(button).close();
    },

    getWindowFromButton: function (button) {
        return button.up().up();
    },

    onDatabaseRegistrationSaved: function (result) {
        var me = this;

        if (result.success) {
            Ext.MessageBox.alert('Database Registration', result.databaseName);
        } else {
            Ext.MessageBox.alert('Database Regstration', 'Database could not be registered');
        }
    }

});