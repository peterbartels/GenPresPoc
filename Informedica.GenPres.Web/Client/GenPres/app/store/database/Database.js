Ext.define('GenPres.store.database.Database', {
    extend: 'Ext.data.Store',
    alias: 'widget.databasestore',
    storeId: 'databasestore',
    // This requires is necessary when Ext.Loader is enabled
    requires: ['GenPres.model.database.Database'],

    model: 'GenPres.model.database.Database',
    autoLoad: true,

    proxy: {
        type: 'direct',
        directFn: Database.GetDatabases
    }
});