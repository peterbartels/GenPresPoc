if (!Ext.global.console) {
    Ext.global.console = {};
}
Ext.applyIf(Ext.global.console, {
    log: Ext.emptyFn,
    debug: Ext.emptyFn,
    info: Ext.emptyFn,
    warn: Ext.emptyFn,
    error: Ext.emptyFn,
    assert: Ext.emptyFn,
    clear: Ext.emptyFn,
    dir: Ext.emptyFn,
    dirxml: Ext.emptyFn,
    trace: Ext.emptyFn,
    group: Ext.emptyFn,
    groupCollapsed: Ext.emptyFn,
    groupEnd: Ext.emptyFn,
    time: Ext.emptyFn,
    timeEnd: Ext.emptyFn,
    exception: Ext.emptyFn,
    table: Ext.emtpyFn,
    
    occurrences: {},
    countOccurrences: function(text) {
        console.occurrences[text] = console.occurrences[text] || 0;
        return ++console.occurrences[text];
    },
    
    markTimeline: function(text) {
        // Check to see if Ajax DynaTrace plugin is enabled
        // http://ajax.dynatrace.com
        if (typeof (_dt_addMark) !== 'undefined') {
            _dt_addMark(text);
        } else {
            console.log(text);
        }    
    },
    
    // we don't get profile and profileEnd in IE6, 7 or 8. MS did add it in IE9.
    // Add a markTimeline in for them.
    profile: function(text) {
        console.markTimeline(text);
    },
    profileEnd: function(text) {
        console.markTimeline(text);
    },
    
    /**
     * Marks the start and end of a particular method within the timeline of
     * profiling tools like Ajax DynaTrace or Google's SpeedTracer. 
     * Example: console.markMethod(Ext.view.Grid, 'refresh');
     * @param {Object} theClass The class you'd like to profile
     * @param {String} method The method you'd like to profile
     */
    markMethod: function(theClass, method) {
        var o = {},
            clsName = Ext.ClassManager.getName(theClass);
        o[method] = function() {
            var identifier = 'Mark:' + clsName + ':' + method,
                times = console.countOccurrences(identifier),
                marker = identifier + ' ' + times;

            console.markTimeline('Begin: ' + marker);
            this.callOverridden(arguments);
            console.markTimeline('End: ' + marker);
        };
        theClass.override(o);
    },
    
    /**
     * Profiles the method of a particular class. Also tracks what occurrence
     * of executing the method is (1st, 2nd, 3rd, etc).
     * Example: console.profileMethod(Ext.view.Grid, 'refresh');
     * @param {Object} theClass The class you'd like to profile
     * @param {String} method The method you'd like to profile
     */
    profileMethod: function(theClass, method) {
        var o = {},
            clsName = Ext.ClassManager.getName(theClass);
        o[method] = function() {
            var identifier = 'Profile:' + clsName + ':' + method,
                times = console.countOccurrences(identifier),
                marker = identifier + ' ' + times;

            console.profile(marker);
            this.callOverridden(arguments);
            console.profileEnd(marker);
        };
        theClass.override(o);
    }
});