
GenPres = {};

Ext.define('GenPres', {

    _viewManager: {},

    Init: function() {
        _viewManager = new GenPres.ViewManager();
        _viewManager.LoadView("GenPres.Views.Viewport");
    }
});

Ext.onReady(function() {
    var genPres = new GenPres();
    genPres.Init();
});