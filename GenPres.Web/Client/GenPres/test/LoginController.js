describe("Application", function() {

    it('Controller validateLogin should call validateLoginForm', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        spyOn(controller, 'validateLoginForm');
        controller.validateLogin({username:"", password:""});
        expect(controller.validateLoginForm.wasCalled).toBeTruthy();
    });

    it('Controller validateLogin should call a backend login function', function () {
        var controller = GenPresApplication.getController("GenPres.controller.user.LoginController");
        
        spyOn(controller, 'loginCallBackFunction');

        controller.validateLogin({username:"blah", password:"blah"});
        
        GenPres.session.PatientSession.setLogicalUnit(1, "test")

        waitsFor(function () {
            return controller.loginCallBackFunction.wasCalled;
        }, 'waiting for loginCallBackFunction call', 2000);

    });
});


