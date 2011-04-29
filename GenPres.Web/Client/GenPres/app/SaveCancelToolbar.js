
Ext.define('GenForm.view.component.SaveCancelToolBar', {
    extend: 'Ext.toolbar.Toolbar',

    saveBtnText  : 'Save',
    cancelBtnText: 'Cancel',

    items:[
        '->',
        {xtype:'button', handler:this.triggerSave, scope:this, text:this.saveBtnText},
        {xtype:'button', handler:this.triggerCancel, scope:this, text:this.cancelBtnText}
    ],
    
    initComponent: function() {
        var me = this;

        me.addEvents(
            'save',
            'cancel'
        );
        
        me.callParent();
    },

    constructor: function (config) {
        this.initConfig(config);
        return this;
    },

   triggerSave: function () {
        this.fireEvent("save", this);
   },
    
   triggerCancel: function () {
        this.fireEvent("cancel", this);
   }
});