Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");

View.PrescribeMedicationForm.Scenarios = function(MainForm) {

    //var isDrug.Name = (this.HasValue("Drug.Name") == "") ? true : false;
    //var M1 = ["Prescription.Onrequest", "Prescription.Frequency", "Dose.Quantity", "Dose.Total"]

    this.overlays = {}
    this.boxes = {}
    this.view = "";
	this.IsValid = true;
    this.IsTrue = function(id) {
        switch (id) {
            case "Id":
            	return (MainForm.Id > 0);
            	break;
            case "DoseVolume":
                var value = MainForm.getField("Substance.Quantity").getValue().Unit;
                return (value == "ml" || value == "l" || value == "dl" || value == "cl");
            case "AdministrationVolume":
                return MainForm.getField("Prescription.Solution").getValue();
            case "Prescription.Onrequest":
                return MainForm.getField("Prescription.Onrequest").getValue();
            case "Prescription.Infusion":
                return MainForm.getField("Prescription.Infusion").getValue();
            case "Prescription.Continuous":
                return MainForm.getField("Prescription.Continuous").getValue();
			case "Prescription.Generic":
                return (MainForm.getField("Drug.Name").getValue() != "");
                
        }
    }

    this.show = function(id) {
        var field = MainForm.getField(id);
        
        if (typeof (field) == "undefined") {
        	console.log(id);
        }else{
        	var value = field.getValue();
        	if(typeof(value.Value) != "undefined"){
				if(!MainForm.IsTemplate && value.Value == 0){
					this.IsValid = false;
				}
        	}
        	field.getEl().parent().dom.style.visibility = '';
        }
    }
    this.hide = function(id) {
        var field = MainForm.getField(id);
        
        if (typeof (field) == "undefined") {
        	console.log(id);
        	return;
        }
        //field.hide();
        field.getEl().parent().dom.style.visibility = 'hidden';
        //field.getEl().parent().parent().parent().parent().parent().dom.style.visibility = 'hidden';
    }
    this.disable = function(id) {
        var field = MainForm.getField(id);
        field.container.addClass("fieldDisabled");
        
        if(typeof(field.setReadOnly) == "function") {
        	field.setReadOnly(true);
        }
        //field.setDisabled(true);
    }
    this.enable = function(id) {
        var field = MainForm.getField(id);
        if (typeof (field) == "undefined") {
        	 console.log(id);
        	return;
        }
        field.container.removeClass("fieldDisabled");
        if(typeof(field.setReadOnly) == "function") {
        	field.setReadOnly(false);
        }
        //field.setDisabled(false);
    }
    
    this.start = function() {
        //console.log("Scenario check");
        this.IsValid = true;
        
        var scenarios = [];
        scenarios.push(
        	{
            	//=0
                criteria: ["-DoseVolume", "-AdministrationVolume"],
                hidefields: [
                    "-Drug.Quantity",
                    "-Drug.SolutionType",
                    "-Substance.DrugConcentration",
                    "+Prescription.Continuous",
                    "-Prescription.Infusion",
                    "+Prescription.Onrequest",
                    "+Prescription.Frequency",
                    "-Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "+Dose.Total",
                    "+Prescription.Total",
                    "-Dose.Rate",
                    "-Prescription.Rate"
                ]
            }, {
                //=1
                criteria: ["-DoseVolume", "+AdministrationVolume"],
                hidefields: [
                    "+Drug.Quantity",
                    "+Drug.SolutionType",
                    "+Substance.DrugConcentration",
                    "+Prescription.Continuous",
                    "+Prescription.Infusion",
                    "+Prescription.Onrequest",
                    "+Prescription.Frequency",
                    "-Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "+Dose.Total",
                    "+Prescription.Total",
                    "-Dose.Rate",
                    "-Prescription.Rate"
                ]
            }, {
                //=2
                criteria: ["+DoseVolume", "+AdministrationVolume"],
                hidefields: [
                    "-Drug.Quantity",
                    "+Drug.SolutionType",
                    "-Substance.DrugConcentration",
                    "+Prescription.Continuous",
                    "+Prescription.Infusion",
                    "+Prescription.Onrequest",
                    "+Prescription.Frequency",
                    "-Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "+Dose.Total",
                    "+Prescription.Total",
                    "-Dose.Rate",
                    "-Prescription.Rate"
                ]
            }, {
                //=3
                criteria: ["-Prescription.Onrequest", "-Prescription.Continuous", "-Prescription.Infusion"],
                hidefields: [
                    "+Prescription.Frequency",
                    "+Substance.Quantity",
                    "-Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "+Dose.Total",
                    "+Prescription.Total",
                    "-Dose.Rate",
                    "-Prescription.Rate"
                ]
            }, {
                //=4
                criteria: ["+Prescription.Onrequest", "-Prescription.Continuous", "-Prescription.Infusion"],
                hidefields: [
                    "+Prescription.Frequency",
                    "+Substance.Quantity",
                    "-Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
                    "-Dose.Rate",
                    "-Prescription.Rate"
                ]
            }, {
                //=5
                criteria: ["+Prescription.Onrequest", "+Prescription.Continuous", "-Prescription.Infusion"],
                hidefields: [
                    "+Prescription.Frequency",
                    "+Substance.Quantity",
                    "-Prescription.Time",
                    "-Dose.Quantity",
                    "-Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
                    "+Dose.Rate",
                    "+Prescription.Rate"
                ]
            }, {
                //=6
                criteria: ["+Prescription.Onrequest", "-Prescription.Continuous", "+Prescription.Infusion"],
                hidefields: [
                    "+Prescription.Frequency",
                    "+Substance.Quantity",
                    "+Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
					"-Dose.Rate",
                    "+Prescription.Rate"
                ]
            }, {
                //=7
                criteria: ["+Prescription.Onrequest", "+Prescription.Continuous", "+Prescription.Infusion"],
                hidefields: [
                    "+Prescription.Frequency",
                    "+Substance.Quantity",
                    "+Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
                    "+Dose.Rate",
                    "+Prescription.Rate"
                ]
            }, {
                //=8
                criteria: ["-Prescription.Onrequest", "+Prescription.Continuous", "-Prescription.Infusion", "+AdministrationVolume"],
                hidefields: [
                    "+Drug.Quantity",
                    "+Substance.DrugConcentration",
                    "+Substance.Quantity",
                    "-Prescription.Frequency",
                    "-Prescription.Time",
                    "-Dose.Quantity",
                    "-Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
                    "+Dose.Rate",
                    "+Prescription.Rate"
                ]
            }, {
                //=9
                criteria: ["-Prescription.Onrequest", "+Prescription.Continuous", "+Prescription.Infusion"],
                hidefields: [
                    "+Prescription.Frequency",
                    "+Substance.Quantity",
                    "+Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
                    "+Dose.Rate",
                    "+Prescription.Rate"
                ]
            }, {
                //=10
                criteria: ["-Prescription.Onrequest", "-Prescription.Continuous", "+Prescription.Infusion"],
                hidefields: [
                    "+Prescription.Frequency",
                    "+Substance.Quantity",
                    "+Prescription.Time",
                    "+Dose.Quantity",
                    "+Prescription.Quantity",
                    "+Dose.Total",
                    "+Prescription.Total",
                    "-Dose.Rate",
                    "+Prescription.Rate"
                ]
            },{//=11
        		criteria: ["-Prescription.Generic"],
                hidefields: [
                	"-Drug.Quantity",
                	"+Substance.Quantity",
                    "-Substance.DrugConcentration",
                    "-Prescription.Frequency",
                    "-Prescription.Time",
                    "-Dose.Quantity",
                    "-Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
                    "-Dose.Rate",
                    "-Prescription.Rate"
                ]
        	},{
                //=12
                criteria: ["-Prescription.Onrequest", "+Prescription.Continuous", "-Prescription.Infusion", "-AdministrationVolume"],
                hidefields: [
                    "-Drug.Quantity",
                    "-Substance.DrugConcentration",
                    "+Substance.Quantity",
                    "-Prescription.Frequency",
                    "-Prescription.Time",
                    "-Dose.Quantity",
                    "-Prescription.Quantity",
                    "-Dose.Total",
                    "-Prescription.Total",
                    "+Dose.Rate",
                    "+Prescription.Rate"
                ]
            },{ //=13
                criteria: ["+Id"],
                hidefields: [
                    "DSubstance.Quantity",
                    "DDrug.Name",
                    "DDrug.Shape",
                    "DDrug.Route",
                    "DDrug.SolutionType",
                    "-Prescription.Solution",
                    "DSubstance.DrugConcentration",
                    "DSubstance.Quantity",
                    "DDrug.Quantity"
                ]
            },{ //=14
                criteria: ["-Id"],
                hidefields: [
                    "EDrug.Name",
                    "EDrug.Shape",
                    "EDrug.Route",
                    "EDrug.SolutionType",
                    "+Prescription.Solution"
                ]
            }
        );

        var foundScenarios = [];

        for (var i = 0; i < scenarios.length; i++) {
            var scenario = scenarios[i];
            var AllCriteriaTrue = true;
            for (var c = 0; c < scenario.criteria.length; c++) {
                var field = scenario.criteria[c];

                var add = (field.match(".")[0] == "+");
                var id = field.match(".(.*)")[1];
                if (add) {
                    AllCriteriaTrue = AllCriteriaTrue && this.IsTrue(id);

                } else {
                    AllCriteriaTrue = AllCriteriaTrue && !this.IsTrue(id)
                }
            }
            if (AllCriteriaTrue) {
                foundScenarios.push(i);
                for (var f = 0; f < scenario.hidefields.length; f++) {
                    var field = scenario.hidefields[f];
                    var add = field.match(".")[0];
                    var id = field.match(".(.*)")[1];
                    if (add=="+") {
                    	this.show(id);
                    	this.enable(id);
                    }
                    if (add=="-") this.hide(id);
                    if (add=="D") this.disable(id);
                    if (add=="E") this.enable(id);
                }
            }
        }
        var text = "";
        var options = [];
        if (foundScenarios[0] == 0 && foundScenarios[1] == 3) {
            //text = "paracetamol 240 mg zetpil rect 3 keer per dag 1 zetpil = 240 mg, 21 zetpil/week = 72 mg/ kg/dag";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 0 && foundScenarios[1] == 4) {
            //text = "paracetamol 240 mg zetpil rect  zo nodig  1 zetpil = 240 mg";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "zo nodig", "Prescription.Quantity^ =", "Dose.Quantity"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 3) {
            //text = "ondansetron 2 mg infusievloeistof iv  3 keer per dag 2,5 ml = 5 mg/m2, 7,5 ml/dag = 15 mg/m2/dag";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^ = ", "Dose.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 4) {
            //text = "epinephrine 1 mg infusievloeistof iv  zo nodig 1 keer per 5 min 1 ml = 10 mcg/kg";
            //1 keer per 5min????
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "zo nodig", "Prescription.Frequency", "Prescription.Quantity^ =", "Dose.Quantity"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 5) {
            //text = "morfine 5 mg infusievloeistof iv  in FZ 50 ml (0,1 mg/ ml)  zo nodig , 1 ml/uur = 0,24 mg/ kg/dag";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 6) {
            //text = "ondansetron 2 mg infusievloeistof iv  zo nodig  2,5 ml = 5 mg/m2 in 10 min, 15 ml/uur";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "zo nodig", "Prescription.Quantity^ =", "Dose.Quantity^ in", "Prescription.Time^, ", "Prescription.Rate"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 7) {
            //text = "";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 8) {
            //text = "morfine 5 mg infusievloeistof iv  in FZ 50 ml (0,1 mg/ ml) , 1 ml/uur = 0,24 mg/ kg/dag";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "in", "Drug.SolutionType", "Drug.Quantity", "(", "Substance.DrugConcentration", "), ", "Prescription.Rate^ = ", "Dose.Rate"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 9) {
            //text = "lidocaine 100 mg infusievloeistof iv  1 keer per dag 18 ml = 360 mg in 6 uur, 18 ml/uur = 6 mg/ kg/uur";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^ =", "Dose.Quantity^ in ", "Prescription.Time", "Prescription.Rate^ = ", "Dose.Rate"];
        }
        if (foundScenarios[0] == 1 && foundScenarios[1] == 10) {
            //text = "flucanozol 100 mg infusievloeistof iv  1 keer per dag 25 ml = 50 mg in 1 uur, 25 ml/uur, 25 ml/dag = 5 mg/ kg/dag";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 3) {
            //text = "";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 4) {
            //text = "";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 5) {
            //text = "";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 6) {
            //text = "fysiologisch zout 1000 ml infusievloeistof iv  zo nodig  200 ml = 20 ml/kg in 30 min, 400 ml/uur";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 7) {
            //text = "";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 8) {
            //text = "fysiologisch zout 1000 ml infusievloeistof iv , 40 ml/uur = 96 ml/ kg/dag";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 9) {
            //text = "";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 10) {
            //text = "";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        if (foundScenarios[0] == 2 && foundScenarios[1] == 3) {
            //text = "paracetamol 240 mg zetpil rect 3 keer per dag 1 zetpil = 240 mg, 21 zetpil/week = 72 mg/ kg/dag";
            options = ["Drug.Name", "Substance.Quantity", "Drug.Shape", "Drug.Route", "Prescription.Frequency", "Prescription.Quantity^, ", "Prescription.Total^ = ", "Dose.Total"];
        }
        

        for (var i = 0; i < options.length; i++) {
            var optionSplit = options[i].split("^");
            var val = this.GetValue(optionSplit[0]);
            if (val != "" && val != 0) {
                var val = this.GetValue(optionSplit[0]);
                for (var a = 1; a < optionSplit.length; a++) {
                    val = val + this.GetValue(optionSplit[a]);
                }
                text = text + " " + val;
            }
        }
		if(typeof(Ext.getCmp("Verbalization")) != "undefined"){
			Ext.getCmp("Verbalization").update(text);
		}
        	
        if(this.IsValid){
        	Ext.getCmp("prescribeMedicationFormSaveButton").setDisabled(false);
        	Ext.getCmp("prescribeMedicationFormSaveButton").setText("Opslaan");
        }else{
        	Ext.getCmp("prescribeMedicationFormSaveButton").setDisabled(true)
        	Ext.getCmp("prescribeMedicationFormSaveButton").setText("Niet alle velden<br />	zijn ingevuld.");
        }
    }

    this.GetValue = function(name, postFix) {
        if (typeof (MainForm.getField(name)) == "undefined") {
            return name;
        }
        if (typeof (MainForm.getField(name).getText) == "function") {
            return MainForm.getField(name).getText();
        }
        return MainForm.getField(name).getValue();
    }
}