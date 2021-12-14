//自动配置layui的base目录，必须优先加载layui.js
(function(){
	layui.config({
		version:window.UIVersion ? window.UIVersion.JS : 'basic',
		base:(function(){
			//获取layui包路径
			var location = window.document.location,
				curWwwPath = location.href,
				pathName = location.pathname,
				pos = curWwwPath.indexOf(pathName),
				localhostPath = curWwwPath.substring(0,pos),
				name = pathName.split('/').slice(1,3).join('/'),
				uiPath = localhostPath + '/' + name + '/layui';
				
			return uiPath;
		})() + '/'
	});
	
	layui.extend({
		uxutil:'ux/util'
	}).use(['uxutil','layer'],function(){
		var uxutil = layui.uxutil;
		
		//session过期编码
		var SessionOutCode = "session_out";
		//session有效性检验
		window.onSessionValid = function(code){
			var isValid = true;
			//session过期处理
			if(SessionOutCode == code){
				isValid = false;
				var loginUrl = uxutil.path.LAYUI + '/admin/user/login.html';//登录界面
				//防止多次弹出
				if(!window.isSessionOut){
					window.isSessionOut = true;
					layer.msg('session过期，准备前往登录界面！',{icon:5,shade:0.3},function(){
						location.href = loginUrl;
					});
				}
			}
			return isValid;
		}
	});
})();