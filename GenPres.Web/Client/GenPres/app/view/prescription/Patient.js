Ext.define('GenPres.view.prescription.Patient', {

    extend: 'Ext.form.Panel',
    
    region: 'center',

    alias:'widget.prescriptionpatient',

    border:false,

    width:300,

    height:141,

    bodyPadding:'25 0 0 0',

    title:'Patient',

    initComponent : function(){
        var me = this;

        var patientWeight = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Gewicht',
            labelAlign:'left',
            isHidden:false,
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['gram'],['kg']]
            }),
            unit:'kg',
            name:'patientWeight'
        });

        var patientHeight = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Lengte',
            labelAlign:'left',
            isHidden:false,
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['cm']]
            }),
            unit:'cm',
            name:'patientHeight'
        });

        var patientBSA = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'BSA',
            labelAlign:'left',
            isHidden:false,
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['m2']]
            }),
            unit:'m2',
            name:'patientBSA'
        });

        me.items = [patientWeight, patientHeight, patientBSA];

        me.callParent();
    }
});