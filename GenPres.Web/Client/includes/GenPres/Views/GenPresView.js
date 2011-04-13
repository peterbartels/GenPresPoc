
var CreateContentHeader = function(caption){
	return '<div class="contentbox"><div class="contentbox_left_top" ></div><div class="contentbox_right_top" ></div><div class="contentbox_inner" >'+caption+'</div></div>';
}

GenPresView = function() {
    this.patientTree = new View.PatientTree();
    this.patientInfo = new View.PatientInfo();
    this.prescribeMedicationForm = new View.PrescribeMedicationForm(this);
    this.prescribeMedicationGrid = new View.PrescribeMedicationGrid(this);
    this.TPNForm = new View.TPNForm(this);
    
    this.totalView = new View.Total();
    this.overview = new View.Overview();
	
    this.tabs = new Ext.TabPanel({
        activeTab: 0,
        region: 'south',
        split: true,
        height: 293,
        items: [
            this.prescribeMedicationGrid,
            this.totalView.GetPanel(),
            this.overview.GetPanel()
        ]
    });
	
    this.patientInfoGroup = new Ext.ButtonGroup({
        columns: 1,
        title:'Patient',
        width:400,
        height:20,
        items : [{width:200, html:'',xtype:'box'}]
	});
	
	this.userInfoGroup = new Ext.ButtonGroup({
    	columns: 1,
    	title:'Medewerker',
    	items:[{width:198,height:56,html:'',xtype:'box'}]
	});

	this.disableButtons = function(disable){
		Ext.getCmp('tbClearButton').setDisabled(disable);
		Ext.getCmp('tbNewMedicineButton').setDisabled(disable);
		Ext.getCmp('tbTPNButton').setDisabled(disable);
	}
	
	
    this.OpenTPN = function(){
    	this.TPNForm.Show(this.prescribeMedicationForm.prescribeMedicationPanel);
    }
    
	this.home = function(){
    	this.prescribeMedicationForm.GoHome();
    	GenPres.SelectedPatient = {
                Unit: "",
                Bed: "",
                FirstName: "",
                LastName: "",
                Birthdate: "",
                Age: "",
                WeightGuess: "",
                Weight:0,
                Length: 0,
                WeightMedication: "",
                WeightActual: "",
                WeightAddmission: "",
                WeightBirth: "",
                LengthGuessed: "",
                LengthActual: "",
                AddmissionDate: "",
                CurrentDate: "",
                Days: "",
                PID: ""
            }
    	
    	this.UpdatePatient("", "");
    	this.prescribeMedicationForm.IsTemplate = true;
	}

	//this.patientInfo.GetPanel()
    var viewport = new Ext.Viewport({
        layout: 'border',
        items: [
            new Ext.Panel({
				layout : 'vbox',
				width:'250',
	            region: 'west',
	            baseCls: 'x-blue',
				items : [
					{xtype:'panel',height:70,baseCls: 'x-bubble',width:100,frame:true,html: '<img src="Client/images/logo.png" style="background-color:white;margin-top:0px;margin-left:15px;" />', width:240,margins: "5 5 5 5", id:'logo'},
					this.patientTree.patientTreePanel
				]
            }),{
	            region: 'center',
				layout:'border',
	            items: [
	            	new Ext.Panel({region:'north',height:60,tbar: {
	            		id:'buttonGroups',
	            		items:[
	            			{xtype: 'buttongroup',
					        columns: 5,
					        title:'Algemeen',
					        items:[
	            				{width:60,iconAlign: "top", scale: 'large', text: "Home", id:'tbHomeButton', disabled: false, icon: 'client/images/Home_32.png',
		            				handler:this.home.createDelegate(this)
		            			},
		            			{xtype: 'tbseparator',height:20},
	            				{width:60,iconAlign: "top", scale: 'large', text: "Voorschriften", id:'tbClearButton', disabled: true, icon: 'client/images/Prescription_32.png',
		            				handler:this.prescribeMedicationForm.clear.createDelegate(this.prescribeMedicationForm)
	            				},
		            			{xtype: 'tbseparator',height:20},
	            				{width:60,iconAlign: "top", scale: 'large', text: "TPN Blad", id:'tbTPNButton', disabled: true, icon: 'client/images/TPN_32.png',
		            				handler:this.OpenTPN.createDelegate(this)
	            				}
            				]},
            				{xtype: 'tbseparator',height:50},
            				{xtype: 'buttongroup',
					        columns: 3,
					        title:'Opties',
					        items : [
	            				{width:60,iconAlign: "top", scale: 'large', text: "Sjablonen", id:'tbTemplateButton', disabled: false, icon: 'client/images/Template_32.png',
		            				handler:this.prescribeMedicationForm.newTemplate.createDelegate(this.prescribeMedicationForm)
	            				},
	            				{xtype: 'tbseparator',height:20},
	            				{width:60,iconAlign: "top", scale: 'large', text: "Nieuw medicament", id:'tbNewMedicineButton', disabled: true, icon: 'client/images/NewMedicine_32.png',
		            				handler:this.prescribeMedicationForm.NewMedicine.createDelegate(this.prescribeMedicationForm)
	            				}
            				]},this.patientInfoGroup,this.userInfoGroup
	            		]}
	            	}),new Ext.Panel({
						layout: 'border',
						id:'mainPanel',
						tbar: {
							items:[
								{width:60, scale: 'small', text: "Nieuw", id:'tbNewButton', disabled: false, icon: 'client/document_new.png',
		            				handler:this.prescribeMedicationForm.clear.createDelegate(this.prescribeMedicationForm)
		            			},
								{xtype:'box',html:'<div id="infoBar">Geen patient geselecteerd.</div>'}
							]
						},
						border: true,
						xtype:'panel',
						margins: "0 0 0 0",
						region: 'center',
						items: [
							this.prescribeMedicationForm.GetPanel(),
							{xtype:'box',html:'', region:'center'},
							this.tabs
						]
					})
				]
            }
        ]
    });
    
    this.SetPatient = function(){
		
	    this.patientInfoGroup = new Ext.ButtonGroup({
	        columns: 1,
	        title:'Patient',
	        width:400,
	        items : [new Ext.Panel({width:328, padding:"3 3 3 3", height:56,items:[GetUI()]})]
		});
		
		Ext.getCmp('buttonGroups').remove(3);
		Ext.getCmp('buttonGroups').remove(4);
		Ext.getCmp('buttonGroups').add(this.patientInfoGroup);
		Ext.getCmp('buttonGroups').add(this.userInfoGroup);
		
		Ext.getCmp('buttonGroups').doLayout();
		
    }
    
    this.UpdatePatient = function(pid, name) {
        GenPres.GridStore.baseParams.patientID = GenPres.SelectedPatient.PID;
        GenPres.GridStore.reload();
        GenPres.TotalStore.baseParams.patientID = GenPres.SelectedPatient.PID;
        GenPres.TotalStore.reload();
        
        this.SetPatient();
        document.getElementById('infoBar').innerHTML = "";
        document.getElementById('infoBar').innerHTML = GenPres.SelectedPatient.LastName + ", " + GenPres.SelectedPatient.FirstName;
        
        if(pid != "" && pid != "0") {
        	this.disableButtons(false);
        }
        if(pid == "0") {
        	this.patientTree.clearSelection();
        	this.disableButtons(true);
        }
        if(pid == "") {
        	this.patientTree.clearSelection();	
        	document.getElementById('infoBar').innerHTML = "Geen patient geselecteerd."
        	this.disableButtons(true);
        }else{
        	this.prescribeMedicationForm.SetPatient(pid, name);
        }
    }
    this.SetPatient();
}

var noPatientSelected = {margins:"0 0 0 0", html:'<div style="padding:5px;font-size:14px;"><b>Welkom bij GenPres Beta</b><br /><br />GenPres is een product ontwikkeld door Dr. Casper Bollen en Peter Bartels.</div>',xtype:'box'};