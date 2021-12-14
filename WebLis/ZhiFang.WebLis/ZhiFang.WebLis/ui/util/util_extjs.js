/**
 * 系统公用功能类-extjs
 * @author Jcall
 * @version014-01-05
 */
var Shell = Shell || {};

Shell.extjsUtil = Shell.extjsUtil || {};

Shell.extjsUtil.Function = {};

/**提示信息*/
Shell.extjsUtil.Msg = {
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
	}
};

/**窗口*/
Shell.extjsUtil.Win = {
	/**打开路径页面*/
	openUrl:function(url,config){
		var win = Shell.extjsUtil.Win.open('Shell.ux.panel.Panel',Ext.apply({
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
			Shell.extjsUtil.Msg.showError('页面不存在！');
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
		var win = Shell.extjsUtil.Win.open('Shell.ux.panel.Panel',{
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

/**应用类*/
Shell.extjsUtil.Class = {
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