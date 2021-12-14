//自动配置layui的base目录，必须优先加载layui.js
(function () {
	layui.config({
		version: window.UIVersion ? window.UIVersion.JS : 'basic',
		base: (function () {
			//获取layui包路径
			var location = window.document.location,
				curWwwPath = location.href,
				pathName = location.pathname,
				pos = curWwwPath.indexOf(pathName),
				localhostPath = curWwwPath.substring(0, pos),
				name = pathName.split('/').slice(1, 3).join('/'),
				uiPath = localhostPath + '/' + name + '/layui';

			return uiPath;
		})() + '/'
	});
})();