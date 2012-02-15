(function() {
    var scripts = document.getElementsByTagName('script'),
        rootPath, i, ln, scriptSrc, match, bootstrap;

    for (i = 0, ln = scripts.length; i < ln; i++) {
        scriptSrc = scripts[i].src;

        match = scriptSrc.match(/bootstrap\.js$/);

        if (match) {
            rootPath = scriptSrc.substring(0, scriptSrc.length - match[0].length);
            break;
        }
    }

    bootstrap = this.ExtBootstrap = {
        rootPath: rootPath,

        disableCaching: window.location.search.match('(\\?|&)nocache') !== null,

        loadScript: function(path) {
            document.write('<script type="text/javascript" src="' + rootPath + path +
                           ((this.disableCaching) ? ('?' + (new Date()).getTime()) : '') + '"></script>');
        }
    };

    bootstrap.loadScript('bootstrap/data.js');
    bootstrap.loadScript('bootstrap/core.js');
})();
