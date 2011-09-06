Ext.define('GenPres.lib.util.ASyncEventManager', {
    singleton: true,

    isBusy:false,

    queue : [],

    queueIndex : 0,

    isFirst : true,

    updateQueue : function(){
        var me = this;
        me.queue[0].pop();
        if(me.queue[0].length == 0){
            me.queue.shift();
            me.queueIndex--;
            me.isBusy = false;
            if(me.queue.length > 0) {
                me.execute(true);
            }
            return;
        }
    },
    
    checkQueue : function(){
        var me = this;
        if(typeof(me.queue[me.queueIndex]) == "undefined"){
            me.queue[me.queueIndex] = [];
        }
    },

    registerEventListener : function(instance, eventName, params){
        var me = this;
        me.checkQueue();

        instance.events[eventName].removeListener(me.updateQueue, me)
        
        instance.on(eventName, me.updateQueue, me);

        var obj = {
            instance:instance,
            func:instance[eventName],
            eventName:eventName,
            params:params
        };
        me.queue[me.queueIndex].push(obj);

    },

    registerDirectEvent : function(func, params){
        var me = this;
        me.checkQueue();
        var returnFunc = params.pop();
        
        me.queue[me.queueIndex].push({
            directFunc:func,
            returnFunc:me.linkReturnFunc(returnFunc),
            params:params
        });
    },

    linkReturnFunc : function(returnFunc){
        var me = this;
        return Ext.Function.createSequence(returnFunc, me.updateQueue, me);
    },

    execute : function(disableUpdateIndex){
        var me = this;
        
        if(typeof(disableUpdateIndex) == "undefined"){
            me.queueIndex++;
        }

        if(me.isBusy == true) return;

        if(me.queue.length > 0){
            me.isBusy = true;
            var funcs = [];
            for(var i=me.queue[0].length-1; i>=0; i--){

                var returnFunc = me.queue[0][i]['returnFunc'];
                var funcConfig = me.queue[0][i];

                for(var p=0;p<funcConfig['params'].length;p++){
                    if(typeof(funcConfig['params'][p]) == "function"){
                        var d = funcConfig['params'][p]();
                        funcConfig['params'][p] = d;
                    }
                }
        
                if(typeof(me.queue[0][i]['directFunc']) != "undefined"){
                    var params = Ext.Array.merge(funcConfig['params'], returnFunc);
                    var newFunc = Ext.Function.pass(funcConfig['directFunc'], params);
                    funcs.push(newFunc);
                } else{
                    var newFunc = Ext.Function.bind(funcConfig['func'], funcConfig['instance'], params);
                    funcs.push(newFunc);
                }
            }
            
            for(var i=0; i<funcs.length; i++){
                funcs[i]();
            }
        }
    }
})