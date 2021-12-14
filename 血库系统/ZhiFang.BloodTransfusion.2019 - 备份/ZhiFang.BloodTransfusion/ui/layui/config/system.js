(function(){
	var JcallShell = window.JcallShell || {};
	JcallShell.System = JcallShell.System || {};
	//系统名称
	JcallShell.System.Name = 'LIIP_实验室信息集成平台';
	//同域系统映射
	JcallShell.System.Map = {
		'REA.CLIENT':'zf.rea.client',
		'QMS':'ZhiFang.QMS',
		'GENE':'ZhiFang.Gene'
	};
	window.JcallShell = JcallShell;
})();

//自动配置layui的base目录，必须优先加载layui.js
(function(){
	//静态地址
	var path = {
		getLayuiPath: function(){
			var location = window.document.location,
				curWwwPath = location.href,
				pathName = location.pathname,
				pos = curWwwPath.indexOf(pathName),
				localhostPath = curWwwPath.substring(0,pos),
				name = pathName.split('/').slice(1,3).join('/'),
				uiPath = localhostPath + '/' + name + '/layui';
				
			return uiPath;
		}
	};
	layui.config({
		version:'2020091001',
		base:path.getLayuiPath() + '/'
	});
})();
