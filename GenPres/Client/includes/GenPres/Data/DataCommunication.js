DataProvider = function() {

	this.send = 0;
	this.returned = 0;
	this.timer = 0;
	this.queue = [];
	this.busy = false;
	this.intervalStarted = false;
	this.interval = null;
	
	this.GetTPNPrescriptions = function(returnFunc){
		TPN.GetTPNPrescriptions(GenPres.SelectedPatient.PID, returnFunc);
	}
	
    this.StopPrescription = function(id, status){
	    Prescription.StopPrescription(id, status, function(){
	    	GenPres.GridStore.reload();
	    	GenPres.TotalStore.reload();
	    });
    }
    
    this.UpdatePatientData = function(weight, length){
    	Prescription.UpdatePatientData(GenPres.SelectedPatient.PID, weight, length, function(){});
    }
    
    this.UpdateTPN =  function(prescription, drug, components, save, returnFunction) {
    	TPN.UpdateTPN(
    		GenPres.SelectedPatient.PID,
    		save,
    		prescription,
    		drug,
    		components,
    		returnFunction
    	);
    }
    this.UpdatePrescription = function(prescription, dose, drug, component, substance, save, returnFunction) {
		
    	var obj = {
    		prescription : prescription,
    		dose:dose,
    		drug:drug,
    		component:component,
    		substance:substance,
    		save:save,
    		returnFunction:returnFunction
    	}
    	if(!this.busy) this.queue = [];
    	
		this.queue.push(obj);
		
		//console.log(this.queue.length);
		
        var patient = {}
        patient.PID = GenPres.SelectedPatient.PID;
        patient.id = GenPres.SelectedPatient.id;
        
        if(this.queue.length == 1){
			this.ProcessQueue();
        }else{
        	this.startInterval();
        }
    }
    
    this.startInterval = function(){
    	var func = this.processQueueWait.createDelegate(this);
    	if(this.busy){
    		if(this.interval!=null) clearTimeout(this.interval);
    		this.interval = setTimeout(func, 400);
    	}
    }
    
    this.processQueueWait = function(){
    	this.ProcessQueue();
    	this.emptyQueue();
    }
    this.emptyQueue = function(){
    	if(!this.busy) this.queue = [];
    }
    
    this.ProcessQueue = function(){
		var obj = this.queue[this.queue.length - 1];
		Prescription.UpdatePrescription(
	    	obj.prescription,
			obj.dose,
			obj.drug,
			obj.component,
			obj.substance,
			GenPres.SelectedPatient.PID,
			obj.save,
	        obj.returnFunction
	    );
	    this.busy = false;
	    var func = this.emptyQueue.createDelegate(this);
	    this.interval = setTimeout(func, 400);
    }
    
    this.GetPrescriptionById = function(id, returnFunction) {
        Prescription.GetPrescriptionById(id, GenPres.SelectedPatient.PID, returnFunction);  
    }

    this.ClearPrescription = function(returnFunction) {
        Prescription.ClearPrescription(
        	GenPres.SelectedPatient.PID, 
        	((GenPres.SelectedPatient.Weight > 0) ?  GenPres.SelectedPatient.Weight.toString() : 0),
			((GenPres.SelectedPatient.Length > 0) ?  GenPres.SelectedPatient.Length.toString() : 0),
        	returnFunction
    	);
    }
}

