Ext.define('GenPres.test.usecase.LoginTest', {
    describe: 'Login tests that',

    tests: function () {
        var me = this,
            loginMessage = "",
            refusalMessage = "Login geweigerd",
            successMessage = "Login succesvol",
            errorMessage = "GenPres 2011 Login Error",
            waitingTime = 5000;

        me.getLoginController = function () {
            return GenPres.application.getController('user.Login');
        };

        me.getLoginWindow = function () {
            return Ext.ComponentQuery.query('window[title="GenPres Login"]')[0];
        };

        me.getLoginButton = function () {
            return Ext.ComponentQuery.query('toolbar button[action=login]')[0];
        };

        me.getFormField = function (fieldname) {
            return Ext.ComponentQuery.query('textfield[name=' + fieldname + ']')[0];
        };

        me.getPatientInfoView = function (fieldname) {
            return Ext.ComponentQuery.query('dataview[name=' + fieldname + ']')[0];
        };

        me.setFormField = function (textfield, value) {
            textfield.inputEl.dom.value = value;
            textfield.value = value;
            return true;
        };

        me.clickButton = function (button) {
            button.btnEl.dom.click();
        };

        me.checkLoginMessage = function () {
            var results = Ext.ComponentQuery.query('messagebox');
            if (results.length > 0)
            {

                if (results[0].cfg) {
                    if (results[0].cfg.msg === loginMessage || results[0].cfg.title == loginMessage)
                    {
                        Ext.ComponentQuery.query('button[text=OK]')[0].btnEl.dom.click();
                        return true;
                    }
                }
            }
            return false;
        };


        it('There should be a login controller', function () {
            expect(me.getLoginController()).toBeDefined();
        });

        it('The user should see a login window at start up with title GenPres Login', function () {
            var window = me.getLoginWindow();
            expect(window).toBeDefined();
        });

        it('This window should not be closable', function () {
            var window = me.getLoginWindow();
            expect(window.closable === false).toBeTruthy();
        });

        it('User must select a username, password and department', function () {
            var button = me.getLoginButton();
            loginMessage = errorMessage;
            waitsFor(me.checkLoginMessage, 'waiting for an error message', waitingTime);
            me.clickButton(button);
        });

        it('User can set username and password', function () {
            var userField = me.getFormField('username'),
                passwField = me.getFormField('password');

            me.setFormField(userField, "Invalid");
            me.setFormField(passwField, "Invalid");

            expect(userField.value).toBe("Invalid");
            expect(passwField.value).toBe("Invalid");
        });

        it('User can select a departement', function () {
            var departmentDataView = me.getPatientInfoView('loginLogicalUnitSelector');
            waitsFor(function () {
                if(departmentDataView.store.getCount()>0){
                    departmentDataView.el.dom.childNodes[0].childNodes[0].click();
                    return departmentDataView.selModel.getSelection().length == 1;
                }
                return false;
            }, 'logicalUnitSelector to be rendered', 2000);
        });

        it('If Username or password is invalid, user still cannot login', function () {
            var button = me.getLoginButton();

            me.clickButton(button);
            loginMessage = refusalMessage;
            waitsFor(me.checkLoginMessage, 'waiting for refusal message', waitingTime)
        });

        it('User can login using a valid name and password', function () {
            var button = me.getLoginButton(),
                userField = me.getFormField('username'),
                passwField = me.getFormField('password');

            me.setFormField(userField, "test");
            me.setFormField(passwField, "Test");

            me.clickButton(button);
            loginMessage = successMessage;
            waitsFor(me.checkLoginMessage, "waiting for successfull login", waitingTime);
        });

    }
});