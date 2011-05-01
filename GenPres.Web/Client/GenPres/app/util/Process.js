Ext.define('GenPres.util.Process', {

    currentProcessNr : 0,

    constructor: function(config) {
        var me = this;

        Ext.apply(me, config);
        
        me.spot = Ext.create('GenPres.util.Spotlight', {
            easing: 'easeOut',
            duration: 300
        });

    },

    doProcess:function(name){
        var me = this;

        me.name = name;
        
        var component;

        var process = me.Processes[name];
        
        var action = process[me.currentProcessNr];

        var queryResult = me.query(action.component);

        if(queryResult.length == 1){
            component = queryResult[0];
        }

        if(component.el){
           me.spot.show(component.container);
        }

        component.on('click', function(){
            if(me.currentProcessNr == (process.length - 1)) {
                me.spot.hide();
                me.tooltip.destroy();
            }
        });

        var config = {
            target: component.el,
            anchor: 'left',
            title: name,
            html:action.text,
            dockedItems: [{
                xtype: 'toolbar',
                baseCls:'none',
                dock: 'bottom',
                items: ['->', new Ext.Button({
                    text: 'Volgende',
                    hidden:(me.currentProcessNr == (process.length - 1) ),
                    handler:me.processNext,
                    scope:me
                })]
            }],
            minWidth:100,
            minHeight:100,
            autoHide : false,
            closable : true
        }
        
        me.tooltip = Ext.create('Ext.tip.ToolTip', config);

        var setZIndex = Ext.Function.createDelayed(function(){
            me.tooltip.el.dom.style.zIndex = 222001;
        }, 50);

        me.tooltip.show();

        me.tooltip.zIndexManager.setBase().zseed = 222001;
        
        setZIndex();
    },

    processNext : function(){
        var me = this;
        me.tooltip.destroy();
        me.currentProcessNr++;
        me.doProcess(me.name);
    }
});