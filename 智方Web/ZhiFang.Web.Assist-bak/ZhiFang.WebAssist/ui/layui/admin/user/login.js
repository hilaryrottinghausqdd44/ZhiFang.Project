layui.config({
	base: '../layuiadmin/' //静态资源所在路径
}).extend({
	index: 'lib/index', //主入口模块
	uxutil: '../../../ux/util',
	webassistconfig: '../../../config/webassistconfig',
	csserver: '../../../views/interface/csserver',
	cachedata: '../../../views/modules/common/cachedata'
}).use(['index', 'user', 'uxutil', 'csserver', 'webassistconfig', 'cachedata'], function() {

	var $ = layui.$,
		uxutil = layui.uxutil,
		layer = layui.layer,
		form = layui.form;
	var csserver = layui.csserver;
	var webassistconfig = layui.webassistconfig;
	var cachedata = layui.cachedata;
	
	//请求登入服务地址/ServerWCF
	var LOGIN_URL = uxutil.path.ROOT + webassistconfig.Common.LOGINOFPUSER_URL;

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
	$('#Button_Reg').on('click', function() {
		
		var url = uxutil.path.EXTJS+'/interface/one/index.html?className=Shell.class.sysbase.puser.reg.App';
		layer.open({
			title: '账号注册',
			type: 2,
			content: url,
			area: ['320px', '420px'],
			yes: function(index, layero) {
				layer.close(index);
			},
			cancel: function(index, layero) {
				layer.close(index);
			}
		});
	});
	$('#Button_LisSyncHis').on('click', function() {
		var url=uxutil.path.ROOT +"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveLisSyncHisDataInfo";
		var layerIndex = parent.layer.msg('同步账号处理中,请耐心等待...', {
			time: 0,
			icon: 16,
			shade: 0.6
		});
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if (layerIndex != null) layer.close(layerIndex);
			layer.msg('同步账号完成', {
				icon: 1,
				time: 3000
			});
		});
	});
	
	form.render();
	//提交
	form.on('submit(LAY-user-login-submit)', function(obj) {
		onLogin();
	});

	function onLogin() {
		onBSLogin();
	};
	function jsoncallback(data) {
		alert(data.status);
	};
	//调用BS登录服务
	function onBSLogin() {
		var account = $("#LAY-user-login-username").val();
		var password = $("#LAY-user-login-password").val();
		var url = LOGIN_URL + '?strUserAccount=' + account + '&strPassWord=' + password+"&t="+ new Date().getTime();
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
				layer.msg('登入成功', {
					icon: 1,
					time: 500
				}, function() {
					location.href = '../index.html?t=' + new Date().getTime(); //主页
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
	//手工调用数据库升级服务
	function onDBUpdate(callback) {
		var isDBUpdate = cachedata.getCache("BT_SYS_DBUpdate");
		//console.log(isDBUpdate);
		if(isDBUpdate == true) {
			return callback(true);
		}
		
		if (!webassistconfig.Common.LOGIN_AFTER_ISUPDATEDB) {
			return callback(true);
		} else {
			layer.load();
			var url = uxutil.path.ROOT + webassistconfig.Common.DBUPDATE_URL;
			//console.log(url);
			uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if (data === true) {
					cachedata.setCache("BT_SYS_DBUpdate", true);
					callback(data);
				}
			});
		}
	};
	
	//手工调用数据库升级服务
	onDBUpdate(function(data) {
		//console.log(data);
	});
});
