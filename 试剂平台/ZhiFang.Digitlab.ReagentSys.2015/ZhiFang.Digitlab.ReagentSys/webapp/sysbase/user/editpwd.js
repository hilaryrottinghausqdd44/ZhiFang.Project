$(function() {
	//显示错误信息
	function showError(msg){
		$("#error").html(msg);
		$("#error").show();
	}
	//隐藏错误信息
	function hideError(){
		$("#error").hide();
	}

	//保存处理
	$("#submit").on(Shell.util.Event.click, function() {
		hideError();
		
		getPwd(function(password){
			var account = Shell.util.Cookie.get(Shell.util.Cookie.map.ACCOUNTNAME);
			var entity = {
				entity: {
					Id: Shell.util.Cookie.get(Shell.util.Cookie.map.ACCOUNTID),
					PWD: password
				},
				fields:'Id,PWD'
			};
			//修改密码
			var url = Shell.util.Path.ROOT + "/RBACService.svc/RBAC_UDTO_UpdateRBACUserByField";
			ShellComponent.mask.submit();
			Shell.util.Server.ajax({
				type:'post',
				url: url,
				data:Shell.util.JSON.encode(entity)
			}, function(data){
				ShellComponent.mask.hide();
				if(data.success == true){
					afterEdit(account,password);
				}else{
					showError(data.msg);
				}
			});
		});
	});
	//检验并获取密码
	function getPwd(callback){
		var p = $("#password").val(),
			p1 = $("#password1").val(),
			p2 = $("#password2").val(),
			error = null,
			newP = null;
			
		if(p1 != p2){error = "两次新密码不一致!";}
		if(p == p1){error = "新密码与当前密码相同,不需要保存!";}
		if(!p1){error = "新密码不能为空!";}
		
		if(error){
			showError(error);
			return;
		}
		
		//判断当前密码是否正确
		var url = Shell.util.Path.ROOT + "/RBACService.svc/RBAC_BA_Login?isValidate=true&strUserAccount=" + 
			Shell.util.Cookie.get(Shell.util.Cookie.map.ACCOUNTNAME) + "&strPassWord=" + p;
			
		ShellComponent.mask.submit();
		//获取账户信息
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			if(data == true){
				callback(p1);
			}else{
				showError("当前密码错误,请重新输入!");
			}
		});
	}
	//修改成功后处理
	function afterEdit(account,password){
		//清空当前用户密码
		Shell.util.LocalStorage.addAccount(account,"");
		//默认选中首页
		Shell.util.LocalStorage.set(Shell.util.LocalStorage.map.INDEXTYPE,0);
		//转向到登录页面
		location.href = Shell.util.Path.UI + '/sysbase/main/login.html';
	}
});