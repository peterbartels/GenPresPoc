describe("Application", function() {

    it('Controller validateLogin should call validateLoginForm', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        spyOn(controller, 'validateLoginForm');
        controller.validateLogin({username:"", password:""});
        expect(controller.validateLoginForm).toHaveBeenCalled();
    });

    it('Controller validateLogin should call a backend login function', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        spyOn(controller, 'loginCallBackFunction');

        GenPres.session.PatientSession.setLogicalUnit(1, "test")

        controller.validateLogin({username:"blah", password:"blah"});

        waitsFor(function () {
            return controller.loginCallBackFunction.wasCalled;
        }, 'waiting for loginCallBackFunction call', 3000);
    });

    it('Controller validateLogin throw an error when no username and password is chosen', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        GenPres.session.PatientSession.setLogicalUnit(1, "test");
        var validated = controller.validateLoginForm({username:"", password:""});
        expect(validated).toBe(false);
    });

    it('Controller validateLogin throw an error when no password is chosen', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        GenPres.session.PatientSession.setLogicalUnit(1, "test");
        var validated = controller.validateLoginForm({username:"test", password:""});
        expect(validated).toBe(false);
    });

    it('Controller validateLogin throw an error when no username is chosen', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        GenPres.session.PatientSession.setLogicalUnit(1, "test");
        var validated = controller.validateLoginForm({username:"", password:"test"});
        expect(validated).toBe(false);

    });

    it('Controller validateLogin throw an error when no logicalUnit is chosen', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        GenPres.session.PatientSession.setLogicalUnit(0, "");
        var validated = controller.validateLoginForm({username:"test", password:"test"});
        expect(validated).toBe(false);
    });
});


