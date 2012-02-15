describe("MainScreen", function() {
    var mainView;

    GenPres.session.PatientSession.setLogicalUnit(1, "Pelikaan");

    var GetViewport = function(){
        return GenPres.application.viewport;
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

    var GetTree = function(){
        var query = GetWestRegion().query('treepanel');
        return query[0];
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
        expect(GetTree().$className).toBe("GenPres.view.main.PatientTree");
    });

});