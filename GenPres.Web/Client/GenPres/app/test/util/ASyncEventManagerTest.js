Ext.define('GenPres.test.util.ASyncEventManagerTest', {

    describe: 'GenPres.lib.util.ASyncEventManagerTest',

    tests: function() {

        var ASyncEventManager;
        var func = function(data){
            console.log(data);
        };
        Ext.require('GenPres.lib.util.ASyncEventManager');

        it('ASyncEventManager be initialized', function () {
            waitsFor(function() {
			    return typeof(GenPres.lib.util) != "undefined";
		    }, "waiting for loading ASyncEventMan.", 200);

		    runs(function() {
			    ASyncEventManager = GenPres.lib.util.ASyncEventManager;
                expect(ASyncEventManager).toBeDefined();
            });
        });

        it('ASyncEventManager can register async events1', function () {
            ASyncEventManager.registerASyncEvent(Tests.GetTestData, [func]);
            ASyncEventManager.registerASyncEvent(Tests.GetTestData, [func]);
            ASyncEventManager.registerASyncEvent(Tests.GetTestData, [func]);
            ASyncEventManager.execute();
            expect(ASyncEventManager.syncEvents.length == 1).toBeTruthy();
            expect(ASyncEventManager.syncEvents[0].length == 3).toBeTruthy();
        });
        /*
        it('ASyncEventManager can execute async events3', function () {
            ASyncEventManager.executeASync();
            waitsFor(function() {
			    return ASyncEventManager.current == 1;
		    }, "waiting for loading ASyncEventMan.", 200);
        });*/

        it('ASyncEventManager can execute sync after async events2', function () {
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.execute();
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.execute();
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.registerEvent(Tests.GetTestData, [func]);
            ASyncEventManager.execute();
        });


    }
});