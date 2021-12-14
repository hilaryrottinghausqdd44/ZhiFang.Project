
/**@description 系统路径
 * JcallShell.System.Path.LOCAL 主机地址
 * JcallShell.System.Path.ROOT 主机地址/项目名
 * JcallShell.System.Path.UI 主机地址/项目名/UI包名
 * JcallShell.System.Path.DEFAULT_ERROR_URL 主机地址/项目名/UI包名/server/error.js
 * JcallShell.System.Path.MODULE_ICON_ROOT_16 主机地址/项目名/Images/Icons/16
 * JcallShell.System.Path.MODULE_ICON_ROOT_32 主机地址/项目名/Images/Icons/32
 * JcallShell.System.Path.MODULE_ICON_ROOT_64 主机地址/项目名/Images/Icons/64
 */

var Shell = {};

Shell.util = {
    /**判断数据类型*/
    typeOf: function (value) {
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
        Shell.util.Msg.showLog('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value, true);
        //</debug>
    },
    /**克隆数据*/
    clone: function (item) {
        var type, i, j, k, clone, key;

        if (item === null || item === undefined) {
            return item;
        }

        if (item.nodeType && item.cloneNode) {
            return item.cloneNode(true);
        }

        type = Object.prototype.toString.call(item);

        if (type === '[object Date]') {//时间
            return new Date(item.getTime());
        }

        if (type === '[object Array]') {//数组
            i = item.length;
            clone = [];
            while (i--) {
                clone[i] = Shell.util.clone(item[i]);
            }
        }
        //对象
        else if (type === '[object Object]' && item.constructor === Object) {
            clone = {};

            for (key in item) {
                clone[key] = Shell.util.clone(item[key]);
            }

            if (enumerables) {
                for (j = enumerables.length; j--;) {
                    k = enumerables[j];
                    clone[k] = item[k];
                }
            }
        }

        return clone || item;
    }
};

Shell.util.Path = {
    LOCAL: '',
    ROOT: '',
    UI: '',
    DEFAULT_ERROR_URL: '',
    DEFAULT_ERROR_URL_ASPX: '',
    init: function () {
        this.LOCAL = this.getLocal();
        this.ROOT = this.getRootPath();
        this.UI = this.getUiPath();
        this.DEFAULT_ERROR_URL = this.UI + '/server/error.js';
        this.DEFAULT_ERROR_URL_ASPX = this.UI + '/server/error.aspx';

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
    /**获取页面传递的参数*/
    getRequestParams: function () {
        var url = location.search;//获取url中"?"符后的字串

        if (url.indexOf("?") == -1) return {};

        var str = url.substr(1),
            strs = str.split("&"),
            len = strs.length,
            params = {};

        for (var i = 0; i < len; i++) {
            var arr = strs[i].split("=");
            params[arr[0]] = decodeURI(arr[1]);
        }

        return params;
    }
};
/**字符串*/
Shell.util.String = {
    /**字符串转码*/
    encode: function (value) {
        //v = encodeURI(v);//不转义保留字符
        //转义保留字符
        return encodeURIComponent(value || '');
    },
    /**字符串解码*/
    decode: function (value) {
        return decodeURI(value || '');
    },
    /**字符串-获取以ASCII编码字节数 英文占1字节 中文占2字节*/
    lenASCII: function (str) {
        if (Shell.util.typeOf(str) != 'string') return -1;
        //将所有非\x00-\xff字符换为xx两个字符,再计算字符串
        return str.replace(/[^\x00-\xff]/g, 'xx').length;
    },
	/**获取固定字节数的子串
	 * @param str 字符串
	 * @param start 开始的位置
	 * @param lenASCII 字节长度
	 */
    substrASCII: function (str, start, lenASCII) {
        if (typeof (str) != 'string') return null;
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
    inArray: function (str, array) {
        var arr = array || [],
            len = arr.length;

        for (var i = 0; i < len; i++) {
            if (arr[i] == str) return true;
        }

        return false;
    }
};
/**数字*/
Shell.util.Number = {
    /**需要处理的数字错误信息*/
    valueErrorInfo: '错误!你正在处理一个无效的数字',
    /**小数位数错误信息*/
    numErrorInfo: '错误!小数位数不能小于0，参数num',
	/**转化为N位小数(不足补零)的字符串
	 * @param value 需要处理的数字
	 * @num 小数位数
	 */
    retainDecimaltoString: function (value, num) {
        var fNum = parseFloat(value),
            num = parseInt(num);

        if (isNaN(fNum)) {
            var errorInfo = "Shell.util.Number.retainDecimal " + this.valueErrorInfo + ":" + value;
            Shell.util.Msg.showLog(errorInfo, true);
            return false;
        }

        if (isNaN(num)) {
            var errorInfo = "Shell.util.Number.retainDecimal " + this.numErrorInfo + "=" + num;
            Shell.util.Msg.showLog(errorInfo, true);
            return false;
        }
        if (num === 0) return parseInt(value);

        var temp = Math.pow(10, num);

        fNum = Math.round(fNum * temp) / temp;

        var sNum = fNum.toString(),
            index = sNum.indexOf('.');

        if (index < 0) {
            index = sNum.length;
            sNum += '.';
        }
        while (sNum.length <= index + 2) {
            sNum += '0';
        }

        return sNum;
    }
};
/**时间*/
Shell.util.Date = {
    /**获取时间对象,不能转为时间的返回null*/
    getDate: function (value) {
        if (!value) return null;

        var type = Shell.util.typeOf(value),
            date = null;

        if (type == 'date') {
            date = Shell.util.clone(value);
        } else if (type == 'string') {
            if (value.length == 26 && value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                // /Date(1413993600000+0800)/
                value = parseInt(value.slice(6, -7));
            } else if (value.length == 27 && value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                // /Date(-1413993600000+0800)/
                value = parseInt(value.slice(6, -7));
            } else {
                value = value.replace(/-/g, '/');
            }
            date = new Date(value);
        } else if (type == 'number') {
            date = new Date(value);
        }

        var isDate = (Date.parse(date) == Date.parse(date));

        if (isDate) return date;
        return null;
    },
    /**校验对象是否是时间*/
    isValid: function (value) {
        var date = this.getDate(value);
        return date ? true : false;
    },
	/**获取距离value这个时间num天的时间对象;
	 * @param {date/string/number} value 当前时间
	 * @param {number} num 默认为1,可以负数,例如-1就是昨天,1是明天;
	 * @return {}
	 */
    getNextDate: function (value, num) {
        var date = this.getDate(value);
        if (!date) return null;

        var n = isNaN(num) ? 1 : parseInt(num);

        date.setDate(date.getDate() + n);

        return date;
    },
    /**获取时间字符串*/
    toString: function (value, onlyDate) {
        var date = this.getDate(value);
        if (!date) return null;

        var info = '',
            year = date.getFullYear() + '',
            month = (date.getMonth() + 1) + '',
            day = date.getDate() + '';

        month = month.length == 1 ? '0' + month : month;
        day = day.length == 1 ? '0' + day : day;

        info = year + '-' + month + '-' + day;

        if (!onlyDate) {
            var hours = date.getHours() + '',
                minutes = date.getMinutes() + '',
                seconds = date.getSeconds() + '';

            hours = hours.length == 1 ? '0' + hours : hours;
            minutes = minutes.length == 1 ? '0' + minutes : minutes;
            seconds = seconds.length == 1 ? '0' + seconds : seconds;

            info += ' ' + hours + ':' + minutes + ':' + seconds;
        }

        return info;
    },
    /**将时间转化为后台需要的格式,例如:\/Date(1359779125000)\/*/
    toServerDate: function (value) {
        var date = this.getDate(value);
        if (!date) return null;

        date = "\/Date(" + date.getTime() + "+0000)\/";
        return date;
    }
};

/**JSON数据处理*/
Shell.util.JSON = {
    /**解码错误信息-需要支持多语言*/
    decodeErrorInfo: '错误!你正在解码一个无效的JSON字符串',
    /**编码符号*/
    encodeMark: '"',
    /**解码*/
    decode: function (json, safe) {
        try {
            return Shell.util.JSON.doDecode(json);
        } catch (e) {
            if (safe === true) return null;

            var errorInfo = 'Shell.util.JSON.decode ' + this.decodeErrorInfo + ':' + json;
            layer.msg(errorInfo);
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
        var type = Shell.util.typeOf(value);
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

/**提示信息*/
Shell.util.Msg = {
    /**查看log错误信息*/
    showLog: function (value, isShow) {
        layer.msg(value);
    }
};

Shell.util.Cookie = {
	/**COOKIE默认缓存时间:单位小时*/
	DEFAULT_COOKIE_HOUSE: 24,
	/**@description cookie键值映射
	 * @type {Object}
	 */
	map: {
		'LABID': '000100', //实验室ID
        'LABNAME': '000101', //实验室名称
        'LABCODE': '000102',//实验室编码
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
		'REMEMBERPWD': '100006', //是否记住密码
		'LOGINDATETIMES': '100101' //记录当前登录时的本地时间毫秒数
	},
	/**@description 根据名称获取cookie值
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
	//设置cookie
	set:function(cname, cvalue, exdays) {
	    var d = new Date();
	    var exdays = exdays || 1;//默认过期时间1天
	    d.setTime(d.getTime() + (exdays*24*60*60*1000));
	    var expires = "expires="+d.toUTCString();
	    document.cookie = cname + "=" + cvalue + "; " + expires+"; path=/";
	},
	/**@description 取得COOKIE失效时间*/
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
		
		document.cookie= name + "=" + cval + ";expires=" + exp.toGMTString()+"; path=/";
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
				document.cookie=keys[i]+'=0;expires=' + new Date(0).toUTCString()+"; path=/";
			}
		} 
	}
};
/**系统信息*/
Shell.util.System = {
    //获得服务器当前时间
    getServerNowTime: function (callback) {
        var url = Shell.util.Path.ROOT + '/ServerWCF/ConstructionService.svc/CS_UDTO_GetServerInformation';
        $.ajax({
            type: "get",
            url: url,
            dataType: 'json',
            async:false,
            success: function (res) {
                if (res.success) {
                    if (res.ResultDataValue) {
                        var data = JSON.parse(res.ResultDataValue);
                        if (typeof(callback) == "function") {
                            callback(data.ServerCurrentTime);
                        }
                    }
                } else {
                    layer.msg("服务器时间获取失败！", { icon: 5, anim: 6 });
                }
            }
        });
    }
};
Shell.util.Path.init();