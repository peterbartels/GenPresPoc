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
});


