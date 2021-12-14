/**
 * 系统公用功能类
 * @author Jcall
 * @version 2015-01-04
 */
var Shell = Shell || {};

Shell.util = {
	/**判断数据类型*/
	typeOf:function(value){
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
		Shell.util.Msg.showLog('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value,true);
	    //</debug>
	},
	/**克隆数据*/
	clone:function(item){
	    var type,i,j,k,clone,key;
	    
	    if(item === null || item === undefined){
	        return item;
	    }
		
	    if(item.nodeType && item.cloneNode){
	        return item.cloneNode(true);
	    }
	
	    type = Object.prototype.toString.call(item);
		
	    if(type === '[object Date]'){//时间
	        return new Date(item.getTime());
	    }
		
	    if(type === '[object Array]'){//数组
	        i = item.length;
	        clone = [];
	        while(i--){
	            clone[i] = Shell.util.clone(item[i]);
	        }
	    }
	    //对象
	    else if(type === '[object Object]' && item.constructor === Object){
	        clone = {};
	
	        for(key in item){
	            clone[key] = Shell.util.clone(item[key]);
	        }
	
	        if(enumerables){
	            for(j = enumerables.length; j--;){
	                k = enumerables[j];
	                clone[k] = item[k];
	            }
	        }
	    }
	
	    return clone || item;
	}
};

/**配置*/
Shell.util.Config = {
	/**是否在debug中显示log错误信息*/
	showLog:true,
	/**是否在窗口中显示log错误信息*/
	showLogWin:false,
	/**是否开启直接访问页面模式*/
	directCall:false
};

/**系统路径*/
Shell.util.Path = {
	/**项目根目录*/
	rootPath:'',
	/**ui包根目录*/
	uiPath:'',
	/**帮助文档目录*/
	helpPath:'',
	/**登录页面路径*/
	loginPath:'',
	/**初始化路径内容*/
	init:function(){
		this.rootPath = this.getRootPath();
		this.uiPath = this.rootPath + '/ui';
		this.helpPath = this.uiPath + '/help';
		this.loginPath = this.rootPath + '/Login.aspx';
	},
	/**js获取项目根路径,如:http://localhost:8080/A*/
	getRootPath:function(){
		//获取当前网址,如:http://localhost:8080/Web/ui/test.html
		var href = window.document.location.href;
		//获取主机地址之后的目录,如:Web/ui/test.html
		var pathname = window.document.location.pathname;
		var pos = href.indexOf(pathname);
		//获取主机地址,如:http://localhost:8080
		var localhostPaht=href.substring(0,pos);
		//获取带"/"的项目名,如:/Web
		var projectName = pathname.substring(0,pathname.substr(1).indexOf('/')+1);
		return(localhostPaht + projectName);
	},
	/**获取页面传递的参数*/
	getRequestParams:function(){
		var url = location.search;//获取url中"?"符后的字串
   			
   		if(url.indexOf("?") == -1) return {};
   		
		var str = url.substr(1),
			strs = str.split("&"),
			len = strs.length,
			params = {};
			
		for(var i=0;i<len;i++){
			var arr = strs[i].split("=");
			params[arr[0]] = decodeURI(arr[1]);
  		}
  		
   		return params;
	}
};Shell.util.Path.init();

/**字符串*/
Shell.util.String = {
	/**字符串转码*/
	encode:function(value){
		//v = encodeURI(v);//不转义保留字符
		//转义保留字符
		return encodeURIComponent(value || '');
	},
	/**字符串解码*/
	decode:function(value){
		return decodeURI(value || '');
	},
	/**字符串-获取以ASCII编码字节数 英文占1字节 中文占2字节*/
	lenASCII:function(str){
		if(Shell.util.typeOf(str) != 'string') return -1;
		//将所有非\x00-\xff字符换为xx两个字符,再计算字符串
		return str.replace(/[^\x00-\xff]/g,'xx').length;
	},
	/**获取固定字节数的子串
	 * @param str 字符串
	 * @param start 开始的位置
	 * @param lenASCII 字节长度
	 */
	substrASCII:function(str,start,lenASCII){
		if(typeof(str) != 'string') return null;
		var arr = str.split(''),
			length = arr.length,
			result = [],
			count = 0,
			len = 0;
			
		start = start < 0 ? 0 : start;
		lenASCII = lenASCII < 0 ? 0 : lenASCII;
		lenASCII = lenASCII > (length-start) ? (length - start) : lenASCII;
			
		for(var i=start;i<length;i++){
			len = this.lenASCII(arr[i]);
			count += len;
			if(count > lenASCII) break;
			result.push(arr[i]);
		}
		
		return result.join('');
	},
	/**字符串是否在数组中存在*/
	inArray:function(str,array){
		var arr = array || [],
			len = arr.length;
		
		for(var i=0;i<len;i++){
			if(arr[i] == str) return true;
		}
		
		return false;
	}
};
/**数字*/
Shell.util.Number = {
	/**需要处理的数字错误信息*/
	valueErrorInfo:'错误!你正在处理一个无效的数字',
	/**小数位数错误信息*/
	numErrorInfo:'错误!小数位数不能小于0，参数num',
	/**转化为N位小数(不足补零)的字符串
	 * @param value 需要处理的数字
	 * @num 小数位数
	 */
	retainDecimaltoString:function(value,num){
		var fNum = parseFloat(value),
			num = parseInt(num);
			
	    if (isNaN(fNum)){
	    	var errorInfo = "Shell.util.Number.retainDecimal " + this.valueErrorInfo + ":" + value;
	        Shell.util.Msg.showLog(errorInfo,true);
	        return false;
	    }
	    
	    if(isNaN(num)){
	    	var errorInfo = "Shell.util.Number.retainDecimal " + this.numErrorInfo + "=" + num;
	        Shell.util.Msg.showLog(errorInfo,true);
	        return false;
	    }
	    if(num === 0) return parseInt(value);
	    
	    var temp = Math.pow(10,num);
	    
	    fNum = Math.round(fNum * temp) / temp;
	    
	    var sNum = fNum.toString(),
	   		index = sNum.indexOf('.');
	   		
	    if (index < 0){
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
	getDate:function(value){
		if(!value) return null;
		
		var type = Shell.util.typeOf(value),
			date = null;
		
		if(type == 'date'){
			date = Shell.util.clone(value);
		}else if(type == 'string'){
			if(value.length == 26 && value.slice(0,6) == "/Date(" && value.slice(-2) == ")/"){
				// /Date(1413993600000+0800)/
				value = parseInt(value.slice(6,-7));
			}else if(value.length == 27 && value.slice(0,6) == "/Date(" && value.slice(-2) == ")/"){
				// /Date(-1413993600000+0800)/
				value = parseInt(value.slice(6,-7));
			}else{
				value = value.replace(/-/g,'/');
			}
			date = new Date(value);
		}else if(type == 'number'){
			date = new Date(value);
		}
		
		var isDate = (Date.parse(date) == Date.parse(date));
		
		if(isDate) return date;
		return null;
	},
	/**校验对象是否是时间*/
	isValid:function(value){
		var date = this.getDate(value);
		return date ? true : false;
	},
	/**获取距离value这个时间num天的时间对象;
	 * @param {date/string/number} value 当前时间
	 * @param {number} num 默认为1,可以负数,例如-1就是昨天,1是明天;
	 * @return {}
	 */
	getNextDate:function(value,num){
		var date = this.getDate(value);
		if(!date) return null;
		
		var n = isNaN(num) ? 1 : parseInt(num);
		
		date.setDate(date.getDate() + n);
		
		return date;
	},
	/**获取时间字符串*/
	toString:function(value,onlyDate){
		var date = this.getDate(value);
		if(!date) return null;
		
		var info = '',
			year = date.getFullYear() + '',
			month = (date.getMonth() + 1) + '',
			day = date.getDate() + '';
			
		month = month.length == 1 ? '0' + month : month;
		day = day.length == 1 ? '0' + day : day;
			
		info = year + '-' + month + '-' + day;
		
		if(!onlyDate){
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
	toServerDate:function(value){
		var date = this.getDate(value);
		if(!date) return null;
		
		date = "\/Date(" + date.getTime() + "+0000)\/";
		return date;
	}
};

/**对象*/
Shell.util.Object = {
	/**将扁平化的对象立体化*/
	toStereo:function(obj){
		if(Shell.util.typeOf(obj) != 'object') return null;
		
		var maxLength = 0,//最大的层数
			length,
			iArr,
			mOb = {};
		
		for(var i in obj){
			iArr = i.split('_');
	        length = iArr.length;
	        if(length > maxLength){
	            maxLength = length;
	        }
	        
	        if(Shell.util.typeOf(obj[i]) === 'date'){
	    		obj[i] = Shell.util.Date.toServerDate(obj[i]);
	    	}else if(iArr.slice(-1) == 'DataTimeStamp'){
	    		obj[i] = obj[i] != null ? obj[i].split(',') : null;
	    	}
	    	
	    	//暂时存储于
	        mOb['L' + length] = mOb['L' + length] || [];
	        mOb['L' + length].push({key:i,value:obj[i]});
	    }
	    
	    var result = {};
	    
	    var change = function(value){
	    	var arr = value.key.split('_'),
	    		len = arr.length,
	    		res = result;
	    		
	    	for(var i=1;i<len;i++){
	    		if(i == len-1){
	    			res[arr[i]] = value.value;
	    		}else{
	    			res = res[arr[i]] = res[arr[i]] || {};
	    		}
	    	}
	    };
	    
	    for(var i=1;i<maxLength+1;i++){
	    	var arr = mOb['L' + i] || [],
	    		len = arr.length;
	    		
	    	for(var j=0;j<len;j++){
	    		change(arr[j]);
	    	}
	    }
	    
	    return result;
	}
};

/**数组*/
Shell.util.Array = {
	/**重新排序,支持正序和倒序,默认正序
	 * @param list 需要排序的列表
	 * @param key 排序字段
	 * @param isDesc 是否反序
	 */
	reorder:function(list,key,isDesc){
		if(!key) return list;
		if(Shell.util.typeOf(list) != 'array') return list;
		
		var arr = Shell.util.clone(list) || [],
			len = arr.length;
			
		//校验数组的每一个元素是否存在key属性,全部存在才排序,否则直接返回数组
		for(var i=0;i<len;i++){
			if(arr[i][key] == null) return list;
		}
		
		//重新排序
		for(var i=0;i<len-1;i++){
			for(var j=i+1;j<len;j++){
				var bo = isDesc ? (arr[i][key] < arr[j][key]) : (arr[i][key] > arr[j][key]);
				if(bo){
					var temp = arr[i];
					arr[i] = arr[j];
					arr[j] = arr[i];
				}
			}
		}
		
		return arr;
	}
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
            Shell.util.Msg.showLog(errorInfo,true);
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

/**提示信息*/
Shell.util.Msg = {
	/**查看log错误信息*/
	showLog:function(value,isShow){
		if(Shell.util.Config.showLog || isShow){
			if(window.console){window.console.log(value);}
		}
		if(Shell.util.Config.showLogWin){
			alert(value);
		}
	},
	/**查看重写信息*/
	showOverrideInfo:function(name){
		alert(name + '方法必须重写!');
	},
	/**提示信息*/
	showInfo:function(value,scope){
		alert(value);
	},
	/**提示警告*/
	showWarning:function(value,scope){
		alert(value);
	},
	/**提示错误*/
	showError:function(value,scope){
		alert(value);
	},
	/**弹出提示框*/
	showMsg:function(config,scope){
		var type = Shell.util.typeOf(config),
			backDiv = document.getElementById("util_msgDiv_back"),
			contentDiv = document.getElementById("util_contentDiv"),
			titleDiv = document.getElementById("util_titleDiv"),
			msgDiv = document.getElementById("util_msgDiv"),
			title = "",
			msg = "",
			width = 200,
			height = 100;
			
		if(!backDiv){
			//遮罩层DIV
			backDiv = document.createElement("div");
			backDiv.id = "util_msgDiv_back";
			//backDiv.style="background:rgba(120,120,120,0.7);left:0;top:0;width:100%;height:100%;position:absolute;z-index:99999;";
			backDiv.style.background = "rgb(120,120,120)";
			backDiv.style.left = 0;
			backDiv.style.top = 0;
			backDiv.style.width = "100%";
			backDiv.style.height = "100%";
			backDiv.style.position = "absolute";
			backDiv.style["z-index"] = 99999;
			
			//容器DIV
			contentDiv = document.createElement("div");
			contentDiv.id = "util_contentDiv";
			//contentDiv.style="background:#ffffff;opacity:1;border:1px solid cad9ea;margin:5px auto;";
			contentDiv.style.background = "#ffffff";
			contentDiv.style.opacity = 1;
			contentDiv.style.border = "1px solid cad9ea";
			contentDiv.style.margin = "5px auto";
			
			//标题DIV
			titleDiv = document.createElement("div");
			titleDiv.id = "util_titleDiv";
			//titleDiv.style="background:#F4F4F4;width:100%;height:26px;";
			titleDiv.style.background = "#F4F4F4";
			titleDiv.style.width = "100%";
			titleDiv.style.height = "26px";
			
			//信息DIV
			msgDiv = document.createElement("div");
			msgDiv.id = "util_msgDiv";
			//msgDiv.style="background:#ffffff;width:100%;";
			msgDiv.style.background = "#ffffff";
			msgDiv.style.width = "100%";
			
			contentDiv.appendChild(titleDiv);
			contentDiv.appendChild(msgDiv);
			
			backDiv.appendChild(contentDiv);
			document.body.appendChild(backDiv);
		}
		
		switch(type){
			case "string" : msg = config;break;
			case "object" : 
				title = config.title || "";
				msg = config.msg || "";
				width = config.width || width;
				height = config.height || height;
				break;
		}
		//容器DIV
		contentDiv.style.width = width + "px";
		contentDiv.style.height = height + "px";
		//标题DIV
		titleDiv.innerHTML = "<b style='color:blue;'>" + title + 
			"</b><img style='width:16px;height:16px;float:right;margin:4px;' src='" + 
			Shell.util.Path.uiPath + "/css/images/buttons/del.png' class='hand' title='关闭' " +
			"onclick='this.parentNode.parentNode.parentNode.style.display=\"none\";'/>";
		//信息DIV
		msgDiv.style.height = (height - 22) + "px";
		msgDiv.innerHTML = msg;
		//遮罩层DIV
		backDiv.style.display = "block";
	}
};

/**处理*/
Shell.util.Action = {
	/**错误信息-需要支持多语言*/
	errorInfo:'参数错误:fun参数不是function!',
	/**延时处理*/
	delay:function(fun,scope,delayTime){
		if(Shell.util.typeOf(fun) != 'function'){
			Shell.util.Msg.showLog('Shell.util.Action.delay ' + this.errorInfo,true);
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

/**cookie*/
Shell.util.Cookie = {
	/**取得cookie属性*/
	getCookie:function(name){
//	    var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
//	        if(arr=document.cookie.match(reg)) return unescape(decodeURI(arr[2]));
//	        else return null;
		
		var arrStr = document.cookie.split("; ");
		for(var i = 0;i < arrStr.length;i ++){
			var temp = arrStr[i];//.trim();
			temp = temp.split("=");
			if(temp[0] == name){
				if(temp.length == 1){
					return null;
				}
				var v = v === "undefined" ? "" : temp[1];
				var value = decodeURI(v).replace(/\+/g,"%20");
				return unescape(value);
			}
		}
	},
	/**设置cookie属性*/
	setCookie:function(name,value){
	    var Days = 30,
	    	exp = new Date();
	    	
        exp.setTime(exp.getTime() + Days*24*60*60*1000);
        document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
	},
	/**删除cookie属性*/
	delCookie:function(name){
	    var exp = new Date();
	    exp.setTime(exp.getTime() - 1);
	    var cval = this.getCookie(name);
	    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
	}
};

/**浏览器判断*/
Shell.util.Browser = {
	/**版本信息*/
	sys:{},
	/**初始化洌览器版本信息*/
	initBrowserInfo:function(){
		var ua = navigator.userAgent.toLowerCase();
        	
        window.ActiveXObject ? this.sys.ie = ua.match(/msie ([\d.]+)/)[1] : "";
//        document.getBoxObjectFor ? this.sys.firefox = ua.match(/firefox\/([\d.]+)/)[1] :
//        window.MessageEvent && !document.getBoxObjectFor ? this.sys.chrome = ua.match(/chrome\/([\d.]+)/)[1] :
//        window.opera ? this.sys.opera = ua.match(/opera.([\d.]+)/)[1] :
//        window.openDatabase ? this.sys.safari = ua.match(/version\/([\d.]+)/)[1] : 0;
	},
	/**获取洌览器信息*/
	getBrowserInfo:function(){
		return this.sys;
	},
	/**是否是IE浏览器,接收版本参数,例如:isIE("8.0")*/
	isIE:function(version){
		return this.isBrowser("ie",version);
	},
	/**
	 * 是否是指定类型和版本的浏览器
	 * @param {} type 浏览器类型
	 * @param {} version 洌览器版本
	 * @return {Boolean}
	 */
	isBrowser:function(type,version){
		if(version) return (this.sys[type] == version);
		if(this.sys[type]) return true;
		return false;
	}
};Shell.util.Browser.initBrowserInfo();

/**打印*/
Shell.util.Print = {
	objectId:'LODOP_OB',
	embedId:'LODOP_EM',
	E32:Shell.util.Path.uiPath + '/print/resources/install_lodop32.exe',
	E64:Shell.util.Path.uiPath + '/print/resources/install_lodop64.exe',
	
	/**获取打印组件*/
	getLodopObj:function(taskName){
		if(!this.objectId) return null;
		var lodop = this.getLodop(document.getElementById(this.objectId),document.getElementById(this.embedId));
		if(lodop){
			lodop.PRINT_INIT(taskName);
	    	lodop.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
		}
	    return lodop;
	},
	/**获取打印组件对象*/
	getLodop:function(object,embed){
		var noLodop = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='{0}'>执行安装</a>,安装后请刷新页面或重新进入。</font>",
			updatelodop = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='{0}'>执行升级</a>,升级后请重新进入。</font>",
			strHtmInstall = noLodop.replace("{0}",this.E32),
			strHtm64_Install = noLodop.replace("{0}",this.E64),
			strHtmUpdate = updatelodop.replace("{0}",this.E32),
			strHtm64_Update = updatelodop.replace("{0}",this.E64),
			strHtmFireFox = "<font color='red'>注意：如曾安装过Lodop旧版附件npActiveXPLugin,</br>" +
				"请在【工具】->【附加组件】->【扩展】中先卸它。</font>",
			lodop = embed,
			errorInfo = [];
					
		try{		     
			if(navigator.appVersion.indexOf("MSIE") >= 0) lodop = object;
			if((lodop == null) || (typeof(lodop.VERSION) == "undefined")){
				if(navigator.userAgent.indexOf('Firefox') >= 0)
				 	errorInfo.push(strHtmFireFox);
				if(navigator.userAgent.indexOf('Win64') >= 0){
					errorInfo.push(strHtm64_Install);
				}else{
					errorInfo.push(strHtmInstall);
				}
				
				if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
			
				return lodop;
			}else if(lodop.VERSION < "6.1.2.0"){
				if(navigator.userAgent.indexOf('Win64') >= 0){
					errorInfo.push(strHtm64_Update);
				}else{
					errorInfo.push(strHtmUpdate);
				}
				
				if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
				
			 	return lodop;
			}
			
			if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
			
			return lodop; 
		}catch(err){
			if(navigator.userAgent.indexOf('Win64') >=0){
				errorInfo.push("Error:" + strHtm64_Install);
			}else{
				errorInfo.push("Error:" + strHtmInstall);
			}
			
			if(errorInfo.length > 0){this.showError(errorInfo.join("</br>"));}
			
		    return lodop; 
		}
	},
	/**显示错误信息*/
	showError:function(info){
		Shell.util.Msg.showMsg({
			title:"提示信息",
			msg:info,
			width:400,
			height:200
		});
		//document.documentElement.innerHTML = info + document.documentElement.innerHTML;
	},
	/**初始化网页打印控件*/
	initLodopDom:function(){
		//已存在object的就不在创建
    	if(document.getElementById("LODOP_OB")) return;
    	
    	var firstChild = document.body.firstChild,
    		object = document.createElement("object"),
    		embed = document.createElement("embed");
    		
    	object.id = "LODOP_OB";
    	object.setAttribute("classid","clsid:2105C259-1E0C-4534-8141-A753534CB4CA");
    	//object.classid = "clsid:2105C259-1E0C-4534-8141-A753534CB4CA";
    	object.style.display = "none";
    	
    	embed.id = "LODOP_EM";
    	embed.type = "application/x-print-lodop";
    	embed.setAttribute("pluginspage",Shell.util.Path.uiPath + "/util/print/resources/install_lodop32.exe");
    	
    	object.appendChild(embed);
    		
        //在body的最前端加入网页打印控件
        document.body.insertBefore(object,firstChild);
	}
};

//页面加载完成后执行
window.onload = function(){
	//判断是否开启直接访问页面模式
	if(!Shell.util.Config.directCall){
//		var ZhiFangUser = Shell.util.Cookie.getCookie("ZhiFangUser");
//		//不符合要求的直接跳转到登录页面
//		if(!ZhiFangUser) window.location.href = Shell.util.Path.loginPath;
	}
	
	//初始化网页打印控件
	//Shell.util.Print.initLodopDom();
}
//页面按键过滤
window.onkeydown = function(event){
	//屏蔽F5刷新按键
	if(event.keyCode==116){event.keyCode=0;return false;}
}