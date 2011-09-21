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
            event:instance[eventName],
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

    registerFunction : function(func, params){
        var me = this;
        me.checkQueue();
        var returnFunc = params.pop();

        me.queue[me.queueIndex].push({
            directFunc:me.linkReturnFunc(func),
            params:params
        });
    },

    linkReturnFunc : function(func){
        var me = this;
        return Ext.Function.createSequence(func, me.updateQueue, me);
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
            for(var i=0; i<me.queue[0].length; i++){

                var returnFunc = me.queue[0][i]['returnFunc'];
                var funcConfig = me.queue[0][i];

                for(var p=0;p<funcConfig['params'].length;p++){
                    if(typeof(funcConfig['params'][p]) == "function"){
                        var d = funcConfig['params'][p]();
                        funcConfig['params'][p] = d;
                    }
                }

                if(typeof(me.queue[0][i]['func']) != "undefined"){
                    var newFunc = Ext.Function.pass(funcConfig['func'], params);
                    funcs.push(newFunc);
                }

                if(typeof(me.queue[0][i]['directFunc']) != "undefined"){
                    var params = Ext.Array.merge(funcConfig['params'], returnFunc);
                    var newFunc = Ext.Function.pass(funcConfig['directFunc'], params);
                    funcs.push(newFunc);
                }

                if(typeof(me.queue[0][i]['event']) != "undefined"){
                    var newFunc = Ext.Function.bind(funcConfig['event'], funcConfig['instance'], params);
                    funcs.push(newFunc);
                }
            }
            
            for(var i=0; i<funcs.length; i++){
                funcs[i]();
            }
        }
    }
})