describe("Application", function() {

    it('GenPres.applicationshould exist', function () {
        expect(GenPres.application).toBeDefined();
    })

    it('Application should have GenPres config', function () {
        expect(Ext.Loader.config.paths.GenPres).toBeDefined();
        expect(GenPres.application.name).toEqual('GenPres');
    })

    it('Application should have GenPres path', function () {
        expect(Ext.Loader.config.paths.GenPres).toEqual('./Client/GenPres/app');
    })

    it('Application should have a viewport', function () {
        expect(GenPres.application.viewport).toBeDefined();
    })
});


