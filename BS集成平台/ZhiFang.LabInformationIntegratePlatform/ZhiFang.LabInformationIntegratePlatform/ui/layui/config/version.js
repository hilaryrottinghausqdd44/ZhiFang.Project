//版本号文件
(function(){
	var UIVersion = {
		//JS版本号
		JS:'1.1.0.1',//2021-11-03
		//CSS版本号
		CSS: '1.1.0.1',//2021-11-03
		//图标版本
		ICON: '1.1.0.1',//2021-11-03
		//Layui版本号
		LAYUI: '2.4.5',//2021-11-03
		//测试模式
		DEBUG:false,
		//是否开启WebSocket通讯
		WEBSOCKET: false
	};
	//UI地址
	UIVersion.UI = (function(){
		var head = document.getElementsByTagName('head')[0],
			scripts = head.getElementsByTagName('script'),//获所有script标签
			script = scripts[scripts.length - 1],
			uiUrl= script.src.split('/layui/config/version.js')[0];
			
		return uiUrl;
	})();
	
	//加载JS文件
	UIVersion.getJS = function(url){
		document.write('<script type="text/javascript" src="' + url + '?v=' + UIVersion.JS + '"><\/script>');
	};
	//加载CSS文件
	UIVersion.getCSS = function(url){
		document.write('<link rel="stylesheet" href="' + url + '?v=' + UIVersion.CSS + '" media="all">');
	};
	//加载图标文件
	UIVersion.getIcon = function(url){
		document.write('<link rel="shortcut icon" href="' + url + '?v=' + UIVersion.ICON + '">');
	};
	//加载Layui核心库文件
	UIVersion.loadLayui = function(version){
		var layuiUrl = UIVersion.UI + '/src/layui/' + (version || UIVersion.LAYUI);
		layuiUrl += (UIVersion.DEBUG ? '/src' : '/dist');
		
		UIVersion.getCSS(UIVersion.UI + '/logo-16.png');
		UIVersion.getCSS(layuiUrl + '/css/layui.css');
		//UIVersion.getCSS(UIVersion.UI + '/layui/css/layui.form.sm.all.css');
		UIVersion.getJS(layuiUrl + '/layui.js');
		UIVersion.getJS(UIVersion.UI + '/layui/config/system.js');
	};
	
	window.UIVersion = UIVersion;
})();