Ext.define('GenPres.test.usecase.PrescriptionTest', {
    describe: 'Prescription tests that',

    tests: function () {
        var me = this;

        me.getGridCount = function(){
            return me.getGrid().store.getCount();
        }

        me.getGrid = function(){
            var prescriptiongrid = GenPres.application.MainCenter.query('prescriptiongrid')[0];
            return prescriptiongrid;
        }

        me.checkGridCount = function(oldCount){
            if(me.getGridCount() == oldCount + 1){
                globalvars["checkGridCount"]=true;
            }
        }

        me.checkPrescriptionIsSet = function(){
            var prController = GenPres.application.getController('prescription.PrescriptionController');
            var values = prController.getValues();
            if(values.generic !="" && values.route != "" && values.shape != ""){
                globalvars["checkPrescriptionIsSet"]=true;
            }
        }

        me.checkPrescriptionIsClear = function(){
            var prController = GenPres.application.getController('prescription.PrescriptionController');
            debugger;
            var values = prController.getValues();
            
            if(values.generic =="" && values.route == "" && values.shape == ""){
                globalvars["checkPrescriptionIsClear"]=true;
            }
        }

        it('A prescription can be saved', function () {
            var currentCount = me.getGridCount();
            var savebutton =  Ext.ComponentQuery.query("button[action=save]")[0];
            savebutton.getActionEl().dom.click();
            globalvars["checkGridCount"]=false;
            waitsFor(createBindFunction(me.checkGridCount, me, [currentCount], "checkGridCount", 200), "grid to have a new prescription", 3000);
        });

        it('A prescription can be cleared', function () {
            var grid = me.getGrid();
            var newbutton =  Ext.ComponentQuery.query("button[action=new]")[0];
            newbutton.getActionEl().dom.click();
            globalvars["checkPrescriptionIsClear"]=false;
            waitsFor(createBindFunction(me.checkPrescriptionIsClear, me, [], "checkPrescriptionIsClear", 200), "prescription to be cleared", 3000);
        });

        it('The grid nodes count is the same as the store count', function () {
            var grid = me.getGrid();
            var count = me.getGridCount();
            var nodes = Ext.DomQuery.select(".x-grid-row", grid.el.dom);
            expect(count == nodex.length).toBeTruthy();
        });
        
        it('A prescription can be opened from the grid', function () {
            var grid = me.getGrid();
            var nodes = Ext.DomQuery.select(".x-grid-row", grid.el.dom);
            nodes[nodes.length - 1].click();
            globalvars["checkPrescriptionIsSet"]=false;
            waitsFor(createBindFunction(me.checkPrescriptionIsSet, me, [], "checkPrescriptionIsSet", 200), "prescription to be opened", 3000);
        });


        it('After a patient selection a prescription is cleared', function () {
            

        });
        
    }
});