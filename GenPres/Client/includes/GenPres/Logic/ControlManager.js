
ControlManager = function(form) {

    this.ControlTypes = {
        Select: 0,
        UnitField: 1,
        Checkbox: 2
    };

    this.controls = {};

    this.getForm = function() {
        return form;
    }
    this.SetValue = function(fieldname, value, setAsUserState, allowZeroValues) {
        
        var isUnit = (fieldname.match(/Unit$/) != null) ? true : false;
        //if(value == 0 && !allowZeroValues) return;
        if (isUnit) {
            fieldname = fieldname.replace(/Unit$/, "")
            if (typeof form[fieldname] == "undefined") { debugger; }
            return form[fieldname].unitfield.setUnitValue(value);
        }
        var isSelect = (fieldname.match(/Select$/) != null || fieldname.match(/Time$/) != null || fieldname.match(/Adjust$/) != null) ? true : false;
        if (isSelect) {
            return form[fieldname].setValue(value);
        }

        if (typeof form[fieldname] == "undefined") { debugger; }

        var result = value;

        if (!setAsUserState) {
            if (value != 0 && this.getState(fieldname) != Logic.StateManager.states.User) this.changeState(fieldname, Logic.StateManager.states.Calculated);
        } else {
            if (value != 0) this.changeState(fieldname, Logic.StateManager.states.User);
        }
        if (value == 0) this.changeState(fieldname, Logic.StateManager.states.NotSet);
        return form[fieldname].setValue(result);
    }

    this.GetValue = function(fieldname) {
        
        var isUnit = (fieldname.match(/Unit$/) != null) ? true : false;

        if (this.getState(fieldname) == Logic.StateManager.states.Calculated) return 0;

        if (isUnit) {
            fieldname = fieldname.replace(/Unit$/, "")
            if (typeof form[fieldname] == "undefined") { debugger; }
            return form[fieldname].unitfield.getUnitValue();
        }
        if (typeof form[fieldname] == "undefined") { debugger; }

        var value = form[fieldname].getValue();

        return value;
    }


    this.AddStateChanger = function(fieldname) {
        var field = form[fieldname];
        if (typeof (field.getEl()) != "undefined") {
            var parent = Ext.get(field.getEl().dom.parentNode);

            var stateChangeDiv = parent.createChild({
                tag: 'div',
                "class": 'stateChangeElement',
                style: 'width:5px; height:5px; overflow:hidden; position:absolute; background-color:green;top:0px;left:0px; cursor:pointer'
            });
            stateChangeDiv.on("click", function(event, dom, obj, fieldname) {
                this.changeState(fieldname, Logic.StateManager.states.User);
            } .createDelegate(this, [fieldname], true));
        }
    }
    this.updateField = function(fieldname, state) {
        var color = "red";

        var field = form[fieldname];

        if (typeof (field) == "undefined")
            return;

        if (state == Logic.StateManager.states.NotSet) {
            color = "green";
        }
        if (state == Logic.StateManager.states.User) {
            color = "red";
        }
        if (state == Logic.StateManager.states.Calculated) {
            color = "orange";
        }

        var parent = Ext.get(field.getEl().dom.parentNode);
        if (parent.child(".stateChangeElement") != null) parent.child(".stateChangeElement").dom.style.backgroundColor = color;
    }

    this.changeState = function(fieldname, state) {
        //If the fieldname end with "Unit" or "Total", this means it's a selectbox, we use to have changing states for selectboxes
        //but at this moment we decided to remove it
        //if(fieldname.match(/Unit$/) != null || fieldname.match(/Total$/) != null || fieldname.match(/Time$/) != null) return;

        //fieldname = fieldname.replace(/Unit$/,"")
        Logic.StateManager.fields[fieldname] = state;
        this.updateField(fieldname, state)
    }

    this.getState = function(fieldname) {
        return Logic.StateManager.fields[fieldname];
    }

    this.setUnitFieldValue = function(fieldname, value) {
        form[fieldname].unitfield.setUnitValue(value)
    }
    this.setComboBoxValue = function(fieldname, value) {
        form[fieldname].setValue(value)
    }
}

