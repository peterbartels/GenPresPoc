Ext.define('GenPres.util.Process', {

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
        var component;

        var process = me.Processes[name];

        var action = process[0];

        var queryResult = me.query(action.component);

        if(queryResult.length == 1){
            component = queryResult[0];
        }

        if(component.el){
           me.spot.show(component.el);
        }

        var config = {
            target: component.el,
            anchor: 'left',
            title: 'My Tip Title',
            html: 'Click the X to close me',
            autoHide : false,
            closable : true
        }

        var tooltip = Ext.create('Ext.tip.ToolTip', config);
        tooltip.show();
        tooltip.on("afterrender", function(){
            alert('f');
            tooltip.el.dom.style.zIndex = 22222;
        })


    }
});