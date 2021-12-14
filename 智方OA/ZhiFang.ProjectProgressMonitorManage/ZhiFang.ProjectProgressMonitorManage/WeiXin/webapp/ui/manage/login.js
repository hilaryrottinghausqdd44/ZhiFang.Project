$(function() {
	/**手机号码信息*/
	var cellphone = {
		/**提示信息*/
		info: {
			/**请输入手机号码*/
			is_empty: "请输入手机号码"
		},
		/**获取手机号框的值*/
		getValue: function() {
			var value = $("#cellphone_number").val();
			value = !value ? "" : $.trim(value);
			return value;
		}
	};
	/**密码信息*/
	var password = {
		/**提示信息*/
		info: {
			/**请输入密码*/
			is_empty: "请输入密码"
		},
		/**获取密码框的值*/
		getValue: function() {
			var value = $("#password").val();
			value = !value ? "" : $.trim(value);
			return value;
		}
	};
	/**信息处理*/
	var info = {
		/**显示正确信息*/
		show: function(value) {
			ShellComponent.messagebox.msg(value);
		},
		/**显示错误信息*/
		error: function(value) {
			var msg = '<b style="color:red;">' + value + '</b>';
			ShellComponent.messagebox.msg(msg);
		}
	};
	/**服务端处理*/
	var server = {
		/**提交登录信息*/
		login: function(params, callback) {
			Shell.util.Server.ajax({
				url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/Login?MobileCode=" +
					params.cellphone_number + "&Pwd=" + params.pwd
			}, callback);
		}
	};
	/**数据校验*/
	var valided = {
		/**登录提交数据的校验*/
		login_params: function(cellphone_number, pwd) {
			//判断手机号码是否通过校验
			if (!cellphone_number) {
				info.error(cellphone.info.is_empty);
				return;
			}

			//判断密码是否通过校验
			if (!pwd) {
				info.error(password.info.is_empty);
				return;
			}

			return true;
		}
	};


	//确定按钮处理
	$("#submit_btn").on("click", function() {
		var cellphone_number = cellphone.getValue(),
			pwd = password.getValue();

		//校验需要提交的信息
		var isValid = valided.login_params(cellphone_number, pwd);

		if (!isValid) return;
		
		//提交登录信息
		ShellComponent.mask.submit();
		server.login({
			cellphone_number: cellphone_number,
			pwd: pwd
		}, function(data) {
			ShellComponent.mask.hide();
			if (data.success) {
				after_login_ok();
			} else {
				data.msg = data.msg ? data.msg : "密码错误";
				info.error(data.msg);
			}
		});
	});

	//登录成功后处理
	function after_login_ok() {
		//alert("注册成功");
		window.location.href = Shell.util.Path.uiPath + "/index.html";
	}
	
	//初始化头像和名称
	var account_img = "../img/icon/default_user_photo.png";
	$("#account_img").attr("src",account_img);
	
	var account_name = "微信用户";
	$("#account_name").html(account_name);
});