Ext.define('GenPres.controller.patient.PatientController', {
    extend: 'Ext.app.Controller',

    stores:['patient.LogicalUnitStore'],

    models:['patient.LogicalUnitModel'],

    views:['main.PatientTree'],

    init: function() {

    },

    onLaunch: function() {
        
    }

});