;
!
function(win) {
	"use strict";
	var Lay = function() {
			this.v = '1.0.9_rls'
		};
	Lay.fn = Lay.prototype;
	var doc = document,
		config = Lay.fn.cache = {},
		getPath = function() {
			var js = doc.scripts,
				jsPath = js[js.length - 1].src;
			return jsPath.substring(0, jsPath.lastIndexOf('/') + 1)
		}(),
		error = function(msg) {
			win.console && console.error && console.error('Layui hint: ' + msg)
		},
		isOpera = typeof opera !== 'undefined' && opera.toString() === '[object Opera]',
		modules = {
			layer: 'modules/layer',
			laydate: 'modules/laydate',
			laypage: 'modules/laypage',
			laytpl: 'modules/laytpl',
			layim: 'modules/layim',
			layedit: 'modules/layedit',
			form: 'modules/form',
			upload: 'modules/upload',
			tree: 'modules/tree',
			table: 'modules/table',
			element: 'modules/element',
			util: 'modules/util',
			flow: 'modules/flow',
			carousel: 'modules/carousel',
			code: 'modules/code',
			jquery: 'modules/jquery',
			mobile: 'modules/mobile',
			'layui.all': 'dest/layui.all'
		};
	config.modules = {};
	config.status = {};
	config.timeout = 10;
	config.event = {};
	Lay.fn.define = function(deps, callback) {
		var that = this,
			type = typeof deps === 'function',
			mods = function() {
				typeof callback === 'function' && callback(function(app, exports) {
					layui[app] = exports;
					config.status[app] = true
				});
				return this
			};
		type && (callback = deps, deps = []);
		if (layui['layui.all'] || (!layui['layui.all'] && layui['layui.mobile'])) {
			return mods.call(that)
		}
		that.use(deps, mods);
		return that
	};
	Lay.fn.use = function(apps, callback, exports) {
		var that = this,
			dir = config.dir = config.dir ? config.dir : getPath;
		var head = doc.getElementsByTagName('head')[0];
		apps = typeof apps === 'string' ? [apps] : apps;
		if (window.jQuery && jQuery.fn.on) {
			that.each(apps, function(index, item) {
				if (item === 'jquery') {
					apps.splice(index, 1)
				}
			});
			layui.jquery = jQuery
		}
		var item = apps[0],
			timeout = 0;
		exports = exports || [];
		config.host = config.host || (dir.match(/\/\/([\s\S]+?)\//) || ['//' + location.host + '/'])[0];
		if (apps.length === 0 || (layui['layui.all'] && modules[item]) || (!layui['layui.all'] && layui['layui.mobile'] && modules[item])) {
			return onCallback(), that
		}
		function onScriptLoad(e, url) {
			var readyRegExp = navigator.platform === 'PLaySTATION 3' ? /^complete$/ : /^(complete|loaded)$/if (e.type === 'load' || (readyRegExp.test((e.currentTarget || e.srcElement).readyState))) {
				config.modules[item] = url;
				head.removeChild(node);
				(function poll() {
					if (++timeout > config.timeout * 1000 / 4) {
						return error(item + ' is not a valid module')
					};
					config.status[item] ? onCallback() : setTimeout(poll, 4)
				}())
			}
		}
		var node = doc.createElement('script'),
			url = (modules[item] ? (dir + 'lay/') : (config.base || '')) + (that.modules[item] || item) + '.js';
		node.async = true;
		node.charset = 'utf-8';
		node.src = url +
		function() {
			var version = config.version === true ? (config.v || (new Date()).getTime()) : (config.version || '');
			return version ? ('?v=' + version) : ''
		}();
		if (!config.modules[item]) {
			head.appendChild(node);
			if (node.attachEvent && !(node.attachEvent.toString && node.attachEvent.toString().indexOf('[native code') < 0) && !isOpera) {
				node.attachEvent('onreadystatechange', function(e) {
					onScriptLoad(e, url)
				})
			} else {
				node.addEventListener('load', function(e) {
					onScriptLoad(e, url)
				}, false)
			}
		} else {
			(function poll() {
				if (++timeout > config.timeout * 1000 / 4) {
					return error(item + ' is not a valid module')
				};
				(typeof config.modules[item] === 'string' && config.status[item]) ? onCallback() : setTimeout(poll, 4)
			}())
		}
		config.modules[item] = url;

		function onCallback() {
			exports.push(layui[item]);
			apps.length > 1 ? that.use(apps.slice(1), callback, exports) : (typeof callback === 'function' && callback.apply(layui, exports))
		}
		return that
	};
	Lay.fn.getStyle = function(node, name) {
		var style = node.currentStyle ? node.currentStyle : win.getComputedStyle(node, null);
		return style[style.getPropertyValue ? 'getPropertyValue' : 'getAttribute'](name)
	};
	Lay.fn.link = function(href, fn, cssname) {
		var that = this,
			link = doc.createElement('link');
		var head = doc.getElementsByTagName('head')[0];
		if (typeof fn === 'string') cssname = fn;
		var app = (cssname || href).replace(/\.|\//g, '');
		var id = link.id = 'layuicss-' + app,
			timeout = 0;
		link.rel = 'stylesheet';
		link.href = href + (config.debug ? '?v=' + new Date().getTime() : '');
		link.media = 'all';
		if (!doc.getElementById(id)) {
			head.appendChild(link)
		}
		if (typeof fn !== 'function') return;
		(function poll() {
			if (++timeout > config.timeout * 1000 / 100) {
				return error(href + ' timeout')
			};
			parseInt(that.getStyle(doc.getElementById(id), 'width')) === 1989 ?
			function() {
				fn()
			}() : setTimeout(poll, 100)
		}())
	};
	Lay.fn.addcss = function(firename, fn, cssname) {
		layui.link(config.dir + 'css/' + firename, fn, cssname)
	};
	Lay.fn.img = function(url, callback, error) {
		var img = new Image();
		img.src = url;
		if (img.complete) {
			return callback(img)
		}
		img.onload = function() {
			img.onload = null;
			callback(img)
		};
		img.onerror = function(e) {
			img.onerror = null;
			error(e)
		}
	};
	Lay.fn.config = function(options) {
		options = options || {};
		for (var key in options) {
			config[key] = options[key]
		}
		return this
	};
	Lay.fn.modules = function() {
		var clone = {};
		for (var o in modules) {
			clone[o] = modules[o]
		}
		return clone
	}();
	Lay.fn.extend = function(options) {
		var that = this;
		options = options || {};
		for (var o in options) {
			if (that[o] || that.modules[o]) {
				error('\u6A21\u5757\u540D ' + o + ' \u5DF2\u88AB\u5360\u7528')
			} else {
				that.modules[o] = options[o]
			}
		}
		return that
	};
	Lay.fn.router = function(hash) {
		var hashs = (hash || location.hash).replace(/^#/, '').split('/') || [];
		var item, param = {
			dir: []
		};
		for (var i = 0; i < hashs.length; i++) {
			item = hashs[i].split('=');
			/^\w+=/.test(hashs[i]) ?
			function() {
				if (item[0] !== 'dir') {
					param[item[0]] = item[1]
				}
			}() : param.dir.push(hashs[i]);
			item = null
		}
		return param
	};
	Lay.fn.data = function(table, settings) {
		table = table || 'layui';
		if (!win.JSON || !win.JSON.parse) return;
		if (settings === null) {
			return delete localStorage[table]
		}
		settings = typeof settings === 'object' ? settings : {
			key: settings
		};
		try {
			var data = JSON.parse(localStorage[table])
		} catch (e) {
			var data = {}
		}
		if (settings.value) data[settings.key] = settings.value;
		if (settings.remove) delete data[settings.key];
		localStorage[table] = JSON.stringify(data);
		return settings.key ? data[settings.key] : data
	};
	Lay.fn.device = function(key) {
		var agent = navigator.userAgent.toLowerCase();
		var getVersion = function(label) {
				var exp = new RegExp(label + '/([^\\s\\_\\-]+)');
				label = (agent.match(exp) || [])[1];
				return label || false
			};
		var result = {
			os: function() {
				if (/windows/.test(agent)) {
					return 'windows'
				} else if (/linux/.test(agent)) {
					return 'linux'
				} else if (/iphone|ipod|ipad|ios/.test(agent)) {
					return 'ios'
				}
			}(),
			ie: function() {
				return ( !! win.ActiveXObject || "ActiveXObject" in win) ? ((agent.match(/msie\s(\d+)/) || [])[1] || '11') : false
			}(),
			weixin: getVersion('micromessenger')
		};
		if (key && !result[key]) {
			result[key] = getVersion(key)
		}
		result.android = /android/.test(agent);
		result.ios = result.os === 'ios';
		return result
	};
	Lay.fn.hint = function() {
		return {
			error: error
		}
	};
	Lay.fn.each = function(obj, fn) {
		var that = this,
			key;
		if (typeof fn !== 'function') return that;
		obj = obj || [];
		if (obj.constructor === Object) {
			for (key in obj) {
				if (fn.call(obj[key], key, obj[key])) break
			}
		} else {
			for (key = 0; key < obj.length; key++) {
				if (fn.call(obj[key], key, obj[key])) break
			}
		}
		return that
	};
	Lay.fn.stope = function(e) {
		e = e || win.event;
		e.stopPropagation ? e.stopPropagation() : e.cancelBubble = true
	};
	Lay.fn.onevent = function(modName, events, callback) {
		if (typeof modName !== 'string' || typeof callback !== 'function') return this;
		config.event[modName + '.' + events] = [callback];
		return this
	};
	Lay.fn.event = function(modName, events, params) {
		var that = this,
			result = null,
			filter = events.match(/\(.*\)$/) || [];
		var set = (events = modName + '.' + events).replace(filter, '');
		var callback = function(_, item) {
				var res = item && item.call(that, params);
				res === false && result === null && (result = false)
			};
		layui.each(config.event[set], callback);
		filter[0] && layui.each(config.event[events], callback);
		return result
	};
	win.layui = new Lay()
}(window);