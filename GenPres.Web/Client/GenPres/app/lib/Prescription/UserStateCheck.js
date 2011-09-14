Ext.define('GenPres.lib.Prescription.UserStateCheck', {

    singleton: true,

    combinations :[
        ["adminTotal", "adminQuantity", "prescriptionFrequency"]
    ],

    checkStates : function(components){
        var me = this;
        
        var combinations = GenPres.lib.Prescription.UserStateCheck.combinations;

        for(var comp=0;comp<components.length;comp++){
            var stateCount = 0;
            var foundStateCounts = [];
            var foundCombinations = [];
            var component = components[comp];
            
            for(var i=0;i<combinations.length;i++){
                stateCount = 0;
                var containsFieldName = false;
                for(var c=0;c<combinations[i].length;c++){

                    if (component.getState() == GenPres.control.states.user && component.getInputValue() > 0) stateCount++;

                    if(combinations[i][c] == component.name){
                        containsFieldName = true;
                        foundCombinations.push(combinations[i]);
                    }
                }
                if(containsFieldName) {
                    foundStateCounts.push(stateCount);
                }
            }

            for(var i=0;i<foundCombinations.length;i++){
                var foundCombination = foundCombinations[i];
                if(foundStateCounts[i] == foundCombination.length){
                    for(var c=0;c<foundCombination.length;c++){
                        var component =me.getComponent(components, foundCombination[c]);
                        component.setState(GenPres.control.states.calculated);
                        break;
                    }
                }
            }
        }

    },

    getComponent : function(components, componentName){
        for(var comp=0;comp<components.length;comp++){
            if(componentName == components[comp].name){
                return components[comp];
            }
        }
    }

})