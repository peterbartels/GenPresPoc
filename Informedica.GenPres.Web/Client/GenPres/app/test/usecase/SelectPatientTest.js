Ext.define('GenPres.test.usecase.SelectPatientTest', {
    describe: 'Select Patient tests that',

    tests: function () {
        var me = this,
        treeClicked = false;

        me.getPatientController = function () {
            return GenPres.application.getController('patient.Patient');
        };

        me.getTreeComp = function () {
            return Ext.ComponentQuery.query('treepanel')[0];
        };

        me.getPatientInfoView = function () {
            return Ext.ComponentQuery.query('patientinfo')[0];
        };

        me.checkDataViewHasItems = function (){
            var el = me.getFirstNodeEl();
            if(me.getPatientInfoView().store.data.items.length == 1){
                return true;
            }
            return false;
        };

        me.getFirstNodeEl = function () {
            treeComp = me.getTreeComp();
            if(typeof(treeComp.el.dom) != "undefined"){
                var el = Ext.DomQuery.select('.x-grid-cell', treeComp.el.dom)[1];
                if(typeof(el) != "undefined"){
                    if(treeClicked == false){
                        Ext.Function.defer(function(){
                            el.click();
                            treeClicked=true;
                        }, 500);
                    }
                }
            } 
        };

        it('There should be a patient controller', function () {
            expect(me.getPatientController()).toBeDefined();
        });

        it('User can select a patient and a dataview is loaded with the patient', function () {
            waitsFor(me.checkDataViewHasItems, 'waiting for patient to be selected', 3000);
        });
    }
});
