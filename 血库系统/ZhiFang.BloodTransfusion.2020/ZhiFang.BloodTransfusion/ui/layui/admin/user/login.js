layui.config({
	base: '../layuiadmin/' //静态资源所在路径
}).extend({
	index: 'lib/index', //主入口模块
	uxutil: '../../../ux/util',
	bloodsconfig: '../../../config/bloodsconfig',
	csserver: '../../../views/interface/csserver'
}).use(['index', 'user', 'uxutil', 'csserver', 'bloodsconfig'], function() {

	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form;
	var csserver = layui.csserver;
	var bloodsconfig = layui.bloodsconfig;

	//请求登入服务地址/ServerWCF
	var LOGIN_URL = uxutil.path.ROOT + bloodsconfig.Common.LOGINOFPUSER_URL;

	//用户名回车监听
	$("#LAY-user-login-username").keypress(function(e) {
		if (e.which == 13) {
			$("#Button_Login").click();
		}
	});
	//密码回车监听
	$("#LAY-user-login-password").keypress(function(e) {
		if (e.which == 13) {
			$("#Button_Login").click();
		}
	});

	form.render();
	//提交
	form.on('submit(LAY-user-login-submit)', function(obj) {
		onLogin();
	});

	function onLogin() {
		if (bloodsconfig.Common.ISHASCSLOGIN == true) {
			onCSLogin();
		} else {
			onBSLogin();
		}
	};

	function jsoncallback(data) {
		alert(data.status);
	};
	//调用CS登录服务
	function onCSLogin() {
		var account = $("#LAY-user-login-username").val();
		var password = $("#LAY-user-login-password").val();
		var url = csserver.CS_DOMAIN + csserver.CS_LONIN;
		if (csserver.CS_PORT.length > 0)
			url = url + ':' + csserver.CS_PORT;
		url = url + '?U=' + account + '&P=' + password + '&O=' + csserver.CS_LABID;
		//请求登入接口
		layer.load();
		csserver.ajax({
			url: url
			//data:params,
			//dataType:"POST"
		}, function(data) {
			layer.closeAll('loading');
			if (data.success === true) {
				if (result[csserver.resultParams.value]) {
					var value = result[csserver.resultParams.value];
					var useName = uxutil.cookie.get(uxutil.cookie.map.USERNAME);
					if (value && value.length > 0 && !useName) {
						//用户信息写入到coolie
						var userNo = value[0][csserver.CS_USER.USERID];
						var useCName = value[0][csserver.CS_USER.USERNAME]
						var obj1 = {
							"name": uxutil.cookie.map.USERID,
							"value": userNo
						};
						var obj2 = {
							"name": uxutil.cookie.map.USERNAME,
							"value": useCName
						};
						uxutil.cookie.set(obj1);
						uxutil.cookie.set(obj2);
					}

				}

				layui.data('user', {
					'account': account,
					'password': password
				});
				//手工调用数据库升级服务
				onDBUpdate(function(data) {
					layer.msg('登入成功', {
						icon: 1,
						time: 500
					}, function() {
						location.href = '../index.html?t=' + new Date().getTime(); //主页
					});
				});
			} else {
				layer.msg('账号或密码错误！');
			}
		});

	};
	//调用BS登录服务
	function onBSLogin() {
		var account = $("#LAY-user-login-username").val();
		var password = $("#LAY-user-login-password").val();
		var url = LOGIN_URL + '?strUserAccount=' + account + '&strPassWord=' + password;
		layer.load();
		//请求登入接口
		uxutil.server.ajax({
			url: url
		}, function(data) {
			layer.closeAll('loading');
			var success = data.success;
			if (success == undefined || success == null) success = data;
			//临时登录帐号
			if (success === true) {
				layui.data('user', {
					'account': account,
					'password': password
				});
				//记录用户信息
				onChangeUserData(account, password);
				if (account != "admin") {
					getSysDoctorInfo(account, function() {

					});
				}
				//手工调用数据库升级服务
				onDBUpdate(function(data) {
					layer.msg('登入成功', {
						icon: 1,
						time: 500
					}, function() {
						location.href = '../index.html?t=' + new Date().getTime(); //主页
					});
				});
			} else {
				layer.msg('账号或密码错误！');
			}
		});
	};
	//记录用户信息
	function onChangeUserData(account, password) {
		var userList = layui.data('userList'),
			firstUserKey = null,
			count = 0,
			MAX = 3;

		for (var i in userList) {
			if (userList[i].account == account) {
				delete userList[i];
				layui.data('userList', {
					"key": i,
					"remove": true
				})
			} else {
				if (count == 0) {
					firstUserKey = i;
				}
				count++;
			}
		}
		//最大数量已满，删除最前面的账号信息
		if (count >= MAX) {
			layui.data('userList', {
				"key": firstUserKey,
				"remove": true
			});
		}

		var value = {
				"account": account
			},
			remember = $("#remember").prop("checked");

		//记住密码
		if (remember) {
			value.password = password;
		}

		layui.data('userList', {
			"key": new Date().getTime(),
			"value": value
		});
		layui.data('remember', {
			"key": "remember",
			"value": remember
		});
	};
	//帐号登录成功后,按登录帐号的HisOrderCode获取对应的医生信息
	function getSysDoctorInfo(account, callback) {
		var hisDeptCode = "";
		var url = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_SYS_GetSysCurDoctorInfoByAccount";
		url = url + '?account=' + account;
		layer.load();
		//请求接口
		uxutil.server.ajax({
			url: url
		}, function(data) {
			layer.closeAll('loading');
			var success = data.success;
			if (success == undefined || success == null) success = data;
			if (success === true) {
				setSysDoctorInfo(data.value || {});
				if (callback) callback(success);
			} else {
				if (callback) callback(success);
			}
		});
	};
	//设置HIS调用时,依传入HIS医生ID,获取到的医生信息
	function setSysDoctorInfo(data) {
		var key = bloodsconfig.cachekeys.SYSDOCTORINFO_KEY;
		var settings = {
			"key": key,
			"value": data || null
		};
		bloodsconfig.setData(key, settings);
	};
	//手工调用数据库升级服务
	function onDBUpdate(callback) {
		if (!bloodsconfig.Common.LOGIN_AFTER_ISUPDATEDB) {
			callback();
		} else {
			layer.load();
			var url = uxutil.path.ROOT + bloodsconfig.Common.DBUPDATE_URL;
			uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if (data === true) {
					callback(data);
				}
			});
		}
	};
});
