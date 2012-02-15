Ext.define('GenPres.model.user.Login',  {
    extend: 'Ext.data.Model',
    // This requires is necessary if Ext.Loader is enabled,
    // otherwise this model is not defined

    fields: [
        { name: 'username' , type: 'string' },
        { name: 'password', type: 'string'}
    ],

    proxy: {
        type: 'direct',
        directFn: User.Login
    }
});