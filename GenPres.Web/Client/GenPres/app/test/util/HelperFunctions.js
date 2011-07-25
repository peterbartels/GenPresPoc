
Ext.define('GenPres.test.util.HelperFunctions', {
    singleton : true,

    getButton: function (fieldname) {
        return Ext.ComponentQuery.query("button[action=" + buttontext + "]")[0];
    },

    getCombobox: function (fieldname) {
        return Ext.ComponentQuery.query('combobox [action=' + fieldname + ']')[0];
    }
});