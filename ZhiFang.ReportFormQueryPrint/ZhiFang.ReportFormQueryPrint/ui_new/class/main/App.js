Ext.Loader.setConfig({
    enabled:true,
    paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function () {
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.useShims = true;//防止PDF挡住Exj原始组件内容
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
	    window.location.href = Shell.util.Path.uiPath + '/class/main/userSign/index.html';
	}   
    
    
    


    panel = Ext.create('Shell.class.main.app.App',{
    	header:false,
    	urls:[{
    		text:'对外功能',
    		leaf:false,
    		expanded:true,
    		children:[
    			{text:"医生页面",leaf:true,url:Shell.util.Path.uiPath + '/doctor.html'},
		        {text:"护士页面",leaf:true,url:Shell.util.Path.uiPath + '/nurse.html'},
		        {text:"自助页面",leaf:true,url:Shell.util.Path.uiPath + '/selfhelp.html'},
		        {text:"技师站",leaf:true,url:Shell.util.Path.uiPath + '/lis.html'},
		        { text: "门诊页面", leaf: true, url: Shell.util.Path.uiPath + '/odp.html' },
                { text: "站点页面", leaf: true, url: Shell.util.Path.uiPath + '/siteQuery.html' },
                { text: "微信查询", leaf: true, url: Shell.util.Path.rootPath + '/web_app/ui/report/show/list.html' },
                { text: "集中打印", leaf: true, url: Shell.util.Path.uiPath + '/focusPrint.html' }
    		]
    	},{
    		text:'对外服务说明',
    		leaf:false,
    		expanded:true,
    		children:[
    			
    		]
    	},{
    		text:'内部功能',
    		leaf:false,
    		expanded:true,
    		children: [
                { text: "页面设置", leaf: true, url: Shell.util.Path.uiPath + '/setting.html' },
		        { text: "报告测试", leaf: true, url: Shell.util.Path.uiPath + '/class/debug/index.html' },
		        {text:"删除临时文件",leaf:true,url:Shell.util.Path.uiPath + '/class/manage/delete/index.html'}
    		]
    	}]
   });

    //总体布局
    var viewport = Ext.create('Ext.container.Viewport',{
        layout:'fit',
        padding:1,
        items:[panel]
   });
});
