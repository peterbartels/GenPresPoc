Ext.define('GenPres.lib.util.ASyncEventManager', {
    singleton: true,

    isBusy:false,

    syncEvents : [],

    queueLength : 0,

    checkQueue : function(){
        var me = this;
        if(typeof(me.syncEvents[me.queueLength]) == "undefined"){
            me.syncEvents[me.queueLength] = [];
        }
    },

    registerEvent : function(func, params){
        var me = this;
        me.checkQueue();
        me.syncEvents[me.queueLength].push({func:func, params:params});
    },

    execute : function(){
        var me = this;
        
        me.queueLength++;

        if(me.isBusy == true) return;

        me.isBusy = true;
        
        if(me.syncEvents.length > 0){
            for(var i=0; i<me.syncEvents[0].length; i++){
                var returnFunc = me.syncEvents[0][i]['params'][0];
                returnFunc = Ext.Function.createSequence(returnFunc, function(){
                    me.syncEvents[0].pop();
                    if(me.syncEvents[0].length == 0){
                        me.syncEvents.shift();
                        me.isBusy = false;
                        me.executeASync();
                        return;
                    }
                });
                var newFunc = me.syncEvents[0][i]['func'](returnFunc);
            }
        }
    }
})