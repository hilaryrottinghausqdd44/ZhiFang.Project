/**
	@name：layui.ux.util 工具集
	@author：Jcall
	@version 2019-03-25
 */
var scripts = document.head.getElementsByTagName('script'), //获所有script标签
	script = scripts[scripts.length - 1],
	url = script.src,
	UI = url.split('/layui/ux/util.js')[0],
	ROOT = (UI.indexOf('/ui') >= 0) ? UI.split('/ui')[0] : UI.split('/UI')[0],
	LOCAL = ROOT.substring(0, ROOT.lastIndexOf('/')),
	EXTJS = UI + '/extjs',
	LAYUI = UI + '/layui';

layui.define('jquery', function(exports) {
	"use strict";

	var $ = layui.$;

	//外部接口
	var uxutil = {
		//是否存在集成平台
		ISHASLIIP: false,
		//BS服务调用的超时设置
		BS_TIME_OUT:66000,
		//静态地址
		path: {
			LOCAL: LOCAL, //主机地址
			ROOT: ROOT, //主机地址/项目名
			UI: UI, //主机地址/项目名/UI包名
			EXTJS: EXTJS,
			LAYUI: LAYUI,
			LIIP_ROOT: LOCAL + '/ZhiFang.LabInformationIntegratePlatform',
			RBAC_ROOT: '', //集成平台URL,为空时取当前应用程序的ROOT			
			init: function() {
				this.LOCAL = this.getLocal();
				this.ROOT = this.getRootPath();
				this.RBAC_ROOT = this.getRBACRootPath();
				this.UI = this.getUiPath();
				this.EXTJS = this.getExtjsPath();
				this.LAYUI = this.getLayuiPath();
			},
			getLocal: function() {
				var location = window.document.location,
					curWwwPath = location.href,
					pathName = location.pathname,
					pos = curWwwPath.indexOf(pathName),
					localhostPath = curWwwPath.substring(0, pos);

				return localhostPath;
			},
			getRootPath: function() {
				var pathName = window.document.location.pathname,
					projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1),
					rootPath = this.getLocal() + projectName;

				return rootPath;
			},
			getRBACRootPath: function() {
				var rootPath = this.RBAC_ROOT;
				if (!rootPath) rootPath = this.getRootPath();
				//console.log("util.getRBACRootPath:"+rootPath);
				return rootPath;
			},
			getUiPath: function() {
				var pathName = window.document.location.pathname,
					name = pathName.split('/').slice(1, 3).join('/'),
					uiPath = this.getLocal() + '/' + name;

				return uiPath;
			},
			getExtjsPath: function() {
				var pathName = window.document.location.pathname,
					name = pathName.split('/').slice(1, 3).join('/'),
					uiPath = this.getLocal() + '/' + name + '/extjs';

				return uiPath;
			},
			getLayuiPath: function() {
				var pathName = window.document.location.pathname,
					name = pathName.split('/').slice(1, 3).join('/'),
					uiPath = this.getLocal() + '/' + name + '/layui';

				return uiPath;
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
				'REMEMBERPWD': '100006' ,//是否记住密码
				
				'DOCTORID': '200001', //6.6人员帐号绑定的所属医生Id
				'DOCTORCNAME': '200002', //6.6人员帐号绑定的所属医生姓名
				'GRADEID': '200003' //6.6人员帐号绑定的所属医生的等级
			},
			//根据名称获取cookie值
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
			 * @name 设置cookie键值内容
			 * @param {Array Object} value Cookie键值对象或数组
			 * @param {Number} days 过期时间
			 * @example [{name:'a_key',value:'a_value',expires:{Date}}]
			 */
			set: function(value, days) {
				if (!value) return;

				var type = typeof(value);
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
			delCookie: function(name) {
				var cval = this.get(name);
				if (!cval) return;
				this.set({
					name: name,
					value: ''
				}, -1);
			},
			//获取所有cookie
			getAllCookie: function() {
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
			clearCookie: function() {
				var keys = document.cookie.match(/[^ =;]+(?=\=)/g);
				if (keys) {
					for (var i = 0; i < keys.length; i++) {
						this.delCookie(keys[i]);
					}
				}
			},

			//删除cookie
			del: function(name) {
				return this.delCookie(name);
			},
			//清理所有cookie
			clear: function() {
				return this.clearCookie();
			},
			//获取所有cookie
			getAll: function() {
				return this.getAllCookie();
			}
		},
		//参数
		params: {
			//获取页面传递的参数
			//@param toUpperCase 是否将参数名转化为大写
			get: function(toUpperCase) {
				var url = location.search; //获取url中"?"符后的字串

				if (url.indexOf("?") == -1) return {};

				var str = url.substr(1),
					strs = str.split("&"),
					len = strs.length,
					params = {};

				for (var i = 0; i < len; i++) {
					//原来代码				
					//var arr = strs[i].split("=");
					
					/**
					 * 修改日期:2020-03-16
					 * 修改人:longfc
					 * 解决问题:(1)解决样例:"moduleCode=/layui/views/bloodtransfusion/sysbase/bloodusedesc/index.html?testCode=123&t=1584340648709"	
					 * (2)需要返回的是arr[0]=moduleCode,arr[1]=/layui/views/bloodtransfusion/sysbase/bloodusedesc/index.html?testCode=123&t=1584340648709
					 */
					var index1=strs[i].indexOf("=");//取第一个等号的索引
					var str2 = strs[i].substr(index1+1);//取arr[1]的结果值
					var arr = strs[i].split("=");
					if(arr.length>=2)arr[1]=str2;
					if (toUpperCase) {
						arr[0] = arr[0].toLocaleUpperCase();
					}
					params[arr[0]] = decodeURI(arr[1]);
				}

				return params;
			}
		},
		//服务器交互
		server: {
			//返回参数
			resultParams: {
				success: "success",
				msg: "ErrorInfo",
				value: "ResultDataValue",
				HasInterface: "HasInterface",
				InterfaceSuccess: "InterfaceSuccess",
				InterfaceMsg: "InterfaceMsg"
			},
			//ajax请求
			//可配置参数showError：true返回原始错误信息，false返回替换的错误信息
			ajax: function(config, callback, showError) {
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
					con.success = function(data, textStatus) {
						if (typeof(data) != 'object') {
							callback(data);
							return;
						}

						var msg = data[me.resultParams.msg],
							value = data[me.resultParams.value];

						//data.msg = (!data.success && msg && !showError) ? "服务器出错了 " : msg;
						data.msg = (!data.success && msg) ? msg:"服务器出错了 " ;
						if (value && typeof(value) === "string") {
							if (isNaN(value)) {
								value = value.replace(/\r\n/g, '').replace(/\r/g, '').replace(/\n/g, '');
								try {
									value = eval("(" + value + ")");
								} catch (e) {
									data.success = false;
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
			},

			//服务器时间
			date: {
				//每隔一段时间向服务器校准时间，单位：秒
				seconds: 300,
				//失败时尝试请求的次数
				tryTimes: 10,
				//当前的尝试次数
				_tryCount: 0,
				_sysTime: null,
				_url: '/ServerWCF/ConstructionService.svc/CS_UDTO_GetServerInformation',
				_leftSeconds: null,
				_milliseconds: 1000,
				_isError: null,
				_cookieKey: 'serverdate',
				//服务器错误
				isError: function() {
					return this._isError;
				},
				//获取服务器时间对象
				getDate: function() {
					var me = this;
					me._sysTime = uxutil.cookie.get(me._cookieKey);
					return new Date(parseInt(me._sysTime));
				},
				//获取服务器时间-毫秒数
				getTimes: function() {
					var me = this;
					me._sysTime = uxutil.cookie.get(me._cookieKey);
					return me._sysTime;
				},
				_next: function() {
					var me = this;
					me._leftSeconds--;

					if (me._leftSeconds == 0) {
						me.init();
					} else {
						me._sysTime = new Date(parseInt(me._sysTime) + me._milliseconds).getTime();
						uxutil.cookie.set({
							name: me._cookieKey,
							value: me._sysTime
						});
						setTimeout(function() {
							me._next();
						}, me._milliseconds);
					}
				},
				//启动
				init: function(callback) {
					var me = this;
					me._leftSeconds = me.seconds;

					uxutil.server.ajax({
						url: uxutil.path.ROOT + this._url
					}, function(data) {
						if (data.success) {
							me._isError = false;
							var d = data.value.ServerCurrentTime;
							me._sysTime = new Date(d).getTime();
							uxutil.cookie.set({
								name: me._cookieKey,
								value: me._sysTime
							});

							setTimeout(function() {
								me._next();
							}, me._milliseconds);
							if (callback) {
								callback();
							}
						} else {
							if (me._tryCount < me.tryTimes) {
								me._tryCount++;
								setTimeout(function() {
									me.init(callback);
								}, 1000);
							} else {
								me._isError = true;
							}
						}
					});
				}
			}
		},
		//时间处理
		date: {
			//获取时间对象,不能转为时间的返回null
			getDate: function(value) {
				if (!value) return null;

				var type = typeof value,
					date = null,
					isDate = value instanceof Date;

				if (isDate) {
					date = new Date(value.getTime());
					return date;
				} else {
					if (type == 'number' || !isNaN(value)) {
						if (!isNaN(value)) {
							value = parseInt(value);
						}
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
			isValid: function(value) {
				var date = this.getDate(value);
				return date ? true : false;
			},
			/**获取距离value这个时间num天的时间对象
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
			/**获取时间字符串
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
			//将时间转化为后台需要的格式,例如:\/Date(1359779125000)\/
			toServerDate: function(value) {
				var v = this.getDate(value);
				if (!v) return null;
				return "\/Date(" + v.getTime() + "+0000)\/";
			},
			//校验是否一个月的第一天
			isMonthFirstDate: function(value) {
				var v = this.getDate(value);
				if (!v) return false;

				//每个月的1号就是第一天
				if (v.getDate() == 1) return true;

				return false;
			},
			//校验是否一个月的最后一天
			isMonthLastDay: function(value) {
				var v = this.getDate(value);
				if (!v) return false;

				var month = v.getMonth();
				var month2 = this.getNextDate(v).getMonth();

				//符合条件：当天时间加上一天就是下个月的第一天
				if ((month2 - month - 1) % 12 == 0) return true;

				return false;
			},
			//检验是否整月
			isFullMonth: function(start, end) {
				var s = this.getDate(start);
				var e = this.getNextDate(end);

				//start < end
				if (Date.parse(s) >= Date.parse(e)) return false;

				//start的日期 = end的日期
				if (s.getDate() != e.getDate()) return false;

				return true;
			},
			//获取一个月的第一天
			getMonthFirstDate: function(year, month, toString) {
				var m = ((month + '').length == 1 ? '0' : '') + month;
				var date = year + '-' + m + '-01';

				if (!toString) date = this.getDate(date);
				return date;
			},
			//获取一个月的最后一天
			getMonthLastDate: function(year, month, toString) {
				var date = new Date(year, month, 0);
				if (toString) date = this.toString(date, true);
				return date;
			},
			//时间差
			difference: function(start, end) {
				var date1 = this.getDate(start);
				var date2 = this.getDate(end);
				if (!date1 || !date2) {
					return null;
				}

				var times = date2.getTime() - date1.getTime();
				var result = [];

				if (times < 0) {
					times = 0 - times;
					result.push('-');
				}
				//计算出相差天数
				var days = Math.floor(times / (24 * 3600 * 1000));
				if (days > 0) {
					result.push(days + '天');
				}
				//计算出小时数
				var leave1 = times % (24 * 3600 * 1000); //计算天数后剩余的毫秒数
				var hours = Math.floor(leave1 / (3600 * 1000));
				if (hours > 0) {
					result.push(hours + '小时');
				}
				//计算相差分钟数
				var leave2 = leave1 % (3600 * 1000); //计算小时数后剩余的毫秒数
				var minutes = Math.floor(leave2 / (60 * 1000));
				if (minutes > 0) {
					result.push(minutes + '分钟');
				}
				//计算相差秒数
				var leave3 = leave2 % (60 * 1000); //计算分钟数后剩余的毫秒数
				var seconds = Math.round(leave3 / 1000);
				if (seconds > 0) {
					result.push(seconds + '秒');
				}

				return result.join('');
			}
		},
		//数字处理
		number: {
			//获取大写金额
			getMoney: function(num) {
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
			//字符串-获取以ASCII编码字节数 英文占1字节 中文占2字节
			lenASCII: function(str) {
				if (typeof str != 'string') return -1;
				//将所有非\x00-\xff字符换为xx两个字符,再计算字符串
				return str.replace(/[^\x00-\xff]/g, 'xx').length;
			},
			//获取固定字节数的子串
			substrASCII: function(str, start, lenASCII) {
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
			inArray: function(str, array) {
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
			toSize: function(bytes) {
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
			delay: function(fun, delayTime, scope) {
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
		}
	};

	//uxutil.path.init();
	//暴露接口
	exports('uxutil', uxutil);
});
