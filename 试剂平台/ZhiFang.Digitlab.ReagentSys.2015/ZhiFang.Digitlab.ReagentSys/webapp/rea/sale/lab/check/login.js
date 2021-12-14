var Shell = Shell || {};
Shell.check = Shell.check || {};
Shell.check.login = Shell.check.login || {};
//登录
Shell.check.loginIn = function(userInfo, callback) {
	//判断本地缓存是否存在,不存在,需要重新登录
	userInfo.LABID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
	if(!userInfo.LABID) userInfo.ISLOGIN = 1;
	
	if(userInfo.ACCOUNT && userInfo.PASSWORD) {
		if(userInfo.ISLOGIN == 1) {
			var url = Shell.util.Path.ROOT + '/RBACService.svc/RBAC_BA_Login' +
				'?strUserAccount=' + userInfo.ACCOUNT + '&strPassWord=' + userInfo.PASSWORD + "&t=" + new Date().getTime();
			ShellComponent.mask.loading();
			//获取账户信息
			Shell.util.Server.ajax({
				async: true,
				url: url
			}, function(data) {
				ShellComponent.mask.hide();
				setTimeout(function() {					
					var result={
						success:data,
						msg:"",
					};
					if(result.success == true) {
						userInfo.ISLOGIN =0;
						Shell.check.login.loginSuccess(userInfo, function(userInfo, result2) {
							callback(userInfo, result2);
						});
					} else {
						userInfo.ISLOGIN = 1;
						if(!result.msg) result.msg = "验证账号或密码错误！";
						callback(userInfo, result);
					}
				}, 100);
			});
		} else {
			//如果不需要重新登录
			var result = {
				success: true,
				msg: ""
			};
			callback(userInfo, result);
		}
	} else {
		var result = {
			success: true,
			msg: ""
		};
		userInfo.ISLOGIN = 1;
		result.msg = "参数:ACCOUNT,PASSWORD的账号或密码错误！";
		callback(userInfo, result);
	}
};
//登录成功
Shell.check.login.loginSuccess = function(userInfo, callback) {
	Shell.Rea.onAfterLogin(function(data) {
		if(data.success) {
			userInfo.ISLOGIN =0;
			//机构id
			userInfo.LABID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
		} else {
			userInfo.ISLOGIN = 1;
			if(!data.msg) data.msg = "参数:ACCOUNT,PASSWORD的账号或密码错误！获取机构信息错误！";
		}
		callback(userInfo, data);
	});
};
//获取供应商选择数据
Shell.check.login.loadCompOrgOptionData = function(LABID, callback) {
	if(!LABID) {
		ShellComponent.messagebox.msg("非法登录:缺失账户信息!");
		return;
	}
	var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_GetLabInterfaceOrgList';
	url += ("?t=" + new Date().getTime());
	ShellComponent.mask.loading();
	Shell.util.Server.ajax({
		async: false,
		url: url
	}, function(data) {
		ShellComponent.mask.hide();
		if(data.success) {
			var list = [];
			if(data.value && data.value.list) list = data.value.list;
			callback(list);
		} else {
			ShellComponent.messagebox.msg(data.msg); //data.ErrorInfo
			callback([]);
		}
	});
};