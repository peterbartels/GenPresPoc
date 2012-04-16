Ext.define('GenPres.test.control.UnitValueFieldTest', {

    describe: 'GenPres.control.UnitValueField',

    tests: function () {
        var me = this, instance, win;

        me.getViewForControl = function (config) {
            if (!instance) {
                var store = Ext.create('Ext.data.ArrayStore', {
                    autoDestroy: true,
                    fields: ['Value'],
                    data : [['g'],['mg'],['microg']]
                });
                instance = Ext.create('GenPres.control.UnitValueField', {
                    fieldLabel: 'Test',
                    labelAlign:'left',
                    unitStore:store,
                    name:'test'
                })
            }
            return instance;
        };

        me.createTestWindow = function(){
            win = Ext.create('Ext.Window', {
                items:me.getViewForControl(),
                padding:'50 50 50 50',
                border:0,
                height:520,
                width:900
            });
            win.show();
            return win;
        };
        it('can be created', function () {
            expect(me.getViewForControl()).toBeDefined();
        });

        it('can be rendered', function () {
            expect(me.createTestWindow()).toBeDefined();
        });
        it('has a setValue function', function () {
            expect(typeof(me.getViewForControl().setValue) == "function").toBeTruthy();
        })
        it('has a getValue function', function () {
            expect(typeof(me.getViewForControl().getValue) == "function").toBeTruthy();
        })
        it('can set a and get value', function () {
            me.getViewForControl().setValue({
                value:200,
                unit:'mg'
            });
            var value = me.getViewForControl().getValue();
            expect(value.value == 200 && value.unit == "mg").toBeTruthy();
        });
        it('has a setState function', function () {
            expect(typeof(me.getViewForControl().setState) == "function").toBeTruthy();
        })
        it('can set state to user', function () {
            me.getViewForControl().setState(GenPres.control.states.calculated);
            var value = me.getViewForControl().getValue();
            expect(me.getViewForControl().state == GenPres.control.states.calculated).toBeTruthy();
        });
        it('has a getInputEl function', function () {
            expect(typeof(me.getViewForControl().getInputEl) == "function").toBeTruthy();
        })
        it('has a setInputColor function', function () {
            expect(me.getViewForControl().setInputColor).toBeDefined();
        })
        it('can set input color to orange', function () {
             me.getViewForControl().setInputColor("orange");
            expect(me.getViewForControl().getInputEl().style.borderColor == "orange").toBeTruthy();
        });
        it('can set state to user', function () {
            me.getViewForControl().setState(GenPres.control.states.user);
            expect(me.getViewForControl().getInputEl().style.borderColor == "lime").toBeTruthy();
        });
        it('has a setHidden function', function () {
            expect(me.getViewForControl().setHidden).toBeDefined();
        })
        it('can be set hidden', function () {
            me.getViewForControl().setHidden(true);
        });
        it('can be set hidden', function () {
            me.getViewForControl().setHidden(true);
            expect(me.getViewForControl().getEl().dom.style.visibility == "hidden").toBeTruthy();
        });
        it('can be set visible', function () {
            me.getViewForControl().setHidden(false);
            expect(me.getViewForControl().getEl().dom.style.visibility == "").toBeTruthy();
        });
        it('can be destroyed', function () {
            me.getViewForControl().destroy();
            expect(me.getViewForControl().isDestroyed).toBeTruthy();
            win.close();
        });

    }
});