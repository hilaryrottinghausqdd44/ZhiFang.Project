/**
 * 系统公用功能类
 * @type 
 */
var Shell = Shell || {};
Shell.util = {};
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
	defaultHelpClassName:'Shell.help.Default',
	rootPath:'',//项目根目录
	uiPath:'',//ui包根目录
	helpPath:'',//帮助文档目录
	buildPath:'buildfile',//构建文件根目录
	reportPath: 'ReportFiles',//报告文件目录
	init:function(){
		this.rootPath = this.getRootPath();
		this.uiPath = this.getUiPath();
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
	getUiPath:function (){
	    var curWwwPath=window.document.location.href;
	    var pathName = window.document.location.pathname;
	    var name = pathName.split('/').slice(1, 3).join('/');
	    var pos = curWwwPath.indexOf(pathName);
	    var localhostPaht = curWwwPath.substring(0, pos);
	    return localhostPaht + '/' + name;
	},
	/**获取页面传递的参数
	 * @param toUpperCase 是否将参数名转化为大写
	 */
	getRequestParams:function(toUpperCase){
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
};Shell.util.Path.init();
/**处理*/
Shell.util.Action = {
	/**延时处理*/
	delay:function(fun,scope,delayTime){
		if(Ext.typeOf(fun) != 'function'){
			Shell.util.Msg.showLog('Shell.util.Action.delay方法参数错误:fun参数不是function!');
			return;
		}
		
		var me = scope || this,
			delayTime = delayTime || 500;
		
		me.etime = new Date().getTime();
		
		if(me.etime - me.stime < delayTime && me.waitAction){
			clearTimeout(me.waitAction);
		}
		
		me.waitAction = setTimeout(fun,delayTime);
		
		me.stime = new Date().getTime();
	}
};
//系统信息
Shell.util.SysInfo = {
	/**开启系统信息初始化*/
	hasToInitSystemIfno:false,
	/**当前时间*/
	getSYS_DATE:function(){return getSystemTime();},
	/**员工ID*/
	getSYS_USER_ID:function(){return getSystemInfo('EmployeeID');},
	/**员工姓名*/
	getSYS_USER_NAME:function(){return getSystemInfo('EmployeeName');},
	/**员工账户名*/
	getSYS_USER_ACCOUNT:function(){return getSystemInfo('UserAccount');},
	/**部门ID*/
	getSYS_USER_ORG_ID:function(){return getSystemInfo('HRDeptID');},
	/**部门编号*/
	getSYS_USER_ORD_CODE:function(){return getSystemInfo('HRDeptCode');},
	/**部门名称*/
	getSYS_USER_ORG_NAME:function(){return getSystemInfo('HRDeptName');},
	/**初始化系统信息*/
	initSystemIfno:function(){
		//根据项目配置内容
		/**获取机构ID*/
		this.getSYS_CEN_ORG_ID = this.getSYS_CEN_ORG_ID || function(){return getCookie('CenOrg_Id');};
		/**获取机构编码*/
		this.getSYS_CEN_ORG_NO = this.getSYS_CEN_ORG_NO || function(){return getCookie('CenOrg_OrgNo');};
		/**获取机构名称*/
		this.getSYS_CEN_ORG_NAME = this.getSYS_CEN_ORG_NAME || function(){return getCookie('CenOrg_CName');};
		
	    var url = getRootPath() + '/ReagentSysService.svc/ST_UDTO_SearchCenOrgByHQL';
	    url += "?isPlanish=true&fields=CenOrg_Id,CenOrg_OrgNo,CenOrg_CName";
	    var UserOrgCode = this.getSYS_USER_ORD_CODE();
	    url += "&where=cenorg.OrgNo='" + UserOrgCode + "'";
		getToServer(url,function(text){
			var result = Ext.JSON.decode(text);
	        var value = null;
	        
	        if(result.success){
	        	if(!result.ResultDataValue) return;
	            var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
	            if(ResultDataValue.count == 1){
		            var info = ResultDataValue.list[0];
		            setCookie('CenOrg_Id',info.CenOrg_Id);
		            setCookie('CenOrg_OrgNo',info.CenOrg_OrgNo);
		            setCookie('CenOrg_CName',info.CenOrg_CName);
	            }
	        }else{
	            //alertError(result.ErrorInfo);
	            Shell.util.Msg.showLog(result.ErrorInfo);
	        }
		},false);
	},
	/**初始化*/
	init:function(){if(this.hasToInitSystemIfno){this.initSystemIfno();}}
};Shell.util.SysInfo.init();
/**获取数据*/
Shell.util.Server = {
	/**返回参数*/
	resultParams: {
		success: "success",
		msg: "ErrorInfo",
		value: "ResultDataValue"
	},
	get:function(url,callback,async,timeout,isString){
		var t = timeout || 30000;
    	this.toServer('GET',url,callback,async,null,null,t);
	},
	post:function(url,params,callback,defaultPostHeader,async,timeout,isString){
		var t = timeout || 30000;
	    this.toServer('POST',url,callback,async,params,defaultPostHeader,t);
	},
	toServer:function(method,url,callback,async,params,defaultPostHeader,timeout,isString){
		var me = this;
		Ext.Ajax.defaultPostHeader = defaultPostHeader || 'application/json';
	    var bo = async === false ? false :true;
	    
	    var con = {
	        url:url,
	        async:bo,
	        method:method,
	        success:function(response,opts){
	            if(Ext.typeOf(callback) == "function"){
	            	var d = isString ? response.responseText : me.toJson(response.responseText);
	                callback(d);//回调函数
	            }
	        },
	        failure:function(response,options){
	        	if(Ext.typeOf(callback) == "function"){
	        		var value = "";
	        		if(response.request.timedout){//请求超时
	        			value = "{success:false,ErrorInfo:'请求服务超时'}";
	        		}else{
	        			value = "{success:false,ErrorInfo:'" + me.getMsgByStatus(response.status) + "'}";
	        		}
	                var d = isString ? response.responseText : me.toJson(value);
	                callback(d);//回调函数
	            }
	        }
	    };
	    
	    if(params){con.params = params;}//POST参数
	    if(timeout){con.timeout = timeout;}//超时
	    
	    Ext.Ajax.request(con);
    },
    /**转化成对象*/
    toJson:function(data){
    	var v = Ext.JSON.decode(data.replace(/\\u000/g,'')),
    		success = v[this.resultParams.success],
			value = v[this.resultParams.value];
		
		if(value && typeof(value) === "string"){
			if(isNaN(value)){
				try{
					value = Ext.JSON.decode(value);
				}catch(e){
					value = value;
				}
			}
		}
		
		if(success === "true") success = true;
		if(success === "false") success = false;
		
		return {
			success:success,
			msg:v[this.resultParams.msg],
			value:value
		};
    },
	/**根据状态码获取错误信息*/
	getMsgByStatus: function(status) {
		var msg = "";
		switch (status) {
			case 404:
				msg = "无法找到地址";
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

/**构建封装*/
Shell.util.Build = {};
Shell.util.Build.Combobox = {
	/**加载数据*/
	loadData:function(comp,url,fieldMap){
		var me = this;
		Shell.util.Server.get(url,function(result){
			var data=[];
			if(result.success){
				var list=result.value.list || [];
				for(var i in list){
					data.push(me.changFieldMap(list[i],fieldMap));
				}
			}
			comp.store.loadData(data);
		});
	},
	/**转化数据*/
	changFieldMap:function(data,fieldMap){
		var obj = {};
		for(var i in fieldMap){
			obj[fieldMap[i][0]] = data[fieldMap[i][1]];
		}
		return obj;
	}
};

/**系统信息*/
Shell.util.System = {
	/**登录用户信息*/
	UserInfo:{},
	/**服务器信息*/
	ServerInfo:{
		/**服务器时间*/
		Date:null
	}
};
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
		if(Ext.typeOf(str) != 'string') return -1;
		//将所有非\x00-\xff字符换为xx两个字符,再计算字符串
		return str.replace(/[^\x00-\xff]/g,'xx').length;
	},
	/**获取固定字节数的子串*/
	substrASCII:function(str,start,lenASCII){
		if(Ext.typeOf(str) != 'string') return null;
		var arr = str.split(''),
			length = arr.length,
			result = [],
			count = 0,
			len = 0;
			
		start = start < 0 ? 0 : start;
		lenASCII = lenASCII < 0 ? 0 : lenASCII;
		lenASCII = lenASCII > (length-start) ? (length - start) : lenASCII;
			
		for(var i=start;i<length;i++){
			len = Shell.util.String.lenASCII(arr[i]);
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
		
		var type = Ext.typeOf(value),
			date = null;
		
		if(type == 'date'){
			date = Ext.clone(value);
		}else if(type == 'string'){
			if(value.length == 26 && value.slice(0,6) == "/Date(" && value.slice(-2) == ")/"){
				// /Date(1413993600000+0800)/
				value = parseInt(value.slice(6,-7));
			}else if(value.length == 27 && value.slice(0,6) == "/Date(" && value.slice(-2) == ")/"){
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
		var date = Shell.util.Date.getDate(value);
		return date ? true : false;
	},
	/**获取距离value这个时间num天的时间对象;
	 * @param {date/string/number} value 当前时间
	 * @param {number} num 默认为1,可以负数,例如-1就是昨天,1是明天;
	 * @return {}
	 */
	getNextDate:function(value,num){
		var date = Shell.util.Date.getDate(value);
		if(!value) return null;
		
		var n = isNaN(num) ? 1 : parseInt(num);
		
		date.setDate(date.getDate() + n);
		
		return date;
	},
	/**获取时间字符串*/
	toString:function(value,onlyDate){
		var value = Shell.util.Date.getDate(value);
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
		var value = Shell.util.Date.getDate(value);
		if(!value) return null;
		
		value = "\/Date(" + value.getTime() + "+0000)\/";
		return value;
	}
};
/**对象*/
Shell.util.Object = {
	/**将扁平化的对象立体化*/
	toStereo:function(obj){
		if(Ext.typeOf(obj) != 'object') return null;
		
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
	        
	        if(Ext.typeOf(obj[i]) === 'date'){
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
	},
	/**将对象转化成字符串*/
	toString:function(obj){
		//转化数组
		var encodeArray = function(o){
			var a = ["[",""];
			var length = o.length;
			for(var i=0;i<length;i++){
				a.push(encodeValue(o[i]),",");
			}
			a[a.length-1] = "]";
			return a.join("");
		};
		//转化对象属性名
		var encodeKey = function(value){
			return "'" + value + "'";
		};
		//转化对象
		var encodeObj = function(o){
			var a = ["{",""];
			for(var i in o){
				a.push(encodeKey(i),":",encodeValue(o[i]),",")
			}
			a[a.length-1] = "}";
			return a.join("");
		};
		//转化字符串
		var encodeStr = function(str){
			return str.replace(/\\/g,"\\\\").replace(/'/g,"\\'")
		};
		//转化未确定类型
		var encodeValue = function(value){
			var type = Ext.typeOf(value);
			if(type === 'null' || type === 'undefined'){
				return "null";
			}else if(type === 'number' || type === 'boolean'){
				return value + "";
			}else if(type === 'string'){
				return "'" + encodeStr(value) + "'";
			}else if(type === 'array'){
				return encodeArray(value);
			}else if(type === 'object'){
				return encodeObj(value);
			}
		};
		
		return encodeValue(obj);
	}
};
/**数组*/
Shell.util.Array = {
	/**重新排序,支持正序和倒序,默认正序*/
	reorder:function(list,key,isDesc){
		if(!key) return list;
		if(Ext.typeOf(list) != 'array') return list;
		
		var arr = Ext.clone(list) || [],
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
/**提示信息*/
Shell.util.Msg = {
	/**查看log错误信息*/
	showLog:function(value){
		if(Shell.util.Config.showLog){
			console.log(value);
		}
		if(Shell.util.Config.showLogWin){
			
		}
	},
	/**查看重写信息*/
	showOverrideInfo:function(name){
		this.showWarning(name + '方法必须重写!');
	},
	
	/**提示信息*/
	showInfo:function(value,scope){
		this.showMsg({
			title:'提示信息',
			icon:Ext.Msg.INFO,
			msg:value,
			buttons:Ext.Msg.OK
		},scope);
	},
	/**提示警告*/
	showWarning:function(value,scope){
		this.showMsg({
			title:'警告信息',
			icon:Ext.Msg.WARNING,
			msg:value,
			buttons:Ext.Msg.OK
		},scope);
	},
	/**提示错误*/
	showError:function(value,scope){
		this.showMsg({
			title:'错误信息',
			icon:Ext.Msg.ERROR,
			msg:value,
			buttons:Ext.Msg.OK
		},scope);
	},
	/**删除数据确认框*/
	confirmDel:function(fn,scope){
		this.showMsg({
            title:'删除确认',
            msg:'确定要删除吗？',
            icon:Ext.Msg.WARNING,
            buttons:Ext.Msg.OKCANCEL,
            callback:fn
		},scope);
	},
	/**弹出提示框*/
	showMsg:function(config,scope){
		var me = scope;
			
		if(me){
			height = me.getHeight() - 20,
			width = me.getWidth() - 20;
			
			var msgbox = me.msgbox = me.msgbox || new Ext.window.MessageBox({
				renderTo:me.floating ? Ext.getBody() : me.getEl(),
				autoScroll:true,
				buttonText:{ok:'确定',yes:'是',no:'否',cancel:'取消'}
			});
			
			msgbox.maxHeight = height;
			msgbox.maxWidth = width;
			config.msg += '</br>';
			msgbox.show(config);
		}else{
			Ext.Msg.show(config);
		}
	},
	
	error:function(msg,title){
		title = title || '错误信息';
		msg = '<b>' + msg + '</b>';
		this.show(msg,title,Ext.Msg.ERROR);
	},
	warning:function(msg,title){
		title = title || '警告信息';
		msg = '<b>' + msg + '</b>';
		this.show(msg,title,Ext.Msg.WARNING);
	},
	alert:function(msg,title){
		title = title || '提示信息';
		msg = '<b>' + msg + '</b>';
		this.show(msg,title,Ext.Msg.INFO);
	},
	show:function(msg,title,icon){
		Ext.Msg.show({
			title:title,
			msg:msg,
			icon:icon,
			modal:true,
			buttons:Ext.Msg.YES
		});
	}
};
/**窗口*/
Shell.util.Win = {
	/**打开路径页面*/
	openUrl:function(url,config){
		var win = Shell.util.Win.open('Shell.ux.panel.Panel',Ext.apply({
			title:'窗口面板',width:2400,height:1200,
			html:"<iframe height='100%' width='100%' frameborder='0' style='overflow:hidden;overflow-x:hidden;" +
				"overflow-y:hidden;height:100%;width:100%;position:absolute;top:0px;left:0px;right:0px;bottom:0px'" +
				" src='" + url + "' ></iframe>"
		},config));
		return win;
	},
	openClass:function(name,config){
		var maxHeight=document.body.clientHeight*0.98;
		var maxWidth=document.body.clientWidth*0.98;
		var par = {
			autoScroll:false,
			modal:true,
			floating:true,
			closable:true,
			draggable:true,
			resizable:true
		};
		for(var i in config){
			par[i] = config[i];
		}
		par.autoScroll = false;
		
		var win = Ext.create(name,par);
		if(win.height > maxHeight){win.setHeight(maxHeight-20);}
		if(win.width > maxWidth){win.setWidth(maxWidth-20);}
		
		return win;
	},
	/**打开窗口*/
	open:function(className,config){
		if(!className){
			Shell.util.Msg.showError('页面不存在！');
			return;
		}
		
		config = Ext.apply({
			autoScroll:false,
			minWidth:100,
			minHeight:50
		},config);
		
		return this.openClass(className,config).show();
	},
	/**启动执行*/
	begin:function(){
		//屏蔽快捷键-
		Ext.getDoc().on("contextmenu",function(e){     
	        e.stopEvent();
	    });   
	      
	    if(document.addEventListener){  
	        document.addEventListener("keydown",maskBackspace,true);  
	    }else{  
	        document.attachEvent("onkeydown",maskBackspace);  
	    }  
	      
	    function maskBackspace(event){  
	        var event = event || window.event;  //标准化事件对象  
	        var obj = event.target || event.srcElement;  
	        var keyCode = event.keyCode ? event.keyCode : event.which ?  
	                event.which : event.charCode; 
	                
	        if(keyCode != 8) return;//回退键
	        
	        if(obj != null && obj.tagName != null && (obj.tagName.toLowerCase() == "input"    
	               || obj.tagName.toLowerCase() == "textarea")){  
	            event.returnValue = true;
	            
	            if(!Ext.getCmp(obj.id)) return;
	            if(!Ext.getCmp(obj.id).readOnly) return;
	            
	            if(window.event){
	                event.returnValue = false ;//or event.keyCode=0
	            }else{
	                event.preventDefault();//for ff
	            }
	        }else{
	            if(window.event){
	                event.returnValue = false ;// or event.keyCode=0
	            }else{
	                event.preventDefault();//for ff
	            }
	        }
	    }
	    
	    var map = new Ext.KeyMap(document,[{  
	        key:[116],//F5 
	        fn:function(){},
	        stopEvent:true,
	        scope:this
	    },{
	        key:[37,39,115],//方向键左,右,F4
	        alt:true,
	        fn:function(){},
	        stopEvent:true,
	        scope:this
	    },{
	        key:[82],//ctrl+R
	        ctrl:true,
	        fn:function(){},
	        stopEvent:true,
	        scope:this
	    }]);
	    map.enable();
	}
};


//=========================获取路径-START=========================
/**
 * js获取项目根路径，如： http://localhost:8083/uimcardprj
 * @return {}
 */
function getRootPath(){
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
};
/**
 * 根据大小获取图标根目录
 * 例如需要获取16*16的图标根目录就可以调用getIconRootPathBySize(16)
 * 结果就是http://192.168.0.134/LabStarLIMS/images/icons/16
 * @param {} size
 * @return {}
 */
function getIconRootPathBySize(size){
	return getRootPath() + "/Images/Icons/" + size;
}
/**
 * 获取logo图片的根路径
 * @return {}
 */
function getLogoRootPath(){
	return getRootPath() + "/Images/Logo";
}
/**
 * 获取HTML背景图片的根路径
 * @param {} bo 是否相对路径，默认false（绝对路径）
 * @return {}
 */
function getHtmlBackgroundPictureRootPath(bo){
	var before = bo ? "" : getRootPath();
	return before + "/Images/HtmlBackground";
}
/**
 * 获取应用解析页面路径
 * @return {}
 */
function getAppHtmlPath(){
	return getRootPath() + "/ui/app/app.html";
}
//=========================获取路径-END=========================

/**
 * 树节点的勾选处理
 * 当节点被勾选时，该节点的子节点全部被勾选，往上追溯一直到根节点，判断每个父节点是否需要被勾选；
 * 当节点去掉勾选时，该节点的子节点全部去掉勾选，往上追溯一直到根节点，每个父节点全部去掉勾选；
 * @param {} node
 * @param {} checked
 */
function treeNodeCheckedChange(node,checked){
	/*向上遍历父结点*/ 
	var nodep=function(node){
		var bnode=true;
		Ext.Array.each(node.childNodes,function(v){ 
			if(!v.data.checked){
				bnode=false;
				return;
			}
		});
		return bnode;
	};
	var parentnode=function(node){
		if(node.parentNode != null){
			if(nodep(node.parentNode)){
				node.parentNode.set('checked',true);
			}else{ 
				node.parentNode.set('checked',false);
			}
			parentnode(node.parentNode);
		}
	};
	/*遍历子结点 选中 与取消选中操作*/
	var chd=function(node,check){
		node.set('checked',check);
		if(node.isNode){
			node.eachChild(function(child){
				chd(child,check);
			});
		}
	};
	
	if(checked){
		node.eachChild(function (child){
			chd(child,true);
		});
	}else{
		node.eachChild(function (child){
			chd(child,false);
		});
	}
	parentnode(node);//进行父级选中操作 
};
/**
 * 将传入的字符串参数转换成毫秒数返回
 * 传入的字符串参数："\/Date(1359779125000)\/"
 * @param {} str
 * @return {}
 */
function getMillisecondsFromStr(str){
	if(str == null) return "";
	if(str.length != 21) return "";
	
	var bef = str.substring(0,6);
	var aft = str.substring(str.length-2,str.length);
	if(bef != "/Date(" || aft!= ")/") return "";
	
	var milliseconds = str.substring(6,str.length-2);
	return milliseconds;
};
/***
 * JSON返回DateTime/Date('123123123')/
 * @param {} jsondate
 * @return {}
 */
function convertJSONDateToJSDateObject(jsondate) {
    if(jsondate == null) return "";
    var value = null;
    var type = Ext.typeOf(jsondate);
    if(type === 'string' && jsondate != ''){
    	var d = new Date(jsondate.replace(/-/g,'/'));
	    value = "\/Date(" + d.getTime() + "+0000)\/";
    }else if(type === 'date'){
    	value = "\/Date(" + jsondate.getTime() + "+0000)\/";
    }
    return value;
};
/**
 * 取得cookie 
 * @param {} name
 * @return {}
 */
function getCookie(name){
    var nameT = name,
		reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
		arr = document.cookie.match(reg);
		
	if(arr){
		var a = arr[2];
		var b = decodeURI(a);
	}
	
		
	if (arr) return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
	return null;
};
/**设置cookie属性*/
function setCookie(name, value) {
	var days = 30,
		exp = new Date();

	exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
	document.cookie = name + "=" + encodeURI(value) + "; expires=" + exp.toGMTString() + "; path=/";
};
/**
 * 取得cookie缓存时间
 * @return {}
 */
function getCookieDate(){
	var hourse = 24;//24小时
    var date = new Date(); 
	var ms = hourse*3600*1000; 
	date.setTime(date.getTime() + ms);
	return date;
};
/**
 * 模拟点击事件
 * @param {} id
 */
function simulateClickById(id){
	var button = document.getElementById(id);
	button.click();
};
/**
 * 打印条码
 * @param {} info 条码打印信息
 */
function PrintCode(info){
	var main = getMainWin(true);
	main.print(1,info);
};
/**
 * 打印报告
 * @param {} url 报告文件路径
 * @param {} printFlag 打印标志 [1报告;2清单]
 */
function PrintReport(url,printFlag){
	var main = getMainWin(true);
	main.print(2,url,printFlag);
};
/**
 * 获取主页面
 * @return {}
 */
function getMainWin(isTop){
	var win = null;
	for(var i=0;i<10;i++){
		win = getWin(i);
		if(win){
			var div = win.document.getElementById('main-page-div');
			if(div){
				if(isTop){
					if(win == win.parent) return win;
				}else{
					return win;
				}
			}
		}else{
			return null;
		}
	}
	return null;
}
/**
 * 是否可以打印条码
 * @return {}
 */
function canPrintCode(){
	var main = getMainWin(true);
	var but = main.document.getElementById('main-printcode-button');
	var state = but.getAttribute('PrintState');
	var bo = state == '0' ? false : true;
	return bo;
};
/**
 * 是否可以打印报告
 * @return {}
 */
function canPrintReport(){
	var main = getMainWin(true);
	var but = main.document.getElementById('main-printreport-button');
	var state = but.getAttribute('PrintState');
	var bo = state == '0' ? false : true;
	return bo;
};
/**
 * 根据层数获取window对象
 * @param {} num
 * @return {}
 */
function getWin(num){
	var length = num || 0;
	length = length > 0 ? length : 0;
	var win = window;
	for(var i=0;i<length;i++){
		win = win.parent;
	}
	return win;
};
/**
 * 根据ID从应用列表中获取应用信息
 * callback返回参数data:{success:true,ErrorInfo:'',data:{}}
 * @param {} id
 * @param {} callback
 */
function getAppObjectById(id,callback){
	var url = getRootPath()+'/ServerWCF/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById?id='+id;
	Ext.Ajax.defaultPostHeader = 'application/json';
	Ext.Ajax.request({
		async:false,//非异步
		url:url,
		method:'GET',
		success:function(response,opts){
			var result = Ext.JSON.decode(response.responseText);
			var info = {success:result.success,ErrorInfo:'',data:{}};
			if(result.success){
				var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
				var data = ResultDataValue.ClassCode;
				data = data.replace(/\\\\/g,"\\");
				data = data.replace(/\\\"/g,"\"");
				data = data.replace(/\\\'/g,"\'");
				
				info.data = eval(data);
			}else{
				info.ErrorInfo = result.ErrorInfo;
			}
			if(Ext.typeOf(callback)=='function'){
				callback(info);
			}
		},
		failure:function(response,options){
			var info = {success:false,ErrorInfo:'获取应用请求服务失败！',data:{}};
			if(Ext.typeOf(me.callback)=='function'){
				callback(info);
			}
		}
	});
};
/**
 * 获取页面
 * @private
 * @param {} info
 * @param {} par
 * @return {}
 */
function getPanelByInfoAndPar(info,par){
	var me = this;
	var p = null;
	
	par.autoScroll = true;
	
	if(info.success){
		p = Ext.create(info.data,par);
	}else{
		par.html = info.ErrorInfo;
		p = Ext.create('Ext.panel.Panel',par);
	}
	return p;
};
/**
 * 从后台获取数据
 * @private
 * @param {} url
 * @param {} obj
 * @param {} callback
 */
function getDataFromServer(url,obj,callback,params){
	var par = params || {};
	Ext.Ajax.defaultPostHeader = par.head || "application/json";
	
	var method = par.method || "POST";
	var param = par.method != 'GET' ? Ext.JSON.encode(obj) : null;
	
	Ext.Ajax.request({
		async:false,//非异步
		url:url,
		method:method,
		params:param,
		success:function(response,opts){
			if(Ext.typeOf(callback) == "function"){
				var result = Ext.JSON.decode(response.responseText);
				var data = {
					success:result.success,
					ErrorInfo:result.ErrorInfo,
					data:{count:0,list:[]}
				}
				if(result.success && result.ResultDataValue && result.ResultDataValue != ""){
					var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
					data.data = ResultDataValue;
				}
				callback(data);//回调函数
			}
		},
		failure : function(response,options){
			if(Ext.typeOf(callback) == "function"){
				var data = {
					success:false,
					ErrorInfo:'请求失败',
					data:{count:0,list:[]}
				}
				callback(data);//回调函数
			}
		}
	});
};
/**
 * 将压平的数据对象化
 * @param {} values
 * @param {} isEdit
 * @return {}
 */
function getObjByValue(values,isEdit){
	for(var i in values){
    	if(!values[i] || values[i] == ''){
    		delete values[i];
    	}
    }
	var maxLength = 0;//最大的层数
    for(var i in values){
        var arr = i.split('_');
        if(arr.length > maxLength){
            maxLength = arr.length;
        }
    }
    
    var obj = {};
    var addObj = function(key,num,value){
        var keyArr = key.split('_'); //键
        var ob = 'obj';
        for(var i=1;i<keyArr.length;i++){
            ob = ob + '["' + keyArr[i] + '"]';
            if(Ext.typeOf(eval(ob))==='undefined'){//对象不存在
                eval(ob + '={};');
            }
        }
        if(keyArr.length == num+1){//当前层赋值
        	var lastWord = keyArr.slice(-1);
        	value = lastWord == 'DataTimeStamp' ? value.split(',') : value;
        	value = lastWord == 'Birthday' ? convertJSONDateToJSDateObject(value) : value;
        	value = lastWord == 'DataAddTime' ? convertJSONDateToJSDateObject(value) : value;
        	value = lastWord == 'DataUpdateTime' ? convertJSONDateToJSDateObject(value) : value;
        	value = lastWord == 'UpdateTime' ? convertJSONDateToJSDateObject(value) : value;
        	if(!(isEdit && lastWord == 'DataTimeStamp')){
        		eval(ob + '=value;');
        	}
        }
    };
    
    for(var i=1;i<maxLength;i++){
        for(var j in values){
            var value = values[j];//值
			addObj(j,i,value);//键、层、值
		} 
	}
	
	//删除掉不需要的元素
	var isEmptyObject=function(obj){for(var name in obj){return false;}return true;};
	var deleteNodeArr=[]; 
	for(var i in obj){
		if(isEmptyObject(obj[i])){deleteNodeArr.push(i);}
	}
	for(var i in deleteNodeArr){
		delete obj[deleteNodeArr[i]];
	}
    
    return obj;
};
/**
 * 将压平的数据对象化
 * @param {} values
 * @param {} isEdit
 * @return {}
 */
function strToObj(values,isEdit){
	for(var i in values){
    	if(values[i] === 'undefined' || values[i] === null || values[i] === ''){
    		delete values[i];
    	}
    	if(Ext.typeOf(values[i]) === 'date'){
    		values[i] = convertJSONDateToJSDateObject(values[i]);
    	}
    }
	var maxLength = 0;//最大的层数
    for(var i in values){
        var arr = i.split('_');
        if(arr.length > maxLength){
            maxLength = arr.length;
        }
    }
    
    var obj = {};
    var addObj = function(key,num,value){
        var keyArr = key.split('_'); //键
        var ob = 'obj';
        for(var i=1;i<keyArr.length;i++){
            ob = ob + '["' + keyArr[i] + '"]';
            if(Ext.typeOf(eval(ob))==='undefined'){//对象不存在
                eval(ob + '={};');
            }
        }
        if(keyArr.length == num+1){//当前层赋值
        	var lastWord = keyArr.slice(-1);
        	if(lastWord == 'DataTimeStamp'){
        		if(!isEdit){//新增
        			if(keyArr.length > 2){
        				if(Ext.typeOf(value) === 'string'){
		        			eval(ob + '=value.split(",");');
		        		}else if(Ext.typeOf(value) === 'array'){
		        			eval(ob + '=value;');
		        		}else{
		        			eval(ob + '=null;');
		        		}
        			}
        		}else{//修改
        			if(keyArr.length == 2){
        				if(Ext.typeOf(value) === 'string'){
		        			eval(ob + '=value.split(",");');
		        		}else if(Ext.typeOf(value) === 'array'){
		        			eval(ob + '=value;');
		        		}else{
		        			eval(ob + '=null;');
		        		}
        			}
        		}
        	}else{
        		eval(ob + '=value;');
        	}
        }
    };
    
    for(var i=1;i<maxLength;i++){
        for(var j in values){
            var value = values[j];//值
			addObj(j,i,value);//键、层、值
		} 
	}
	
	//删除掉不需要的元素
	var isEmptyObject=function(o){
		for(var name in o){
			return false;
		}
		return true;
	};
	var deleteEmptyObject=function(o){
        for(var i in o){
        	var type = Ext.typeOf(o[i]);
            if(type === 'object'){
            	if(isEmptyObject(o[i])){
            		delete o[i];
            	}else{
            		deleteEmptyObject(o[i]);
            	}
            }else if(type === 'undeined' || type === 'null' || (type === 'string' && o[i] == '')){
                delete o[i];
			}
        }
    };
    deleteEmptyObject(obj);
    
    return obj;
};
 /***
 * 将传入的字符串(如BTDModuleType_DataTimeStamp)
 * 转换为hql查询格式btdmoduletype.DataTimeStamp
 * 第一个数据对象的字母为小写,其他的数据对象不变,以实心点代替'_'
 * @param {} objectName
 * @return {}
 */
function transformHqlStr(objectName) {
    var me = this;
    var defaultValueArr=objectName.split('_');
    var tempStr='';
    for(var j=0;j<defaultValueArr.length-1;j++){
        if(j==0){
            var tempVlue=defaultValueArr[j];
            tempStr=tempStr+tempVlue.toLowerCase()+'.';
        }
        else if(j<defaultValueArr.length-1){
            tempStr=tempStr+defaultValueArr[j]+'.';
        }
    }
    var hqlStr =tempStr+defaultValueArr[defaultValueArr.length-1];
    return hqlStr;
 };
 /**
  * 模块访问权限判断
  * @param {} id
  * @param {} callback
  */
function competence(id,callback){
 	//var mId = id || '';
 	//var bo = mId == '' ? false :true;
 	//(Ext.typeOf(callback) == 'function') && callback(bo);
 	var url = getRootPath() + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID?id=' + id;
 	var c = function(text){
 		if(Ext.typeOf(callback) == 'function'){callback(text);}
 	};
 	getToServer(url,c);
};
/**
 * 动态加载js文件
 * @param {} url
 * @param {} callback
 */
function loadJs(url,callback){
	var script = document.createElement('script');  
	script.type = 'text/javascript';  
	if (callback){
		script.onload = script.onreadystatechange = function(){    
			if (script.readyState && script.readyState != 'loaded' && script.readyState != 'complete') return;    
			script.onreadystatechange = script.onload = null;    
			callback();   
		};
	}
	script.src = url;  
	document.getElementsByTagName('head')[0].appendChild (script); 
};
/**
 * 判断执行程序
 * @param {} moduleId
 * @param {} url
 */
function initApp(url){
	var id = getQueryString("moduleId");
	var callback = function(text){
		var result = Ext.JSON.decode(text);
		if(result.success){
			loadJs(url);
		}else{
			var errorInfo = "<b style='color:red'>" + result.ErrorInfo + "</b>";
			document.write(errorInfo);
		}
	};
	competence(id,callback);
};
/**
 * POST方式与后台交互,callback返回response.responseText
 * @param {} url 服务地址
 * @param {} params 参数
 * @param {} callback 回调函数
 * @param {} defaultPostHeader 请求头(选配)，默认为application/json
 * @param {} async 是否异步,默认false
 * @param {} timeout 超时时间,默认30秒
 */
function postToServer(url,params,callback,defaultPostHeader,async,timeout){
	var t = timeout || 30000;
    uiToServer('POST',url,callback,async,params,defaultPostHeader,t);
};
/**
 * GET方式与后台交互,返回response.responseText
 * @param {} url 服务地址
 * @param {} callback 回调函数
 * @param {} async 是否异步,默认false
 * @param {} timeout 超时时间,默认30秒
 */
function getToServer(url,callback,async,timeout){
	var t = timeout || 30000;
    uiToServer('GET',url,callback,async,null,null,t);
};
/**
 * 前后台交互
 * @param {} url url 服务地址
 * @param {} callback 回调函数
 * @param {} async 是否异步,默认false
 * @param {} params POST参数
 * @param {} defaultPostHeader 请求头(选配)，默认为application/json
 * @param {} timeout 超时时间,默认30秒
 */
function uiToServer(method,url,callback,async,params,defaultPostHeader,timeout){
	Ext.Ajax.defaultPostHeader = defaultPostHeader || 'application/json';
    var bo = async === false ? false :true;
    
    var con = {
        url:url,
        async:bo,
        method:method,
        success:function(response,opts){
            if(Ext.typeOf(callback) == "function"){
                callback(response.responseText);//回调函数
            }
        },
        failure:function(response,options){
        	if(Ext.typeOf(callback) == "function"){
        		var value = "";
        		if(response.request.timedout){//请求超时
        			value = "{success:false,ErrorInfo:'请求服务超时!',ResultDataValue:''}";
        		}else{
        			value = "{success:false,ErrorInfo:'请求服务失败!状态码:" + response.status + "',ResultDataValue:''}";
        		}
                callback(value);//回调函数
            }
        }
    };
    
    if(params){//POST参数
    	con.params = params;
    }
    
    if(timeout){//超时
    	con.timeout = timeout;
    }
    
    Ext.Ajax.request(con);
};
/**
 * 根据中文名称获取拼音字头
 * @param {} CName 中文名称
 * @param {} callback 回调函数，直接返回拼音字头
 */
function getPinYinZiTouFromServer(CName,callback){
	var me = this;
	var url = encodeURI(getRootPath()+'/ServerWCF/ConstructionService.svc/GetPinYin' + "?chinese=" + CName);
	
	if(!CName || CName == ""){
		if(Ext.typeOf(callback) == "function"){callback("");return;}
	}
	
	var c = function(text){
		var result = Ext.JSON.decode(text);
		var value = null;
		if(result.success){
			value = result.ResultDataValue;
		}else{
			alertError(result.ErrorInfo);
		}
		if(Ext.typeOf(callback) == "function"){callback(value);}
	};
	getToServer(url,c,false);
};
/**
 * 获取服务器时间
 * @param {} 
 * @return {} callback 回调函数，直接获取服务时间
 */
function getServerInformation(callback){
    var me = this;
    var url = getRootPath() + '/ConstructionService.svc/CS_UDTO_GetServerInformation';///ServerWCF
    var c = function(text){
    	var result = Ext.JSON.decode(text);
        var value = null;
        
        if(result.success){
            var ResultDataValue = result.ResultDataValue;
            if(ResultDataValue != ""){
	            var obj = Ext.decode(ResultDataValue);
	            value = obj.ServerCurrentTime;
            }
        }else{
            alertError(result.ErrorInfo);
        }
        
        if(Ext.typeOf(callback) == "function"){callback(value);} 
    }
	getToServer(url,c,false);  
};
/**
 * 对象处理：将一些特殊字符进行转换
 * @param {} obj
 * @return {}
 */
function changeObj(obj){
	var co = function(o){
		if(Ext.typeOf(o) == 'string'){
			o = o.replace(/<br\/>/g,'\r\n');
		}else{
			for(var i in o){
				o[i] = co(o[i]);
			}
		}
		return o;
	};
	return co(obj);
};
/**
 * HQL字符串转换处理--将单引号,百分号特殊字符进行转换
 * 先替换百分号再替换单引号
 * @param {} str
 * @return {}
 */
function changeCharacter(str){
    var result=str;
    result = result.replace(/\%/g,'%25');
    result = result.replace(/'/g,'%27');
    return result;
};
/**
 * 找不到模块图标时使用默认图标
 * @param {} src
 */
function mofindModuleImg(img){
	img.src=getRootPath()+'/ui/main/images/main/missingmodule32.png'; 
	img.onerror=null;
};
/**
 * 弹出错误提示
 * @param {} text
 * @param {} [title]
 */
function alertError(text,title){
	Ext.Msg.show({
		title:title || '<b>错误提示</b>',
		msg:text,
		modal:true,
		buttons:Ext.Msg.YES,
		icon:Ext.Msg.ERROR
	});
};
/**
 * 弹出信息提示
 * @param {} text
 * @param {} [title]
 */
function alertInfo(text,title){
	Ext.Msg.show({
		title:title || '<b>信息提示</b>',
		msg:text,
		modal:true,
		buttons:Ext.Msg.YES,
		icon:Ext.Msg.INFO
	});
};
/**
 * 根据ID获取应用
 * @param {} id
 * @param {} callback
 * @param {} fields
 */
function getAppInfo(id,callback,fields,isPlanish){
	var me = this;
	var bo = isPlanish === false ? false : true;
	if(id && id != -1){
		var url = getRootPath()+"/ServerWCF/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById?isPlanish=" + bo + "&id=" + id;
		url += "&fields=" + (fields || "");
		//回调函数
        var c = function(text){
        	var result = Ext.JSON.decode(text);
        	var info = {success:false,ErrorInfo:'',appInfo:""};
        	if(result.success){
                if(result.ResultDataValue && result.ResultDataValue != ""){
                	info.success = true;
                	//result.ResultDataValue =result.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
                	info.appInfo = Ext.JSON.decode(result.ResultDataValue);
                	if(info.appInfo.BTDAppComponents_ClassCode){
                		info.appInfo.BTDAppComponents_ClassCode = info.appInfo.BTDAppComponents_ClassCode.replace(/%22/g,'"');
                	}
                }else{
                	info.success = false;
            		info.ErrorInfo = "没有获取到应用组件信息！";
                }
            }else{
            	info.ErrorInfo = result.ErrorInfo;
            }
            if(Ext.typeOf(callback) == "function"){
            	callback(info);//回调函数
            }
        };
       	//util-POST方式交互
    	getToServer(url,c);
	}
};
function openFormWin(id,config,callback){
	var c = function(info){
		if(info.success){
			var ClassCode = info.appInfo['BTDAppComponents_ClassCode'];
			var panel = eval(ClassCode);
			openWin(panel,config,callback);
		}else{
			alertError(info.ErrorInfo);
		}
	};
	var fields = "BTDAppComponents_ClassCode";
	getAppInfo(id,c,fields);
};
function openHtmlWin(config,callback){
	var panel = "Ext.panel.Panel";
	if(config.url){
		config.html = "<html><body><iframe src='" + config['url'] + "' style='height:100%;width:100%;' frameborder='no'></iframe></body></html>";
	}
	openWin(panel,config,callback);
};
/**
 * 弹出窗口
 * @param {} panel
 * @param {} config
 * @param {} callback
 * @param {} e
 */
function openWin(panel,config,callback,e){
	var maxHeight=document.body.clientHeight*0.98;
	var maxWidth=document.body.clientWidth*0.98;
	var par = { 
		maxWidth:maxWidth, 
		autoScroll:true,
		modal:true,
		//frame:true,
		floating:true,
		closable:true,
		draggable:true,
		resizable:true
	};
	for(var i in config){
		par[i] = config[i];
	}
	
	var win = Ext.create(panel,par);
	if(win.height > maxHeight){win.setHeight(maxHeight-20);}
	if(e){
		win.showAt(e.getXY());
	}else{
		win.show();
	}
	if(Ext.typeOf(callback) === 'function'){callback(win);}
	return win;
};
/**
 * 分组查询特殊字符串转码
 * @param {} value
 * @return {}
 */
function groupingSearchString(value){
    var v = value || "";
    v=v.replace(/\%25/g,"%")////先还原百分号
    v=v.replace(/\%27/g,"'");//还原单引号
    v=v.replace(/\%2B/g,"+");//还原加号
    v=v.replace(/\%2D/g,"-");//还原减号
    return v;
};
/**
 * 表单查询HQLWhere串处理
 * @param {} items 表单项组件集合
 * @param {} length 表单项组件集合长度
 * @param {} logicalType 逻辑运算符
 */
function selectFormChangeHQL(items,length,logicalType){
    var lastValue='',myValue=''; //myValue:单个组件的结果值;lastValue:最后where串
    var operation=' like ';//关系运算符,默认为like
    for(var i=0;i<length;i++){
    var ob =items[i];
    var myType= ob.xtype;
    var defaultValueArr = ob.itemId.split('_');
    var myItemId = defaultValueArr[defaultValueArr.length-1];
    //处理运算关系符下拉列表框,当myItemId=operation时
    if(myType === 'label'||myType === 'button'||myType === 'image'||myType === 'filefield'||myType === 'htmleditor'||myItemId==='operation'){
    
    }else{
    var tempStr='';
    for(var j=0;j<defaultValueArr.length-1;j++){
        if(j==0){
            var tempVlue=defaultValueArr[j];
            tempStr=tempStr+tempVlue.toLowerCase()+'.';
        }
        else if(j<defaultValueArr.length-1){
            tempStr=tempStr+defaultValueArr[j]+'.';
        }
    }
    myItemId =tempStr+defaultValueArr[defaultValueArr.length-1];
    //取出相关字段的运算符
    //处理当前组件是否绑定带有运算关系符
    var isOperation=ob.isOperation;
    if(isOperation==true){
        var item=form.getComponent(ob.itemId+'_operation');
        operation=' '+item.getValue()+' ';  
    }else{
        operation="";
    }
    //如果组件为单选,复选组,不需要关系运算,默认为 in
     if(myType === 'checkboxgroup'||myType === 'radiogroup'){
        var arr=ob.getChecked();
        myValue=' in (';
        for(var j=0;j<arr.length;j++){
            myValue=((j+1)==arr.length)?(myValue +"'"+ arr[j].inputValue+"'"):(myValue+"'" + arr[j].inputValue +"',");
        };
        myValue=myValue+')';
        //复选组的空值处理
        if(arr.length<1||myValue=='in ()'){}else{
            lastValue=lastValue+(myItemId+myValue+" "+  logicalType ); 
        } 
      }else if(myType === 'dateintervals'||myType === 'daterange'){//如果组件为日期区间,不需要关系运算,默认为 between and
            var tempValue=ob.getValue();
            var tempArr=[];
            if(tempValue!=""){
                tempArr=tempValue.split('|');
                if(tempArr.length>0){
                    var valueOne=tempArr[0];
                    var valueTwo=tempArr[1];
                    if(valueOne==''||valueTwo==''){
 
                    }else{
	                    //格式化为日期格式
	                    var dateOne=Ext.util.Format.date(tempArr[0],'Y-m-d')+" 00:00:00";
	                    var dateTwo=Ext.util.Format.date(tempArr[1],'Y-m-d')+" 23:59:59";
	                    var tempStr=(" ("+myItemId+" between "+"'"+dateOne+"'"
	                    +" and "+"'"+dateTwo+"'"+") "+ logicalType );
	                    lastValue=lastValue+tempStr;
                    }
                } 
            }
       }else if(myType === 'timeintervals'){
            var tempValue=ob.getValue();
            var tempArr=[];
            if(tempValue!=""){
                tempArr=tempValue.split('|');
                if(tempArr.length>0){
                    var valueOne=tempArr[0];
                    var valueTwo=tempArr[1];
                    if(valueOne==''||valueTwo==''){

                    }else{
	                     //格式化为时间格式
	                    var dateOne=Ext.util.Format.date(tempArr[0],'H:i');
	                    var dateTwo=Ext.util.Format.date(tempArr[1],'H:i');
	                    var tempStr=(myItemId+" between "+"'"+dateOne+"'"+" and "+"'"+dateTwo+"'"+" "+ logicalType );
	                    lastValue=lastValue+tempStr;
                    }
                } 
            }
         }else if(myType === 'numbersintervals'){
            var tempValue=ob.getValue();
            var tempArr=[];
            if(tempValue!=""){
                tempArr=tempValue.split('|');
                if(tempArr.length>0){
                    var valueOne=tempArr[0];
                    var valueTwo=tempArr[1];
                    if(valueOne==''||valueTwo==''){
           
                    }else{
	                    var tempStr=(myItemId+" >= "+valueOne+" and " +myItemId+"<="+valueTwo+" "+ logicalType );
	                    lastValue=lastValue+tempStr;
                    }
                } 
            }
         }else if(myType === 'textfield'){
            operation=' like ';
            myValue=ob.getValue();
            if(myValue!=""){
                myValue=''+myValue+'%';//模糊查询
                var tempStr=(myItemId+operation +"'"+myValue+"' "+ logicalType );
                lastValue=lastValue+tempStr;
            }
         }else if(myType === 'combobox'){//下拉列表时
            var multiSelect=ob.multiSelect;
            myValue=ob.getValue();
            
            if(multiSelect==false){//单项选择
                operation='=';
                if((myValue==='null')||(myValue==null)||(myValue==="")){
            
	            }else if(myValue!=""&&myValue!=null){
	                var tempStr=(myItemId+operation +"'"+myValue+"' "+ logicalType );
	                lastValue=lastValue+tempStr;
	            }
            }else if(multiSelect==true){//多项选择
                operation=' in ';
                var tempStr="";
                if((myValue==='null')||(myValue==null)||(myValue==="")){
            
                }else if(myValue!=""&&myValue!=null){
                    var tempValue=myValue.toString().split(",");
                    if(tempValue.length==1){
                        operation=' = ';
                        tempStr=(myItemId+operation +""+myValue+" "+ logicalType );
                        lastValue=lastValue+tempStr;
                    }else if(tempValue.length>1){
                        tempStr=(myItemId+operation +"("+myValue+") "+ logicalType );
                        lastValue=lastValue+tempStr;
                    }
                }
            }
            
         }else{
            if((operation=='')||(operation==null)||(operation==='null')||(myValue=="")){
                operation=' = ';
            }
            myValue=ob.getValue();
            if((myValue==='null')||(myValue==null)||(myValue==="")){
            
            }else{
                if(myValue!=""){
                    if(myType == 'datetimenew'){
                       myValue=''+Ext.util.Format.date(myValue,'Y-m-d H:i:s');
                    }
                    //日期类型
                    if(myType === 'datefield'){
                        myValue=''+Ext.util.Format.date(myValue,'Y-m-d');
                    }
                    if(myType === 'timefield'){
                        myValue=''+Ext.util.Format.date(myValue,'H:i:s');
                    }
                    var tempStr=(myItemId+ operation+"'"+myValue+"' "+ logicalType );
                    lastValue=lastValue+tempStr;
                }
            }
       }
    }
   }
   if(lastValue==""||lastValue==null||lastValue==undefined||lastValue=='undefined'){
      lastValue='1=1';
   }else{
      lastValue=lastValue+" 1=1";
   }
   return lastValue;
};
/**
 * 字符串转码
 * @param {} value
 * @return {}
 */
function encodeString(value){
	var v = value || "";
	//v = encodeURI(v);//不转义保留字符
	v = encodeURIComponent(v);//转义保留字符
	
	return v;
};
/**
 * 字符串解码
 * @param {} value
 * @return {}
 */
function decodeString(value){
	var v = value || "";
	v = decodeURI(v);
	return v;
};
/**
 * 特殊字符转码
 * @param {} value
 * @return {}
 */
function encodeSpecialChar(value){
	var v = value || "";
	if(v === "'"){
		v = "##";
	}else if(v === "\""){
		v = "@@";
	}
	return v;
};
/**
 * 特殊字符解码
 * @param {} value
 * @return {}
 */
function decodeSpecialChar(value){
	var v = value || "";
	if(v === "##"){
		v = "'";
	}else if(v === "@@"){
		v = "\"";
	}
	return v;
};
//=========================系统信息-START=========================
/**
 * 获取服务器当前时间
 * @param {} callback
 */
function getServerTime(callback){
	var me = this;
	var c = function(text){
		var result = Ext.JSON.decode(text);
        var date = null;
        if(result.success){
            var ResultDataValue = result.ResultDataValue;
            if(ResultDataValue != ""){
	            var obj = Ext.decode(ResultDataValue);
	            date = new Date(obj.ServerCurrentTime.replace(/-/g,'/'));
				setCookie('systemTime',obj.ServerCurrentTime);
            }
        }
        if(Ext.typeOf(callback) === 'function'){callback(date);}
	};
	var url = getRootPath()+'/ConstructionService.svc/CS_UDTO_GetServerInformation';///ServerWCF
	getToServer(url,c);
}
/**
 * 获取系统时间
 * @param {boolean} isString 是否已字符串方式返回日期
 */
function getSystemTime(isString){
	var p = null;
	for(var i=0;i<10;i++){
		p = getWin(i);
		if(p){
			if(p.ServerInfo){
				return isString ? getDateString(p.ServerInfo.Time) : p.ServerInfo.Time;
			}
		}else{
			break;
		}
	}
	
	var systemTime = getCookie('systemTime');
	
	if(systemTime){
		var date = new Date(systemTime.replace(/-/g,'/'));
		return isString ? getDateString(date) : date;
	}
	
	return null;
}
/**
 * 获取日期字符串
 * @param {} date
 * @param {} hasHMS 是否需要时分秒
 * @param {} hasDay 是否需要星期
 * @return {}
 */
function getDateString(date,hasHMS,hasDay){
	var d = null;
	var type = Ext.typeOf(date);
	if(type == 'date'){
		d = date;
	}else if(type == 'string'){
		d = new Date(date.replace(/-/g,'/'));
	}else{
		d = new Date();
	}
	
	var year = d.getFullYear() + '',
		month = (d.getMonth() + 1) + '',
		date2 = d.getDate() + '';
		
		month = month.length == 1 ? "0" + month : month;
		date2 = date2.length == 1 ? "0" + date2 : date2;
		
	var info = year + "-" + month + "-" + date2;
	
	if(hasHMS){
		var hours = d.getHours() + '';
		var minutes = d.getMinutes() + '';
		var seconds = d.getSeconds() + '';
		
		hours = hours.length == 1 ? "0" + hours : hours;
		minutes = minutes.length == 1 ? "0" + minutes : minutes;
		seconds = seconds.length == 1 ? "0" + seconds : seconds;
		
		info += " " + hours + ":" + minutes + ":" + seconds;
	}
	if(hasDay){
		var day = d.getDay();
		var text = "星期";
		switch(day){
			case 0:text += "日";break;
			case 1:text += "一";break;
			case 2:text += "二";break;
			case 3:text += "三";break;
			case 4:text += "四";break;
			case 5:text += "五";break;
			case 6:text += "六";break;
			
		}
		info += " " + text;
	}
	
	return info;
}
/**
 * 获取距离date这个时间num天的时间对象;
 * @param {date/string} date 当前时间
 * @param {number} num 默认为1,可以负数,例如-1就是昨天,1是明天;
 * @return {}
 */
function getNextDate(date,num){
	var d = null;
	var n = isNaN(num) ? 1 : parseInt(num);
	
	var type = Ext.typeOf(date);
	if(type === 'date'){
		d = new Date(date.getTime());
		d.setDate(d.getDate() + n);
	}else if(type === 'string'){
		d = new Date(date.replace(/-/g,'/'));
		d.setDate(d.getDate() + n);
	}
	
	return d;
}

/**
 * 获取当前月的第一天
 * @param {} date
 * @return {}
 */
function getCurrentMonthFirst(date){
	if(!date) return null;
	
	var d = Ext.clone(date);
	d = d || new Date();
	
	var type = Ext.typeOf(d);
	if(type == 'string'){
		d = new Date(d.replace(/-/g,'/'));
	}
	
 	d.setDate(1);
 	return d;
}
/**
 * 获取当前月的最后一天
 * @param {} date
 * @return {}
 */
function getCurrentMonthLast(date){
	if(!date) return null;
	
 	var d = date || new Date();
	
	var type = Ext.typeOf(d);
	if(type == 'string'){
		d = new Date(d.replace(/-/g,'/'));
	}
	
 	var currentMonth = d.getMonth();
 	var nextMonth = ++currentMonth;
 	var nextMonthFirstDay = new Date(date.getFullYear(),nextMonth,1);
 	var oneDay = 1000*60*60*24;
 	return new Date(nextMonthFirstDay - oneDay);
}

/**
 * 根据参数名称获取系统信息，不传参数默认返回所有系统信息
 * @param {string} name 需要返回的参数名称，可以为空
 * @return {}
 */
function getSystemInfo(name){
	var result = {
		LabID:getCookie('000100'),//实验室ID
		LabName:getCookie('000101'),//实验室名称
		EmployeeID:getCookie('000200'),//员工ID
		EmployeeName:getCookie('000201'),//员工姓名
		UserID:getCookie('000300'),//员工账户ID
		UserAccount:getCookie('000301'),//员工账户名
		UseCode:getCookie('000302'),//员工代码
		HRDeptID:getCookie('000400'),//部门ID
		HRDeptName:getCookie('000401'),//部门名称
		HRDeptCode:getCookie('000402'),//部门编号
		ModuleID:getCookie('000500'),//模块ID
		ModuleName:getCookie('000501'),//模块名称
		ModuleOperID:getCookie('000600'),//模块操作ID
		ModuleOperName:getCookie('000601')//模块操作名称
	};
	if(name){
		result = result[name] || "";
	}
	return result;
}
/**cookie中属性对应表*/
var cookie = {
	'000100':'LabID',//实验室ID
	'000101':'LabName',//实验室名称
	'000200':'EmployeeID',//员工ID
	'000201':'EmployeeName',//员工姓名
	'000300':'UserID',//员工账户ID
	'000301':'UserAccount',//员工账户名
	'000302':'UseCode',//员工代码
	'000400':'HRDeptID',//部门ID
	'000401':'HRDeptName',//部门名称
	'000500':'ModuleID',//模块ID
	'000501':'ModuleName',//模块名称
	'000600':'ModuleOperID',//模块操作ID
	'000601':'ModuleOperName',//模块操作名称
	'100001':'608EE9C7CA151681C73',//默认模块ID
	'100002':'isLocked',//锁定标记
	'100003':'oldUserAccount',//历史账户名
	'100004':'openedModuleIds',//历史打开记录
	'100005':'rememberUser',//是否记住用户名
	'100006':'rememberPwd'//是否记住密码
};
//=========================系统信息-END=========================

/**
 * Ext.grid.Panel合并单元格
 * @param {} grid  要合并单元格的grid对象
 * @param {} cols  要合并哪几列 [1,2,4]
 */
function mergeCells(grid,cols){
    var arrayTr=document.getElementById(grid.getId()+"-body").firstChild.firstChild.firstChild.getElementsByTagName('tr');
    var trCount = arrayTr.length;
    var arrayTd;
    var td;
    var merge = function(rowspanObj,removeObjs){//定义合并函数
        if(rowspanObj.rowspan != 1){
            arrayTd =arrayTr[rowspanObj.tr].getElementsByTagName("td");//合并行
            td=arrayTd[rowspanObj.td-1];
            td.rowSpan=rowspanObj.rowspan;
            td.vAlign="middle";                
            Ext.each(removeObjs,function(obj){ //隐身被合并的单元格    
                arrayTd =arrayTr[obj.tr].getElementsByTagName("td");   
                arrayTd[obj.td-1].style.display='none';               
            });   
        }      
    };     
    var rowspanObj = {}; //要进行跨列操作的td对象{tr:1,td:2,rowspan:5}        
    var removeObjs = []; //要进行删除的td对象[{tr:2,td:2},{tr:3,td:2}]    
    var col;   
    Ext.each(cols,function(colIndex){ //逐列去操作tr
		var isCheckColumn = grid.columns[colIndex-1].xtype == 'checkcolumn';//是否是勾选列
        var rowspan = 1;
        var divHtml = null;//单元格内的数值
		var checkV = null;//勾选框的值===========
        for(var i=1;i<trCount;i++){//i=0表示表头等没用的行
			var record = grid.store.getAt(i-1);//===========
            arrayTd = arrayTr[i].getElementsByTagName("td");
            var cold=0;
//          Ext.each(arrayTd,function(Td){ //获取RowNumber列和check列
//              if(Td.getAttribute("class").indexOf("x-grid-cell-special") != -1)
//                  cold++;
//          });
            col=colIndex+cold;//跳过RowNumber列和check列
            if(!divHtml){
				checkV = record.get(grid.checkField);//===========
                divHtml = arrayTd[col-1].innerHTML;
                rowspanObj = {tr:i,td:col,rowspan:rowspan}
            }else{
				var nowV = record.get(grid.checkField);//===========
                var cellText = arrayTd[col-1].innerHTML;
                var addf=function(){
                    rowspanObj["rowspan"] = rowspanObj["rowspan"]+1;
                    removeObjs.push({tr:i,td:col});
                    if(i==trCount-1)
                        merge(rowspanObj,removeObjs);//执行合并函数
                };
                var mergef=function(){
                    merge(rowspanObj,removeObjs);//执行合并函数
					checkV = nowV;//==============
                    divHtml = cellText;
                    rowspanObj = {tr:i,td:col,rowspan:rowspan}
                    removeObjs = [];
                };
                if(cellText == divHtml){
					if(isCheckColumn){//勾选列
						if(nowV == checkV){
							record.set(grid.displayField,true);
							addf();
						}else{
							record.set(grid.displayField,false);
							mergef();
						}
					}else{
						if(colIndex!=cols[0]){
	                        var leftDisplay=arrayTd[col-2].style.display;//判断左边单元格值是否已display
	                        if(leftDisplay=='none')
	                            addf();
	                        else
	                            mergef();
	                    }else
	                        addf();
					}
                }else
                    mergef();
            }
        }
    });
};
/**
 * Ext.grid.Panel合并单元格
 * @param {} grid  要合并单元格的grid对象
 * @param {} cols  要合并哪几列 [1,2,4]
 */
function mergeCells_Resouce(grid,cols){
    var arrayTr=document.getElementById(grid.getId()+"-body").firstChild.firstChild.firstChild.getElementsByTagName('tr');
    var trCount = arrayTr.length;
    var arrayTd;
    var td;
    var merge = function(rowspanObj,removeObjs){//定义合并函数
        if(rowspanObj.rowspan != 1){
            arrayTd =arrayTr[rowspanObj.tr].getElementsByTagName("td");//合并行
            td=arrayTd[rowspanObj.td-1];
            td.rowSpan=rowspanObj.rowspan;
            td.vAlign="middle";                
            Ext.each(removeObjs,function(obj){ //隐身被合并的单元格    
                arrayTd =arrayTr[obj.tr].getElementsByTagName("td");   
                arrayTd[obj.td-1].style.display='none';               
            });   
        }      
    };     
    var rowspanObj = {}; //要进行跨列操作的td对象{tr:1,td:2,rowspan:5}        
    var removeObjs = []; //要进行删除的td对象[{tr:2,td:2},{tr:3,td:2}]    
    var col;   
    Ext.each(cols,function(colIndex){ //逐列去操作tr
        var rowspan = 1;
        var divHtml = null;//单元格内的数值
        for(var i=1;i<trCount;i++){//i=0表示表头等没用的行
            arrayTd = arrayTr[i].getElementsByTagName("td");
            var cold=0;
//          Ext.each(arrayTd,function(Td){ //获取RowNumber列和check列
//              if(Td.getAttribute("class").indexOf("x-grid-cell-special") != -1)
//                  cold++;
//          });
            col=colIndex+cold;//跳过RowNumber列和check列
            if(!divHtml){
                divHtml = arrayTd[col-1].innerHTML;
                rowspanObj = {tr:i,td:col,rowspan:rowspan}
            }else{
                var cellText = arrayTd[col-1].innerHTML;
                var addf=function(){
                    rowspanObj["rowspan"] = rowspanObj["rowspan"]+1;
                    removeObjs.push({tr:i,td:col});
                    if(i==trCount-1)
                        merge(rowspanObj,removeObjs);//执行合并函数
                };
                var mergef=function(){
                    merge(rowspanObj,removeObjs);//执行合并函数
                    divHtml = cellText;
                    rowspanObj = {tr:i,td:col,rowspan:rowspan}
                    removeObjs = [];
                };
                if(cellText == divHtml){
                    if(colIndex!=cols[0]){
                        var leftDisplay=arrayTd[col-2].style.display;//判断左边单元格值是否已display
                        if(leftDisplay=='none')
                            addf();
                        else
                            mergef();
                    }else
                        addf();
                }else
                    mergef();
            }
        }
    });
};
//=========================页面信息-START=========================
/**
 * 获取request的参数值
 * @param name：匹配参数名称
 * @param {} name
 * @return {}
 */
function getQueryString(name) {
	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)","i");
	var r = window.location.search.substr(1).match(reg);
	if (r!=null) return unescape(r[2]); return null;
}
/**
 * 更改页面风格
 * @param url
 */
function changeStyle(url){
	Ext.util.CSS.swapStyleSheet('theme',url);
}
//=========================页面信息-END=========================

/**加载后处理*/
(function(){
	/**页面风格*/
	var sysStyleUrl = getQueryString("sysStyleUrl");
	sysStyleUrl && changeStyle(sysStyleUrl);
})();