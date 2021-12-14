/**
 * @author guohx
 * @version 2020-04-8
*/
Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.useShims=true;//防止PDF挡住Exj原始组件内容
	Shell.util.Win.begin();//屏蔽快捷键
	
	//根据参数决定是否显示log信息
	var params = Shell.util.Path.getRequestParams(true);
	for(var i in params){
		if(i.toLowerCase() === "SHOWLOG" && params[i] === "true"){
			Shell.util.Config.showLog = true;
		}else if(i.toLowerCase() === "SHOWLOGWIN" && params[i] === "true"){
			Shell.util.Config.showLogWin = true;
		}
	}
	//判断用户是否登录
	function getCookie(name) {
			var cookies = document.cookie;
			var list = cookies.split("; ");    // 解析出名/值对列表
			      
			for(var i = 0; i < list.length; i++) {
				var arr = list[i].split("=");  // 解析出名和值
				if(arr[0] == name)
					return decodeURIComponent(arr[1]);  // 对cookie值解码
			} 
			return "";
		}
	var webconfigStaticUser = getCookie("webconfigStaticUser");
  	var cookie = {webconfigStaticUser:webconfigStaticUser};
	
	if (webconfigStaticUser == null|| webconfigStaticUser == "") {
	    window.location.href = Shell.util.Path.uiPath + '/class/setting/userSign/index.html';
	} 	
	
	
	
	panel = Ext.create('Shell.class.setting.app.App', {
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:1,
		items:[panel]
	});
});