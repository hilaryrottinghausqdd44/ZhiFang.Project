layui.extend({
	uxutil: 'ux/util',
	bloodsconfig: 'config/bloodsconfig',
	csserver: 'views/interface/csserver',
	cachedata: '/views/modules/bloodtransfusion/cachedata'
}).use(['form', 'uxutil', 'layer', 'csserver', 'bloodsconfig', 'cachedata'], function() {


	//'uxutil', 'csserver', 'bloodsconfig'
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form;
	var csserver = layui.csserver;
	var bloodsconfig = layui.bloodsconfig;
	var cachedata = layui.cachedata;
	var layer = layui.layer;

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
		var layerIndex = layer.msg('登录中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		//请求登入接口
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if (layerIndex != null) layer.close(layerIndex);
			var success = data.success;
			if (success == undefined || success == null) success = data;
			//临时登录帐号
			if (success === true) {
				layui.data('user', {
					'account': account,
					'password': password
				});

				getSysDoctorInfo(account, function() {

				});
				//手工调用数据库升级服务
				onDBUpdate(function(data) {
					layer.msg('登入成功', {
						icon: 1,
						time: 500
					}, function() {
						//location.href = '../index.html?t=' + new Date().getTime(); //主页
						openPage();
					});
				});
			} else {
				layer.msg('账号或密码错误！');
			}
		});
	};
	//帐号登录成功后,按登录帐号的HisOrderCode获取对应的医生信息
	function getSysDoctorInfo(account, callback) {
		var hisDeptCode = "";
		var url = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_SYS_GetSysCurDoctorInfoByAccount";
		url = url + '?account=' + account;
		var layerIndex = layer.msg('获取医生信息中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		//请求接口
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if (layerIndex != null) layer.close(layerIndex);
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
			return callback();
		}
		var isDBUpdate = cachedata.getCache("BT_SYS_DBUpdate");
		if (isDBUpdate == true) {
			return callback();
		} else {
			var layerIndex = layer.msg('数据库升级中...', {
				time: 0,
				icon: 16,
				shade: 0.5
			});
			var url = uxutil.path.ROOT + bloodsconfig.Common.DBUPDATE_URL;
			uxutil.server.ajax({
				url: url
			}, function(data) {
				if (layerIndex != null) layer.close(layerIndex);
				cachedata.setCache("BT_SYS_DBUpdate", true);
				callback(data);
			});
		}
	};

	//功能模块映射处理
	var moduleCodeStrategy = function() {
		//内部算法集合封装:type=2指BS血库模块为外部系统调用
		var strategy = {
			doctor: function(moduleCode) {
				return doctormoduleCodeList(moduleCode);
			},
			doctor2: function() {

			}
		};
		//调用接口
		return function(moduleType, moduleCode) {
			return strategy[moduleType] && strategy[moduleType](moduleCode);
		}
	}();

	//医生功能模块映射处理
	var doctormoduleCodeList = function() {
		//内部算法集合封装:type=2指BS血库模块为外部系统调用
		//医生用血审核验证
		var doctorVerification = function(GradeLimit, callback) {
			var info = [];
			var href = "";
			var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);
			if (!sysCurUserInfo) {
				info.push("获取当前操作的医生信息为空!");
			} else {
				//当前医生的用血量审核阀值范围
				var lowLimit = sysCurUserInfo.LowLimit;
				var upperLimit = sysCurUserInfo.UpperLimit;
				if (!lowLimit) lowLimit = 0;
				if (!upperLimit) upperLimit = 0;
				upperLimit = parseFloat(upperLimit);
				lowLimit = parseFloat(lowLimit);
				if (upperLimit < GradeLimit.LowLimit) {
					info.push('当前医生审核的用血申请量范围为:' + lowLimit + '~' + upperLimit + ',不能进行医务处审批操作!');
				}
			};
			if (info.length > 0) {
				href = "/layui/views/bloodtransfusion/sysprompt/index.html?type=2";
				href = href + "&t=" + new Date().getTime() + "&info=" + info.join("<br />");
				href = uxutil.path.UI + href;
			}
			if (callback) callback(href);
		};

		var strategy = {
			apply: function() {
				var href = uxutil.path.UI + "/layui/views/bloodtransfusion/doctorstation/apply/index.html?type=2";
				return href;
			},
			applyandreview: function() { //医嘱申请+
				var href = uxutil.path.UI + "/layui/views/bloodtransfusion/doctorstation/applyandreview/index.html?type=2";
				return href;
			},
			senior: function() {
				//上级审核等级用血量阀值范围
				var seniorGrade = {
					LowLimit: 0,
					UpperLimit: 800
				};
				var isNeedV = false;
				var href = uxutil.path.UI + "/layui/views/bloodtransfusion/doctorstation/senior/index.html?type=2";
				doctorVerification(seniorGrade, function(Errhref) {
					if (Errhref || Errhref != "") return href = Errhref;
					href = href + "&isNeedV=" + isNeedV + "&t=" + new Date().getTime();
				});
				return href;
			},
			director: function() {
				//主任医生审核等级用血量阀值范围
				var directorGrade = {
					LowLimit: 801,
					UpperLimit: 1600
				};
				var isNeedV = false;
				var href = uxutil.path.UI + "/layui/views/bloodtransfusion/doctorstation/director/index.html?type=2";
				doctorVerification(directorGrade, function(Errhref) {
					if (Errhref || Errhref != "") return href = Errhref;
					href = href + "&isNeedV=" + isNeedV + "&t=" + new Date().getTime();
				});
				return href;
			},
			medical: function() {
				//医务科审核等级用血量阀值范围
				var docGrade = {
					LowLimit: 1601,
					UpperLimit: 1000000000
				};
				var isNeedV = false;
				var href = uxutil.path.UI + "/layui/views/bloodtransfusion/doctorstation/medical/index.html?type=2";
				doctorVerification(docGrade, function(Errhref) {
					if (Errhref || Errhref != "") return href = Errhref;
					href = href + "&isNeedV=" + isNeedV + "&t=" + new Date().getTime();
				});
				return href;
			}
		};
		//调用接口 
		return function(moduleCode) {
			return strategy[moduleCode] && strategy[moduleCode]();
		}
	}();

	function openPage() {
		//接收传入参数
		var params = uxutil.params.get();
		var moduleType = params["moduleType"];
		var moduleCode = params["moduleCode"];
		var href = moduleCodeStrategy(moduleType, moduleCode);

		if (href.length > 0) {
			location.href = href;
		} else {
			href = "/layui/views/bloodtransfusion/sysprompt/index.html?type=2";
			href = href + "&t=" + new Date().getTime() + "&info=传入的参数:模块分类(moduleType)及模块代码(moduleCode)错误!";
			href = uxutil.path.UI + href;
		}

	};
});
