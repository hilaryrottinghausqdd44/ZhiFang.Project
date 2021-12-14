/***
 * 公共引用文件
 * @author Jcall
 * @version 2015-01-29
 */
(function(){
	//js获取项目根路径,如:http://localhost:8080/A
	function getRootPath(){
		//获取当前网址,如:http://localhost:8080/Web/ui/test.html
		var href = window.document.location.href;
		//获取主机地址之后的目录,如:Web/ui/test.html
		var pathname = window.document.location.pathname;
		var pos = href.indexOf(pathname);
		//获取主机地址,如:http://localhost:8080
		var localhostPaht=href.substring(0,pos);
		//获取带"/"的项目名,如:/Web
		var projectName = pathname.substring(0,pathname.substr(1).indexOf('/')+1);
		return(localhostPaht + projectName);
	}
	//项目根目录路径
	var rootPath = getRootPath();
	
	//公共css代码
	var css =  
	    "<link rel='stylesheet' type='text/css' href='" + rootPath + "/ui/easyui/themes/default/easyui.css'/>" + 
	    "<link rel='stylesheet' type='text/css' href='" + rootPath + "/ui/easyui/themes/icon.css'/>" + 
	    "<link rel='stylesheet' type='text/css' href='" + rootPath + "/ui/css/buttons.css'/>";
	    
	//公共javascript代码
	var javascript = 
		"<script type='text/javascript' src='" + rootPath + "/ui/easyui/jquery.min.js'></script>" + 
		"<script type='text/javascript' src='" + rootPath + "/ui/easyui/jquery.easyui.min.js'></script>" + 
		"<script type='text/javascript' src='" + rootPath + "/ui/easyui/locale/easyui-lang-zh_CN.js'></script>" + 
		"<script type='text/javascript' src='" + rootPath + "/ui/util/util.js'></script>" + 
		"<script type='text/javascript' src='" + rootPath + "/ui/util/util_easyui.js'></script>";
	
	//写入当前页面中
	document.write(css + javascript);
})();
