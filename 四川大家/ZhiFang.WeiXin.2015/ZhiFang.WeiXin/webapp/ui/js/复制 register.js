$(function() {
	/**手机号码信息*/
	var cellphone = {
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
	};
	/**验证码信息*/
	var captcha = {
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
				$("#reload_captcha_div").show();
				$("#captcha_info").hide();
			} else {
				$("#reload_captcha_div").hide();
				html = '如果没有收到验证码,&nbsp;' + last_times + '&nbsp;秒之后重新获取';
				$("#captcha_info").html(html);

				$("#captcha_info").show();
			}
		},
		/**重新获取验证码*/
		reload: function() {
			var cellphone_number = cellphone.getValue(),
				captcha_value = captcha.getValue();

			//校验需要提交的信息
			var isValid = valided.next_params(cellphone_number, captcha_value);

			if (!isValid) return;

			//清空错误信息
			info.clear();

			//服务器校验手机号是否已被注册,没注册就发送验证码
			server.cellphone_number_is_used({
				cellphone_number: cellphone_number
			}, function(data) {
				if (data.success) {
					captcha.server_value = data.value.vaildcode;
					var max_times = data.value.TimeOut;
					time_meter.start(function() {
						if (max_times-- == 0) {
							time_meter.end();
						} else {
							captcha.show_captcha_info(max_times);
						}
					});
				} else {
					info.error(data.msg);
				}
			});
		}
	};
	/**信息处理*/
	var info = {
		/**显示正确信息*/
		show: function(value) {
			$("#msg").css("color", "green");
			$("#msg").html(value);
		},
		/**显示错误信息*/
		error: function(value) {
			$("#msg").css("color", "red");
			$("#msg").html(value);
		},
		/**清空信息*/
		clear: function() {
			$("#msg").html("");
		}
	};
	/**服务端处理*/
	var server = {
		/**服务器校验手机号是否已经被注册，没注册就发送验证码*/
		cellphone_number_is_used: function(params, callback) {
			Shell.util.Server.ajax({
				url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/VaildMobileCode?MobileCode=" + params.cellphone_number
			}, callback);
		},
		/**提交注册信息*/
		register: function(params, callback) {
			Shell.util.Server.ajax({
				url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinAccountMobileCodeByOpenid?MobileCode=" + params.cellphone_number
			}, callback);
		}
	};
	/**数据校验*/
	var valided = {
		/**手机号校验*/
		cellphone: function(cellphone_number) {
			if (cellphone.is_used === true) {
				info.error(cellphone.info.is_used);
				return;
			}

			if (!cellphone_number) {
				info.error(cellphone.info.is_empty);
				return;
			}

			var is_cellphone_num = Shell.util.Isvalid.isCellPhoneNo(cellphone_number);
			if (!is_cellphone_num) {
				info.error(cellphone.info.is_unvalid);
				return;
			}

			if (cellphone_number === null) {
				info.error(cellphone.info.is_used_valid);
				return;
			}

			return true;
		},
		/**校验码校验*/
		captcha: function(captcha_value) {
			if (captcha_value.length != 6) {
				info.error(captcha.info.is_error_length);
				return;
			}

			if (captcha_value != captcha.server_value) {
				info.error(captcha.info.is_error_not_same);
				return;
			}

			return true;
		},
		/**下一步提交数据的校验*/
		next_params: function(cellphone_number, captcha_value) {
			//判断手机号码是否通过校验
			var isValid = valided.cellphone(cellphone_number);
			if (!isValid) return

			return true;
		},
		/**注册提交数据的校验*/
		register_params: function(cellphone_number, captcha_value, password1, password2) {
			//判断手机号码是否通过校验
			var isValid = valided.cellphone(cellphone_number);
			if (!isValid) return;

			//判断校验码是否通过校验
			isValid = valided.captcha(captcha_value);
			if (!isValid) return;

			return true;
		}
	};
	/**计时器*/
	var time_meter = {
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
	};

	//下一步按钮处理
	$("#next_btn").on(Shell.util.Event.touch, function() {
		var cellphone_number = cellphone.getValue(),
			captcha_value = captcha.getValue();

		//校验需要提交的信息
		var isValid = valided.next_params(cellphone_number, captcha_value);

		if (!isValid) return;

		//清空错误信息
		info.clear();

		//服务器校验手机号是否已被注册,没注册就发送验证码
		server.cellphone_number_is_used({
			cellphone_number: cellphone_number
		}, function(data) {
			if (data.success) {
				captcha.server_value = data.value.vaildcode;
				var max_times = data.value.TimeOut;
				time_meter.start(function() {
					if (max_times-- == 0) {
						time_meter.end();
					} else {
						captcha.show_captcha_info(max_times);
					}
				});
				after_next_ok();
			} else {
				info.error(data.msg);
			}
		});
	});

	//下一步处理成功后处理
	function after_next_ok() {
		$("#page1").hide();
		$("#page2").show();
	}

	//注册确定按钮处理
	$("#submit_btn").on(Shell.util.Event.touch, function() {
		var cellphone_number = cellphone.getValue(),
			captcha_value = captcha.getValue();

		//校验需要提交的信息
		var isValid = valided.register_params(cellphone_number, captcha_value);

		if (!isValid) return;

		//清空错误信息
		info.clear();
		
		//提交注册信息
		server.register({
			cellphone_number: cellphone_number
		}, function(data) {
			if (data.success) {
				after_register_ok();
			} else {
				info.error(data.msg);
			}
		});
	});

	//注册成功后处理
	function after_register_ok() {
		time_meter.end(); //结束计时器
		window.location.href = "index.html";
	}

	$("#reload_captcha_btn").on(Shell.util.Event.touch, function() {
		captcha.reload();
	});
});