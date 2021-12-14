layui.config({
	base:'../layuiadmin/' //静态资源所在路径
}).extend({
	index:'lib/index',//主入口模块
	uxutil: '../../../ux/util'
}).use(['index','user','uxutil'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form;
		
	//请求登入服务地址/ServerWCF
	var LOGIN_URL = uxutil.path.LIIP_ROOT + '/RBACService.svc/RBAC_BA_Login';
	
	//用户名回车监听
	$("#LAY-user-login-username").keypress(function(e){
		if(e.which == 13){
			$("#Button_Login").click();
		}
	});
	//密码回车监听
	$("#LAY-user-login-password").keypress(function(e){
		if(e.which == 13){
			$("#Button_Login").click();
		}
	});
	
	form.render();
	//提交
	form.on('submit(LAY-user-login-submit)',function(obj){
		onLogin();
	});
	
	function onLogin(){
		var account = $("#LAY-user-login-username").val();
		var password = $("#LAY-user-login-password").val();
		var url = LOGIN_URL + '?strUserAccount=' + account + '&strPassWord=' + password;
		
		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			//临时登录帐号admin adminzhifangbeijingkeji8001
			if(data === true||(account=="admin"&&password=="adminzhifangbeijingkeji8001")){
				layui.data('user',{
					'account':account,
					'password':password
				});
				//手工调用数据库升级服务
				onDBUpdate();
				layer.msg('登入成功',{
					icon:1,
					time:500
				},function(){
					location.href = '../index.html?t=' + new Date().getTime();//主页
				});
			}else{
				layer.msg('账号或密码错误！');
			}
		});
	};
	//手工调用数据库升级服务
	function onDBUpdate(){
		if(!uxutil.loginAfterIsUpdateDB)return;
		var url = uxutil.path.ROOT + '/RBACService.svc/RBAC_SYS_DBUpdate';
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data === true){
				
			}
		});		
	};	
});
