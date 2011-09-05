Ext.define('GenPres.test.util.ASyncEventManagerTest', {

    describe: 'GenPres.lib.util.ASyncEventManagerTest',

    tests: function() {

        var ASyncEventManager;
        var func = function(data){
            console.log("result=" + data);
        };
        var _store;

        var getStore = function(){
            if(typeof(_store) == "undefined"){
                _store = Ext.create('Ext.data.Store', {
                    autoLoad:false,
                    fields: [
                        { name: 'test', type: 'string' }
                    ],
                    proxy : {
                        type:'direct',
                        directFn : Tests.GetTestStoreData,
                        extraParams:{
                            contents: "qq"
                        },
                        paramOrder : ['contents']
                    }
                });
            }
            return _store;
        }

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

        it('ASyncEventManager can registor store event', function () {
            getStore().proxy.extraParams['contents'] = "qq2";
            getStore().on("load", function(store,items){
                console.log(getStore().data.items[0].data);
            });
            ASyncEventManager.registerFunction(getStore(),"load",[]);
            expect(ASyncEventManager.queue.length == 1).toBeTruthy();
            expect(ASyncEventManager.queue[0].length == 1).toBeTruthy();
            ASyncEventManager.execute();

            var q = function(){
                
                ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 1",func]);
                ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 2",func]);
                ASyncEventManager.execute();
            };

            var d = Ext.Function.defer(q, 5000);
        });
/*
        it('ASyncEventManager can register async events1', function () {
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 1",func]);
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 2",func]);
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 3",func]);
            ASyncEventManager.execute();
        });

        it('ASyncEventManager can execute sync after async events2', function () {
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 4", func]);
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 5",func]);
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list1 6",func]);
            ASyncEventManager.execute();
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list2 1",func]);
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list2 2",func]);
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list2 3",func]);
            ASyncEventManager.execute();

            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list3 1",func]);
            ASyncEventManager.registerDirectEvent(Tests.GetTestData, ["list3 2",func]);
            ASyncEventManager.execute();


        });*/
        
    }
});