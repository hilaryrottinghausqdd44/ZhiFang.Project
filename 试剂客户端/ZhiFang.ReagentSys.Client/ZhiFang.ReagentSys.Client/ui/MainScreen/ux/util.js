/**
	@name：layui.ux.util 工具集
	@author：Jcall
	@version 2019-03-25
 */
layui.define('jquery', function (exports) {
    "use strict";
    
    var $ = layui.$;

    //外部接口
    var uxutil = {
        //静态地址
        path: {
            LOCAL: '',//主机地址
            ROOT: '',//主机地址/项目名
            UI: '',//主机地址/项目名/UI包名
            EXTJS: '',
            LAYUI: '',
            LIIP_ROOT: '',
            BUSINESSANALYSIS_ROOT: '',//财务系统地址
            init: function () {
                this.LOCAL = this.getLocal();
                this.ROOT = this.getRootPath();
                this.UI = this.ROOT + '/ui';
                this.EXTJS = this.UI + '/extjs';
                this.LAYUI = this.UI + '/layui';
                this.LIIP_ROOT = this.LOCAL + '/ZhiFang.LabInformationIntegratePlatform';
                this.BUSINESSANALYSIS_ROOT = this.LOCAL + '/ZhiFang.Digitlab.BusinessAnalysis';//财务系统地址
            },
            getLocal: function () {
            	var href = window.document.location.href,
            		projectName = window.document.location.pathname.split('/')[1],
            		local = href.split('/' + projectName)[0];
            		
                //主机+端口，例如：http://localhost:8080
                return local;
            },
            getRootPath: function () {
            	//主机+端口+项目名，例如：http://localhost:8080/Test
                return this.getLocal() + '/' + window.document.location.pathname.split('/')[1];
            }
        },
        //cookie信息
        cookie: {
            //COOKIE默认缓存时间:单位小时
            DEFAULT_COOKIE_HOUSE: 24,
            //cookie键值映射
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
            //根据名称获取cookie值
            get: function (name) {
                var nameT = this.map[name] || name,
                    reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
                    arr = document.cookie.match(reg);

                if (arr) {
                    return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
                }

                return null;
            },
			/**
			 * @name 设置cookie键值内容
			 * @param {Array Object} value Cookie键值对象或数组
			 * @param {Number} days 过期时间
			 * @example [{name:'a_key',value:'a_value',expires:{Date}}]
			 */
            set: function (value, days) {
                if (!value) return;

                var type = typeof (value);
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
                    document.cookie = name + "=" + encodeURI(value) + "; expires=" + exp.toGMTString() + "; path=/";
                }
            },
            //删除cookie
            delCookie: function (name) {
                var cval = this.get(name);
                if (!cval) return;
                this.set({ name: name, value: '' }, -1);
            },
            //获取所有cookie
            getAllCookie: function () {
                var keys = document.cookie.match(/[^ =;]+(?=\=)/g);
                var list = [];
                if (keys) {
                    for (var i = 0; i < keys.length; i++) {
                        list.push([keys[i], this.get(keys[i])]);
                    }
                }
                return list;
            },
            //清理所有cookie
            clearCookie: function () {
                var keys = document.cookie.match(/[^ =;]+(?=\=)/g);
                if (keys) {
                    for (var i = 0; i < keys.length; i++) {
                        this.delCookie(keys[i]);
                    }
                }
            },

            //删除cookie
            del: function (name) {
                return this.delCookie(name);
            },
            //清理所有cookie
            clear: function () {
                return this.clearCookie();
            },
            //获取所有cookie
            getAll: function () {
                return this.getAllCookie();
            }
        },
        //本地数据存储
        localStorage: {
            set: function (name, value) {
                localStorage.setItem(name, value);
            },
		    /**
			 * 获取本地数据
			 * @param {Object} name 键值
			 * @param {Object} isDecode 是否解码
			 */
            get: function (name, isDecode) {
                var data = localStorage.getItem(name);
                if (isDecode && data) { data = JSON.parse(data); }
                return data;
            },
            remove: function (name) {
                localStorage.removeItem(name);
            }
        },
        //session本地数据存储
        sessionStorage:{
        	set: function (name, value) {
                sessionStorage.setItem(name, value);
            },
		    /**
			 * 获取本地数据
			 * @param {Object} name 键值
			 * @param {Object} isDecode 是否解码
			 */
            get: function (name, isDecode) {
                var data = sessionStorage.getItem(name);
                if (isDecode && data) { data = JSON.parse(data); }
                return data;
            },
            remove: function (name) {
                sessionStorage.removeItem(name);
            }
        },
        //参数
        params: {
            //获取页面传递的参数
            //@param toUpperCase 是否将参数名转化为大写
            get: function (toUpperCase) {
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
            //获取模块组件JOSN
            getComponentsListJson: function(){
            	var params = this.get(true),
            		value = null;
            		
            	if(params.MODULEID){
            		value = parent.getComponentsListJson(params.MODULEID);
            	}
            	return value;
            }
        },
        //服务器交互
        server: {
            //返回参数
            resultParams: {
                success: "success",
                msg: "ErrorInfo",
                value: "ResultDataValue",
                code: "ResultCode"
            },
            //ajax请求
            //可配置参数showError：true返回原始错误信息，false返回替换的错误信息
            ajax: function (config, callback, showError) {
                var me = this;
                var con = {
                    type: "get",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false
                };

                for (var i in config) {
                    con[i] = config[i];
                }

                if (!con.success) {
                    con.success = function (data, textStatus) {
                        if (typeof (data) != 'object') {
                            callback(data);
                            return;
                        }
                        
                        //session有效性检验
                        if(window.top.onSessionValid && !window.top.onSessionValid(data[me.resultParams.code])){
                        	return;
                        }

                        var msg = data[me.resultParams.msg],
                            value = data[me.resultParams.value];

                        data.msg = (!data.success && msg && !showError) ? "服务器出错了 " : msg;

                        if (value && typeof (value) === "string") {
                            if (isNaN(value)) {
                                value = value.replace(/\r\n/g, '').replace(/\r/g, '').replace(/\n/g, '');
                                try {
                                    value = eval("(" + value + ")");
                                } catch (e) {
                                    //data.success = false;
                                    data.msg = '返回的数据无法解析';
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
            },
			
            //服务器时间
            date: {
                //服务器错误
                isError:function () {
                	var system = layui.system || top.layui.system;
                    if(system){
                    	return system.date.isError();
                	}else{
                		console && console.log("请引入system插件，位置：ux/zf/system");
                	}
                },
                //获取服务器时间对象
                getDate: function () {
                    var system = layui.system || top.layui.system;
                    if(system){
                    	return system.date.getDate();
                	}else{
                		console && console.log("请引入system插件，位置：ux/zf/system");
                	}
                },
                //获取服务器时间-毫秒数
                getTimes: function () {
                    var system = layui.system || top.layui.system;
                    if(system){
                    	return system.date.getTimes();
                	}else{
                		console && console.log("请引入system插件，位置：ux/zf/system");
                	}
                },
                //启动
                init: function (callback) {
                	var system = layui.system || top.layui.system;
                    if(system){
                    	return system.date.init(callback);
                	}else{
                		console && console.log("请引入system插件，位置：ux/zf/system");
                	}
                }
            }
        },
        //时间处理
        date: {
            //获取时间对象,不能转为时间的返回null
            getDate: function (value) {
                if (!value) return null;

                var type = typeof value,
                    date = null,
                    isDate = value instanceof Date;

                if (isDate) {
                    date = new Date(value.getTime());
                    return date;
                } else {
                    if (type == 'number' || !isNaN(value)) {
                        if (!isNaN(value)) { value = parseInt(value); }
                        date = new Date(value);
                    } else if (type == 'string') {
                        if ((value.length == 26 || value.length == 27) &&
                            value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                            // /Date(1413993600000+0800)/ /Date(-1413993600000+0800)/
                            value = parseInt(value.slice(6, -7));
                        } else {
                            value = value.replace(/-/g, '/');
                        }
                        date = new Date(value);
                    }
                    isDate = (Date.parse(date) == Date.parse(date));

                    return isDate ? date : null;
                }
            },
            //校验对象是否是时间
            isValid: function (value) {
                var date = this.getDate(value);
                return date ? true : false;
            },
			/**获取距离value这个时间num天的时间对象
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
			/**获取时间字符串
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
                        case 0: text += "日"; break;
                        case 1: text += "一"; break;
                        case 2: text += "二"; break;
                        case 3: text += "三"; break;
                        case 4: text += "四"; break;
                        case 5: text += "五"; break;
                        case 6: text += "六"; break;
                    }
                    info += " " + text;
                }

                return info;
            },
            //将时间转化为后台需要的格式,例如:\/Date(1359779125000)\/
            toServerDate: function (value) {
                var v = this.getDate(value);
                if (!v) return null;
                return "\/Date(" + v.getTime() + "+0000)\/";
            },
            //校验是否一个月的第一天
            isMonthFirstDate: function (value) {
                var v = this.getDate(value);
                if (!v) return false;

                //每个月的1号就是第一天
                if (v.getDate() == 1) return true;

                return false;
            },
            //校验是否一个月的最后一天
            isMonthLastDay: function (value) {
                var v = this.getDate(value);
                if (!v) return false;

                var month = v.getMonth();
                var month2 = this.getNextDate(v).getMonth();

                //符合条件：当天时间加上一天就是下个月的第一天
                if ((month2 - month - 1) % 12 == 0) return true;

                return false;
            },
            //检验是否整月
            isFullMonth: function (start, end) {
                var s = this.getDate(start);
                var e = this.getNextDate(end);

                //start < end
                if (Date.parse(s) >= Date.parse(e)) return false;

                //start的日期 = end的日期
                if (s.getDate() != e.getDate()) return false;

                return true;
            },
            //获取一个月的第一天
            getMonthFirstDate: function (year, month, toString) {
                var m = ((month + '').length == 1 ? '0' : '') + month;
                var date = year + '-' + m + '-01';

                if (!toString) date = this.getDate(date);
                return date;
            },
            //获取一个月的最后一天
            getMonthLastDate: function (year, month, toString) {
                var date = new Date(year, month, 0);
                if (toString) date = this.toString(date, true);
                return date;
            },
            //时间差
            difference: function (start, end) {
                var date1 = this.getDate(start);
                var date2 = this.getDate(end);
                if (!date1 || !date2) {
                    return null;
                }

                var times = date2.getTime() - date1.getTime();
                
                return this.getDateContentByTimes(times);
            },
            //根据时间毫秒数返回时间文字显示内容
            getDateContentByTimes: function (times){
            	if(!times || isNaN(times)) return null;
            	
            	var result = [];
            	
            	if (times < 0) {
                    times = 0 - times;
                    result.push('-');
                }
            	
            	//计算出相差天数
                var days = Math.floor(times / (24 * 3600 * 1000));
                if (days > 0) { result.push(days + '天'); }
                //计算出小时数
                var leave1 = times % (24 * 3600 * 1000);//计算天数后剩余的毫秒数
                var hours = Math.floor(leave1 / (3600 * 1000));
                if (hours > 0) { result.push(hours + '小时'); }
                //计算相差分钟数
                var leave2 = leave1 % (3600 * 1000);//计算小时数后剩余的毫秒数
                var minutes = Math.floor(leave2 / (60 * 1000));
                if (minutes > 0) { result.push(minutes + '分钟'); }
                //计算相差秒数
                var leave3 = leave2 % (60 * 1000);//计算分钟数后剩余的毫秒数
                var seconds = Math.round(leave3 / 1000);
                if (seconds > 0) { result.push(seconds + '秒'); }

                return result.join('');
            }
        },
        //数字处理
        number: {
            //获取大写金额
            getMoney: function (num) {
                var strUnit = '仟佰拾亿仟佰拾万仟佰拾元角分',
                    strOutput = "";

                num += "00";
                var intPos = num.indexOf('.');
                if (intPos >= 0) {
                    num = num.substring(0, intPos) + num.substr(intPos + 1, 2);
                }
                strUnit = strUnit.substr(strUnit.length - num.length);
                for (var i = 0; i < num.length; i++) {
                    strOutput += '零壹贰叁肆伍陆柒捌玖'.substr(num.substr(i, 1), 1) + strUnit.substr(i, 1);
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
        },
        //字符串处理
        string: {
			/**字符串转码
			 * @param value 需要转码的字符串
			 * @param unReserved 不转义保留字符
			 */
            encode: function (value, unReserved) {
                var v = value || '';
                //不转义保留字符,转义保留字符
                v = unReserved ? encodeURI(v) : encodeURIComponent(v);

                return v;
            },
			/**字符串解码
			 * @param value 需要解码的字符串
			 * @param unReserved 不转义保留字符
			 */
            decode: function (value, unReserved) {
                var v = value || '';
                //不转义保留字符,转义保留字符
                v = unReserved ? decodeURI(v) : decodeURIComponent(v);

                return v;
            },
            //字符串-获取以ASCII编码字节数 英文占1字节 中文占2字节
            lenASCII: function (str) {
                if (typeof str != 'string') return -1;
                //将所有非\x00-\xff字符换为xx两个字符,再计算字符串
                return str.replace(/[^\x00-\xff]/g, 'xx').length;
            },
            //获取固定字节数的子串
            substrASCII: function (str, start, lenASCII) {
                if (typeof str != 'string') return null;
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
            //字符串是否在数组中存在
            inArray: function (str, array) {
                var arr = array || [],
                    len = arr.length;

                for (var i = 0; i < len; i++) {
                    if (arr[i] == str) return true;
                }

                return false;
            }
        },
        //字节处理
        bytes: {
            //计数单位
            UNIT: 1024,
            //自动匹配单位显示
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
        },
        //交互处理
        action: {
            //默认延时时间
            DELAYTIME: 500,
            //延时处理
            delay: function (fun, delayTime, scope) {
                if (typeof fun != 'function') return;

                var me = scope || this,
                    delayTime = delayTime || this.DELAYTIME;

                me.etime = new Date().getTime();

                if (me.etime - me.stime < delayTime && me.waitAction) {
                    clearTimeout(me.waitAction);
                }

                me.waitAction = setTimeout(fun, delayTime);

                me.stime = new Date().getTime();
            }
        },
        //PC电脑
        pc: {
            //桌面
            desktop: {
                width: screen.availWidth,
                height: screen.availHeight
            }
        },
        Random: {
            chars :['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'],
            generateMixed: function (n) {
                var res = "";
                for (var i = 0; i < n; i++) {
                    var id = Math.ceil(Math.random() * 35);
                    res += this.chars[id];
                }
                return res;
            }
        },
        //判断IE版本
		IEVersion:function(){
			var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
			var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1; //判断是否IE<11浏览器  
			var isEdge = userAgent.indexOf("Edge") > -1 && !isIE; //判断是否IE的Edge浏览器  
			var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf("rv:11.0") > -1;
			if(isIE) {
				var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
				reIE.test(userAgent);
				var fIEVersion = parseFloat(RegExp["$1"]);
				if(fIEVersion == 7) {
					return 7;
				} else if(fIEVersion == 8) {
					return 8;
				} else if(fIEVersion == 9) {
					return 9;
				} else if(fIEVersion == 10) {
					return 10;
				} else {
					return 6;//IE版本<=7
				}   
			} else if(isEdge) {
				return 'edge';//edge
			} else if(isIE11) {
				return 11; //IE11  
			}else{
				return -1;//不是ie浏览器
			}
		}
    };
	
	//加载json2，解决IE6、7、8使用JSON.stringify报JSON未定义错误的问题
    var ieVersion = uxutil.IEVersion();
	if(ieVersion == 6 || ieVersion == 7 || ieVersion == 8){//非IE浏览器
		$.getScript(LAYUI + '/ux/json2.js',function(){});
	}
	//初始化路劲
    uxutil.path.init();
    //暴露接口
    exports('uxutil', uxutil);
});
