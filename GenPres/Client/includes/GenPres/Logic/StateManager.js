Ext.namespace("Logic");


Logic.StateManager = function() {
    //Different states for fields and controls
    this.state = {
        NotSet: 0,
        User: 1,
        Calculated: 2,
        Dirty: 3
    };

    //is automatically set bij eventmanager!!
    this.fields = {}
	
	this.SetState = function(controlName, state){
		this.fields[controlName] = state;
	}
	
	this.checkUserStateCount = function(combinations, name){
		var foundCombination;
		
		if(this.GetState(name) != this.state.Calculated) return;
		
		for(var i=0;i<combinations.length;i++){
			var stateCount = 0;
			var containsFieldName = false;
			for(var c=0;c<combinations[i].length;c++){
			    if (this.GetState(combinations[i][c]) == this.state.User) stateCount++;
				if(combinations[i][c] == name){
					containsFieldName = true;
					foundCombination = combinations[i];
				}
			}
			if(containsFieldName) break;
		}
		
		if(stateCount == 2){
			//this.SetState(foundCombination[0], this.state.Calculated);
			//this.SetState(name, this.state.User);
		}else{
			//this.SetState(foundCombination[0], this.state.Calculated);
			//this.SetState(name, this.state.User);
		}
		return 0;
	}
	
	this.GetState = function(controlName) {
	    return this.fields[controlName] || this.state.User;
	}
	
	this.TrySetUserState = function(controlName, value){
		if (value == 0) {
			this.fields[controlName] = this.state.User;
			return false;
		}
		
		if(this.fields[controlName] != this.state.Calculated){
			this.fields[controlName] = this.state.User;
			return true;
		}
		
		return false;
	}
	
	this.TrySetCalculated = function(controlName, value){
		
		if (value == 0) {
			this.fields[controlName] = this.state.User;
			return false;
		}
		
		if(this.fields[controlName] != this.state.User){
			this.fields[controlName] = this.state.Calculated;
			return true;
		}
		return false;
	}
	
	this.IsCalculated = function(controlName) {
		return  (this.fields[controlName] == this.state.Calculated);
	}
}
