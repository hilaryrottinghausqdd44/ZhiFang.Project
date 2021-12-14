/**
 * 系统公用功能类
 * @type 
 */
var Shell = Shell || {};

Shell.util = {};

/**系统路径*/
Shell.util.Path = {
	defaultHelpClassName:'Shell.help.Default',
	rootPath:'',//项目根目录
	uiPath:'',//ui包根目录
	helpPath:'',//帮助文档目录
	buildPath:'buildfile',//构建文件根目录
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
		if(Ext.typeOf(str) != 'string') -1;
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
			date = value;
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
		
		var arr = list || [],
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
//	/**查看信息*/
//	showInfo:function(value){
//		Ext.Msg.show({
//			title:'提示信息',
//			icon:Ext.Msg.INFO,
//			msg:value
//		});
//	},
//	/**查看警告*/
//	showWarning:function(value){
//		Ext.Msg.show({
//			title:'警告信息',
//			icon:Ext.Msg.WARNING,
//			msg:value
//		});
//	},
//	/**查看错误*/
//	showError:function(value){
//		Ext.Msg.show({
//			title:'错误信息',
//			icon:Ext.Msg.ERROR,
//			msg:value
//		});
//	},
	/**查看log错误信息*/
	showLog:function(value){
		console.log(value);
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
	/**打开窗口*/
	open:function(className,config){
		if(!className){
			Shell.util.Msg.showError('页面不存在！');
			return;
		}
		
		var maxWidth = document.body.clientWidth - 20,
			maxHeight = document.body.clientHeight - 20;
			
		config = Ext.apply({
			maxWidth:maxWidth,
			maxHeight:maxHeight,
			minWidth:100,
			minHeight:50,
			//autoScroll:true,
			modal:true,
			//frame:true,
			floating:true,
			closable:true,
			draggable:true,
			resizable:true
		},config);
		
		return Ext.create(className,config).show();
	},
	/**打印文件*/
	print:function(url){
		var win = Shell.util.Win.open('Shell.ux.panel.Panel',{
			title:'文件打印',width:2400,height:1200,
			html:"<iframe height='100%' width='100%' frameborder='0' style='overflow:hidden;overflow-x:hidden;" +
				"overflow-y:hidden;height:100%;width:100%;position:absolute;top:0px;left:0px;right:0px;bottom:0px'" +
				" src='" + Shell.util.Path.rootPath + '/' + url + "' ></iframe>"
		});
		return win;
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

/**处理*/
Shell.util.Action = {
	/**延时处理*/
	delay:function(fun,scope,delayTime){
		if(Ext.typeOf(fun) != 'function'){
			Shell.util.Msg.showLog('Shell.util.Action.delay方法参数错误:fun参数不是function!');
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

/**应用类*/
Shell.util.Class = {
	/**根据功能编码返回类名*/
	getNameByCode:function(code){
		if(!code) return null;
		return "Shell." + Shell.util.Path.buildPath.replace(/\//g,".") + ".ClassCode." + code;
	},
	/**获取应用参数元数据*/
	getMetaDataUrl:function(code){
		if(!code) return null;
		return Shell.util.Path.uiPath + "/" + Shell.util.Path.buildPath + "/DesignCode/" + code + ".TXT";
	}
};