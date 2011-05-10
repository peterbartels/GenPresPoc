
describe("Application", function() {

    it('GenPresApplication should exist', function () {
        expect(GenPresApplication).toBeDefined();
    })

    it('Application should have GenPres config', function () {
        expect(Ext.Loader.config.paths.GenPres).toBeDefined();
        expect(GenPresApplication.name).toEqual('GenPres');
    })

    it('Application should have GenPres path', function () {
        expect(Ext.Loader.config.paths.GenPres).toEqual('./Client/GenPres/app');
    })

    it('Application should have a viewport', function () {
        expect(GenPresApplication.viewport).toBeDefined();
    })

    it('Application should have one item', function () {
        expect(GenPresApplication.items.length).toEqual(1);
    })

    it('Application should have a login window of type GenPres.view.user.LoginWindow', function () {
        expect(GenPresApplication.items[0].$className).toEqual('GenPres.view.user.LoginWindow');
    })
});



describe("LoginWindow", function() {
    var loginWindow;
    
    var GetLoginWindow = function(){
        if(typeof(loginWindow) == "undefined"){
            loginWindow = Ext.create('GenPres.view.main.MainView');
        }
        //return loginWindow;
        return GenPresApplication.items[0];
    }; 

    it('Login window should be an Ext.Window', function () {
        expect(GetLoginWindow().superclass.$className).toEqual('Ext.window.Window');
    });

    it('Login window should have a username field', function () {
        expect(GetLoginWindow().query('form textfield[name=username]').length).toBe(1);
    });

    it('Login window should have a password field', function () {
        expect(GetLoginWindow().query('form textfield[name=password]').length).toBe(1);
    });

    it('Login window should have a login button', function () {
        expect(GetLoginWindow().query('toolbar button[action=login]').length).toBe(1);
    });
});


describe("MainScreen", function() {
    var mainView;

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

    it('After rendering MainView, west borderlayout should have a patientview', function () {
        
    });
});
