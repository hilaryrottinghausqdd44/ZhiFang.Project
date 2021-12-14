var Shell = Shell || {};
Shell.util = Shell.util || {};

/**系统路径*/
Shell.util.Path = {
    /**项目根目录*/
    rootPath: '',
    /**ui包根目录*/
    uiPath: '',
    /**图片根目录*/
    imgPath: '',
    /**初始化路径内容*/
    init: function () {
        this.rootPath = this.getRootPath();
        this.uiPath = this.getUiPath();
        this.imgPath = this.rootPath + '/Img';
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
		var name = pathName.split('/').slice(1, 5).join('/');

		return this.getLocal() + '/' + name;
	},
	getLocal: function() {
		var curWwwPath = window.document.location.href;
		var pathName = window.document.location.pathname;
		var pos = curWwwPath.indexOf(pathName);
		var localhostPath = curWwwPath.substring(0, pos);
		return localhostPath;
	}
}; Shell.util.Path.init();

/**事件处理*/
Shell.util.Event = {
    /**触摸事件,可修改为
	 * touchstart 触摸开始就执行
	 * touchend 触摸结束执行
	 * click 点击执行
	 */
    touch: "touchend",
    /**延时处理*/
    delay: function (fun, scope, delayTime) {
        if (Shell.util.typeOf(fun) != 'function') {
            console.log('Shell.util.Event.delay方法参数错误:fun参数不是function!');
            return;
        }

        var me = scope || this,
			delayTime = delayTime || 300;

        me.etime = new Date().getTime();

        if (me.etime - me.stime < delayTime && me.waitAction) {
            clearTimeout(me.waitAction);
        }

        me.waitAction = setTimeout(fun, delayTime);

        me.stime = new Date().getTime();
    },
    /**初始化DOM对象的点击判断*/
    initTouch: function (dom) {
        if (!dom) return;
        dom.addEventListener("touchstart", function (e) {
            if (e.targetTouches.length == 1 && !self.busy) {
                var xy = e.targetTouches[0];
                dom.touch_start_x = xy.pageX;
                dom.touch_start_y = xy.pageY;
                dom.touch_end_x = xy.pageX;
                dom.touch_end_y = xy.pageY;
            }
        }, false);
        dom.addEventListener("touchmove", function (e) {
            //e.preventDefault();
            if (e.targetTouches.length == 1 && !self.busy) {
                var xy = e.targetTouches[0];
                dom.touch_end_x = xy.pageX;
                dom.touch_end_y = xy.pageY;
            }
        }, false);
        dom.addEventListener("touchend", function (e) {

        }, false);
        //X轴移动距离
        dom.move_x = function () {
            if (dom.touch_end_x == null ||
				dom.touch_start_x == null) return null;
            return dom.touch_end_x - dom.touch_start_x;
        };
        //Y轴移动距离
        dom.move_y = function () {
            if (dom.touch_end_y == null ||
				dom.touch_start_y == null) return null;
            return dom.touch_end_y - dom.touch_start_y;
        };
        //DOM元素内部是否是点击事件
        dom.isClick = function () {
            var move_x = dom.move_x(),
				move_y = dom.move_y(),
				bo = true;

            if (move_x && (move_x > 1 || move_x < -1))
                bo = false;

            if (move_y && (move_y > 1 || move_y < -1))
                bo = false;

            return bo;
        };
    }

};
/**校验处理*/
Shell.util.Isvalid = {
    /**
	 * 手机号码
	 * 移动：134[0-8],135,136,137,138,139,150,151,157,158,159,182,187,188
	 * 联通：130,131,132,152,155,156,185,186
	 * 电信：133,1349,153,180,189
	 */
    isCellPhoneNo: function (value) {
        if (!value || value.length != 11) return false;
        var filter = /^1[3|5|8][0-9]\d{4,8}$/;
        return filter.test(value);
    },
    /**
	 * 身份证号码
	 * 支持15位和18位身份证号码校验
	 */
    isIdCardNo: function (value) {
        var filter = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
        return filter.test(value);
    }
};
/**获取封装的组件*/
Shell.util.UI = {
    /**模板解析样式*/
    pattern: /{(.+?)}/g,
    /**列表*/
    grid: {
        /**创建列表
		 * @param options{
		 * 		id
		 * 		data
		 * 		rowModel
		 * 		loadMoreisMultiObject
		 * 		hideLoadMore
		 * }
		 */
        createGrid: function (config) {
            var config = config || {},
				rows = this.createRows(config.data, config.rowModel, config.isMultiObject),
				divs = [],
				html = "";

            if (rows) {
                divs.push(rows);
                if (!config.hideLoadMore) {
                    divs.push('<div id="' + config.id + '_load_more" class="grid_load_more"><span>加载更多</span></div>');
                }
            } else {
                divs.push('没有查到记录');
            }

            html = divs.join("");
            if (config.id) {
                html = '<div id="' + config.id + '">' + html + '</div>';
            }

            return divs.join("");
        },
        /**创建所有的数据行*/
        createRows: function (data, rowModel, isMultiObject) {
            var rowModel = rowModel || "",
				len = data.length,
				divs = [];

            if (isMultiObject) {
                for (var i = 0; i < len; i++) {
                    divs.push(this.createRowByMultiObject(data[i], rowModel));
                }
            } else {
                for (var i = 0; i < len; i++) {
                    divs.push(this.createRow(data[i], rowModel));
                }
            }

            return divs.join("");
        },
        /**创建数据行*/
        createRow: function (info, rowModel, isMultiObject) {
            //允许行数据整理函数，用于复杂逻辑
            if (typeof (rowModel) == "function") {
                return rowModel(info);
            }

            var arr = rowModel.match(Shell.util.UI.pattern) || [],
				len = arr.length,
				value = rowModel;

            for (var i = 0; i < len; i++) {
                value = value.replace(eval("/" + arr[i] + "/g"), (info[arr[i].slice(1, -1)] || ""));
            }

            return value;
        },
        /**创建数据行_立体对象*/
        createRowByMultiObject: function (info, rowModel) {
            //允许行数据整理函数，用于复杂逻辑
            if (typeof (rowModel) == "function") {
                return rowModel(info);
            }

            var arr = rowModel.match(Shell.util.UI.pattern),
				len = arr.length,
				value = rowModel;

            for (var i = 0; i < len; i++) {
                value = value.replace(eval("/" + arr[i] + "/g"), this.getMultiObjectValue(arr[i].slice(1, -1)));
            }

            return value;
        },
        /**获取立体对象属性值*/
        getMultiObjectValue: function (obj, keyStr) {
            var arr = arr[i].split("."),
				len = arr.length,
				value = obj;

            for (var j = 0; j < len; j++) {
                value = value[arr[j]];
            }

            return value || "";
        }
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
    ajax: function (config, callback) {
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
                var msg = data[Shell.util.Server.resultParams.msg],
					value = data[Shell.util.Server.resultParams.value];

                data.msg = (!data.success && msg && !config.showError) ? "服务器出错了 " : msg;

                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
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
                callback({
                    success: false,
                    msg: Shell.util.Server.getMsgByStatus(XMLHttpRequest.status)
                });
            };
        }
        con.headers = con.headers || {};
        //智方科技MD5加密值
        con.headers.Type = "503DC7557EB732E2E9ACBC1F4DE5455B";
        con.headers["User-Agent-Zip-Fa"] = "6D0F3E94-B672-4BFD-B614-00398C73447D";
        
        //秘钥加密处理
    	var MD5Count = 3;//加密次数
    	var startIndex = 5;//下标开始位数：5
    	var endIndex = 9;//下标结束位置：9
    	var userId = Shell.util.Cookie.getCookie("EmployeeID");
    	var ServerCurrentTimeKey = Shell.util.Cookie.getCookie("ServerCurrentTimeKey");
    	if(ServerCurrentTimeKey){
    		var indexs = ServerCurrentTimeKey.slice(startIndex,endIndex+1);
        	var keys = [];
        	for(var i in indexs){
        		keys.push(userId[indexs[i]]);
        	}
        	var keyValue = userId + keys.join("");
        	for(var i=0;i<MD5Count;i++){
        		keyValue = Shell.util.MD5.encode(keyValue);
        	}
        	con.headers['User-Agent-ZF'] = keyValue;
    	}
        
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

/**cookie操作*/
Shell.util.Cookie = {
    /**cookie键值映射*/
    mapping: {
        "openId": "100001",
        "userId": "100002",
        "EmployeeID": "000200",
        "EmployeeName": "000201",
        "HRDeptID": "000400",
		'ServerCurrentTimeKey':'100007',//密钥
    },
    /**取得cookie属性*/
    getCookie: function (name) {
        var nameT = Shell.util.Cookie.mapping[name] || name,
			reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
			arr = document.cookie.match(reg);

        if (arr) return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
        return null;
    },
    /**设置cookie属性*/
    setCookie: function (name, value) {       
        document.cookie = name + "=" + encodeURI(value) + ";path=/";
    },
    /**删除cookie属性*/
    delCookie: function (name) {
        this.setCookie(name, '', -1);
    }
};

/**获取页面传递的参数
 * @param toUpperCase 是否将参数名转化为大写
 */
Shell.util.getRequestParams = function (toUpperCase) {
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
Shell.util.typeOf = function (value) {
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
    decode: function (json, safe) {
        try {
            return Shell.util.JSON.doDecode(json);
        } catch (e) {
            if (safe === true) return null;

            var errorInfo = 'Shell.util.JSON.decode ' + this.decodeErrorInfo + ':' + json;
            window.console.log(errorInfo);
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
	encodeStr:function(str){
		return str.replace(/\\/g,'\\\\').replace(/[\r\n]/g,'\\n').replace(
			eval('/' + this.encodeMark + '/g'),'\\' + this.encodeMark);
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

/**
 * @description 时间处理
 */
Shell.util.Date = {
    /**获取时间对象,不能转为时间的返回null*/
    getDate: function (value) {
        if (!value) return null;

        var type = Shell.util.typeOf(value),
			date = null;

        if (type == 'date') {
            var times = value.getTime();
            date = new Date(times);
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
    isValid: function (value) {
        var date = this.getDate(value);
        return date ? true : false;
    },
    /**
	 * 获取距离value这个时间num天的时间对象;
	 * @param {date/string/number} value 当前时间
	 * @param {number} num 默认为1,可以负数,例如-1就是昨天,1是明天;
	 * @return {}
	 */
    getNextDate: function (value, num) {
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
    toString: function (value, onlyDate, hasMilliseconds, hasDay) {
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
    toServerDate: function (value) {
        var v = this.getDate(value);
        if (!v) return null;

        return "\/Date(" + v.getTime() + "+0000)\/";
    },
    /**校验是否一个月的第一天*/
    isMonthFirstDate: function (value) {
        var v = this.getDate(value);
        if (!v) return false;

        //每个月的1号就是第一天
        if (v.getDate() == 1) return true;

        return false;
    },
    /**校验是否一个月的最后一天*/
    isMonthLastDay: function (value) {
        var v = this.getDate(value);
        if (!v) return false;

        var month = v.getMonth();
        var month2 = this.getNextDate(v).getMonth();

        //符合条件：当天时间加上一天就是下个月的第一天
        if ((month2 - month - 1) % 12 == 0) return true;

        return false;
    },
    /**检验是否整月*/
    isFullMonth: function (start, end) {
        var s = this.getDate(start);
        var e = this.getNextDate(end);

        //start < end
        if (Date.parse(s) >= Date.parse(e)) return false;

        //start的日期 = end的日期
        if (s.getDate() != e.getDate()) return false;

        return true;
    },
    /**获取一个月的第一天*/
    getMonthFirstDate: function (year, month, toString) {
        var m = ((month + '').length == 1 ? '0' : '') + month;
        var date = year + '-' + m + '-01';

        if (!toString) date = this.getDate(date);

        return date;
    },
    /**获取一个月的最后一天*/
    getMonthLastDate: function (year, month, toString) {
        var date = new Date(year, month, 0);

        if (toString) date = this.toString(date, true);

        return date;
    }
};
// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

Shell.util.Bytes = {
    /**计数单位*/
    UNIT: 1024,
    /**字符串转码
	 * @param value 需要转码的字符串
	 * @param unReserved 不转义保留字符
	 */
    toSize: function (bytes) {
        if (bytes === 0) {
            return '0 B';
        }

        var k = this.UNIT,
	        sizes = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
	        i = Math.floor(Math.log(bytes) / Math.log(k));

        var result = (bytes / Math.pow(k, i)).toPrecision(3) + ' ' + sizes[i];

        return result;
    }
};
//Base64加密
Shell.util.MD5 = {
    // private property
    _keyStr:"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
    // public method for encoding
    encode:function (input) {
    	var me = this;
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = me._utf8_encode(input);
        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);
            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;
            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }
            output = output +
            me._keyStr.charAt(enc1) + me._keyStr.charAt(enc2) +
            me._keyStr.charAt(enc3) + me._keyStr.charAt(enc4);
        }
        return output;
    },
    // public method for decoding
    decode:function (input) {
    	var me = this;
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;
        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");
        while (i < input.length) {
            enc1 = _keyStr.indexOf(input.charAt(i++));
            enc2 = _keyStr.indexOf(input.charAt(i++));
            enc3 = _keyStr.indexOf(input.charAt(i++));
            enc4 = _keyStr.indexOf(input.charAt(i++));
            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;
            output = output + String.fromCharCode(chr1);
            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }
        }
        output = me._utf8_decode(output);
        return output;
    },
    // private method for UTF-8 encoding
    _utf8_encode:function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            } else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            } else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }
        return utftext;
    },
    // private method for UTF-8 decoding
    _utf8_decode:function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;
        while (i < utftext.length) {
            c = utftext.charCodeAt(i);
            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            } else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            } else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }
        return string;
    }
};