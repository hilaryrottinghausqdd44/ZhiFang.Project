(function() {
	//-----------系统名称-----------
	var System = 'REA';
	var debug = true;
	//-----------------------------
	
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true),
		content = [];
	var t = new Date().getTime();
	content.push('/config/config_System.js?v=' + JShell.System.JS_VERSION);//加载系统框架文件
	content.push('/config/config_QMS.js?v=' + JShell.System.JS_VERSION);//加载系统框架文件
	content.push('/config/config_' + System + '.js?v=' + JShell.System.JS_VERSION);//加载系统配置文件
	//content.push('/echarts/esl.js');//加载图表框架文件
	/***
	 * 作者：longfuchu
	 * 时间：2019-02-27
	 * 描述：echarts由原echarts2.0升级为echart4.2.1
	 */
	content.push('/echart4.2.1/echarts.min.js');//图表文件
	for(var i in content){
		content[i] = '<script type="text/javascript" src="' + JShell.System.Path.UI + content[i] + '"></script>';
	}
	
	//写入当前页面中
	if(content.length > 0){
		document.write(content.join(''));
	}
})();