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
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'patientWeight'
        });

        var patientLength = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'Lengte',
            labelAlign:'left',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'patientLength'
        });

        var patientBSA = Ext.create('GenPres.control.UnitValueField', {
            fieldLabel: 'BSA',
            labelAlign:'left',
            unitStore: Ext.create('GenPres.store.prescription.SubstanceUnit'),
            name:'patientBSA'
        });

        me.items = [patientWeight, patientLength, patientBSA];

        me.callParent();
    }
});