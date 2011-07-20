Ext.app.config = {
    name: 'GenPres',

    'autoCreateViewport': false,

    controllers: [
        'user.Login',
        'patient.Patient',
        'prescription.PrescriptionController',
        'prescription.DrugComposition'
    ],

    launch: function() {
        var me = this;
        GenPres.application = me;

        this.viewport = Ext.create('Ext.container.Viewport', {
            layout: 'fit'
        });
        me.showLoginWindow();
    },

    showLoginWindow: function () {
        var me = this, window;
        window = me.getLoginWindow().show();
        me.getController('user.Login').setDefaultDatabase(window);
    },

    getLoginWindow: function () {
        var me = this;
        return me.getController('user.Login').getLoginWindow();
    }
};