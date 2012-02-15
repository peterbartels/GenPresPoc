describe("PatientTree", function () {
    var mainView;
    var tree;

    GenPres.session.PatientSession.setLogicalUnit(1, "Pelikaan");

    var GetMainView = function () {
        if (typeof (mainView) == "undefined") {
            mainView = Ext.create('GenPres.view.main.MainView');
        }
        return mainView;
    };

    var GetWestRegion = function () {
        return GetMainView().items.findBy(function (obj) {
            if (obj.region == 'west') return obj;
        });
    };

    var GetTree = function () {
        var query = GetWestRegion().query('treepanel');
        return query[0];
    };

    var GetPatientInfo= function () {
        var query = GetMainView().query('patientinfo');
        return query[0];
    };

    it('after select in a patient tree, a patient record should be loaded', function () {
        tree = GetTree();
        var patientInfo = GetPatientInfo();
        var patientRecordSelected = false;

        tree.on("load", function(){
            var func = function(){
                tree.view.getNodes()[tree.view.getNodes().length - 1].click();
            }
            Ext.Function.defer(func, 3000);
        });

        waitsFor(function () {
            return patientInfo.store.data.items.length == 1;
        }, 'waiting for patienttree to be filled', 5000);
    });

    it('after select a patient tree, a prescriptionForm should be created', function () {
        tree = GetTree();
        var patientInfo = GetPatientInfo();
        var patientRecordSelected = false;

        tree.on("load", function(){
            var func = function(){
                tree.view.getNodes()[tree.view.getNodes().length - 1].click();
            }
            Ext.Function.defer(func, 3000);
        });

        waitsFor(function () {
            return GetMainView().query('prescriptionform').length == 1;
        }, 'waiting for patienttree to be filled', 5000);
    });

    it('PatientTree should be loaded with items', function () {
        var tree = GetTree();
        waitsFor(function () {
            var rootnode = tree.store.getRootNode();
            return rootnode.childNodes.length > 2;
        }, 'waiting for patienttree to be filled', 5000);
    });
});