describe("MainScreen", function() {
    var mainView;
    var tree;

    var GetViewport = function(){
        return GenPresApplication.viewport;
    };

    var GetMainView = function(){
        if(typeof(mainView) == "undefined"){
            mainView = Ext.create('GenPres.view.main.MainView');
        }
        return mainView;
    };

    var GetWestRegion = function(){
        return GetMainView().items.findBy(function(obj){
            if(obj.region == 'west') return obj;
        });
    };

    var GetCenterRegion = function(){
        return GetMainView().items.findBy(function(obj){
            if(obj.region == 'center') return obj;
        });
    };

    it('After loading MainView, viewport should have a border layout', function () {
        expect(GetViewport().items.length > 0);
        expect(GetMainView().$className).toEqual('GenPres.view.main.MainView');
    });

    it('MainScreen borderlayout should have a west and center region', function () {
        expect(GetWestRegion()).toBeDefined();
        expect(GetCenterRegion()).toBeDefined();
        expect(GetWestRegion()).toNotBe(null);
        expect(GetCenterRegion()).toNotBe(null);
    });

    it('After rendering MainView, west borderlayout should have a patienttree', function () {
        var query = GetWestRegion().query('treeview');
        expect(query.length).toBe(1);
        tree = query[0];
        expect(tree.$className).toBe("Ext.tree.View");
    });

    it('PatientTree should be loaded with items', function () {
        tree.store.on("load", function(){
            alert('f');
        })
        
        expect(tree.store.count() > 0).toBeTruthy();
    });
});