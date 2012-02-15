Ext.define('GenPres.view.prescription.Solution', {

    extend: 'Ext.form.Panel',
    
    layout:{
        type:'table',
        columns:2
    },
    alias:'widget.prescriptionformfrequencyduration',

    collapsible:true,
    
    border:false,

    title:'Frequentie en tijdsduur',

    width:700,

    colspan:2,

    initComponent : function(){
        var me = this;

        me.items = [drugQuantity, substanceDrugConcentration];
        me.callParent();

    }

});
