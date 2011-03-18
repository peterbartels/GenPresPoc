Ext.namespace("View");

View.PatientInfo = function(){
    
    this.GetPanel = function(){
        if(typeof this.patientInfoPanel != "undefined") return this.patientInfoPanel;
        this.patientInfoPanel = new Ext.Panel({
            layout:'vbox',
            region:'north',
            margins : "0 0 0 0",
            height:300,
            resizable:true,
            split: true,
            items:[{html:CreateContentHeader("Patient informatie"), width:200}]
        });
        
        return this.patientInfoPanel;
    }
    
    this.SetPatient = function(pid,name){
    	
		this.patientInfoPanel.removeAll(true);
		this.patientInfoPanel.add({html:CreateContentHeader(""), width:250, height:45});
        this.patientInfoPanel.add(GetUI());
        this.patientInfoPanel.doLayout();
        
    }
    
    return this;
}

function GetUI() {
	    var tpl = [];
	    if(GenPres.SelectedPatient.PID != "" && GenPres.SelectedPatient.PID != "0") {
	    	var patientNameInfo = '<div>' + 
	            '<div class="patientIcon">&nbsp;</div>' +
	            	'<div class="patientNameInfo">' + 
	            		'<b>' + GenPres.SelectedPatient.LastName + ', ' + GenPres.SelectedPatient.FirstName + '</b><br />' +
	            		GenPres.SelectedPatient.PID + '<br />' +
	            		GenPres.SelectedPatient.Birthdate
        			'</div>' +
	           	+ '</div>';
	        var patientOtherInfo =  
	            	'<div class="patientInfoValue">' + 
	            		'<div class="patientInfoHeader">Afdeling/bed:</div>' + GenPres.SelectedPatient.Unit + " - " + GenPres.SelectedPatient.Bed  + '<br />' +
	            		'<div class="patientInfoHeader">Opname:</div>' + GenPres.SelectedPatient.AddmissionDate + '<br />' +
	            		'<div class="patientInfoHeader">Ligdag:</div>' + GenPres.SelectedPatient.Days +
        			'</div>';
	    	tpl.push({html:patientNameInfo,width:190});
	    	tpl.push({html:patientOtherInfo});
	    }else{
	    	tpl.push({html:"<center><b>Geen patient geselecteerd</b></center>"});
	    }
    return tpl;
	    //return new Ext.Container({items:{html:tpl},id:"patientInfo"});
}


