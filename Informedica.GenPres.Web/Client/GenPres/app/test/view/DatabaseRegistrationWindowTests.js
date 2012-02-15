Ext.define('GenPres.test.view.DatabaseRegistrationWindowTests', {
    describe: 'DatabaseRegistrationWindowShould',

    tests: function () {
        var me = this, databaseregistrationWindow = Ext.create('GenPres.view.database.RegisterDatabaseWindow');

        me.getDatabaseNameField = function () {
            return databaseregistrationWindow.getDatabaseNameField();
        };

        me.setDatbaseNameField = function (name) {
            me.getDatabaseNameField().value = name;
        };

        me.getMachineNameField = function () {
            return databaseregistrationWindow.getMachineNameField();
        };

        me.setMachineNameField = function (name) {
            me.getMachineNameField().value = name;
        };

        me.getGenPresConnectionStringField = function () {
            return databaseregistrationWindow.getGenPresConnectionStringField();
        };

        me.getPdmsConnectionStringField = function () {
            return databaseregistrationWindow.getPDMSConnectionStringField();
        };
        
        me.getGenFormField = function () {
            return databaseregistrationWindow.getGenFormWebserviceField();
        };

        me.setGenPresConnectionStringField = function (genpresConn) {
            me.getGenPresConnectionStringField().value = genpresConn;
        };

        me.setPdmsConnectionStringField = function (pdmsConn) {
            me.getPdmsConnectionStringField().value = pdmsConn;
        };

        me.setGenFormField = function (genFormWS) {
            me.getGenFormField().value = genFormWS;
        };

        it('Be defined', function () {
            expect(databaseregistrationWindow).toBeDefined();
        });

        it('Have a field for the database name', function () {
            expect(databaseregistrationWindow.getDatabaseName).toBeDefined();
        });
        
        it('Have a field for the machine name', function () {
            expect(databaseregistrationWindow.getMachineName).toBeDefined();
        });

        it('Have a field for the GenPres connection string', function () {
            expect(databaseregistrationWindow.getGenPresConnectionString).toBeDefined();
        });

        it('Have a field for the PDMS connection string', function () {
            expect(databaseregistrationWindow.getPDMSConnectionString).toBeDefined();
        });
        
        it('Have a field for the GenForm webservice', function () {
            expect(databaseregistrationWindow.getGenFormWebservice).toBeDefined();
        });

        it('Be able to set the database name field', function () {
            me.setDatbaseNameField('test');
            expect(me.getDatabaseNameField().value).toBe('test');
        });

        it('Be able to set the machine name field', function () {
            me.setMachineNameField('test');
            expect(me.getMachineNameField().value).toBe('test');
        });

        it('Be able to set the genpres connection string field', function () {
            me.setGenPresConnectionStringField('test1');
            expect(me.getGenPresConnectionStringField().value).toBe('test1');
        });

        it('Be able to set the pdms connection string field', function () {
            me.setPdmsConnectionStringField('test1');
            expect(me.getPdmsConnectionStringField().value).toBe('test1');
        });

        it('Be able to set the GenFormWebservice field', function () {
            me.setGenFormField('test1');
            expect(me.getGenFormField().value).toBe('test1');
        });
    }

});