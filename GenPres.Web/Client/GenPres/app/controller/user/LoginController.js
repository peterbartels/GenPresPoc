

Ext.define('GenPres.controller.user.LoginController', {
    extend: 'Ext.app.Controller',

    loggedIn: false,
    loginWindow: null,

    logicalUnitId : 0,

    init: function() {
        this.control({
            'toolbar button[action=login]': {
                click: this.validateLogin
            },
            'dataview' : {
                itemclick : function(view, record, item, index, event){
                    this.logicalUnitId = record.data.id
                }
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
            this.closeLoginWindow();
        } else {
            Ext.MessageBox.alert('GenPres 2011 Login', 'Login geweigerd');
        }
    },

    closeLoginWindow: function() {
        this.loginWindow.close();
        Ext.create('GenPres.view.main.MainView', {logicalUnitId:this.logicalUnitId});
    }
});