/**
 * Load the library located at the same path with this file
 * 此文件会默认自动加载ext-all-debug.js文件
 * 1.当主机名是localhost
 * 2.当主机名是ipv4地址
 * 3.协议是file协议
 * 4.带有debug参数的
 * 例如(http://foo/test.html?debug)
 *
 * 1.如果在url后加nodebug即可加载ext-all.js文件
 * 例如(http://foo/test.html?nodebug)
 */
(function() {
    var scripts = document.getElementsByTagName('script'),
        localhostTests = [
            /^localhost$/,
            /\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(:\d{1,5})?\b/ // IP v4
        ],
        host = window.location.hostname,
        isDevelopment = null,
        queryString = window.location.search,
        test, path, i, ln, scriptSrc, match;

    for (i = 0, ln = scripts.length; i < ln; i++) {
        scriptSrc = scripts[i].src;

        match = scriptSrc.match(/ext-bootstrap\.js$/);

        if (match) {
            /**
             * use a path without the ext-bootstrap.js file on it. http://path/to/ext/ext-bootstrap.js will become
             * http://path/to/ext/
             */
            path = scriptSrc.substring(0, scriptSrc.length - match[0].length);
            break;
        }
    }

    if (isDevelopment === null) {
        for (i = 0, ln = localhostTests.length; i < ln; i++) {
            test = localhostTests[i];

            if (host.search(test) !== -1) {
                //host is localhost or an IP address
                isDevelopment = true;
                break;
            }
        }
    }

    if (isDevelopment === null && window.location.protocol === 'file:') {
        isDevelopment = true;
    }

    if (!isDevelopment && queryString.match('(\\?|&)debug') !== null) {
        //debug is present in the query string
        isDevelopment = true;
    } else if (isDevelopment && queryString.match('(\\?|&)nodebug') !== null) {
        //nodebug is present in the query string
        isDevelopment = false;
    }

    document.write('<script type="text/javascript" charset="UTF-8" src="' +
        path + 'ext-all' + (isDevelopment ? '-debug' : '') + '.js"></script>');
})();