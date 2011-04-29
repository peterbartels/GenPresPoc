

Ext.define('GenPres.controller.user.LoginController', {
    extend: 'Ext.app.Controller',

    loggedIn: false,
    loginWindow: null,

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
        var win, form, record, vals;

        win = button.up('window');
        this.loginWindow = win;
        form = win.down('form');
        record = form.getRecord();
        vals = form.getValues();

        User.Login(vals.username, vals.password, this.loginCallBackFunction, this);
    },

    loginCallBackFunction: function(result) {
        this.loggedIn = result.success;

        if (result.success) {
            Ext.MessageBox.alert('GenPres 2011 Login', 'Login succesvol', this.closeLoginWindow, this);
        } else {
            Ext.MessageBox.alert('GenPres 2011 Login', 'Login geweigerd');
        }
    },

    closeLoginWindow: function() {
        this.loginWindow.close();
    }

});