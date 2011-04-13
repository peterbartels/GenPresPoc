Ext.namespace("Logic");


Logic.UnitConversion = function(){
    
}


Logic.UnitVolume = Ext.extend(Logic.UnitConversion, {
    unitKeys : {
        l : ['l', 1],
        ml : ['ml', 1/1000]
    },
    
    baseUnit : 'l',
    
    GetBaseUnitValue : function(value, unit){
        return value * this.unitKeys[unit][1];
    },
    GetUnitValue : function(value, unit){
        return value / this.unitKeys[unit][1];
    }
});


Logic.UnitMass = Ext.extend(Logic.UnitConversion, {
    unitKeys : {
        kg : ['kg', 1000],
        gram : ['gram', 1],
        mg : ['mg', 1/1000],
        mcg : ['mcg', 1/(1000*1000)],
        nanog : ['nanog',1/(1000*1000*1000)]
    },
    
    baseUnit : 'gram',
    
    GetBaseUnitValue : function(value, unit){
        return value * this.unitKeys[unit][1];
    },
    GetUnitValue : function(value, unit){
        return value / this.unitKeys[unit][1];
    }
});


Logic.UnitTime = Ext.extend(Logic.UnitConversion, {
    unitKeys : {
        jaar : ['jaar',  60 * 60 * 24 * 7 * 52],
        maand : ['maand', 60 * 60 * 24 * 7 * 30],
        week : ['week', 60 * 60 * 24 * 7],
        dag : ['dag', 60*60*24],
        uur : ['uur', 60*60],
        min : ['min', 60],
        s : ['s', 1]
    },
    
    baseUnit : 's',
    
    GetBaseUnitValue : function(value, unit){
        return value * this.unitKeys[unit][1];
    },
    
    GetUnitValue : function(fromUnit, toUnit){
        return value / this.unitKeys[unit][1];
    }
});

function getUnitValue(value, unitvalue, unitvalue2){
    var time = new Logic.UnitTime();
    var mass = new Logic.UnitMass();
    var volume = new Logic.UnitVolume();
    var result = value;
    result2 = 1;
    
    if(typeof time.unitKeys[unitvalue] != "undefined"){
        result =  time.GetUnitValue(value, unitvalue);
    }
    if(typeof mass.unitKeys[unitvalue] != "undefined"){
        result = mass.GetUnitValue(value, unitvalue);
    }
    if(typeof volume.unitKeys[unitvalue] != "undefined"){
        result = volume.GetUnitValue(value, unitvalue);
    }
    
    /*if(typeof unitvalue2 != "undefined"){
        debugger
        if(typeof time.unitKeys[unitvalue2] != "undefined"){
            result2 = time.GetBaseUnitValue(result, unitvalue2);
        }
        if(typeof mass.unitKeys[unitvalue2] != "undefined"){
            result2 = mass.GetBaseUnitValue(result, unitvalue2);
        }
        if(typeof volume.unitKeys[unitvalue2] != "undefined"){
            result2 = volume.GetBaseUnitValue(result, unitvalue2);
        }    
    }*/
    
    return result;
}

function getBaseUnitValue(value, unitvalue, unitvalue2){
    var time = new Logic.UnitTime();
    var mass = new Logic.UnitMass();
    var volume = new Logic.UnitVolume();
    var result = value;
    result2 = 1;/*
    if(typeof unitvalue2 != "undefined"){
        
        if(typeof time.unitKeys[unitvalue2] != "undefined"){
            result2 = time.GetBaseUnitValue(result, unitvalue2);
        }
        if(typeof mass.unitKeys[unitvalue2] != "undefined"){
            result2 = mass.GetBaseUnitValue(result, unitvalue2);
        }
        if(typeof volume.unitKeys[unitvalue2] != "undefined"){
            result2 = volume.GetBaseUnitValue(result, unitvalue2);
        }    
    }*/
    if(typeof time.unitKeys[unitvalue] != "undefined"){
        result =  time.GetBaseUnitValue(value, unitvalue);
    }
    if(typeof mass.unitKeys[unitvalue] != "undefined"){
        result = mass.GetBaseUnitValue(value, unitvalue);
    }
    if(typeof volume.unitKeys[unitvalue] != "undefined"){
        result = volume.GetBaseUnitValue(value, unitvalue);
    }
    
    return result;
}