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
	showLog:false,
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
	reportPath: 'ReportFormFiles',//报告文件目录
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
	getUiPath: function () {
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
	toString: function (value, onlyDate, hasMilliseconds) {
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
		if (hasMilliseconds) {
            info += ' ' + value.getMilliseconds();
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
					arr[j] = temp;
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
			setTimeout(function(){
				msgbox.show(config);
			},10);
		}else{
			setTimeout(function(){
				Ext.Msg.show(config);
			},10);
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
//adobe组件
Shell.util.Adobe = {
	//地址
	Url:Shell.util.Path.rootPath + '/web_src/adobe/Adobe Reader XI_11.0.0.379.exe',
	//IE8下载地址
	IE8Url:Shell.util.Path.rootPath + '/web_src/ie/IE8-WindowsXP-x86-CHS.exe',
	//下载HTML内容
	getDownLoadHtml:function(){
	    var html = 
        '<div style="width:100%;padding:50px;text-align:center;">' +
		    '<b>浏览器未安装Adobe Reader，请点击"安装"按钮，下载Adobe Reader进行安装</b></br>' + 
		    '<div style="position: absolute;left:50%;margin-left:-50px;margin-top:50px;width:100px;padding:10px;text-align;center;color:#ffffff;background-color:#169ada;"' +
			    ' onclick="window.open(\'' + this.Url + '\');">安装</div>' +
	    '</div>';
	    return html;
	},
	//判断adobe组件是否已安装
	isInstall:function(){
		//下面代码都是处理IE浏览器的情况 
	    if((window.ActiveXObject)||(navigator.userAgent.indexOf("Trident") > -1)) {
	        for(x = 2; x < 10; x++) {
	            try {
	                oAcro = eval("new ActiveXObject('PDF.PdfCtrl." + x + "');");
	                if(oAcro) {
	                    return true;
	                }
	            } catch(e) {}
	        }
	        try {
	            oAcro4 = new ActiveXObject('PDF.PdfCtrl.1');
	            if(oAcro4)
	                return true;
	        } catch(e) {}
	        try {
	            oAcro7 = new ActiveXObject('AcroPDF.PDF.1');
	            if(oAcro7)
	                return true;
	        } catch(e) {}
	    }else{
	    //chrome和FF、safrai等其他浏览器
	        return true;
	    }
	}
};
//获取IE版本
Shell.util.IE = {
	getIEVersion:function() {
        var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
        var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1; //判断是否IE<11浏览器  
        var isEdge = userAgent.indexOf("Edge") > -1 && !isIE; //判断是否IE的Edge浏览器  
        var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf("rv:11.0") > -1;
        if(isIE) {
        	var docMode = document.documentMode;
            var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
            reIE.test(userAgent);
            var fIEVersion = parseFloat(RegExp["$1"]);
            if((fIEVersion == 7 && docMode != 8 && docMode != 9 && docMode != 10) || docMode == 7) {
                return 7;
            } else if((fIEVersion == 8 && docMode != 7 && docMode != 9 && docMode != 10) || docMode == 8) {
                return 8;
            } else if((fIEVersion == 9 && docMode != 7 && docMode != 8 && docMode != 10) || docMode == 9) {
                return 9;
            } else if((fIEVersion == 10 && docMode != 7 && docMode != 8 && docMode != 9) || docMode == 10) {
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
}