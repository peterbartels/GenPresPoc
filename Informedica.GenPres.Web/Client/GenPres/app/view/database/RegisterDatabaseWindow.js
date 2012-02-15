Ext.define('GenPres.view.database.RegisterDatabaseWindow', {
    extend: 'GenPres.lib.view.window.SaveCancelWindow',
    title: 'Registreer Database',

    layout: 'fit',

    width: 800,
    heigth: 300,

    initComponent: function () {
        var me = this;

        me.items = me.getFormItem();
        me.callParent();
    },

    getFormItem: function () {
        var me = this,
            defaults = {
                width: 700,
                labelWidth: 120,
                labelAlign: 'left'
            };
        return { xtype: 'form', bodyPadding: 10, fieldDefaults: defaults, items: me.getFormItems()}
    },

    getFormItems: function () {
        return [
            { xtype: 'textfield', name: 'databasename', fieldLabel: 'Database Naam'},
            { xtype: 'textfield', name: 'machinename', fieldLabel: 'Machine Naam'},
            { xtype: 'textfield', name: 'genpresconnectionstring', fieldLabel: 'GenPres Connection String'},
            { xtype: 'textfield', name: 'patientdbconnectionstring', fieldLabel: 'PDMS Connection String'},
            { xtype: 'textfield', name: 'genformwebservice', fieldLabel: 'GenForm Webservice'}
        ]
    },

    getDatabaseName: function () {
        var me = this;
        return me.getDatabaseNameField().value;
    },

    getDatabaseNameField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[0];
    },

    getMachineName: function () {
        var me = this;
        return me.getMachineNameField().value;
    },

    getMachineNameField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[1];
    },

    getGenPresConnectionString: function () {
        var me = this;
        return me.getGenPresConnectionStringField().value;
    },

    getGenPresConnectionStringField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[2];
    },

    getPDMSConnectionString: function () {
        var me = this;
        return me.getPDMSConnectionStringField().value;
    },

    getPDMSConnectionStringField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[3];
    },

    getGenFormWebservice: function () {
        var me = this;
        return me.getGenFormWebserviceField().value;
    },

    getGenFormWebserviceField: function () {
        var me = this;
        return me.getDatabaseRegistrationForm().items.items[4];
    },

    getDatabaseRegistrationForm: function () {
        var me = this;
        return me.items.items[0];
    }

});