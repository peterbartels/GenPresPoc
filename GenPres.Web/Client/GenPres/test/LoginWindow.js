describe("LoginWindow", function() {
    var loginWindow;

    var GetLoginWindow = function(){
        if(typeof(loginWindow) == "undefined"){
            loginWindow = Ext.create('GenPres.view.user.LoginWindow');
            loginWindow.show();
        }
        //return loginWindow;
        return loginWindow;
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
