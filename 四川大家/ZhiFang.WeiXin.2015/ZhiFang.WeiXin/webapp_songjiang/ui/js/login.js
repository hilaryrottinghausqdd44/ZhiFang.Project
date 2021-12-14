$(function() {
	//页面所有功能对象
 	var shell_win = {
		/**系统*/
		system: {
			/**微信ID*/
			open_id: Shell.util.Cookie.getCookie("openId"),
			/**账号ID*/
			user_id: Shell.util.Cookie.getCookie("userId"),
			/**用户昵称*/
			UserName:null,
			/**用户手机号码*/
			MobileCode:null,
			/**头像地址*/
			HeadImgUrl:null,
			/**系统初始化*/
			init: function() {
				var me = this,
					url = "/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL",
					fields = "BWeiXinAccount_UserName,BWeiXinAccount_MobileCode,BWeiXinAccount_HeadImgUrl",
					id = me.open_id;
				
				if(!id){
					me.show_error("微信ID不存在 ！");
					return;
				}
				
				url = Shell.util.Path.rootPath + url + "?page=-1&rows=-1&fields=" + fields + 
					"&where=WeiXinAccount='" + id + "'";
				
				//获取账户信息
				Shell.util.Server.ajax({
					async:false,
					url: url
				}, function(data){
					if(data.success){
						if(!data.value || !data.value.list || data.value.list.length == 0){
							me.show_error("没有获取到用户数据！");
							return;
						}
						var obj = data.value.list[0];
						me.UserName = obj.UserName;
					  	me.MobileCode = obj.MobileCode;
					  	me.HeadImgUrl = obj.HeadImgUrl;
					  	//显示页面
						shell_win.show_page();
					}else{
						me.show_error(data.msg);
					}
				});
			},
			/**ianshi错误信息*/
	 		show_error:function(value){
	 			var msg = '<div style="text-align:center;margin:50px;">' + value + '</div>';
				$("#page_home").html(msg);
	 		}
		},
		/**密码信息*/
		password:{
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
			},
			/**判断密码是否通过校验*/
			isValid:function(value){
				if (!value) {
					shell_win.info.error(password.info.is_empty);
					return;
				}
	
				return true;
			}
		},
		/**信息处理*/
		info:{
			/**显示正确信息*/
			show: function(value) {
				ShellComponent.messagebox.msg(value);
			},
			/**显示错误信息*/
			error: function(value) {
				var msg = '<b style="color:red;">' + value + '</b>';
				ShellComponent.messagebox.msg(msg);
			}
		},
		/**显示页面*/
		show_page:function(){
			//初始化头像和名称
			var photo = shell_win.system.HeadImgUrl ? shell_win.system.HeadImgUrl :
				Shell.util.Path.uiPath + "/img/icon/DefaultHeadImg.jpg";
			$("#account_img").attr("src",photo);
			$("#account_name").html(shell_win.system.UserName);
			$("#register_div").show();
			$("#cellphone_number_div").show();
			$("#password_div").show();
			$("#shell_form_button_div").show();
		},
		/**数据提交*/
		submit:function(pwd){
			var url = Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/Login?MobileCode=" +
				shell_win.system.MobileCode + "&Pwd=" + pwd;
				
			ShellComponent.mask.submit();
			Shell.util.Server.ajax({
				url: url
			}, function(data){
				ShellComponent.mask.hide();
				if (data.success) {
					shell_win.after_login_ok();
				} else {
					data.msg = data.msg ? data.msg : "密码错误";
					shell_win.info.error(data.msg);
				}
			});
		},
		/**登录成功后处理*/
		after_login_ok:function(){
			//alert("注册成功");
			window.location.href = "index.html";
		},
		/**确定按钮触摸事件*/
		submit_but_touch:function(){
			var cellphone_number = shell_win.system.MobileCode,
				pwd = shell_win.password.getValue();
	
			//校验需要提交的信息
			var isValid = shell_win.password.isValid(pwd);
	
			if (!isValid) return;
			
			//提交登录信息
			shell_win.submit(pwd);
		},
		/**初始化*/
		init:function(){
			//确定按钮处理
			$("#submit_btn").on(Shell.util.Event.touch,shell_win.submit_but_touch);
			//初始化系统信息
			shell_win.system.init();
		}
	};
	shell_win.init();
});