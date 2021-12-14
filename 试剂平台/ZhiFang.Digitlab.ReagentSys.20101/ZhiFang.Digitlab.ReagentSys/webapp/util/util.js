var Shell = Shell || {};
Shell.util = Shell.util || {};

Shell.System = Shell.System || {};
//不强制登录标志
Shell.System.noLogin = Shell.System.noLogin || false;
//是否开启本地缓存机制
Shell.System.openLocalStorage = (Shell.System.openLocalStorage === false ? false :true);
//判断是否已经登录
Shell.System.isLogin = function() {
	if(Shell.System.noLogin) {
		$("body").show();
		return;
	};

	var ACCOUNTNAME = Shell.util.Cookie.get(Shell.util.Cookie.map.ACCOUNTNAME);
	if(!ACCOUNTNAME) {
		var loginUrl = Shell.util.Path.UI + "/sysbase/main/login.html";
		if(location.href != loginUrl) {
			location.href = loginUrl;
		}
		return false;
	} else {
		$("body").show();
		return true;
	}
};

/**事件处理*/
Shell.util.Event = {
	/**点击事件,可修改为
	 * touchstart 触摸开始就执行
	 * touchend 触摸结束执行
	 * click 点击执行
	 */
	click: "touchend",
	/**是否是点击事件*/
	isClick: function() {
		if(this.click == 'touchend') {
			return this.touch.is_touch();
		} else {
			return true;
		}
	},
	/**初始化*/
	init: function() {
//		if(this.click == 'touchend') {
//			this.touch.init();
//		}
		/**
		 * @description 判断客户端是移动设备还是PC
	     * @author longfc
	     * @modify 2017-05-17
	     * */
		var isPC =this.isPC();
		if(isPC) {
			this.click = "click";
		} else {
			this.click = "touchend";
			this.touch.init();
		}
	},
	/**
	 * @description 判断客户端是移动设备还是PC
	 * @author longfc
	 * @modify 2017-05-17
	 * */
	 isPC:function() {
	 	var isPC=true;
		var sUserAgent = navigator.userAgent.toLowerCase();
		var bIsIpad = sUserAgent.match(/ipad/i) == "ipad";
		var bIsIphoneOs = sUserAgent.match(/iphone os/i) == "iphone os";
		var bIsMidp = sUserAgent.match(/midp/i) == "midp";
		var bIsUc7 = sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4";
		var bIsUc = sUserAgent.match(/ucweb/i) == "ucweb";
		var bIsAndroid = sUserAgent.match(/android/i) == "android";
		var bIsCE = sUserAgent.match(/windows ce/i) == "windows ce";
		var bIsWM = sUserAgent.match(/windows mobile/i) == "windows mobile";
		if((bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM)) {
			isPC=false;
		}
		return isPC;
	},
	/**touch事件处理*/
	touch: {
		/**开始触摸点X位置*/
		touch_start_x: null,
		/**开始触摸点Y位置*/
		touch_start_y: null,
		/**结束触摸点X位置*/
		touch_end_x: null,
		/**结束触摸点Y位置*/
		touch_end_y: null,
		/**X轴移动距离*/
		move_x: function() {
			if(this.touch_end_x == null ||
				this.touch_start_x == null) return null;
			return this.touch_end_x - this.touch_start_x;
		},
		/**Y轴移动距离*/
		move_y: function() {
			if(this.touch_end_y == null ||
				this.touch_start_y == null) return null;
			return this.touch_end_y - this.touch_start_y;
		},
		/**初始化*/
		init: function() {
			var page = window.document.body;
			page.addEventListener("touchstart", function(e) {
				if(e.targetTouches.length == 1 && !self.busy) {
					var xy = e.targetTouches[0];
					Shell.util.Event.touch.touch_start_x = xy.pageX;
					Shell.util.Event.touch.touch_start_y = xy.pageY;
					Shell.util.Event.touch.touch_end_x = xy.pageX;
					Shell.util.Event.touch.touch_end_y = xy.pageY;
				}
			}, false);
			page.addEventListener("touchmove", function(e) {
				//e.preventDefault();
				if(e.targetTouches.length == 1 && !self.busy) {
					var xy = e.targetTouches[0];
					Shell.util.Event.touch.touch_end_x = xy.pageX;
					Shell.util.Event.touch.touch_end_y = xy.pageY;
				}
			}, false);
			page.addEventListener("touchend", function(e) {

			}, false);
		},
		/**是否是触摸*/
		is_touch: function() {
			var move_x = this.move_x(),
				move_y = this.move_y(),
				bo = true;

			if(move_x && (move_x > 1 || move_x < -1))
				bo = false;

			if(move_y && (move_y > 1 || move_y < -1))
				bo = false;

			return bo;
		}
	},
	/**延时处理*/
	delay: function(fun, scope, delayTime) {
		if(Shell.util.typeOf(fun) != 'function') {
			console.log('Shell.util.Event.delay方法参数错误:fun参数不是function!');
			return;
		}

		var me = scope || this,
			delayTime = delayTime || 300;

		me.etime = new Date().getTime();

		if(me.etime - me.stime < delayTime && me.waitAction) {
			clearTimeout(me.waitAction);
		}

		me.waitAction = setTimeout(fun, delayTime);

		me.stime = new Date().getTime();
	}
};
Shell.util.Event.init();
/**校验处理*/
Shell.util.Isvalid = {
	/**
	 * 手机号码
	 * 移动：134[0-8],135,136,137,138,139,150,151,157,158,159,182,187,188
	 * 联通：130,131,132,152,155,156,185,186
	 * 电信：133,1349,153,180,189
	 */
	isCellPhoneNo: function(value) {
		if(!value || value.length != 11) return false;
		var filter = /^1[3|5|8][0-9]\d{4,8}$/;
		return filter.test(value);
	},
	/**
	 * 身份证号码
	 * 支持15位和18位身份证号码校验
	 */
	isIdCardNo: function(value) {
		var filter = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
		return filter.test(value);
	}
};
/**获取数据*/
Shell.util.Server = {
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
		var con = {
			type: "get",
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			async: true
		};

		for(var i in config) {
			con[i] = config[i];
		}

		if(!con.success) {
			con.success = function(data, textStatus) {
				var msg = data[Shell.util.Server.resultParams.msg],
					value = data[Shell.util.Server.resultParams.value];

				data.msg = (!data.success && msg && !config.showError) ? "服务器出错了 " : msg;

				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						try {
							value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
							value = eval("(" + value + ")");
						} catch(e) {
							value = value;
						}
					} else {
						value = value + "";
					}
				}

				data.value = value;

				callback(data);
			};
		}
		if(!con.error) {
			con.error = function(XMLHttpRequest, textStatus, errorThrown) {
				callback({
					success: false,
					msg: Shell.util.Server.getMsgByStatus(XMLHttpRequest.status)
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
		switch(status) {
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

/**cookie操作*/
Shell.util.Cookie = {
	/**cookie键值映射*/
	map: {
		"openId": "100001",
		"userId": "100002",

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
	/**取得cookie属性*/
	getCookie: function(name) {
		var nameT = Shell.util.Cookie.map[name] || name,
			reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
			arr = document.cookie.match(reg);

		if(arr) return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
		return null;
	},
	/**设置cookie属性*/
	setCookie: function(name, value) {
		var days = 30,
			exp = new Date();

		exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
		document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
	},
	/**删除cookie属性*/
	removeCookie: function(name) {
		var exp = new Date();
		exp.setTime(exp.getTime() - 1);
		var cval = this.getCookie(name);
		if(cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
	},
	get: function(name) {
		return this.getCookie(name);
	},
	set: function(name, value) {
		return this.setCookie(name, value);
	},
	remove: function(name) {
		return this.removeCookie(name);
	}
};
Shell.util.LocalStorage = {
	map: {
		'USERLIST': 'S000001', //登录用户列表
		'INDEXTYPE': 'S000002'
	},
	set: function(name, value) {
		if(Shell.System.openLocalStorage){
			localStorage.setItem(name, value);
		}else{
			Shell.util.Cookie.set(name, value);
		}
	},
	get: function(name) {
		if(Shell.System.openLocalStorage){
			return localStorage.getItem(name);
		}else{
			return Shell.util.Cookie.get(name);
		}
	},
	remove: function(name) {
		if(Shell.System.openLocalStorage){
			localStorage.removeItem(name);
		}else{
			this.delCookie(name);
		}
	},
	/**添加用户*/
	addAccount: function(account, password) {
		var userList = this.get(this.map.USERLIST),
			userList = userList ? Shell.util.JSON.decode(userList) : [],
			len = userList.length;

		for(var i = 0; i < len; i++) {
			if(userList[i].split(',')[0] == account) {
				userList.splice(i, 1);
				break;
			}
		}
		userList.unshift(account + ',' + password);
		userList = Shell.util.JSON.encode(userList.slice(0, 3));

		this.set(this.map.USERLIST, userList);
	}
};

/**获取页面传递的参数
 * @param toUpperCase 是否将参数名转化为大写
 */
Shell.util.getRequestParams = function(toUpperCase) {
	var url = location.search; //获取url中"?"符后的字串

	if(url.indexOf("?") == -1) return {};

	var str = url.substr(1),
		strs = str.split("&"),
		len = strs.length,
		params = {};

	for(var i = 0; i < len; i++) {
		var arr = strs[i].split("=");
		if(toUpperCase) {
			arr[0] = arr[0].toLocaleUpperCase();
		}
		params[arr[0]] = decodeURI(arr[1]);
	}

	return params;
}

/**
 * @description 系统路径
 * JcallShell.System.Path.LOCAL 主机地址
 * JcallShell.System.Path.ROOT 主机地址/项目名
 * JcallShell.System.Path.UI 主机地址/项目名/UI包名
 * JcallShell.System.Path.MODULE_ICON_ROOT_16 主机地址/项目名/Images/Icons/16
 * JcallShell.System.Path.MODULE_ICON_ROOT_32 主机地址/项目名/Images/Icons/32
 * JcallShell.System.Path.MODULE_ICON_ROOT_64 主机地址/项目名/Images/Icons/64
 */
Shell.util.Path = {
	LOCAL: '',
	ROOT: '',
	UI: '',
	MODULE_ICON_ROOT_16: '',
	MODULE_ICON_ROOT_32: '',
	MODULE_ICON_ROOT_64: '',
	init: function() {
		this.LOCAL = this.getLocal();
		this.ROOT = this.getRootPath();
		this.UI = this.getUiPath();

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
	/**获取处理后的ROOT包URL*/
	getRootUrl: function(url) {
		if(!url) return '';
		return(url.slice(0, 4) == 'http' ? '' : this.ROOT) + url;
	},
	/**获取处理后的UI包URL*/
	getUiUrl: function(url) {
		if(!url) return '';
		return(url.slice(0, 4) == 'http' ? '' : this.UI) + url;
	}
};
Shell.util.Path.init();

/**判断数据类型*/
Shell.util.typeOf = function(value) {
	var type,
		typeToString;

	if(value === null) return 'null';

	type = typeof value;

	if(type === 'undefined' || type === 'string' || type === 'number' || type === 'boolean') {
		return type;
	}

	typeToString = Object.prototype.toString.call(value);

	switch(typeToString) {
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

	if(type === 'function') return 'function';

	if(type === 'object') {
		if(value.nodeType !== undefined) {
			if(value.nodeType === 3) {
				return(/\S/).test(value.nodeValue) ? 'textnode' : 'whitespace';
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
Shell.util.JSON = {
	/**解码错误信息-需要支持多语言*/
	decodeErrorInfo: '错误!你正在解码一个无效的JSON字符串',
	/**编码符号*/
	encodeMark: '"',
	/**解码*/
	decode: function(json, safe) {
		try {
			return Shell.util.JSON.doDecode(json);
		} catch(e) {
			if(safe === true) return null;

			var errorInfo = 'Shell.util.JSON.decode ' + this.decodeErrorInfo + ':' + json;
			window.console.log(errorInfo);
		}
	},
	/**编码*/
	encode: function(obj, mark) {
		var bo = (mark == '"' || mark == "'");
		if(bo) {
			this.encodeMarkCopy = this.encodeMark;
			this.encodeMark = mark;
		}
		var str = this.encodeValue(obj);
		if(bo) {
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
		for(var i = 0; i < length; i++) {
			a.push(this.encodeValue(o[i]), ',');
		}
		a[a.length - 1] = ']';
		return a.join('');
	},
	/**对象编码*/
	encodeObj: function(o) {
		var a = ['{', ''];
		for(var i in o) {
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
		var type = Shell.util.typeOf(value);
		if(type === 'null' || type === 'undefined') {
			return 'null';
		} else if(type === 'number' || type === 'boolean') {
			return value + '';
		} else if(type === 'string') {
			return this.encodeMark + this.encodeStr(value) + this.encodeMark;
		} else if(type === 'array') {
			return this.encodeArray(value);
		} else if(type === 'object') {
			return this.encodeObj(value);
		}
	},

	doDecode: function(json) {
		return eval('(' + json + ')');
	}
};

/**
 * @description 时间处理
 */
Shell.util.Date = {
	/**获取时间对象,不能转为时间的返回null*/
	getDate: function(value) {
		if(!value) return null;

		var type = Shell.util.typeOf(value),
			date = null;

		if(type == 'date') {
			date = new Date(value.getTime()); //复制
		} else if(type == 'string') {
			if((value.length == 26 || value.length == 27) &&
				value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
				// /Date(1413993600000+0800)/ /Date(-1413993600000+0800)/
				value = parseInt(value.slice(6, -7));
			} else {
				value = value.replace(/-/g, '/');
			}
			date = new Date(value);
		} else if(type == 'number') {
			date = new Date(value);
		}

		var isDate = (Date.parse(date) == Date.parse(date));

		return isDate ? date : null;
	},
	/**校验对象是否是时间*/
	isValid: function(value) {
		var date = this.getDate(value);
		return date ? true : false;
	},
	/**
	 * 获取距离value这个时间num天的时间对象;
	 * @param {date/string/number} value 当前时间
	 * @param {number} num 默认为1,可以负数,例如-1就是昨天,1是明天;
	 * @return {}
	 */
	getNextDate: function(value, num) {
		var date = this.getDate(value);
		if(!value) return null;

		var n = isNaN(num) ? 1 : parseInt(num);

		date.setDate(date.getDate() + n);

		return date;
	},
	/**
	 * 获取时间字符串
	 * @param {date/string/number} value 当前时间
	 * @param {boolean} onlyDate 是否只显示日期
	 * @param {boolean} hasMilliseconds 带毫秒
	 * @param {boolean} hasDay 带星期
	 */
	toString: function(value, onlyDate, hasMilliseconds, hasDay) {
		var v = this.getDate(value);
		if(!v) return null;

		var info = '',
			year = v.getFullYear() + '',
			month = (v.getMonth() + 1) + '',
			date = v.getDate() + '';

		month = month.length == 1 ? '0' + month : month;
		date = date.length == 1 ? '0' + date : date;

		info = year + '-' + month + '-' + date;

		if(!onlyDate) {
			var hours = v.getHours() + '',
				minutes = v.getMinutes() + '',
				seconds = v.getSeconds() + '';

			hours = hours.length == 1 ? '0' + hours : hours;
			minutes = minutes.length == 1 ? '0' + minutes : minutes;
			seconds = seconds.length == 1 ? '0' + seconds : seconds;

			info += ' ' + hours + ':' + minutes + ':' + seconds;
		}
		if(hasMilliseconds) {
			info += ' ' + v.getMilliseconds();
		}

		if(hasDay) {
			var day = v.getDay();
			var text = "星期";
			switch(day) {
				case 0:
					text += "日";
					break;
				case 1:
					text += "一";
					break;
				case 2:
					text += "二";
					break;
				case 3:
					text += "三";
					break;
				case 4:
					text += "四";
					break;
				case 5:
					text += "五";
					break;
				case 6:
					text += "六";
					break;

			}
			info += " " + text;
		}

		return info;
	},
	/**将时间转化为后台需要的格式,例如:\/Date(1359779125000)\/*/
	toServerDate: function(value) {
		var v = this.getDate(value);
		if(!v) return null;

		return "\/Date(" + v.getTime() + "+0000)\/";
	},
	/**校验是否一个月的第一天*/
	isMonthFirstDate: function(value) {
		var v = this.getDate(value);
		if(!v) return false;

		//每个月的1号就是第一天
		if(v.getDate() == 1) return true;

		return false;
	},
	/**校验是否一个月的最后一天*/
	isMonthLastDay: function(value) {
		var v = this.getDate(value);
		if(!v) return false;

		var month = v.getMonth();
		var month2 = this.getNextDate(v).getMonth();

		//符合条件：当天时间加上一天就是下个月的第一天
		if((month2 - month - 1) % 12 == 0) return true;

		return false;
	},
	/**检验是否整月*/
	isFullMonth: function(start, end) {
		var s = this.getDate(start);
		var e = this.getNextDate(end);

		//start < end
		if(Date.parse(s) >= Date.parse(e)) return false;

		//start的日期 = end的日期
		if(s.getDate() != e.getDate()) return false;

		return true;
	},
	/**获取一个月的第一天*/
	getMonthFirstDate: function(year, month, toString) {
		var m = ((month + '').length == 1 ? '0' : '') + month;
		var date = year + '-' + m + '-01';

		if(!toString) date = this.getDate(date);

		return date;
	},
	/**获取一个月的最后一天*/
	getMonthLastDate: function(year, month, toString) {
		var date = new Date(year, month, 0);

		if(toString) date = this.toString(date, true);

		return date;
	}
};
//判断是否登录
Shell.System.isLogin();