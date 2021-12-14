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
		/**手机号码信息*/
		cellphone:{
			/**手机号码是否已经被注册*/
			is_used: null,
			/**最后一次手机号码*/
			last_value: "",
			/**提示信息*/
			info: {
				/**没有输入手机号的提示信息*/
				is_empty: "请输入手机号码",
				/**手机号已被注册的提示信息*/
				is_used: "手机号已经被注册，请换一个手机号",
				/**输入的手机号格式不正确*/
				is_unvalid: "输入的手机号格式不正确",
				/**手机号码正在判断是否已经被注册*/
				is_used_valid: "手机号码正在判断是否已经被注册"
			},
			/**获取手机号框的值*/
			getValue: function() {
				var value = $("#cellphone_number").val();
				value = !value ? "" : $.trim(value);
				return value;
			}
		},
		/**验证码信息*/
		captcha:{
			server_value: null,
			/**提示信息*/
			info: {
				is_error_length: "验证码必须是6位",
				is_error_not_same: "验证码错误"
			},
			/**获取验证码框的值*/
			getValue: function() {
				var value = $("#captcha").val();
				value = !value ? "" : $.trim(value);
				return value;
			},
			/**显示验证码信息*/
			show_captcha_info: function(last_times) {
				var html = "";
				if (last_times == 0) {
					$("#reload_captcha_btn").attr("disabled", false);
					$("#reload_captcha_div").show();
					$("#reload_captcha_btn").html("重新获取验证码");
				} else {
					$("#reload_captcha_btn").attr("disabled", true);
					$("#reload_captcha_div").show();
					$("#reload_captcha_btn").html(last_times +"秒后重新获取验证码");
				}
			},
			/**重新获取验证码*/
			reload: function() {
				var cellphone_number = shell_win.cellphone.getValue(),
					captcha_value = shell_win.captcha.getValue();
	
				//校验需要提交的信息
				var isValid = shell_win.valided.next_params(cellphone_number, captcha_value);
	
				if (!isValid) return;
	
				ShellComponent.mask.to_server("验证验获取中...");
				//服务器校验手机号是否已被注册,没注册就发送验证码
				shell_win.server.cellphone_number_is_used({
					cellphone_number: cellphone_number
				}, function(data) {
					ShellComponent.mask.hide();
					if (data.success) {
						shell_win.captcha.server_value = data.value.vaildcode;
						var max_times = data.value.TimeOut;
						shell_win.time_meter.start(function() {
							if (max_times-- == 0) {
								shell_win.time_meter.end();
							} else {
								shell_win.captcha.show_captcha_info(max_times);
							}
						});
					} else {
						shell_win.info.error(data.msg);
					}
				});
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
			$("#page1").show();
		},
		/**服务端处理*/
		server:{
			/**服务器校验手机号是否已经被注册，没注册就发送验证码*/
			cellphone_number_is_used: function(params, callback) {
				Shell.util.Server.ajax({
					showError:true,
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/VaildMobileCode?MobileCode=" + params.cellphone_number
				}, callback);
			},
			/**提交注册信息*/
			register: function(params, callback) {
				Shell.util.Server.ajax({
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinAccountMobileCodeByOpenid?MobileCode=" + params.cellphone_number
				}, callback);
			}		
		},
		/**数据校验*/
		valided:{
			/**手机号校验*/
			cellphone: function(cellphone_number) {
				if (shell_win.cellphone.is_used === true) {
					shell_win.info.error(shell_win.cellphone.info.is_used);
					return;
				}
	
				if (!cellphone_number) {
					shell_win.info.error(shell_win.cellphone.info.is_empty);
					return;
				}
	
				var is_cellphone_num = Shell.util.Isvalid.isCellPhoneNo(cellphone_number);
				if (!is_cellphone_num) {
					shell_win.info.error(shell_win.cellphone.info.is_unvalid);
					return;
				}
	
				if (cellphone_number === null) {
					shell_win.info.error(shell_win.cellphone.info.is_used_valid);
					return;
				}
	
				return true;
			},
			/**校验码校验*/
			captcha: function(captcha_value) {
				if (captcha_value.length != 6) {
					shell_win.info.error(shell_win.captcha.info.is_error_length);
					return;
				}
	
				if (captcha_value != shell_win.captcha.server_value) {
					shell_win.info.error(shell_win.captcha.info.is_error_not_same);
					return;
				}
	
				return true;
			},
			/**下一步提交数据的校验*/
			next_params: function(cellphone_number, captcha_value) {
				//判断手机号码是否通过校验
				var isValid = shell_win.valided.cellphone(cellphone_number);
				if (!isValid) return
	
				return true;
			},
			/**注册提交数据的校验*/
			register_params: function(cellphone_number, captcha_value, password1, password2) {
				//判断手机号码是否通过校验
				var isValid = shell_win.valided.cellphone(cellphone_number);
				if (!isValid) return;
	
				//判断校验码是否通过校验
				isValid = shell_win.valided.captcha(captcha_value);
				if (!isValid) return;
	
				return true;
			}
		},
		/**计时器*/
		time_meter:{
			/**周期*/
			cycle: 1000,
			/**执行对象*/
			timer: null,
			/**启动*/
			start: function(fun) {
				this.timer = setInterval(fun, this.cycle);
			},
			/**终止*/
			end: function() {
				clearInterval(this.timer);
			}
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
		/**下一步按钮触摸事件*/
		next_but_touch:function(){
			var cellphone_number = shell_win.cellphone.getValue(),
				captcha_value = shell_win.captcha.getValue();
	
			//校验需要提交的信息
			var isValid = shell_win.valided.next_params(cellphone_number, captcha_value);
	
			if (!isValid) return;
	
			ShellComponent.mask.submit();
			//服务器校验手机号是否已被注册,没注册就发送验证码
			shell_win.server.cellphone_number_is_used({
				cellphone_number: cellphone_number
			}, function(data) {
				ShellComponent.mask.hide();
				if (data.success) {
					shell_win.captcha.server_value = data.value.vaildcode;
					var max_times = data.value.TimeOut;
					shell_win.time_meter.start(function() {
						if (max_times-- == 0) {
							shell_win.time_meter.end();
						} else {
							shell_win.captcha.show_captcha_info(max_times);
						}
					});
					shell_win.after_next_ok();
				} else {
					shell_win.info.error(data.msg);
				}
			});
		},
		/**下一步处理成功后处理*/
		after_next_ok:function(){
			$("#page1").hide();
			$("#page2").show();
		},
		/**确定按钮触摸事件*/
		submit_but_touch:function(){
			var cellphone_number = shell_win.cellphone.getValue(),
				captcha_value = shell_win.captcha.getValue();
	
			//校验需要提交的信息
			var isValid = shell_win.valided.register_params(cellphone_number, captcha_value);
	
			if (!isValid) return;
	
			ShellComponent.mask.submit();
			//提交注册信息
			shell_win.server.register({
				cellphone_number: cellphone_number
			}, function(data) {
				ShellComponent.mask.hide();
				if (data.success) {
					shell_win.after_register_ok();
				} else {
					shell_win.info.error(data.msg || "注册失败！");
				}
			});
		},
		/**注册成功后处理*/
		after_register_ok:function(){
			shell_win.time_meter.end(); //结束计时器
			window.location.href = Shell.util.Path.uiPath + "/index.html";
		},
		/**初始化*/
		init:function(){
			//下一步按钮监听
			$("#next_btn").on(Shell.util.Event.touch,shell_win.next_but_touch);
			//注册确定按钮监听
			$("#submit_btn").on(Shell.util.Event.touch,shell_win.submit_but_touch);
			//验证码按钮监听
			$("#reload_captcha_btn").on(Shell.util.Event.touch, function() {
				captcha.reload();
			});
			//初始化系统
			shell_win.system.init();
		}
	};
	shell_win.init();
});