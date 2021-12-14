(function() {
	//-----------系统名称-----------
	var System = 'WFM';
	var debug = true;
	//-----------------------------
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true),
		content = [];
	
	content.push('/config/config_System.js?v=' + JShell.System.JS_VERSION);//加载系统框架文件
	content.push('/config/config_QMS.js?v=' + JShell.System.JS_VERSION);
	content.push('/config/config_' + System + '.js?v=' + JShell.System.JS_VERSION);//加载系统配置文件

	for(var i in content){
		content[i] = '<script type="text/javascript" src="' + JShell.System.Path.UI + content[i] + '"></script>';
	}
	content.push('<script type="text/javascript" src="' + JShell.System.Path.ROOT + '/web_src/echarts/esl.js"></script>');//加载图表框架文件

	//写入当前页面中
	if(content.length > 0){
		document.write(content.join(''));
	}
})();