/**
 * 系统公用功能类
 * @type 
 */
var Shell = Shell || {};

Shell.util = Shell.util || {};
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
	Shell.util.Msg.showLog('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value,true);
    //</debug>
};
/**克隆数据*/
Shell.util.clone = function(item){
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
};

Shell.util.Function = {};

/**配置*/
Shell.util.Config = {
	/**是否在debug中显示log错误信息*/
	showLog:true,
	/**是否在窗口中显示log错误信息*/
	showLogWin:false
};

/**系统路径*/
Shell.util.Path = {
	rootPath:'',//项目根目录
	uiPath:'',//ui包根目录
	helpPath:'',//帮助文档目录
	init:function(){
		this.rootPath = this.getRootPath();
		this.uiPath = this.rootPath + '/ui';
		this.helpPath = this.uiPath + '/help';
	},
	//js获取项目根路径,如:http://localhost:8080/A
	getRootPath:function(){
		//获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
		var curWwwPath=window.document.location.href;
		//获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
		var pathName=window.document.location.pathname;
		var pos=curWwwPath.indexOf(pathName);
		//获取主机地址，如： http://localhost:8083    
		var localhostPaht=curWwwPath.substring(0,pos);
		//获取带"/"的项目名，如：/uimcardprj
		var projectName=pathName.substring(0,pathName.substr(1).indexOf('/')+1);
		return(localhostPaht+projectName);
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
	/**获取固定字节数的子串*/
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
		if(!value) return null;
		
		var n = isNaN(num) ? 1 : parseInt(num);
		
		date.setDate(date.getDate() + n);
		
		return date;
	},
	/**获取时间字符串*/
	toString:function(value,onlyDate){
		var value = this.getDate(value);
		if(!value) return null;
		
		var info = '',
			year = value.getFullYear() + '',
			month = (value.getMonth() + 1) + '',
			date = value.getDate() + '';
			
		month = month.length == 1 ? '0' + month : month;
		date = date.length == 1 ? '0' + date : date;
			
		info = year + '-' + month + '-' + date;
		
		if(!onlyDate){
			var hours = value.getHours() + '',
				minutes = value.getMinutes() + '',
				seconds = value.getSeconds() + '';
			
			hours = hours.length == 1 ? '0' + hours : hours;
			minutes = minutes.length == 1 ? '0' + minutes : minutes;
			seconds = seconds.length == 1 ? '0' + seconds : seconds;
			
			info += ' ' + hours + ':' + minutes + ':' + seconds;
		}
		
		return info;
	},
	/**将时间转化为后台需要的格式,例如:\/Date(1359779125000)\/*/
	toServerDate:function(value){
		var value = this.getDate(value);
		if(!value) return null;
		
		value = "\/Date(" + value.getTime() + "+0000)\/";
		return value;
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
	/**重新排序,支持正序和倒序,默认正序*/
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
	decodeErrorInfo:' 错误!你正在解码一个无效的JSON字符串',
	/**编码符号*/
	encodeMark:'"',
	/**解码*/
	decode:function(json,safe){
		try{
            return Shell.util.JSON.doDecode(json);
        }catch(e){
            if(safe === true) return null;
            
            var errorInfo = 'Shell.util.JSON.decode' + this.decodeErrorInfo + ':' + json;
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
Shell.util.Msg = Shell.util.Msg || {};
/**查看log错误信息*/
Shell.util.Msg.showLog = function(value,isShow){
	if(Shell.util.Config.showLog || isShow){
		console.log(value);
	}
	if(Shell.util.Config.showLogWin){
		
	}
};
/**查看重写信息*/
Shell.util.Msg.showOverrideInfo = function(name){
	alert(name + '方法必须重写!');
};

/**处理*/
Shell.util.Action = {
	/**延时处理*/
	delay:function(fun,scope,delayTime){
		if(Shell.util.typeOf(fun) != 'function'){
			Shell.util.Msg.showLog('Shell.util.Action.delay方法参数错误:fun参数不是function!',true);
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
	    var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
	        if(arr=document.cookie.match(reg)) return unescape(decodeURI(arr[2]));
	        else return null;
	},
	/**设置cookie属性*/
	setCookie:function(name,value){
	    var Days = 30;
	    var exp  = new Date();    //new Date("December 31, 9998");
	        exp.setTime(exp.getTime() + Days*24*60*60*1000);
	        document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
	},
	/**删除cookie属性*/
	delCookie:function(name){
	    var exp = new Date();
	    exp.setTime(exp.getTime() - 1);
	    var cval = getCookie(name);
	    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
	}
};

