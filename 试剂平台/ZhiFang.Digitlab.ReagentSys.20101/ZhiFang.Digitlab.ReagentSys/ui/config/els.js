(function() {
	//-----------系统名称-----------
	var System = 'REA';
	var debug = true;
	//-----------------------------
	
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true),
		content = [];
	
	//加载系统框架文件
	content.push('<script type="text/javascript" src="config/config_System.js"></script>');
	//加载系统配置文件
	content.push('<script type="text/javascript" src="config/config_' + System + '.js"></script>');
	//加载图表框架文件
	content.push('<script type="text/javascript" src="echarts/esl.js"></script>');
	//写入当前页面中
	if(content.length > 0){
		document.write(content.join(''));
	}
})();