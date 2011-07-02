Ext.define('GenPres.view.main.PatientInfo', {

    extend: 'Ext.view.View',

    alias : 'widget.patientinfo',

    itemSelector : 'patientInfo',

    tpl: new Ext.XTemplate('<tpl for=".">',
            '<div class="patientIcon"><div class="PatientInfoPid">{PID}</div></div>' +
                '<div class="patientNameInfo">',
                    '<b><span style="font-size:12px;">{LastName}, {FirstName}</span></b><br />',
                    '<div class="patientInfoValue">',
                        '<div class="patientInfoHeader"><b>Afdeling/bed:</b> {Unit} - {Bed}</div><br />',
                        '<div class="patientInfoHeader"><b>Opname: {RegisterDate}</b></div><br />',
                        '<div class="patientInfoHeader"><b>Ligdag: {Days}</b></div>',
                    '</div>',
                '</div>',
            '</tpl>'),

    store : 'patient.PatientInfoStore',

    initComponent : function(){
        var me=this;
        me.callParent();
    }
});
