(function() {
	var JcallShell = window.JcallShell || {};

	/**空方法*/
	JcallShell.emptyFn = function() {};

	/**数据类型判断*/
	JcallShell.typeOf = function(value) {
		var type,
			typeToString;

		if (value === null) return 'null';

		type = typeof value;

		if (type === 'undefined' || type === 'string' || type === 'number' || type === 'boolean') {
			return type;
		}

		typeToString = Object.prototype.toString.call(value);

		switch (typeToString) {
			case '[object Array]':
				return 'array';
			case '[object Date]':
				return 'date';
			case '[object Boolean]':
				return 'boolean';
			case '[object Number]':
				return 'number';
			case '[object RegExp]':
				return 'regexp';
		}

		if (type === 'function') return 'function';

		if (type === 'object') {
			if (value.nodeType !== undefined) {
				if (value.nodeType === 3) {
					return (/\S/).test(value.nodeValue) ? 'textnode' : 'whitespace';
				} else {
					return 'element';
				}
			}
			return 'object';
		}
		//<debug error>
		window.console.log('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value);
		//</debug>
	};

	/**JSON数据处理*/
	JcallShell.JSON = {
		/**解码错误信息-需要支持多语言*/
		decodeErrorInfo: '错误!你正在解码一个无效的JSON字符串',
		/**编码符号*/
		encodeMark: '"',
		/**解码*/
		decode: function(json, safe) {
			if(!json) return json;
			try {
				return this.doDecode(json);
			} catch (e) {
				if (safe === true) return null;

				var errorInfo = 'Shell.util.JSON.decode ' + this.decodeErrorInfo + ':' + json;
				JcallShell.Msg.log(errorInfo);
			}
		},
		/**编码*/
		encode: function(obj, mark) {
			var bo = (mark == '"' || mark == "'");
			if (bo) {
				this.encodeMarkCopy = this.encodeMark;
				this.encodeMark = mark;
			}
			var str = this.encodeValue(obj);
			if (bo) {
				this.encodeMark = this.encodeMarkCopy;
			}
			return str
		},

		/**属性名编码*/
		encodeKey: function(value) {
			return this.encodeMark + value + this.encodeMark;
		},
		/**数组编码*/
		encodeArray: function(o) {
			var a = ['[', ''];
			var length = o.length;
			for (var i = 0; i < length; i++) {
				a.push(this.encodeValue(o[i]), ',');
			}
			a[a.length - 1] = ']';
			return a.join('');
		},
		/**对象编码*/
		encodeObj: function(o) {
			var a = ['{', ''];
			for (var i in o) {
				a.push(this.encodeKey(i), ':', this.encodeValue(o[i]), ',')
			}
			a[a.length - 1] = '}';
			return a.join('');
		},
		/**字符串编码*/
		encodeStr: function(str) {
			return str.replace(/\\/g, '\\\\').replace(eval('/' + this.encodeMark + '/g'), '\\' + this.encodeMark)
		},
		/**未确定类型编码*/
		encodeValue: function(value) {
			var type = JcallShell.typeOf(value);
			if (type === 'null' || type === 'undefined') {
				return 'null';
			} else if (type === 'number' || type === 'boolean') {
				return value + '';
			} else if (type === 'string') {
				return this.encodeMark + this.encodeStr(value) + this.encodeMark;
			} else if (type === 'array') {
				return this.encodeArray(value);
			} else if (type === 'object') {
				return this.encodeObj(value);
			}
		},

		doDecode: function(json) {
			return eval('(' + json + ')');
		}
	};


	/**本地储存*/
	JcallShell.LocalStorage = {
		getItem: function(key) {
			return this.support() ? window.localStorage.getItem(key) : JcallShell.Cookie.get(key);
		},
		setItem: function(key, value) {
			this.support() ? window.localStorage.setItem(key, value) :
				JcallShell.Cookie.get({
					name: key,
					value: value
				});
		},
		removeItem: function(key) {
			if (this.support()) {
				window.localStorage.removeItem(key)
			}
		},
		/**是否支持本地存储*/
		support: function() {
			return window.localStorage && null !== window.localStorage;
		}
	};
	/**Session储存*/
	JcallShell.SessionStorage = {
		getItem: function(key) {
			return this.support() ? window.sessionStorage.getItem(key) : JcallShell.Cookie.get(key);
		},
		setItem: function(key, value) {
			this.support() ? window.sessionStorage.setItem(key, value) :
				JcallShell.Cookie.get({
					name: key,
					value: value
				});
		},
		removeItem: function(key) {
			if (this.support()) {
				window.sessionStorage.removeItem(key)
			}
		},
		/**是否支持Session储存*/
		support: function() {
			return window.sessionStorage && null !== window.sessionStorage;
		}
	};
	/**Cookie信息*/
	JcallShell.Cookie = {
		/**COOKIE默认缓存时间:单位小时*/
		DEFAULT_COOKIE_HOUSE: 24,
		/**
		 * @description cookie键值映射
		 * @type {Object}
		 */
		map: {
			'LABID': '000100', //实验室ID
			'LABNAME': '000101', //实验室名称
			'USERID': '000200', //员工ID
			'USERNAME': '000201', //员工姓名
			'ACCOUNTID': '000300', //员工账户ID
			'ACCOUNTNAME': '000301', //员工账户名
			'ACCOUNTCODE': '000302', //员工代码
			'DEPTID': '000400', //部门ID
			'DEPTNAME': '000401', //部门名称
			'DEPTCODE': '000402', //部门编号
			'MODULEID': '000500', //模块ID
			'MODULENAME': '000501', //模块名称
			'MODULEOPERID': '000600', //模块操作ID
			'MODULEOPERNAME': '000601', //模块操作名称
			'DEFAULTMODULEID': '100001', //默认模块ID 608EE9C7CA151681C73
			'ISLOCKED': '100002', //锁定标记
			'OLDACCOUNTNAME': '100003', //历史账户名
			'OPENEDMODULEIDS': '100004', //历史打开记录
			'REMEMBERACCOUNT': '100005', //是否记住用户名
			'REMEMBERPWD': '100006' //是否记住密码
		},
		/**
		 * @description 根据名称获取cookie值
		 * @param {String} name
		 */
		get: function(name) {
			var nameT = this.map[name] || name,
				reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
				arr = document.cookie.match(reg);

			if (arr) {
				return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
			}

			return null;
		},
		/**
		 * @description 设置cookie键值内容
		 * @param {Array Object} value Cookie键值对象或数组
		 * @param {Number} days 过期时间
		 * @example
		 * [{
		 * 	name:'a_key',
		 *  value:'a_value',
		 *  expires:{Date}
		 * }]
		 */
		set: function(value, days) {
			if (!value) return;

			var type = JcallShell.typeOf(value);
			var list = type == 'object' ? [value] : value;

			//默认过期时间30天
			var dayTimes = 24 * 60 * 60 * 1000,
				days = days || 30,
				exp = new Date();

			exp.setTime(exp.getTime() + dayTimes * days);

			var len = list.length;
			for (var i = 0; i < len; i++) {
				var name = list[i].name;
				var value = list[i].value;
				if (list[i].expires) {
					exp = list[i].expires;
				}
				var nameT = this.map[name] || name;
				document.cookie = name + "=" + encodeURI(value) + "; expires=" +
					exp.toGMTString() + "; path=/";
			}
		},
		/**
		 * @description 取得COOKIE失效时间
		 */
		getDefaultCookieExpires: function() {
			var date = new Date();
			date.setTime(date.getTime() + this.DEFAULT_COOKIE_HOUSE * 3600 * 1000);
			return date;
		}
	};
	/**系统信息*/
	JcallShell.System = {
		/**登录的键名称*/
		IS_LOGGED_NAME: 'IsLogged',
		/**是否已经登录*/
		isLogged: function(){
			var ACCOUNTNAME = JcallShell.Cookie.get(JcallShell.Cookie.map.ACCOUNTNAME);
			var hasCookie = ACCOUNTNAME ? true : false;
			
			var value = JcallShell.LocalStorage.getItem(this.IS_LOGGED_NAME);
			//var value = JcallShell.SessionStorage.getItem(this.IS_LOGGED_NAME);
			var IsLogged = value == "1" ? true : false;
			
			return hasCookie && IsLogged;
		},
		/**设置是否已登录*/
		setLogged: function(value) {
			JcallShell.LocalStorage.setItem(this.IS_LOGGED_NAME, (value ? "1" : "0"));
			//JcallShell.SessionStorage.setItem(this.IS_LOGGED_NAME, (value ? "1" : "0"));
		}
	};
	/**
	 * @description 系统路径
	 * JcallShell.System.Path.LOCAL 主机地址
	 * JcallShell.System.Path.ROOT 主机地址/项目名
	 * JcallShell.System.Path.UI 主机地址/项目名/UI包名
	 * JcallShell.System.Path.DEFAULT_ERROR_URL 主机地址/项目名/UI包名/server/error.js
	 * JcallShell.System.Path.MODULE_ICON_ROOT_16 主机地址/项目名/Images/Icons/16
	 * JcallShell.System.Path.MODULE_ICON_ROOT_32 主机地址/项目名/Images/Icons/32
	 * JcallShell.System.Path.MODULE_ICON_ROOT_64 主机地址/项目名/Images/Icons/64
	 */
	JcallShell.System.Path = {
		LOCAL: '',
		ROOT: '',
		UI: '',
		MODULE_ICON_ROOT_16: '',
		MODULE_ICON_ROOT_32: '',
		MODULE_ICON_ROOT_64: '',
		DEFAULT_ERROR_URL: '',
		DEFAULT_ERROR_URL_ASPX: '',
		init: function() {
			this.LOCAL = this.getLocal();
			this.ROOT = this.getRootPath();
			this.UI = this.getUiPath();
			this.DEFAULT_ERROR_URL = this.UI + '/server/error.js';
			this.DEFAULT_ERROR_URL_ASPX = this.UI + '/server/error.aspx';

			this.MODULE_ICON_ROOT_16 = this.getModuleIconPathBySize(16);
			this.MODULE_ICON_ROOT_32 = this.getModuleIconPathBySize(32);
			this.MODULE_ICON_ROOT_64 = this.getModuleIconPathBySize(64);
		},
		getRootPath: function() {
			var pathName = window.document.location.pathname;
			var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);

			var rootPath = this.getLocal() + projectName;
			return rootPath;
		},
		getUiPath: function() {
			//获取ui包名称，例如 (项目名/ui)
			var pathName = window.document.location.pathname;
			var name = pathName.split('/').slice(1, 3).join('/');

			return this.getLocal() + '/' + name;
		},
		getLocal: function() {
			var curWwwPath = window.document.location.href;
			var pathName = window.document.location.pathname;
			var pos = curWwwPath.indexOf(pathName);
			var localhostPath = curWwwPath.substring(0, pos);
			return localhostPath;
		},
		/**
		 * @description 根据大小获取图标根目录
		 * @param {Number} size 图片大小
		 * @example
		 * 	例如需要获取16*16的图标根目录就可以调用getIconRootPathBySize(16)
		 *  结果就是http://192.168.0.134/LabStarLIMS/images/icons/16
		 */
		getModuleIconPathBySize: function(size) {
			return this.ROOT + "/Images/Icons/" + size;
		},
		/**获取处理后的URL*/
		getRootUrl: function(url) {
			return (url.slice(0, 4) == 'http' ? '' : this.ROOT) + url;
		},
		/**获取处理后的URL*/
		getUiUrl: function(url) {
			return (url.slice(0, 4) == 'http' ? '' : this.UI) + url;
		}
	};
	JcallShell.System.Path.init();

	/**
	 * @description 提示信息
	 */
	JcallShell.Msg = {
		ALERT_TITLE: '提示信息',
		WARNING_TITLE: '警告信息',
		ERROR_TITLE: '错误信息',
		CONFIRM_TEXT: '确认信息',
		DEL_INFO: '确定要删除吗？',
		OPERATION_INFO: '确定要操作吗？',
		OVERWRITE: '需要重写',
		/**重写信息*/
		overwrite: function(value) {
			var v = value || '';
			v += ' ' + this.OVERWRITE;
			this.warning(v);
		},
		/**log信息*/
		log: function(value) {
			if (JcallShell.System.showLog) {
				window.console.log(value);
			}
		},
		/**提示信息*/
		alert: function(msg, title, times) {
			title = title || this.ALERT_TITLE;
			msg = '<b>' + msg + '</b>';
			this.show(msg, title, Ext.Msg.INFO, times);
		},
		/**警告信息*/
		warning: function(msg, title, times) {
			title = title || this.WARNING_TITLE;
			msg = '<b>' + msg + '</b>';
			this.show(msg, title, Ext.Msg.WARNING, times);
		},
		/**错误信息*/
		error: function(msg, title, times) {
			title = title || this.ERROR_TITLE;
			msg = '<b>' + msg + '</b>';
			this.show(msg, title, Ext.Msg.ERROR, times);
		},
		/**自定义图标信息*/
		show: function(msg, title, icon, times) {
			alert(msg);
		}
	};
	/**获取数据*/
	JcallShell.Server = {
		/**返回参数*/
		resultParams: {
			success: "success",
			msg: "ErrorInfo",
			value: "ResultDataValue"
		},
		/**
		 * ajax请求
		 * @param {Object} config
		 * config:{
		 * 	showError true返回原始错误信息，false返回替换的错误信息
		 * }
		 * @param {Object} callback
		 */
		ajax: function(config, callback) {
			var me = this;
			var con = {
				type: "get",
				dataType: "json",
				contentType:"application/json; charset=utf-8",
				async: true
			};
	
			for (var i in config) {
				con[i] = config[i];
			}
			
			if(!con.success){
				con.success = function(data, textStatus) {
					var msg = data[me.resultParams.msg],
						value = data[me.resultParams.value];
					
					data.msg = (!data.success && msg && !config.showError) ? "服务器出错了 " : msg;
					
					if(value && typeof(value) === "string"){
						if(isNaN(value)){
							value = eval("(" + value + ")");
						}else{
							value = value + "";
						}
					}
					
					data.value = value;
	
					callback(data);
				};
			}
			if(!con.error){
				con.error = function(XMLHttpRequest, textStatus, errorThrown) {
					callback({
						success: false,
						msg: me.getMsgByStatus(XMLHttpRequest.status)
					});
				};
			}
			con.headers = con.headers || {};
			//智方科技MD5加密值
			con.headers.Type = "503DC7557EB732E2E9ACBC1F4DE5455B";
	
			return $.ajax(con);
		},
		/**根据状态码获取错误信息*/
		getMsgByStatus: function(status) {
			var msg = "";
			switch (status) {
				case 404:
					msg = "地球上找不到这个地址";
					break;
				case 500:
					msg = "服务器出错了";
					break;
				default:
					msg = "未定义错误：" + status;
					break;
			}
			return msg;
		}
	};
	
	/**
	 * @description 页面信息
	 */
	JcallShell.Page = {
		/**
		 * 获取页面传递的参数
		 * @param toUpperCase 是否将参数名转化为大写
		 */
		getParams: function(toUpperCase) {
			var url = location.search; //获取url中"?"符后的字串
	
			if (url.indexOf("?") == -1) return {};
	
			var str = url.substr(1),
				strs = str.split("&"),
				len = strs.length,
				params = {};
	
			for (var i = 0; i < len; i++) {
				var arr = strs[i].split("=");
				if (toUpperCase) {
					arr[0] = arr[0].toLocaleUpperCase();
				}
				params[arr[0]] = decodeURI(arr[1]);
			}
	
			return params;
		}
	};
	/**
	 * @description 框架公共弹出层
	 */
	JcallShell.Page.Layer = {
		/**打开页面*/
		//参数API：http://layer.layui.com/api.html#type
		open:function(config){
			var con = {
				type:2//0(信息框,默认)1(页面层)2(iframe层)3(加载层)4(tips层)
	            ,title:""//标题
	            ,shadeClose:true//是否点击遮罩关闭
	            ,maxmin:true//开启最大化最小化按钮
	            ,shift:0//动画
	            ,area:['90%','90%']
	            ,content:""//内容
			};
			if(config && JcallShell.typeOf(config) == 'object'){
				for(var i in config){
					con[i] = config[i];
				}
			}
			
			parent.layer.open(con);
		}
	}
	
	window.JShell = window.JcallShell = JcallShell;

	//显示log信息
	JcallShell.System.showLog = true;
	//登录判断
	if (!JcallShell.System.isLogged()) {
		var url = JcallShell.System.Path.UI + "/index.html";
		if (window.top.location.href != url) {
			window.top.location.href = url;
		}
	}
	//回到顶部监听
	$("#returnTop").on("click",function () {
		var speed=200;//滑动的速度
		$('body,html').animate({ scrollTop: 0 }, speed);
		return false;
	});
})();