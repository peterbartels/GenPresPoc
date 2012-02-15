Ext.define('GenPres.view.prescription.Patient', {

    extend: 'Ext.form.Panel',
    
    region: 'center',

    alias:'widget.prescriptionpatient',

    border:false,

    width:270,

    height:111,

    bodyPadding:'25 0 0 5',

    bodyCls: 'presriptionFormCategory',

    bodyStyle:"margin-left:1px;",

    title:'Patient',

    initComponent : function(){
        var me = this;

        var patientWeight = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Gewicht',
            labelAlign:'left',
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['gram'],['kg']]
            }),
            unit:'kg',
            id:'patientWeight',
            name:'patientWeight'
        });

        var patientLength = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Lengte',
            labelAlign:'left',
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['cm']]
            }),
            unit:'cm',
            id:'patientLength',
            name:'patientLength'
        });

        var patientBSA = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'BSA',
            labelAlign:'left',
            unitStore: Ext.create('Ext.data.ArrayStore', {
                autoDestroy: true,
                fields: ['Value'],
                data : [['m2']]
            }),
            unit:'m2',
            id:'patientBSA',
            name:'patientBSA'
        });

        me.items = [patientWeight, patientLength, patientBSA];

        me.callParent();
    }
});