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
					url = "/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL",
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
		/**帐号信息*/
		strUserAccount: {
		    /**帐号是否已经被绑定*/
			is_used: null,
			/**最后一次手机号码*/
			last_value: "",
			/**提示信息*/
			info: {
				/**没有输入手机号的提示信息*/
				is_empty: "请输入帐号",
				/**手机号已被注册的提示信息*/
				is_used: "帐号已经被绑定，请换一个帐号",
				/**输入的手机号格式不正确*/
				is_unvalid: "输入的帐号格式不正确",
				/**手机号码正在判断是否已经被注册*/
				is_used_valid: "帐号码正在判断是否已经被绑定"
			},
		    /**获取帐号框的值*/
			getValue: function() {
			    var value = $("#useraccount").val();
				value = !value ? "" : $.trim(value);
				return value;
			}
		},
 	    /**密码信息*/
		strPassWord: {
		    /**获取帐号框的值*/
		    getValue: function () {
		        var value = $("#password").val();
		        value = !value ? "" : $.trim(value);
		        return value;
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
			/**提交注册信息*/
			register: function(params, callback) {
				Shell.util.Server.ajax({
				    url: Shell.util.Path.rootPath + "/WeiXinAppService.svc/ST_UDTO_AddBWeiXinEmpLinkByUserAccount?strUserAccount=" + params.strUserAccount + "&strPassWord=" + params.strPassWord
				}, callback);
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
			var url = Shell.util.Path.rootPath + "/WeiXinAppService.svc/Login?MobileCode=" +
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
		/**下一步处理成功后处理*/
		after_next_ok:function(){
			$("#page1").hide();
			$("#page2").show();
		},
		/**确定按钮触摸事件*/
		useraccount_openid_bind_btn_touch: function () {
		    var strUserAccount = shell_win.strUserAccount.getValue();
		    var strPassWord = shell_win.strPassWord.getValue();

			ShellComponent.mask.submit();
			//提交注册信息
			shell_win.server.register({
			    strUserAccount: strUserAccount,
			    strPassWord: strPassWord
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
			window.location.href ="index.html";
		},
		/**初始化*/
		init:function(){
			//注册确定按钮监听
		    $("#useraccount_openid_bind_btn").on(Shell.util.Event.touch, shell_win.useraccount_openid_bind_btn_touch);
			
			//初始化系统
			shell_win.system.init();
		}
	};
	shell_win.init();
});