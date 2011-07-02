describe("PatientTree", function () {
    var mainView;
    var tree;

    GenPres.session.PatientSession.setLogicalUnit(1, "Pelikaan");

    var GetMainView = function () {
        if (typeof (mainView) == "undefined") {
            mainView = Ext.create('GenPres.view.main.MainView');
        }
        return mainView;
    };

    var GetWestRegion = function () {
        return GetMainView().items.findBy(function (obj) {
            if (obj.region == 'west') return obj;
        });
    };

    var GetTree = function () {
        var query = GetWestRegion().query('treepanel');
        return query[0];
    };
    
    it('PatientTree should be loaded with items', function () {
        var tree = GetTree();
        waitsFor(function () {
            var rootnode = tree.store.getRootNode();
            return rootnode.childNodes.length > 2;
        }, 'waiting for childnodes to be filled', 3000);
    });
});