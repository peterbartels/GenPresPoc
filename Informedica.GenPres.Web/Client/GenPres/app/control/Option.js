Ext.require('Ext.form.*');

Ext.define('GenPres.control.Option', {
    extend:'Ext.form.field.Checkbox',

    mixins: {
        visibility: 'GenPres.control.Visible'
    },

    initComponent: function(config){
        var me = this;
        me.callParent(config);
        me.mixins.visibility.constructor.call(me, config);
    }
});
