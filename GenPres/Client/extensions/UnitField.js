/**
 * @class Ext.ux.Unit
 * @extends Ext.util.Observable
 * @author Peter Bartels
 * Creates a Unit control utilized by Ext.ux.form.UnitField
 */
Ext.ux.Unit = Ext.extend(Ext.util.Observable, {
    defaultValue: 0,

    constructor: function(config) {
        Ext.ux.Unit.superclass.constructor.call(this, config);
        Ext.apply(this, config);
        this.mimicing = false;
    },

    init: function(field) {
        this.field = field;

        field.afterMethod('onRender', this.doRender, this);
        field.afterMethod('onEnable', this.doEnable, this);
        field.afterMethod('onDisable', this.doDisable, this);
        field.afterMethod('afterRender', this.doAfterRender, this);
        field.afterMethod('onResize', this.doResize, this);
        field.afterMethod('onFocus', this.doFocus, this);
        field.beforeMethod('onDestroy', this.doDestroy, this);
    },


    doRender: function(ct, position) {
        var el = this.el = this.field.getEl();
        var f = this.field;

        if (!f.wrap) {
            f.wrap = this.wrap = el.wrap({
                cls: "x-form-field-wrap"
            });
        }
        else {
            this.wrap = f.wrap.addClass('x-form-field-wrap');
        }


        var divider = this.wrap.createChild({
            tag: 'div',
            style: 'width:100px; font-size:17px; font-weight:bold; height:25px; position:absolute; ',
            html: this.divider
        });

        this.unitSelector = this.wrap.createChild({
            tag: 'div',
            style: 'width:100px; height:25px; position:absolute; visibility:hidden;'
        });

        if (this.divider != "") {
            divider.setLeft(this.width + 5).setTop(0).show();
            this.unitSelector.setLeft(this.width + 14).setTop(0).show();
        } else {
            this.unitSelector.setLeft(this.width).setTop(0).show();
        }

        var genericStore;

        if (typeof (this.units) == "Array") {

            for (var i = 0; i < this.units.length; i++) {
                this.units[i] = [this.units[i]];
            }

            var genericStore = new Ext.data.ArrayStore({
                fields: ['Name'],
                data: this.units
            });
        } else {
            genericStore = this.units;
        }

        this.unitCombo = new Ext.form.ComboBox({
            store: genericStore,
            displayField: 'Name',
            typeAhead: true,
            height: 20,
            mode: 'local',
            width: 80,
            forceSelection: true,
            triggerAction: 'all',
            emptyText: 'unit...',
            value: this.unitValue,
            selectOnFocus: true,
            renderTo: this.unitSelector
        });

        if (typeof (this.units.length) == "undefined") {
            genericStore.on("load", function() {
                if (genericStore.data.items.length == 1) {
                    this.unitCombo.setValue(genericStore.data.items[0].data.Name);
                    return;
                }
                var unitValue = this.unitCombo.getValue();
                if (genericStore.data.items.length == 3 && genericStore.data.items[1].data.Name == "mg") 
                    if (unitValue == "") 
                        this.unitCombo.setValue(genericStore.data.items[1].data.Name);
                
                if (genericStore.data.items.length == 2 && genericStore.data.items[1].data.Name == "ml") 
                    if (unitValue == "") 
                        this.unitCombo.setValue(genericStore.data.items[1].data.Name);
                        
                //if (unitValue) this.unitCombo.setValue("mg");
            } .createDelegate(this));
        }

        this.setUnitData = function(data) {
            this.unitCombo.getStore().loadData(data, false);
            this.unitCombo.setValue(data[0][0]);
        } .createDelegate(this);

        this.getUnitValue = function() {
            return this.unitCombo.getValue();
        } .createDelegate(this);

        this.setUnitValue = function(val) {
            this.unitCombo.setValue(val);
        } .createDelegate(this);

        this.wrap.setWidth(300);
        this.el.setWidth(300);

        this.field.wrap.setWidth(300);
        this.field.el.setWidth(300);

    },

    doAfterRender: function() {
        var y;
        /*if (Ext.isIE && this.el.getY() != (y = this.trigger.getY())) {
        this.el.position();
        this.el.setY(y);
        }*/
    },

    doEnable: function() {
        if (this.wrap) {
            //this.wrap.removeClass(this.field.disabledClass);
            this.wrap.removeClass("trigger-disabled");

        }

    },

    doDisable: function() {

        if (this.wrap) {
            this.wrap.addClass("trigger-disabled");
            //this.el.removeClass(this.field.disabledClass);
        }


    },

    doResize: function(w, h) {
        if (typeof w == 'number') {
            this.el.setWidth(500);
        }
        this.wrap.setWidth(500);
    },

    doFocus: function() {
        if (!this.mimicing) {
            this.wrap.addClass('x-trigger-wrap-focus');
            this.mimicing = true;
            Ext.get(Ext.isIE ? document.body : document).on("mousedown", this.mimicBlur, this, {
                delay: 10
            });
            this.el.on('keydown', this.checkTab, this);
        }
    },

    // private
    checkTab: function(e) {
        if (e.getKey() == e.TAB) {
            this.unitBlur();
        }
    },

    // private
    mimicBlur: function(e) {
        if (!this.wrap.contains(e.target) && this.field.validateBlur(e)) {
            this.unitBlur();
        }
    },

    // private
    unitBlur: function() {
        this.mimicing = false;
        Ext.get(Ext.isIE ? document.body : document).un("mousedown", this.mimicBlur, this);
        this.el.un("keydown", this.checkTab, this);
        this.field.beforeBlur();
        this.wrap.removeClass('x-trigger-wrap-focus');
        this.field.onBlur.call(this.field);
    },

    // private
    fixPrecision: function(value) {
        var nan = isNaN(value);

        if (!this.field.allowDecimals || this.field.decimalPrecision == -1 || nan || !value) {
            return nan ? '' : value;
        }
        return parseFloat(parseFloat(value).toFixed(this.field.decimalPrecision));
    },

    doDestroy: function() {

        if (this.wrap) {
            this.wrap.remove();
            delete this.field.wrap;
        }

        //TODO add unitselector destroy

        if (this.proxy) {
            this.proxy.remove();
        }
    }
});

//backwards compat
Ext.form.Unit = Ext.ux.Unit;

Ext.ns('Ext.ux.form');

Ext.ux.form.UnitField = Ext.extend(Ext.ux.form.SpinnerField, {
    actionMode: 'wrap',
    deferHeight: true,
    autoSize: Ext.emptyFn,
    onBlur: Ext.emptyFn,
    adjustSize: Ext.BoxComponent.prototype.adjustSize,

	constructor: function(config) {
		var unitFieldConfig = Ext.copyTo({}, config, 'divider,width,units,unitValue,defaultValue');

		var spl = this.unitfield = new Ext.ux.Unit(unitFieldConfig);
        
		var plugins = config.plugins
			? (Ext.isArray(config.plugins)
				? config.plugins.push(spl)
				: [config.plugins, spl])
			: spl;

		Ext.ux.form.UnitField.superclass.constructor.call(this, Ext.apply(config, {plugins: plugins}));
	},

    // private
    getResizeEl: function(){
        return this.wrap;
    },

    // private
    getPositionEl: function(){
        return this.wrap;
    },

    // private
    alignErrorIcon: function(){
        if (this.wrap) {
            this.errorIcon.alignTo(this.wrap, 'tl-tr', [2, 0]);
        }
    },

    validateBlur: function(){
        return true;
    }
});


Ext.reg('unitfield', Ext.ux.form.UnitField);

//backwards compat
Ext.form.UnitField = Ext.ux.form.UnitField;

