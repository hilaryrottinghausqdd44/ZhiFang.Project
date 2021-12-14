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
	init: function() {
		this.rootPath = this.getRootPath();
		this.uiPath = this.rootPath + '/webapp/ui';
		this.imgPath = this.rootPath + '/Img';
	},
	/**js获取项目根路径,如:http://localhost:8080/A*/
	getRootPath: function() {
		//获取当前网址,如:http://localhost:8080/Web/ui/test.html
		var href = window.document.location.href;
		//获取主机地址之后的目录,如:Web/ui/test.html
		var pathname = window.document.location.pathname;
		var pos = href.indexOf(pathname);
		//获取主机地址,如:http://localhost:8080
		var localhostPaht = href.substring(0, pos);
		//获取带"/"的项目名,如:/Web
		var projectName = pathname.substring(0, pathname.substr(1).indexOf('/') + 1);
		return (localhostPaht + projectName);
	}
};Shell.util.Path.init();

/**事件处理*/
Shell.util.Event = {
	/**触摸事件,可修改为
	 * touchstart 触摸开始就执行
	 * touchend 触摸结束执行
	 * click 点击执行
	 */
	touch: "touchend",
	/**延时处理*/
	delay:function(fun,scope,delayTime){
		if(Shell.util.typeOf(fun) != 'function'){
			console.log('Shell.util.Event.delay方法参数错误:fun参数不是function!');
			return;
		}
		
		var me = scope || this,
			delayTime = delayTime || 300;
		
		me.etime = new Date().getTime();
		
		if(me.etime - me.stime < delayTime && me.waitAction){
			clearTimeout(me.waitAction);
		}
		
		me.waitAction = setTimeout(fun,delayTime);
		
		me.stime = new Date().getTime();
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
	isCellPhoneNo: function(value) {
		if(!value || value.length != 11) return false;
		var filter = /^1[3|5|8][0-9]\d{4,8}$/;
		return filter.test(value);
	},
	/**
	 * 身份证号码
	 * 支持15位和18位身份证号码校验
	 */
	isIdCardNo:function(value){
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
		createGrid: function(config) {
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
		createRows: function(data, rowModel, isMultiObject) {
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
		createRow: function(info, rowModel, isMultiObject) {
			//允许行数据整理函数，用于复杂逻辑
			if (typeof(rowModel) == "function") {
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
		createRowByMultiObject: function(info, rowModel) {
			//允许行数据整理函数，用于复杂逻辑
			if (typeof(rowModel) == "function") {
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
		getMultiObjectValue: function(obj, keyStr) {
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
	ajax: function(config, callback) {
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
				var msg = data[Shell.util.Server.resultParams.msg],
					value = data[Shell.util.Server.resultParams.value];
				
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
	mapping:{
		"openId":"100001",
		"userId":"100002",
	},
	/**取得cookie属性*/
	getCookie: function(name) {
		var nameT = Shell.util.Cookie.mapping[name] || name,
			reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
			arr = document.cookie.match(reg);
			
		if (arr) return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
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
	delCookie: function(name) {
		var exp = new Date();
		exp.setTime(exp.getTime() - 1);
		var cval = this.getCookie(name);
		if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
	}
};

/**获取页面传递的参数
 * @param toUpperCase 是否将参数名转化为大写
 */
Shell.util.getRequestParams = function(toUpperCase){
	var url = location.search;//获取url中"?"符后的字串
		
	if(url.indexOf("?") == -1) return {};
	
	var str = url.substr(1),
		strs = str.split("&"),
		len = strs.length,
		params = {};
		
	for(var i=0;i<len;i++){
		var arr = strs[i].split("=");
		if(toUpperCase){
			arr[0] = arr[0].toLocaleUpperCase();
		}
		params[arr[0]] = decodeURI(arr[1]);
	}
	
	return params;
}

/**判断数据类型*/
Shell.util.typeOf = function(value){
    var type,
        typeToString;
    
    if(value === null) return 'null';
	
    type = typeof value;
	
    if(type === 'undefined' || type === 'string' || type === 'number' || type === 'boolean'){
        return type;
    }
	
    typeToString = Object.prototype.toString.call(value);
	
    switch(typeToString){
        case '[object Array]': return 'array';
        case '[object Date]': return 'date';
        case '[object Boolean]': return 'boolean';
        case '[object Number]': return 'number';
        case '[object RegExp]': return 'regexp';
    }

    if(type === 'function') return 'function';

    if(type === 'object'){
        if(value.nodeType !== undefined){
            if(value.nodeType === 3){
                return (/\S/).test(value.nodeValue) ? 'textnode' : 'whitespace';
            }else{
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
	decodeErrorInfo:'错误!你正在解码一个无效的JSON字符串',
	/**编码符号*/
	encodeMark:'"',
	/**解码*/
	decode:function(json,safe){
		try{
            return Shell.util.JSON.doDecode(json);
        }catch(e){
            if(safe === true) return null;
            
            var errorInfo = 'Shell.util.JSON.decode ' + this.decodeErrorInfo + ':' + json;
            window.console.log(errorInfo);
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
		var type = Shell.util.typeOf(value);
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