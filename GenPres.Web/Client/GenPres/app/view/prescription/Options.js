Ext.define('GenPres.view.prescription.Options', {

    extend: 'Ext.form.Panel',
    
    layout:{
        type:'table',
        columns:4
    },
    alias:'widget.prescriptionformoptions',
    
    border:false,

    title:'&nbsp;',

    width:270,

    height:53,

    initComponent : function(){
        var me = this;

        var continuous = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Continu',
            labelAlign:'top',
            labelSeparator:'',
            style: { textAlign: 'center'},
            width:60,
            fieldStyle :'visibility:hidden;',
            name:'prescriptionContinuous'
        });

        var infusion = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Inlooptijd',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:70,
            labelSeparator:'',
            fieldStyle :'visibility:hidden;',
            name:'prescriptionInfusion'
        });

        var onRequest = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Indien nodig',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:74,
            labelSeparator:'',
            name:'prescriptionOnRequest'
        });

        var solution = Ext.create('Ext.form.field.Checkbox',{
            fieldLabel : 'Oplossing',
            labelAlign:'top',
            style: { textAlign: 'center'},
            width:60,
            labelSeparator:'',
            name:'prescriptionSolution'
        });
        
        solution.on("change", function(field, newValue, oldValue, eOpts){
            if(newValue == true){
                continuous.setFieldStyle("visibility:visible;");
                infusion.setFieldStyle("visibility:visible;");
            }else{
                continuous.setFieldStyle("visibility:hidden;");
                infusion.setFieldStyle("visibility:hidden;");
            }
        })


        me.items = [continuous, infusion, onRequest, solution];
        me.callParent();

    }

});
