(function() {
	//-----------系统名称-----------
	var System = 'REA';
	var debug = true;
	//-----------------------------
	
	//语言包处理，默认加载中文语言包
	//var params = JShell.Page.getParams(true);
	var content = [];
	
	//加载系统框架文件
	//content.push('<script type="text/javascript" src="config/config_System.js"></script>');
	
	//content.push('/src/jquery.min.js');
	content.push('/src/bootstrap-3.3.2-dist/js/bootstrap.js');
	content.push('/src/bootstrap-3.3.2-dist/js/bootstrap-switch.js');
	//content.push('/util/util.js');
	content.push('/util/component.js');
	content.push('/config/config_' + System + '.js');//加载系统配置文件
	
	//加载图表框架文件
	//content.push('<script type="text/javascript" src="echarts/esl.js"></script>');
	//写入当前页面中
	var len = content.length;
	if(len > 0){
		for(var i=0;i<len;i++){
			content[i] = '<script type="text/javascript" src="' + 
				Shell.util.Path.UI + content[i] + '"></script>';
		}
		document.write(content.join(''));
	}
	
	//返回按钮监听
	$(".navbar-top-back").on(Shell.util.Event.click,function(){
		history.go(-1);
	});
	//返回按钮监听
	$(".navbar-top-home").on(Shell.util.Event.click,function(){
		location.href = Shell.util.Path.UI + '/sysbase/main/index.html';
	});
})();