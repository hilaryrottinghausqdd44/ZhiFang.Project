//应用记录ID
var id = getQueryString("id");
//模块ID
var moduleId = getQueryString("moduleId");
//是否有功能菜单
var hasMenu = function(){
	var bo = false;
	var UserAccount = Ext.util.Cookies.get('000301');//账号
	if(UserAccount == 'admin'){
		bo = true;
	}
	return bo;
};
//获取菜单
var getMenu = function(e){
	var bo = hasMenu();
	if(bo){
		new Ext.menu.Menu({
			items:[{
				text:'查看模块相关应用',iconCls:'build-button-configuration-blue',
				tooltip:'查看模块相关应用',
				handler:function(but,e){
					openWin('Ext.build.changkanziyingyong',{
						title:'查看子应用',
			       		type:1,
			       		root:{
				            id:id,tid:0,ParentID:0,
				            leaf:false,expanded:true
				        }
					});
				}
			},{
				text:'模块信息管理',iconCls:'build-button-configuration-blue',
				tooltip:'模块信息管理',
				handler:function(but,e){
					openWin('Ext.manage.module.rightApp',{
						title:'模块信息管理',
			       		moduleId:moduleId
					});
				}
			},{
				text:'模块角色操作权限',iconCls:'build-button-configuration-blue',
				tooltip:'模块角色操作权限',
				handler:function(but,e){
					openWin('Ext.manage.module.ModuleRoleOperateList',{
						title:'模块角色操作',
			       		moduleId:moduleId
					});
				}
			},{
				text:'模块操作数据过滤条件',iconCls:'build-button-configuration-blue',
				tooltip:'模块操作数据过滤条件',
				handler:function(but){
					openWin('Ext.manage.datafilters.rightApp',{
						title:'模块操作数据过滤条件',
			       		moduleId:moduleId
					});
				}
			}]
		}).showAt(e.getXY());//让右键菜单跟随鼠标位置
	}
};
//错误显示处理
var showErrorInfo = function(value){
	var error = document.getElementById('error');
	error.innerHTML = value;
}
//获取应用类代码
var getClassCode = function(id,callback){
	var fields = "BTDAppComponents_ClassCode";
	var isPlanish = true;
	var c = function(info){
		if(info.success){
			var ClassCode = info.appInfo['BTDAppComponents_ClassCode'];
			callback(ClassCode);//回调函数
		}else{
			showErrorInfo(info.ErrorInfo);
		}
	};
	//获取应用信息
	getAppInfo(id,c,fields,isPlanish);
};
//获取模块操作
var getModuleOperList = function(id,callback){
	var fields = "RBACModuleOper_Id,RBACModuleOper_UseCode";
	var url = getRootPath() + "/RBACService.svc/RBAC_UDTO_SearchModuleOperByModuleID?isPlanish=true&id=" + id + "&fields=" + fields;
	var c = function(text){
		var result = Ext.JSON.decode(text);
		var data = {count:0,list:[]};
		if(result.ResultDataValue && result.ResultDataValue != ""){
			data = Ext.JSON.decode(result.ResultDataValue);
		}
		callback(data.list);
	};
	//获取模块操作
	getToServer(url,c);
};
//初始化模块
var initModule = function(ClassCode,ModuleOperList){
	Ext.onReady(function(){	
		Ext.QuickTips.init();//初始化后就会激活提示功能
		Ext.Loader.setConfig({enabled: true});//允许动态加载
		Ext.Loader.setPath('Ext.zhifangux', '../zhifangux');
		Ext.Loader.setPath('Ext.build', '../build/class');
		Ext.Loader.setPath('Ext.manage', '../manage/class');
		
		var panel = eval(ClassCode);
		
		var params = Shell.util.Path.getRequestParams(true);
		var config = {
			moduleOperList:ModuleOperList
		};
		
		for(var i in params){
			if(i != 'SYSSTYLEURL' || i != 'ID'){
				config[i] = params[i];
			}
		}
		
		var app = Ext.create(panel,config);
		
		//总体布局
		var view  = Ext.create('Ext.container.Viewport',{
			layout:'fit',
			items:[app],
			listeners:{
				contextmenu:{
					element:'el',
					fn:function(e,t,eOpts){
						//禁用浏览器的右键相应事件 
		        		e.preventDefault();e.stopEvent();
		        		//右键菜单
		        		getMenu(e);
					}
				}
			}
		});
	});
	
};
//初始化页面
var init = function(){
	if(!id || id == ""){
		showErrorInfo("提示:没有传递应用ID！");
		return;
	}
	if(!moduleId || moduleId == ""){
		showErrorInfo("提示:没有传递模块ID！");
		return;
	}
	var callbackCodeClass = function(CodeClass){
		var callbackModuleOperList = function(ModuleOperList){
			initModule(CodeClass,ModuleOperList);
		};
		getModuleOperList(moduleId,callbackModuleOperList);
	};
	getClassCode(id,callbackCodeClass);
};
init();