(function() {
	//-----------系统名称-----------
	var System = 'BLTF';
	var debug = true;
	//-----------------------------
	
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true),
		content = [];
	//var t = new Date().getTime();
	content.push('/ui/extjs/config/config_System.js?v=' + JShell.System.JS_VERSION);//加载系统框架文件
	content.push('/ui/extjs/config/config_' + System + '.js?v=' + JShell.System.JS_VERSION);//加载系统配置文件
	//content.push('/config/config_BLTF.js?v=' + JShell.System.JS_VERSION);//加载系统配置文件
//	content.push('/echarts/esl.js');//加载图表框架文件
	
	for(var i in content){
		content[i] = '<script type="text/javascript" src="' + JShell.System.Path.getRootPath() + content[i] + '"></script>';
	}
	
	//写入当前页面中
	if(content.length > 0){
		document.write(content.join(''));
	}
})();