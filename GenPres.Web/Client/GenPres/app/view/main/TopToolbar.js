
Ext.define('GenPres.view.main.ToolbarButton', {
    extend:'Ext.button.Button',
    text: '',
    scale:'large',
    location: 'Client/GenPres/style/images/TopToolbar/',
    iconAlign:'top',
    disabled: false,
    width:60,
    initComponent:function(){
        var me = this;
        me.icon = me.location + me.icon;
        me.callParent();
    }
});

Ext.define('GenPres.view.main.TopToolbar', {
    extend:'Ext.container.ButtonGroup',
    dock: 'top',

    initComponent : function(){
        var me = this;
        me.items = [
            {
                xtype: 'buttongroup',
                columns: 5,
                height:86,
                title: 'Algemeen',
                items : [
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'Home_32.png', text:'Home', action:'home'}),
                    {xtype: 'tbseparator',height:20},
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'Prescription_32.png', text:'Voorschriften', width:80}),
                    {xtype: 'tbseparator',height:20},
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'TPN_32.png', text:'TPN'})
                ]
            },
            {xtype: 'tbseparator',height:20},
            {
                xtype: 'buttongroup',
                columns: 3,
                height:86,
                title: 'Opties',
                items : [
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'Template_32.png', text:'Sjablonen'}),
                    {xtype: 'tbseparator',height:20},
                    Ext.create('GenPres.view.main.ToolbarButton', {icon:'NewMedicine_32.png', text:'Nieuw medicament', width:105})
                ]
            },
            {
                xtype: 'buttongroup',
                columns: 1,
                title: 'Patient informatie',
                id:'PatientInfoView',
                width:350,
                height:86,
                items : Ext.create('GenPres.view.main.PatientInfo')
            },
            {
                xtype: 'buttongroup',
                columns: 1,
                height:86,
                title: 'Medewerker',
                items : [
                    {html:' ', height:57, width:200}
                ]
            }
        ]
        me.callParent();
    }
})
