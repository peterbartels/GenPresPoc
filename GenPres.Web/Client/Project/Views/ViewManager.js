
Ext.define('GenPres.ViewManager', {
    views: [],

    constructor: function() {
        var me = this;
    },

    LoadView: function(view) {
        Ext.create(view);
    }
});
    
