Ext.define('GenPres.control.NumericBoundListKeyNav', {
    extend: 'Ext.view.BoundListKeyNav',
    mixins: {
        observable: 'Ext.util.Observable'
    },
    constructor: function(el, config) {
        var me = this;
        me.callParent([el, config]);
        me.addEvents('upreached');
        me.addEvents('downreaced');
    },
    defaultHandlers : {
        up: function() {

            var me = this,
                boundList = me.boundList,
                allItems = boundList.all,
                oldItem = boundList.highlightedItem,
                oldItemIdx = oldItem ? boundList.indexOf(oldItem) : -1,
                newItemIdx = oldItemIdx > 0 ? oldItemIdx - 1 : allItems.getCount() - 1; //wraps around

             if(oldItemIdx == 2){
                this.fireEvent("upreached");
             }else{
                me.highlightAt(newItemIdx);
             }
        },

        down: function() {
            var me = this,
                boundList = me.boundList,
                allItems = boundList.all,
                oldItem = boundList.highlightedItem,
                oldItemIdx = oldItem ? boundList.indexOf(oldItem) : -1,
                newItemIdx = oldItemIdx < allItems.getCount() - 1 ? oldItemIdx + 1 : 0; //wraps around
            if(oldItemIdx == allItems.getCount() - 1){
                //this.fireEvent("downreached");
            }else{
                if(oldItemIdx == 2){
                    this.fireEvent("downreached");
                }else{
                    me.highlightAt(newItemIdx);
                }
            }
        },

        pageup: function() {
            //TODO
        },

        pagedown: function() {
            //TODO
        },

        home: function() {

        },

        end: function() {

        },

        enter: function(e) {
            this.selectHighlighted(e);
        }
    }
});