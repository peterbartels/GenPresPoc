Ext.namespace("View");
Ext.namespace("View.PrescribeMedicationForm");

View.PrescribeMedicationForm.MainForm = function(PrescribeMedicationForm){

	this.UpdateForm = PrescribeMedicationForm.UpdateData;
	this.SpinUpdate = PrescribeMedicationForm.IncrementField.createDelegate(PrescribeMedicationForm);
	this.SetLatestControlChanged = PrescribeMedicationForm.SetLatestControlChanged.createDelegate(PrescribeMedicationForm);
	this.GetLatestControlChanged = PrescribeMedicationForm.GetLatestControlChanged.createDelegate(PrescribeMedicationForm);
	this.save = PrescribeMedicationForm.save.createDelegate(PrescribeMedicationForm);;
	this.UpdatePatientData = PrescribeMedicationForm.UpdatePatientData.createDelegate(PrescribeMedicationForm);;;
	
	this.setCalculatedState = function (field){
		var stateMan = PrescribeMedicationForm.StateManager;	
		stateMan.SetState(field,stateMan.state.Calculated);
	}
	this.setUserState = function (field){
		var stateMan = PrescribeMedicationForm.StateManager;
		stateMan.SetState(field,stateMan.state.User);
	}
	
	this.MedicineView = new View.PrescribeMedicationForm.Medicine(this);
	this.PrescriptionView = new View.PrescribeMedicationForm.Prescription(this);
	this.DosageView = new View.PrescribeMedicationForm.Dosage(this);
	this.AdministrationView = new View.PrescribeMedicationForm.Administration(this);
	
	this.formEl = new Ext.Container({
		labelAlign: 'top',
		layout: 'fit',
		items: [{
			layout: 'column',
			items: [{
				columnWidth: 1,
				items: [
					this.MedicineView.formEl, 
					this.PrescriptionView.formEl, 
					this.DosageView.formEl, 
					this.AdministrationView.formEl
				]
			}]
		}]
	});	
	return this;
}