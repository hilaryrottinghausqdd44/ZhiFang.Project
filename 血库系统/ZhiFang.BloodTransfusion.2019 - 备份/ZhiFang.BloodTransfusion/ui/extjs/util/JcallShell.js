/**
 * @description JcallShell
 * @alias JShell
 * @author Jcall
 * @version 2018-01-31
 */
var JcallShell = JcallShell || {};

/**
 * @description 空方法
 */
JcallShell.emptyFn = function() {};

/**
 * @description 数据类型判断
 * @param {Object} obj
 */
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
	JcallShell.Msg.log('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value);
	//window.console.log('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value);
	//</debug>
};

/**JSON数据处理*/
JcallShell.JSON = {
	/**解码错误信息-需要支持多语言*/
	decodeErrorInfo:'错误!你正在解码一个无效的JSON字符串',
	/**编码符号*/
	encodeMark:'"',
	/**解码*/
	decode:function(json,safe){
		try{
            return this.doDecode(json);
        }catch(e){
            if(safe === true) return null;
            
            var errorInfo = 'Shell.util.JSON.decode ' + this.decodeErrorInfo + ':' + json;
            JcallShell.Msg.log(errorInfo);
        }
	},
	/**编码*/
	encode:function(obj,mark){
		var bo = (mark == '"' || mark == "'");
		if(bo){
			this.encodeMarkCopy = this.encodeMark;
			this.encodeMark = mark;
		}
		var str = this.encodeValue(obj);
		if(bo){this.encodeMark = this.encodeMarkCopy;}
		return str
	},
	
	/**属性名编码*/
	encodeKey:function(value){
		return this.encodeMark + value + this.encodeMark;
	},
	/**数组编码*/
    encodeArray:function(o){
		var a = ['[',''];
		var length = o.length;
		for(var i=0;i<length;i++){
			a.push(this.encodeValue(o[i]),',');
		}
		a[a.length-1] = ']';
		return a.join('');
	},
	/**对象编码*/
	encodeObj:function(o){
		var a = ['{',''];
		for(var i in o){
			a.push(this.encodeKey(i),':',this.encodeValue(o[i]),',')
		}
		a[a.length-1] = '}';
		return a.join('');
	},
	/**字符串编码*/
	encodeStr:function(str){
		return str.replace(/\\/g,'\\\\').replace(eval('/' + this.encodeMark + '/g'),'\\' + this.encodeMark)
	},
	/**未确定类型编码*/
	encodeValue:function(value){
		var type = JcallShell.typeOf(value);
		if(type === 'null' || type === 'undefined'){
			return 'null';
		}else if(type === 'number' || type === 'boolean'){
			return value + '';
		}else if(type === 'string'){
			return this.encodeMark + this.encodeStr(value) + this.encodeMark;
		}else if(type === 'array'){
			return this.encodeArray(value);
		}else if(type === 'object'){
			return this.encodeObj(value);
		}
	},
	
	doDecode:function(json){
        return eval('(' + json + ')');
    }
};

/**
 * @description 系统信息
 * @type {Object}
 */
JcallShell.System = JcallShell.System || {};

/**
 * @description 是否在debug中显示log信息
 * @type {Boolean}
 */
JcallShell.System.showLog = true;

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
	RBAC_ROOT: '',//集成平台URL,为空时取当前应用程序的ROOT
	UI: '',
	MODULE_ICON_ROOT_16:'',
	MODULE_ICON_ROOT_32:'',
	MODULE_ICON_ROOT_64:'',
	DEFAULT_ERROR_URL: '',
	DEFAULT_ERROR_URL_ASPX: '',
	init: function() {
		this.LOCAL = this.getLocal();
		this.ROOT = this.getRootPath();
		this.RBAC_ROOT = this.getRBACRootPath();
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
	getRBACRootPath: function() {
		var rootPath =this.RBAC_ROOT;
		if(!rootPath)rootPath=this.getRootPath();
		//console.log("JcallShell.getRBACRootPath:"+rootPath);
		return rootPath;
	},
	getUiPath: function() {
		//获取ui包名称，例如 (项目名/ui)
		var pathName = window.document.location.pathname;
		var name = pathName.split('/').slice(1, 4).join('/');

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
	getUrl:function(url){
		if(!url) return '';
		return (url.slice(0, 4) == 'http' ? '' : this.ROOT) + url;
	},
	/**获取处理后的URL*/
	getRootUrl:function(url){
		if(!url) return '';
		return (url.slice(0, 4) == 'http' ? '' : this.ROOT) + url;
	},
	/**获取处理后的URL*/
	getUiUrl:function(url){
		if(!url) return '';
		return (url.slice(0, 4) == 'http' ? '' : this.UI) + url;
	}
};JcallShell.System.Path.init();

/**本地数据存储*/
JcallShell.LocalStorage = {
    set: function (name, value) {
        localStorage.setItem(name, value);
    },
    get: function (name) {
        return localStorage.getItem(name);
    },
    remove: function (name) {
        localStorage.removeItem(name);
    }
};
/**
 * @description 系统Cookie信息
 */
JcallShell.System.Cookie = {
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
		'REMEMBERPWD': '100006' ,//是否记住密码
		
		'DOCTORID': '200001', //6.6人员帐号绑定的所属医生Id
		'DOCTORCNAME': '200002', //6.6人员帐号绑定的所属医生姓名
		'GRADEID': '200003' //6.6人员帐号绑定的所属医生的等级
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
	},
    /**@description 删除cookie*/
	delCookie:function(name){
		var cval = this.getCookie(name);
		if(!cval) return;
		
		var exp = new Date();
		exp.setTime(exp.getTime() - 1);
		
		document.cookie= name + "=" + cval + ";expires=" + exp.toGMTString();
	},
	/**@description 获取所有cookie*/
	getAllCookie:function(){ 
		var keys = document.cookie.match(/[^ =;]+(?=\=)/g);
		var list = [];
		
		if (keys) { 
			for (var i=0;i<keys.length;i++){
				list.push([keys[i],this.get(keys[i])]);
			}
		}
		
		return list;
	},
	/**@description 清理所有cookie*/
	clearCookie:function(){ 
		var keys = document.cookie.match(/[^ =;]+(?=\=)/g); 
		if (keys) { 
			for (var i=0;i<keys.length;i++){
				document.cookie=keys[i]+'=0;expires=' + new Date(0).toUTCString();
			}
		} 
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
	},
	/**更改语言*/
	changeLangage:function(lang){
		
		
		//加载系统配置文件
		JShell.System.Lang = lang;
		
		var content = [];
		
		if(!lang || lang.toLocaleUpperCase() == 'CN'){
			//加载EXTJS中文包
			content.push('<script type="text/javascript" src="' + JcallShell.System.Path.ROOT + '/ui/src/extjs/locale/ext-lang-zh_CN.js"></script>');
		}else{
			var file = JcallShell.System.Path.UI + '/locale/' + JShell.System.Lang.toLocaleLowerCase() + '/config/config_System.js';
			content.push('<script type="text/javascript" src="' + file + '"></script>');
		}
		
		document.write(content.join(''));
	}
};

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
	/**删除确认*/
	del: function(fn) {
		var config = {
			title: this.CONFIRM_TEXT,
			msg: this.DEL_INFO,
			icon: Ext.Msg.QUESTION,
			buttons: Ext.Msg.OKCANCEL,
			callback: fn
		};
		setTimeout(function() {
			Ext.Msg.show(config);
		}, 10);
	},
	/**确认信息*/
	confirm: function(config, fn) {
		//multiline 多行输入框
		var con = {
			title: this.CONFIRM_TEXT,
			msg: this.OPERATION_INFO,
			buttons: Ext.Msg.OKCANCEL,
			callback: fn
		};
		for (var i in config) {
			con[i] = config[i];
		}
		if (!config.multiline && !con.icon) {
			con.icon = Ext.Msg.QUESTION;
		}
		
		setTimeout(function() {
			Ext.Msg.show(con);
		}, 10);
	},
	/**log信息*/
	log: function(value) {
		if(JcallShell.System.showLog && window.console && 
				JShell.typeOf(window.console.log) == 'function'){
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
		var me = this;
		Ext.Msg.autoScroll = true;

		if (me.delayF) clearTimeout(me.delayF);
		
		setTimeout(function() {
			var view = Ext.Msg.show({
				title: title,
				msg: msg,
				icon: icon,
				modal: true,
				buttons: Ext.Msg.YES
			});
	
			if (times && times > 0) {
				me.delayF = setTimeout(function() {
					view.hide();
				}, times);
			}
		}, 10);
	}
};

/**
 * @description 时间处理
 */
JcallShell.Date = {
	/**获取时间对象,不能转为时间的返回null*/
	getDate: function(value) {
		if (!value) return null;

		var type = JcallShell.typeOf(value),
			date = null;

		if (type == 'date') {
			date = new Date(value.getTime());
		} else if (type == 'string') {
			if ((value.length == 26 || value.length == 27) &&
				value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
				// /Date(1413993600000+0800)/ /Date(-1413993600000+0800)/
				value = parseInt(value.slice(6, -7));
			} else {
				value = value.replace(/-/g, '/');
			}
			date = new Date(value);
		} else if (type == 'number') {
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
		if (!value) return null;

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
		if (!v) return null;

		var info = '',
			year = v.getFullYear() + '',
			month = (v.getMonth() + 1) + '',
			date = v.getDate() + '';

		month = month.length == 1 ? '0' + month : month;
		date = date.length == 1 ? '0' + date : date;

		info = year + '-' + month + '-' + date;

		if (!onlyDate) {
			var hours = v.getHours() + '',
				minutes = v.getMinutes() + '',
				seconds = v.getSeconds() + '';

			hours = hours.length == 1 ? '0' + hours : hours;
			minutes = minutes.length == 1 ? '0' + minutes : minutes;
			seconds = seconds.length == 1 ? '0' + seconds : seconds;

			info += ' ' + hours + ':' + minutes + ':' + seconds;
		}
		if (hasMilliseconds) {
			info += ' ' + v.getMilliseconds();
		}

		if (hasDay) {
			var day = v.getDay();
			var text = "星期";
			switch (day) {
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
		if (!v) return null;

		return "\/Date(" + v.getTime() + "+0000)\/";
	},
	/**校验是否一个月的第一天*/
	isMonthFirstDate: function(value) {
		var v = this.getDate(value);
		if (!v) return false;

		//每个月的1号就是第一天
		if (v.getDate() == 1) return true;

		return false;
	},
	/**校验是否一个月的最后一天*/
	isMonthLastDay: function(value) {
		var v = this.getDate(value);
		if (!v) return false;

		var month = v.getMonth();
		var month2 = this.getNextDate(v).getMonth();

		//符合条件：当天时间加上一天就是下个月的第一天
		if ((month2 - month - 1) % 12 == 0) return true;

		return false;
	},
	/**检验是否整月*/
	isFullMonth: function(start, end) {
		var s = this.getDate(start);
		var e = this.getNextDate(end);

		//start < end
		if (Date.parse(s) >= Date.parse(e)) return false;

		//start的日期 = end的日期
		if (s.getDate() != e.getDate()) return false;

		return true;
	},
	/**获取一个月的第一天*/
	getMonthFirstDate: function(year, month, toString) {
		var m = ((month + '').length == 1 ? '0' : '') + month;
		var date = year + '-' + m + '-01';

		if (!toString) date = this.getDate(date);

		return date;
	},
	/**获取一个月的最后一天*/
	getMonthLastDate: function(year, month, toString) {
		var date = new Date(year, month, 0);

		if (toString) date = this.toString(date, true);

		return date;
	}
};
/**
 * @description 数字处理
 */
JcallShell.Number = {
	/**获取大写金额*/
	getMoney:function(num){
		var strUnit = '仟佰拾亿仟佰拾万仟佰拾元角分',
			strOutput = "";
			
		num += "00";  
  		var intPos = num.indexOf('.');
  		if (intPos >= 0){
  			num = num.substring(0, intPos) + num.substr(intPos + 1, 2);
  		}
		strUnit = strUnit.substr(strUnit.length - num.length);  
		for (var i=0; i < num.length; i++){
			strOutput += '零壹贰叁肆伍陆柒捌玖'.substr(num.substr(i,1),1) + strUnit.substr(i,1);
		}
		var result = strOutput
			.replace(/零角零分$/, '整')
			.replace(/零[仟佰拾]/g, '零')
			.replace(/零{2,}/g, '零')
			.replace(/零([亿|万])/g, '$1')
			.replace(/零+元/, '元')
			.replace(/亿零{0,3}万/, '亿')
			.replace(/^元/, "零元");
			
		return result;
	}
};

/**
 * @description 数组处理
 */
JcallShell.Array = {
	/**
	 * @description 重新排序,支持正序和倒序,默认正序
	 * @param {Array} list
	 * @param {String} key
	 * @param {Boolean} isDesc
	 */
	reorder: function(list, key, isDesc) {
		if (!key) return list;
		if (JcallShell.typeOf(list) != 'array') return list;

		var arr = Ext.clone(list) || [],
			len = arr.length;

		//校验数组的每一个元素是否存在key属性,全部存在才排序,否则直接返回数组
		for (var i = 0; i < len; i++) {
			if (arr[i][key] == null) return list;
		}
		//重新排序
		for (var i = 0; i < len - 1; i++) {
			for (var j = i + 1; j < len; j++) {
				var bo = isDesc ? (arr[i][key] < arr[j][key]) : (arr[i][key] > arr[j][key]);
				if (bo) {
					var temp = arr[i];
					arr[i] = arr[j];
					arr[j] = temp;
				}
			}
		}

		return arr;
	},
	/**
	 * @description 判断是否是数组
	 * @param {Object} obj
	 */
	isArray: function(obj) {
		return Object.prototype.toString.call(obj) === '[object Array]';
	}
};

/**
 * @description 字符串处理
 */
JcallShell.String = {
	PARSEERROR: '解析错误',
	/**
	 * 字符串转码
	 * @param value 需要转码的字符串
	 * @param unReserved 不转义保留字符
	 */
	encode: function(value, unReserved) {
		var v = value || '';
		//不转义保留字符,转义保留字符
		v = unReserved ? encodeURI(v) : encodeURIComponent(v);

		return v;
	},
	/**
	 * 字符串解码
	 * @param value 需要解码的字符串
	 * @param unReserved 不转义保留字符
	 */
	decode: function(value, unReserved) {
		var v = value || '';
		//不转义保留字符,转义保留字符
		v = unReserved ? decodeURI(v) : decodeURIComponent(v);

		return v;
	},
	/**字符串-获取以ASCII编码字节数 英文占1字节 中文占2字节*/
	lenASCII: function(str) {
		if (JcallShell.typeOf(str) != 'string') return -1;
		//将所有非\x00-\xff字符换为xx两个字符,再计算字符串
		return str.replace(/[^\x00-\xff]/g, 'xx').length;
	},
	/**获取固定字节数的子串*/
	substrASCII: function(str, start, lenASCII) {
		if (JcallShell.typeOf(str) != 'string') return null;
		var arr = str.split(''),
			length = arr.length,
			result = [],
			count = 0,
			len = 0;

		start = start < 0 ? 0 : start;
		lenASCII = lenASCII < 0 ? 0 : lenASCII;
		lenASCII = lenASCII > (length - start) ? (length - start) : lenASCII;

		for (var i = start; i < length; i++) {
			len = this.lenASCII(arr[i]);
			count += len;
			if (count > lenASCII) break;
			result.push(arr[i]);
		}

		return result.join('');
	},
	/**字符串是否在数组中存在*/
	inArray: function(str, array) {
		var arr = array || [],
			len = arr.length;

		for (var i = 0; i < len; i++) {
			if (arr[i] == str) return true;
		}

		return false;
	}
};

/**
 * @description 字节处理
 */
JcallShell.Bytes = {
	/**计数单位*/
	UNIT: 1024,
	/**
	 * 字符串转码
	 * @param value 需要转码的字符串
	 * @param unReserved 不转义保留字符
	 */
	toSize: function(bytes) {
		if (bytes === 0){
			return '0 B';
		}
		
	    var k = this.UNIT,
	        sizes = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
	        i = Math.floor(Math.log(bytes) / Math.log(k));
	        
	    var result = (bytes / Math.pow(k, i)).toPrecision(3) + ' ' + sizes[i];
	        
	   return result;
	}
};

/**
 * @description 服务交互
 */
JcallShell.Server = {
	LOADING_TEXT: '数据加载中...',
	SAVE_TEXT: '数据保存中...',
	DEL_TEXT: '数据删除中...',
	NO_DATA: '没有找到数据',
	/**超时_毫秒数*/
	TIME_OUT_MILLISECOND:600000,
	/**后台服务键值对*/
	HEADERS:[
		{key:'User-Agent-Zip-Fa',value:'6D0F3E94-B672-4BFD-B614-00398C73447D'}
	],
	/**状态信息*/
	Status: {
		ERROR_404: '无法找到地址',
		ERROR_505: '服务器出错了',
		ERROR_UNDEFINED: '未定义错误',
		ERROR_TIMEOUT: '请求服务超时',
		ERROR_UNIQUE_KEY: '违反了唯一键约束'
	},
	/**返回参数*/
	resultParams: {
		success: "success",
		batchSign: "BatchSignValue",
		msg: "ErrorInfo",
		value: "ResultDataValue"
	},
	get: function(url, callback, async, timeout, isString) {
		this.toServer('GET', url, callback, async, null, null, timeout, isString);
	},
	post: function(url, params, callback, async, timeout, isString, defaultPostHeader) {
		this.toServer('POST', url, callback, async, params, defaultPostHeader, timeout, isString);
	},
	toServer: function(method, url, callback, async, params, defaultPostHeader, timeout, isString) {
		var me = this;
		timeout = timeout || this.TIME_OUT_MILLISECOND;
		Ext.Ajax.defaultPostHeader = defaultPostHeader || 'application/json';
		var bo = async === false ? false : true;

		var con = {
			url: url,
			async: bo,
			method: method,
			success: function(response, opts) {
				if (JcallShell.typeOf(callback) == "function") {
					var d = isString ? response.responseText : me.toJson(response.responseText);
					callback(d); //回调函数
				}
			},
			failure: function(response, options) {
				if (JcallShell.typeOf(callback) == "function") {
					var value = "";
					if (response.request.timedout) { //请求超时
						value = "{" + me.resultParams.success + ":false," +
							me.resultParams.msg + ":'" + me.Status.ERROR_TIMEOUT + "'}";
					} else {
						value = "{" + me.resultParams.success + ":false," +
							me.resultParams.msg + "'" +
							me.getMsgByStatus(response.status) + "'}";
					}
					var d = isString ? response.responseText : me.toJson(value);
					callback(d); //回调函数
				}
			}
		};

		if (params) {
			con.params = params;
		} //POST参数
		if (timeout) {
			con.timeout = timeout;
		} //超时

		Ext.Ajax.request(con);
	},
	/**转化成对象*/
	toJson: function(data) {
		var result = {};

		try {
			var v = Ext.JSON.decode(data.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '')),
				success = v[this.resultParams.success],
				batchSign = v[this.resultParams.batchSign],
				value = v[this.resultParams.value],
				msg = v[this.resultParams.msg];

			if (value && typeof(value) === "string") {
				if (isNaN(value)) {
					try {
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						try{
							value = Ext.JSON.decode(value);
						}catch (e) {
							//不处理
						}
					} catch (e) {
						value = value;
						success = false;
						msg = JcallShell.All.JSON_ERROR;
					}
				}
			}

			if (success === "true") success = true;
			if (success === "false") success = false;
			
			if (!success) {
				var index = msg.indexOf('UNIQUE KEY');
				if (index != -1) {
					msg = this.Status.ERROR_UNIQUE_KEY;
				}
			}
			result = {
				success: success,
				batchSign: batchSign,
				msg: msg,
				value: value
			};
		} catch (e) {
			result = {
				success: false,
				batchSign: batchSign,
				msg: JcallShell.String.PARSEERROR,
				value: null
			};
		}

		return result;
	},
	/**根据状态码获取错误信息*/
	getMsgByStatus: function(status) {
		var msg = this.Status['ERROR_' + status] ||
			this.Status.ERROR_UNDEFINED + '：' + status;
		return msg;
	},
	/**数据类型映射处理*/
	Mapping: function(data) {
		var value = Ext.clone(data);
		for (var i in value) {
			value[i] = value[i].replace(/<\/br>/g, '\r\n');
			if (value[i] === 'true') value[i] = true;
			if (value[i] === 'false') value[i] = false;
		}
		return value;
	}
};

/**
 * @description 交互处理
 */
JcallShell.Action = {
	/**默认延时时间*/
	DELAYTIME: 500,
	/**延时处理*/
	delay: function(fun, scope, delayTime) {
		if (JcallShell.typeOf(fun) != 'function') return;

		var me = scope || this,
			delayTime = delayTime || this.DELAYTIME;

		me.etime = new Date().getTime();

		if (me.etime - me.stime < delayTime && me.waitAction) {
			clearTimeout(me.waitAction);
		}

		me.waitAction = setTimeout(fun, delayTime);

		me.stime = new Date().getTime();
	}
};

/**@description 窗口处理*/
JcallShell.Win = {
	/**打开窗口*/
	open: function(panel, config) {
		var maxHeight = document.body.clientHeight;
		var maxWidth = document.body.clientWidth;

		var par = {
			maximizable: true, //是否带最大化功能
			maxWidth: maxWidth,
			constrainHeader: true, //窗口约束到可见区域
			modal: true, //模态化
			closable: true, //关闭功能
			draggable: true, //移动功能
			resizable: true, //可变大小功能
			floating: true //浮动模式
		};

		for(var i in config) {
			par[i] = config[i];
		}

		if(par.maximizable) {
			par = Ext.apply(par, this.getMaxMinConfig());
		}

		//2016-11-24,支持模块内部窗口帮助文档
		if(par.SUB_WIN_NO) {
			//par.modal = false;
			par.tools = [{
				type: 'help',
				tooltip: '查看帮助文档',
				handler: function(event, toolEl, tool) {
					var me = this,
						win = me.ownerCt.ownerCt;

					if(win['HELP_' + par.SUB_WIN_NO]) {
						//win['HELP_' + par.SUB_WIN_NO].close();
						var zIndex = win.zIndexManager.zseed + 100;
						win['HELP_' + par.SUB_WIN_NO].getEl().setStyle('z-index', zIndex);
						var pos = win['HELP_' + par.SUB_WIN_NO].getPosition();
						pos[0] = pos[0] + 1;
						win['HELP_' + par.SUB_WIN_NO].showAt(pos);
						var pos2 = win['HELP_' + par.SUB_WIN_NO].getPosition();
						pos2[0] = pos2[0] - 1;
						win['HELP_' + par.SUB_WIN_NO].showAt(pos2);
						return;
					}

					//WIN宽高、位置
					var winHeight = win.getHeight();
					var winWidth = win.getWidth();
					var position = win.getPosition();

					var ModuleId = JShell.System.Cookie.get(JShell.System.Cookie.map.MODULEID);
					var SubWinNo = tool.ownerCt.SUB_WIN_NO;

					win['HELP_' + par.SUB_WIN_NO] = JcallShell.Win.open('Shell.class.qms.file.help.show.Panel', {
						//resizable: false,
						title: '帮助信息',
						modal: false,
						height: winHeight, //帮助文档高度=弹出窗口高度
						ModuleId: ModuleId,
						SubWinNo: SubWinNo,
						listeners: {
							close: function() {
								win['HELP_' + par.SUB_WIN_NO] = null;
								if(!win.maximized) {
									win.showAt(position);
								}
							}
						}
					});

					//浏览器最大宽度
					var maxWidth = document.body.clientWidth;
					//位置信息
					var helpWinWidth = win['HELP_' + par.SUB_WIN_NO].width;

					var winPosition = [position[0] - helpWinWidth / 2, position[1]];
					var helpWinPostion = [winPosition[0] + winWidth, winPosition[1]];

					if(maxWidth < (winWidth + helpWinWidth + 10)) {
						winPosition = [5, position[1]];
						helpWinPostion = [maxWidth - helpWinWidth - 5, winPosition[1]];
					}

					win.showAt(winPosition);
					win['HELP_' + par.SUB_WIN_NO].showAt(helpWinPostion);
				}
			}];
			if(par.maximizable) {
				var tools = this.getMaxMinConfig().tools;
				par.tools = tools.concat(par.tools);
			}
		}

		var win = Ext.create(panel, par);

		if(win.height > maxHeight) {
			win.setHeight(maxHeight);
		}

		if(win.closable && win.closeAction === 'destroy') {
			win.on({
				show: function() {
					//ESC键可以关闭打开的窗口
					new Ext.KeyMap(win.getEl(), {
						key: Ext.EventObject.ESC,
						fn: function(k, e) {
							e.stopEvent();
							win.close();
						},
						scope: this,
						defaultEventAction: "stopEvent"
					}).enable();
				}
			});
		}

		//2016-11-24,支持模块内部窗口帮助文档
		if(par.SUB_WIN_NO) {
			//窗口关闭时，帮助页面也同时关闭掉
			win.on({
				close: function() {
					if(win['HELP_' + par.SUB_WIN_NO]) {
						win['HELP_' + par.SUB_WIN_NO].close();
					}
				}
			});
		}

		return win;
	},
	getMaxMinConfig: function() {
		var config = {
			tools: [{
				type: 'maximize',
				itemId: 'maximize',
				handler: function() {
					var me = this,
						head = me.ownerCt,
						panel = head.ownerCt,
						minimize = head.getComponent('minimize');

					panel.maximized = true;
					me.hide();
					minimize.show();
					head.oldSize = panel.getSize();
					head.oldPosition = panel.getPosition();

					var maxHeight = document.body.clientHeight;
					var maxWidth = document.body.clientWidth;

					panel.setPosition([0, 0]);
					panel.setSize(maxWidth, maxHeight);
					panel.doLayout();
				}
			}, {
				type: 'minimize',
				itemId: 'minimize',
				hidden: true,
				handler: function() {
					var me = this,
						head = me.ownerCt,
						panel = head.ownerCt,
						maximize = head.getComponent('maximize');

					panel.maximized = false;
					me.hide();
					maximize.show();
					panel.setPosition(head.oldPosition);
					panel.setSize(head.oldSize);

					panel.doLayout();
				}
			}]
		};
		return config;
	},
	basic: {
		/**开启全屏模式*/
		launchFullscreen: function(element) {
			if(element.requestFullscreen) {
				element.requestFullscreen();
			} else if(element.mozRequestFullScreen) {
				element.mozRequestFullScreen();
			} else if(element.webkitRequestFullscreen) {
				element.webkitRequestFullscreen();
			} else if(element.msRequestFullscreen) {
				element.msRequestFullscreen();
			}
		},
		/**退出全屏模式*/
		exitFullscreen: function(doc) {
			if(doc.exitFullscreen) {
				doc.exitFullscreen();
			} else if(doc.mozCancelFullScreen) {
				doc.mozCancelFullScreen();
			} else if(doc.webkitExitFullscreen) {
				doc.webkitExitFullscreen();
			} else if(doc.msExitFullscreen) {
				doc.msExitFullscreen();
			}
		}
	},
	frame: {
		/**开启全屏模式*/
		launchFullscreen: function() {
			var win = this.getFrameWin(window);
			JcallShell.Win.basic.launchFullscreen(win.document.documentElement);
		},
		/**退出全屏模式*/
		exitFullscreen: function() {
			var win = this.getFrameWin(window);
			JcallShell.Win.basic.exitFullscreen(win.document);
		},
		getFrameWin: function(win) {
			if(win != win.parent) {
				return win.parent;
			}
			return win;
		}
	}
};

/**
 * @description 全局预定义变量
 */
JcallShell.All = {
	ALL: '全部',
	TRUE: '是',
	FALSE: '否',
	LOADING_TEXT: '数据加载中...',
	ADD: '新增',
	EDIT: '修改',
	SHOW: '查看',
	CHECK_ONE_RECORD: '请选择一行数据进行操作',
	CHECK_MORE_RECORD: '请选择数据进行操作',
	SUCCESS_TEXT: '成功',
	FAILURE_TEXT: '失败',
	JSON_ERROR:'JSON数据解析错误'
};

//系统文字配置
(function() {
	//提示信息
	JcallShell.Msg.ALERT_TITLE = '提示信息';
	JcallShell.Msg.WARNING_TITLE = '警告信息';
	JcallShell.Msg.ERROR_TITLE = '错误信息';
	JcallShell.Msg.CONFIRM_TEXT = '确认信息';
	JcallShell.Msg.DEL_INFO = '确定要删除吗？';
	JcallShell.Msg.OVERWRITE = '需要重写';
	//服务交互
	JcallShell.Server.LOADING_TEXT = '数据加载中...';
	JcallShell.Server.SAVE_TEXT = '数据保存中...';
	JcallShell.Server.DEL_TEXT = '数据删除中...';
	JcallShell.Server.NO_DATA = '没有找到数据';
	//服务交互-状态信息
	JcallShell.Server.Status.ERROR_404 = '无法找到地址';
	JcallShell.Server.Status.ERROR_505 = '服务器出错了';
	JcallShell.Server.Status.ERROR_UNDEFINED = '未定义错误';
	JcallShell.Server.Status.ERROR_TIMEOUT = '请求服务超时';
	JcallShell.Server.Status.ERROR_UNIQUE_KEY = '违反了唯一键约束';
	//字符串
	JcallShell.String.PARSEERROR = '解析错误';
	//全局预定义变量
	JcallShell.All.ALL = '全部';
	JcallShell.All.TRUE = '是';
	JcallShell.All.FALSE = '否';
	JcallShell.All.ADD = '新增';
	JcallShell.All.EDIT = '修改';
	JcallShell.All.SHOW = '查看';
	JcallShell.All.CHECK_ONE_RECORD = '请选择一行数据进行操作';
	JcallShell.All.CHECK_MORE_RECORD = '请选择数据进行操作';
	JcallShell.All.SUCCESS_TEXT = '成功';
	JcallShell.All.FAILURE_TEXT = '失败';
	JcallShell.All.JSON_ERROR = 'JSON数据解析错误';

	window.JShell = JcallShell;
})();