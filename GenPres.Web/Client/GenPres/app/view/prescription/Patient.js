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
            unitValue:'kg',
            name:'patientWeight'
        });

        var patientLength = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Lengte',
            labelAlign:'left',
            isHidden:false,
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['cm']]
            }),
            unitValue:'cm',
            name:'patientLength'
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
            unitValue:'m2',
            name:'patientBSA'
        });

        me.items = [patientWeight, patientLength, patientBSA];

        me.callParent();
    }
});