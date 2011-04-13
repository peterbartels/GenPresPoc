Ext.namespace("View");

View.PatientTree = function(config) {

    this.patientTreePanel = new Ext.tree.TreePanel({
        layout: 'fit',
        region: 'west',
        margins: "5 5 5 5",
        padding:"5 5 5 10",
        autoScroll: true,
        baseCls: 'x-bubble',
        frame:true,
		flex:1,
		width:240,
		id:'patientTree',
        root: {
            id: 'root',
            text: 'Units'
        },
        loader: new Ext.tree.TreeLoader({
            directFn: Patient.GetUnits
        })
    });
	
    this.clearSelection = function(){
    	this.patientTreePanel.selModel.clearSelections()
    }
    
    this.patientTreePanel.on("click", function(node) {
        if (node.attributes.leaf) {
            GenPres.SelectedPatient = {
                Unit: node.attributes.Unit,
                Bed: node.attributes.Bed,
                FirstName: node.attributes.FirstName,
                LastName: node.attributes.LastName,
                Birthdate: node.attributes.Birthdate,
                Age: node.attributes.Age,
                WeightGuess: node.attributes.WeightGuess,
                Weight:(node.attributes.Weight > 0) ? node.attributes.Weight : 0,
                Length: (node.attributes.Length > 0) ? node.attributes.Length : 0,
                WeightMedication: node.attributes.WeightMedication,
                WeightActual: node.attributes.WeightActual,
                WeightAddmission: node.attributes.WeightAddmission,
                WeightBirth: node.attributes.WeightBirth,
                LengthGuessed: node.attributes.LengthGuessed,
                LengthActual: node.attributes.LengthActual,
                AddmissionDate: node.attributes.AddmissionDate,
                CurrentDate: node.attributes.CurrentDate,
                Days: node.attributes.Days,
                PID: node.attributes.PID
            }
            GenPres.GenPresView.UpdatePatient();
        }
    });

    return this;
}

