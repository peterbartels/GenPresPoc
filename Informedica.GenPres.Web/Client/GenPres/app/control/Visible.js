Ext.define('GenPres.control.Visible', {

    visible: false,

    constructor : function(){
        var me = this;
        me.on("render", function(){
            me.setVisibility(me.visible);
        });
    },

    getVisibility : function(){
        var me = this;
        return me.visible;
    },

    setVisibility : function(value){
        var me = this;
        me.visible = value;
        me.el.setVisibilityMode(Ext.Element.VISIBILITY);
        (me.visible ? me.el.show() : me.el.hide());
    },

    getValueVisibilityObj : function(){
        var me = this;
        var value = me.getValue();
        if(typeof(value == obj)){
            value.visible = me.visible;
            return value;
        }else{
            return {
                value : value,
                visible : me.visible
            }
        }
    },

    setValueVisibilityObj : function(valueVisibilityObj){
        debugger;
    }

});