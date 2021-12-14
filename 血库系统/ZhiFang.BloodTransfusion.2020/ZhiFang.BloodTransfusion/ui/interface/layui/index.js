layui.extend({
	uxutil: 'ux/util',
	uxdata: "ux/data",
	bloodsconfig: 'config/bloodsconfig',
	cachedata: '/views/modules/bloodtransfusion/cachedata'
}).use(['uxutil', 'layer', 'bloodsconfig', 'uxdata', 'cachedata'], function() {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var layer = layui.layer;
	var bloodsconfig = layui.bloodsconfig;
	var uxdata = layui.uxdata;
	var cachedata = layui.cachedata;

	/**默认传入参数*/
	var defaultParams = JSON.parse(JSON.stringify(bloodsconfig.HisParams));

	//功能模块映射处理
	var moduleCodeStrategy = function() {
		//内部算法集合封装:type=2指BS血库模块为外部系统调用
		var strategy = {
			apply: function() {
				var href = "/layui/views/bloodtransfusion/doctorstation/apply/index.html?type=2";
				return href;
			},
			applyandreview: function() { //医嘱申请+
				var href = "/layui/views/bloodtransfusion/doctorstation/applyandreview/index.html?type=2";
				return href;
			},
			senior: function() {
				var href = "/layui/views/bloodtransfusion/doctorstation/senior/index.html?type=2";
				return href;
			},
			director: function() {
				var href = "/layui/views/bloodtransfusion/doctorstation/director/index.html?type=2";
				return href;
			},
			medical: function() {
				var href = "/layui/views/bloodtransfusion/doctorstation/medical/index.html?type=2";
				return href;
			}
		};
		//调用接口
		return function(moduleCode) {
			return strategy[moduleCode] && strategy[moduleCode]();
		}
	}();
	//初始化信息前先删除旧的缓存数据
	function clearData() {
		layui.data(bloodsconfig.cachekeys.ISHISCALL_KEY, null);
		layui.data(bloodsconfig.cachekeys.HISPARAMS_KEY, null);
		layui.data(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY, null);
		layui.data(bloodsconfig.cachekeys.HISCALLPATINFO_KEY, null);
	};
	//初始化默认传入参数信息
	function initDefaultParams() {
		clearData();
		//接收传入参数
		var params = uxutil.params.get();
		var info = "";
		//His病区Id
		if (params["wardId"]) defaultParams.HisWardId = params["wardId"];
		//his科室Id
		if (params["deptId"]) defaultParams.HisDeptId = params["deptId"];
		//his医生Id
		if (params["doctorId"]) defaultParams.HisDoctorId = params["doctorId"];
		//His的就诊号
		if (params["admId"]) defaultParams.AdmId = params["admId"];
		//his患者Id
		if (params["patId"]) defaultParams.HisPatId = params["patId"];
		//患者病历号
		if (params["patNo"]) defaultParams.PatNo = params["patNo"];
		//患者姓名
		if (params["cname"]) defaultParams.CName = params["cname"];
		//功能模块编码
		if (params["moduleCode"]) defaultParams.ModuleCode = params["moduleCode"];
		if (!defaultParams.ModuleCode || defaultParams.ModuleCode.length <= 0) info = info + "传入的功能模块编码为空!";
		if (!defaultParams.HisDoctorId || defaultParams.HisDoctorId.length <= 0) info = info + "传入的医生编码参数为空!<br />";

		//医务处审批不需要患者信息
		if (defaultParams.ModuleCode && defaultParams.ModuleCode != "medical") {
			var nonemptyfield = bloodsconfig.HisInterface.NONEMPTYFIELD;
			if (nonemptyfield == "admId") {
				if (!defaultParams.AdmId || defaultParams.AdmId.length <= 0) info = info + "传入的患者就诊号值为空!<br />";
			} else if (nonemptyfield == "patNo") {
				if (!defaultParams.PatNo || defaultParams.PatNo.length <= 0) info = info + "传入的患者病历号值为空!<br />";
			}
		}

		if (info.length > 0) {
			$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
		} else {
			//按HIS传入的医生编码及科室编码获取医生信息及科室信息
			getSysDoctorInfo(function(success) {
				setIshisCall(true);
				locationOpen();
			});
		}
	};
	//HIS调用时,依传入HIS医生ID,获取到的医生信息
	function getSysDoctorInfo(callback) {
		var hisWardCode = defaultParams.HisWardId;
		var hisDeptCode = defaultParams.HisDeptId;
		var hisDoctorCode = defaultParams.HisDoctorId;
		var url = uxutil.path.ROOT + bloodsconfig.Common.HISCALL_URL;
		if(!hisWardCode)hisWardCode="";
		hisWardCode = encodeURI(hisWardCode); //IE需要进行编码
		hisDeptCode = encodeURI(hisDeptCode); //IE需要进行编码
		hisDoctorCode = encodeURI(hisDoctorCode); //IE需要进行编码
		//url = url + '?hisDoctorCode=' + hisDoctorCode + '&hisDeptCode=' + hisDeptCode;
		url = url + '?hisWardCode=' + hisWardCode + '&hisDeptCode=' + hisDeptCode + '&hisDoctorCode=' + hisDoctorCode +"&t=" + (new Date().getTime());
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
				var info = data.ErrorInfo || data.msg;
				$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
			}
		});
	};
	//是否HIS调用设置
	function setIshisCall(b) {
		var key = bloodsconfig.cachekeys.ISHISCALL_KEY;
		var settings = {
			"key": key,
			"value": b
		};
		bloodsconfig.setData(key, settings);
	};
	//设置HIS调用时,依传入HIS医生ID,获取到的医生信息
	function setSysDoctorInfo(data) {
		//当前的病区编码
		defaultParams.WardId = data.WardId;
		//当前的病区名称
		defaultParams.WardCName = data.WardCName;
		//当前的科室编码
		defaultParams.DeptId = data.DeptId;
		//当前的科室名称
		defaultParams.DeptCName = data.DeptCName;		
		//当前用户的用户编码
		defaultParams.UserId = data.UserId;
		//当前用户的用户名称
		defaultParams.UserCName = data.UserCName;
		//当前用户的医生编码
		defaultParams.DoctorId = data.DoctorId;
		//当前用户的医生名称
		defaultParams.DoctorCName = data.DoctorCName;
		
		var key = bloodsconfig.cachekeys.SYSDOCTORINFO_KEY;
		var settings = {
			"key": key,
			"value": data || null
		};
		bloodsconfig.setData(key, settings);
	};
	//设置HIS调用时,依传入HIS医生ID,获取到的医生信息
	function setHisParams(data) {
		var key = bloodsconfig.cachekeys.HISPARAMS_KEY;
		var settings = {
			"key": key,
			"value": data || null
		};
		bloodsconfig.setData(key, settings);
	};
	//封装参数信息
	function getParams() {
		var arr = [];
		//arr.push("hisWardId=" + defaultParams.HisWardId);
		//arr.push("wardId=" + defaultParams.WardId); //病区编码
		arr.push("hisDeptId=" + defaultParams.HisDeptId);
		arr.push("deptId=" + defaultParams.DeptId);
		arr.push("hisDoctorId=" + defaultParams.HisDoctorId);
		arr.push("doctorId=" + defaultParams.DoctorId);
		arr.push("admId=" + defaultParams.AdmId);
		arr.push("patId=" + defaultParams.HisPatId);
		arr.push("patNo=" + defaultParams.PatNo);
		arr.push("cname=" + defaultParams.CName);
		arr.push("moduleCode=" + defaultParams.ModuleCode);
		var newParams = encodeURI(arr.join("&")); //IE需要进行编码
		return newParams;
	};
	//调用模块映射
	function locationOpen() {
		var href = moduleCodeStrategy(defaultParams.ModuleCode);
		if (href.length > 0) {
			onLoginByHisDoctorCode(function() {
				var params = getParams();
				setHisParams(defaultParams);
				href = href + "&t=" + new Date().getTime() + "&" + params;
				location.href = uxutil.path.UI + href;
			});
		}
	};
	/**
	 * 按传入的HIS医生编码,调用BS登录服务
	 * @param {Object} callback
	 */
	function onLoginByHisDoctorCode(callback) {
		var hisDoctorCode = defaultParams.HisDoctorId;
		var url = uxutil.path.ROOT + bloodsconfig.Common.LOGINOFPUSERBYHISCODE_URL;
		url = url + '?hisDoctorCode=' + hisDoctorCode;
		var layerIndex = layer.msg('登录验证中...', {
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
			//登录帐号
			if (success === true) {
				//手工调用数据库升级服务
				onDBUpdate(function(data) {
					layer.msg('登入成功', {
						icon: 1,
						time: 500
					}, function() {
						if (callback) callback();
					});
				});
			} else {
				layer.msg('账号或密码错误！');
			}
		});
	};
	/**
	 * 按系统预置的默认帐号,调用BS登录服务
	 * @param {Object} callback
	 */
	function onLoginByDefault(callback) {
		//BS血库系统的默认登录帐号(供第三方调用BS血库功能模块时,以该帐号进行免登录处理)
		var account = bloodsconfig.Common.DEFAULT_ACCOUNT;
		var password = bloodsconfig.Common.DEFAULT_PWD;
		var url = uxutil.path.ROOT + bloodsconfig.Common.LOGINOFPUSER_URL;
		url = url + '?strUserAccount=' + account + '&strPassWord=' + password;
		var layerIndex = layer.msg('获取医生信息中...', {
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
				//手工调用数据库升级服务
				onDBUpdate(function(data) {
					layer.msg('登入成功', {
						icon: 1,
						time: 500
					}, function() {
						if (callback) callback();
					});
				});
			} else {
				layer.msg('账号或密码错误！');
			}
		});
	};
	/**
	 * 帐号登录成功后,手工调用数据库升级服务
	 * @param {Object} callback
	 */
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
	initDefaultParams();

});
