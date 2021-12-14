var JcallShell = JcallShell || {};

/**系统信息*/
JcallShell.System = JcallShell.System || {};

JcallShell.System.ADMINNAME = 'admin';

/**是否在debug中显示log信息*/
JcallShell.System.showLog = false;
//不强制登录标志
JcallShell.System.noLogin = JcallShell.System.noLogin || false;
/**系统路径*/
JcallShell.System.Path = {
    LOCAL: '',//主机地址
    ROOT: '',//主机地址/项目名
    UI: '',//主机地址/项目名/UI包名
    MODULE_ICON_ROOT_16: '',//主机地址/项目名/Images/Icons/16
    MODULE_ICON_ROOT_32: '',//主机地址/项目名/Images/Icons/32
    MODULE_ICON_ROOT_64: '',//主机地址/项目名/Images/Icons/64

    init: function () {
        this.LOCAL = this.getLocal();
        this.ROOT = this.getRootPath();
        this.UI = this.getUiPath();

        this.MODULE_ICON_ROOT_16 = this.getModuleIconPathBySize(16);
        this.MODULE_ICON_ROOT_32 = this.getModuleIconPathBySize(32);
        this.MODULE_ICON_ROOT_64 = this.getModuleIconPathBySize(64);
    },
    getRootPath: function () {
        var pathName = window.document.location.pathname;
        var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);

        var rootPath = this.getLocal() + projectName;
        return rootPath;
    },
    getUiPath: function () {
        //获取ui包名称，例如 (项目名/ui)
        var pathName = window.document.location.pathname;
        var name = pathName.split('/').slice(1, 4).join('/');

        return this.getLocal() + '/' + name;
    },
    getLocal: function () {
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
    getModuleIconPathBySize: function (size) {
        return this.ROOT + "/Images/Icons/" + size;
    },

    /**获取处理后的ROOT包URL*/
    getRootUrl: function (url) {
        if (!url) return '';
        return (url.slice(0, 4) == 'http' ? '' : this.ROOT) + url;
    },
    /**获取处理后的UI包URL*/
    getUiUrl: function (url) {
        if (!url) return '';
        return (url.slice(0, 4) == 'http' ? '' : this.UI) + url;
    }
}; JcallShell.System.Path.init();
//判断是否已经登录
JcallShell.System.isLogin = function() {
	if(JcallShell.System.noLogin) {
		$("body").show();
		return;
	};

	var ACCOUNTNAME = JcallShell.Cookie.get(JcallShell.Cookie.map.ACCOUNTNAME);
	if(!ACCOUNTNAME) {
		var loginUrl = JcallShell.System.Path.UI + "/login/index.html";
		if(location.href != loginUrl) {
			location.href = loginUrl;
		}
		return false;
	} else {
		$("body").show();
		return true;
	}
};

/**后台数据交互*/
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
    ajax: function (config, callback) {
        var me = this;
        var con = {
            type: "get",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: true
        };

        for (var i in config) {
            con[i] = config[i];
        }

        if (!con.success) {
            con.success = function (data, textStatus) {
                var msg = data.msg || data[me.resultParams.msg],
					value = data.value || data[me.resultParams.value];
				
				if(msg == "登录过期，请重新登录系统！"){
					JcallShell.Msg.error(msg);
					setTimeout(function(){
						location.href = JcallShell.System.Path.UI +"/login/index.html";
					},1000);
					return;
				}
				
                data.msg = (!data.success && msg && !config.showError) ? "服务器出错了 " : msg;

                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                    	try{
                    		value = eval("(" + value + ")");
                    	}catch(e){
                    		//不处理
                    	}
                    } else {
                        value = value + "";
                    }
                }

                data.value = value;

                callback(data);
            };
        }
        if (!con.error) {
            con.error = function (XMLHttpRequest, textStatus, errorThrown) {
            	var data = {
            		success: false,
	                msg: me.getMsgByStatus(XMLHttpRequest.status)
	            };
            	try{
            		data = JcallShell.JSON.decode(XMLHttpRequest.responseText);
            		if(data.success == 'true'){data.success = true;}
            		if(data.success == 'false'){data.success = false;}
            		var msg = data.msg || data[me.resultParams.msg],
					value = data.value || data[me.resultParams.value];
					
					if(msg == "登录过期，请重新登录系统！"){
						JcallShell.Msg.error(msg);
						setTimeout(function(){
							location.href = JcallShell.System.Path.UI +"/login/index.html";
						},1000);
						return;
					}
	                data.msg = (!data.success && msg && !config.showError) ? "服务器出错了 " : msg;
	
	                if (value && typeof (value) === "string") {
	                    if (isNaN(value)) {
	                    	try{
	                    		value = eval("(" + value + ")");
	                    	}catch(e){
	                    		//不处理
	                    	}
	                    } else {
	                        value = value + "";
	                    }
	                }
	
	                data.value = value;
            	}catch(e){
            	}
            	
                callback(data);
            };
        }
        con.headers = con.headers || {};
        //智方科技MD5加密值
        con.headers.Type = "503DC7557EB732E2E9ACBC1F4DE5455B";

        return $.ajax(con);
    },
    /**根据状态码获取错误信息*/
    getMsgByStatus: function (status) {
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

/**获取页面传递的参数
 * @param toUpperCase 是否将参数名转化为大写
 */
JcallShell.getRequestParams = function (toUpperCase) {
    var url = location.search;//获取url中"?"符后的字串

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

/**判断数据类型*/
JcallShell.typeOf = function (value) {
    var type,
        typeToString;

    if (value === null) return 'null';

    type = typeof value;

    if (type === 'undefined' || type === 'string' || type === 'number' || type === 'boolean') {
        return type;
    }

    typeToString = Object.prototype.toString.call(value);

    switch (typeToString) {
        case '[object Array]': return 'array';
        case '[object Date]': return 'date';
        case '[object Boolean]': return 'boolean';
        case '[object Number]': return 'number';
        case '[object RegExp]': return 'regexp';
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
    if (JcallShell.System.showLog) {
        window.console.log('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value);
    }
    //</debug>
};

/**JSON数据处理*/
JcallShell.JSON = {
    /**解码错误信息-需要支持多语言*/
    decodeErrorInfo: '错误!你正在解码一个无效的JSON字符串',
    /**编码符号*/
    encodeMark: '"',
    /**解码*/
    decode: function (json, safe) {
        try {
            return JcallShell.JSON.doDecode(json);
        } catch (e) {
            if (safe === true) return null;

            //<debug error>
            if (JcallShell.System.showLog) {
                var errorInfo = 'JcallShell.JSON.decode ' + this.decodeErrorInfo + ':' + json;
                window.console.log(errorInfo);
            }
            //</debug>
        }
    },
    /**编码*/
    encode: function (obj, mark) {
        var bo = (mark == '"' || mark == "'");
        if (bo) {
            this.encodeMarkCopy = this.encodeMark;
            this.encodeMark = mark;
        }
        var str = this.encodeValue(obj);
        if (bo) { this.encodeMark = this.encodeMarkCopy; }
        return str
    },

    /**属性名编码*/
    encodeKey: function (value) {
        return this.encodeMark + value + this.encodeMark;
    },
    /**数组编码*/
    encodeArray: function (o) {
        var a = ['[', ''];
        var length = o.length;
        for (var i = 0; i < length; i++) {
            a.push(this.encodeValue(o[i]), ',');
        }
        a[a.length - 1] = ']';
        return a.join('');
    },
    /**对象编码*/
    encodeObj: function (o) {
        var a = ['{', ''];
        for (var i in o) {
            a.push(this.encodeKey(i), ':', this.encodeValue(o[i]), ',')
        }
        a[a.length - 1] = '}';
        return a.join('');
    },
    /**字符串编码*/
    encodeStr: function (str) {
        return str.replace(/\\/g, '\\\\').replace(eval('/' + this.encodeMark + '/g'), '\\' + this.encodeMark)
    },
    /**未确定类型编码*/
    encodeValue: function (value) {
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

    doDecode: function (json) {
        return eval('(' + json + ')');
    }
};

/**本地数据存储*/
JcallShell.LocalStorage = {
    set: function (name, value) {
        localStorage.setItem(name, value);
    },
    /**
	 * 获取本地数据
	 * @param {Object} name 键值
	 * @param {Object} isDecode 是否解码
	 */
    get: function (name,isDecode) {
    	var data = localStorage.getItem(name);
    	if(isDecode){data = JcallShell.JSON.decode(data);}
        return data;
    },
    remove: function (name) {
        localStorage.removeItem(name);
    }
};

/**cookie操作*/
JcallShell.Cookie = {
    /**COOKIE默认缓存时间:单位小时*/
    DEFAULT_COOKIE_HOUSE: 24,
    /**cookie键值映射*/
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
        'DEPTCODE': '000402' //部门编号
    },
    /**取得cookie属性*/
    get: function (name) {
        var nameT = this.map[name] || name,
			reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
			arr = document.cookie.match(reg);

        if (arr) {
            return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
        }

        return null;
    },
    /**设置cookie属性*/
    set: function (name, value) {
        var times = this.DEFAULT_COOKIE_HOUSE * 3600 * 1000,
			exp = new Date();

        exp.setTime(exp.getTime() + times);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + "; path=/";
    },
    /**删除cookie属性*/
    remove: function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);

        var cval = this.get(name);
        if (cval != null) {
            document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + "; path=/";
        }
    }
};

/**字符串操作*/
JcallShell.String = {
	/**字符串转码
	 * @param value 需要转码的字符串
	 * @param unReserved 不转义保留字符
	 */
	encode: function(value, unReserved) {
		var v = value || '';
		//不转义保留字符,转义保留字符
		v = unReserved ? encodeURI(v) : encodeURIComponent(v);

		return v;
	},
	/**字符串解码
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
	},
	/**字符串首位去空格*/
	trim: function(str){
		return str.replace(/(^\s*)|(\s*$)/g,"");
	},
	/**转UTF-8*/
	toUtf8:function(str) {
		if(!str) return "";
		
		var len = str.length,
			out = "";
			
		for(var i = 0; i < len; i++) {
	    	var c = str.charCodeAt(i);
			if ((c >= 0x0001) && (c <= 0x007F)) {
				out += str.charAt(i);
			} else if (c > 0x07FF) {
				out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
				out += String.fromCharCode(0x80 | ((c >>  6) & 0x3F));
				out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));
			} else {
				out += String.fromCharCode(0xC0 | ((c >>  6) & 0x1F));
				out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));
			}
		}
		
		return out;
	}
};

/**提示信息*/
JcallShell.Msg = {
	/**log信息*/
	log: function(value) {
		if(JcallShell.System.showLog && window.console && 
				JcallShell.typeOf(window.console.log) == 'function'){
			window.console.log(value);
		}
	},
	/**提示信息*/
	alert: function(msg) {
		$.toptip(msg, 'info');
	},
	/**警告信息*/
	warning: function(msg) {
		$.toptip(msg, 'warning');
	},
	/**错误信息*/
	error: function(msg) {
		$.toptip(msg, 'error');
	}
};

/**事件处理*/
JcallShell.Event = {
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
			var me = this;
			var page = window.document;
			page.addEventListener("touchstart", function(e) {
				if(e.targetTouches.length == 1 && !self.busy) {
					var xy = e.targetTouches[0];
					me.touch_start_x = xy.pageX;
					me.touch_start_y = xy.pageY;
					me.touch_end_x = xy.pageX;
					me.touch_end_y = xy.pageY;
				}
			}, false);
			page.addEventListener("touchmove", function(e) {
				//e.preventDefault();
				if(e.targetTouches.length == 1 && !self.busy) {
					var xy = e.targetTouches[0];
					me.touch_end_x = xy.pageX;
					me.touch_end_y = xy.pageY;
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
			if(window.console){
				window.console.log('Shell.util.Event.delay方法参数错误:fun参数不是function!');
			}
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
JcallShell.Event.init();
JcallShell.System.isLogin();
