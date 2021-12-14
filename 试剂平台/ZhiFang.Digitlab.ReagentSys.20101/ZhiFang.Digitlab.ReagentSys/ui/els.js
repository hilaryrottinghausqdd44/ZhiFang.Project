(function() {
	//-----------系统名称-----------
	var System = 'REA';
	var debug = true;
	//-----------------------------
	
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true),
		content = [];
	var t = new Date().getTime();
	content.push('/config/config_System.js?t=' + t);//加载系统框架文件
	content.push('/config/config_' + System + '.js');//加载系统配置文件
	content.push('/echarts/esl.js');//加载图表框架文件
	
	for(var i in content){
		content[i] = '<script type="text/javascript" src="' + JShell.System.Path.UI + content[i] + '"></script>';
	}
	
	//写入当前页面中
	if(content.length > 0){
		document.write(content.join(''));
	}
})();