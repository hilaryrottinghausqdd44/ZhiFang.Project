layui.extend({
	uxutil: 'ux/util',
	uxdata: "ux/data",
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	webassistconfig: 'config/webassistconfig',
	runParams: '/config/runParams'
}).use(['uxutil', 'layer', 'uxdata', 'cachedata', 'runParams', 'webassistconfig'], function() {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var layer = layui.layer;
	var webassistconfig = layui.webassistconfig;
	var uxdata = layui.uxdata;
	var cachedata = layui.cachedata;
	var runParams = layui.runParams;

	/**默认传入参数*/
	var defaultParams = JSON.parse(JSON.stringify(webassistconfig.HisParams));
	/**
	 * @description 医生站功能模块映射处理
	 */
	var doctorStationStrategy = function() {
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
		/**
		 * @description 调用接口
		 * @param {Object} moduleCode
		 */
		return function(moduleCode) {
			var url = "";
			if (strategy[moduleCode]) { //传入为模块枚举
				url = strategy[moduleCode]();
			} else { //直接传入模块访问路径
				url = moduleCode;
			}
			return url;
		}
	}();
	/**
	 * @description 护士站功能模块映射处理
	 */
	var nurseStationStrategy = function() {
		var strategy = {
			bloodprohandover: function() { //血袋接收
				var href = "/extjs/interface/one/index.html?className=Shell.class.assist.nursestation.bloodprohandover.App";
				return href;
			},
			transrecord: function() { //输血过程记录
				var href = "/extjs/interface/one/index.html?className=Shell.class.assist.nursestation.transrecord.App";
				return href;
			},
			bloodbagretrieve: function() { //血袋回收
				var href = "/layui/views/bloodtransfusion/nursestation/bloodbagretrieve/index.html";
				return href;
			}
		};
		//调用接口
		return function(moduleCode) {
			return strategy[moduleCode] && strategy[moduleCode]();
		}
	}();
	/**
	 * @description 统计和报表功能模块映射处理
	 */
	var statisticsStrategy = function() {
		var strategy = {
			reqntegrated: function() { //输血综合查询
				var href = "/extjs/interface/one/index.html?className=Shell.class.assist.statistics.reqntegrated.App";
				return href;
			}
		};
		//调用接口
		return function(moduleCode) {
			return strategy[moduleCode] && strategy[moduleCode]();
		}
	}();
	/**
	 * @description 初始化信息前先删除旧的缓存数据
	 */
	function clearData() {
		layui.data(webassistconfig.cachekeys.ISHISCALL_KEY, null);
		layui.data(webassistconfig.cachekeys.HISPARAMS_KEY, null);
		layui.data(webassistconfig.cachekeys.SYSDOCTORINFO_KEY, null);
		layui.data(webassistconfig.cachekeys.HISCALLPATINFO_KEY, null);
	};
	/**
	 * @description 初始化默认传入参数信息
	 */
	function initSysInfo() {
		clearData();
		//接收传入参数
		var params = uxutil.params.get();
		var info = "";
		//子系统编码
		if (params["sysCode"]) defaultParams.SysCode = params["sysCode"];
		//His病区Id
		if (params["wardId"]) defaultParams.HisWardId = params["wardId"];
		//his科室Id
		if (params["deptId"]) defaultParams.HisDeptId = params["deptId"];
		//his医生Id/his护士Id
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
		if (defaultParams.SysCode == "doctorStation" && defaultParams.ModuleCode && defaultParams.ModuleCode != "medical") {
			var nonemptyfield = webassistconfig.HisInterface.NONEMPTYFIELD;
			if (nonemptyfield == "admId") {
				if (!defaultParams.AdmId || defaultParams.AdmId.length <= 0) info = info + "传入的患者就诊号值为空!<br />";
			} else if (nonemptyfield == "patNo") {
				if (!defaultParams.PatNo || defaultParams.PatNo.length <= 0) info = info + "传入的患者病历号值为空!<br />";
			}
		}

		if (info.length > 0) {
			$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
		} else {
			//获取人员信息
			getUserInfo(function() {
				setIshisCall(true);
				if (JcallShell && JcallShell.BLTF) {
					JcallShell.BLTF.RunInfo.initAll(function() {
						locationOpen();
					});
				} else {
					//					runParams.initRunParams(function() {
					//						locationOpen();
					//					}); 
					locationOpen();
				}
			});
		}
	};
	/**
	 * @description 调用模块映射
	 */
	function locationOpen() {
		var href = "";
		switch (defaultParams.SysCode) {
			case "doctorStation":
				href = doctorStationStrategy(defaultParams.ModuleCode);
				break;
			case "nurseStation":
				href = nurseStationStrategy(defaultParams.ModuleCode);
				break;
			case "statistics":
				href = statisticsStrategy(defaultParams.ModuleCode);
				break;
			default:
				href = doctorStationStrategy(defaultParams.ModuleCode);
				break;
		}
		if (href.length > 0) {
			var params = getParams();
			setHisParams(defaultParams);
			href += ((href.indexOf('?') == -1 ? '?' : '&') + "t=" + new Date().getTime() + "&" + params);
			location.href = uxutil.path.UI + href;
		}
	};
	/**
	 * @description 获取人员信息
	 * @param {Object} callback
	 */
	function getUserInfo(callback) {
		switch (defaultParams.SysCode) {
			case "doctorStation":
				onLoginByHisCode(function() {
					if (callback) callback();
				});
				break;
			case "nurseStation":
				onLoginByHisCode(function() {
					//判断HIS工号及科室对照是否成功					
					if (callback) callback();
				});
				break;
			case "statistics":
				onLoginByHisCode(function() {
					if (callback) callback();
				});
				break;
			default:
				onLoginByHisCode(function() {
					if (callback) callback();
				});
				break;
		}
	};
	/**
	 * @description 按传入的HIS编码,调用BS登录服务
	 * @param {Object} callback
	 */
	function onLoginByHisCode(callback) {
		var hisWardCode = defaultParams.HisWardId;
		var hisDeptCode = defaultParams.HisDeptId;
		var hisDoctorCode = defaultParams.HisDoctorId;
		var url = url = uxutil.path.ROOT + webassistconfig.Common.LOGINOFPUSERBYHISCODE_URL;
		hisWardCode = encodeURI(hisWardCode); //IE需要进行编码
		hisDeptCode = encodeURI(hisDeptCode); //IE需要进行编码
		hisDoctorCode = encodeURI(hisDoctorCode); //IE需要进行编码
		url = url + '?hisWardCode=' + hisWardCode + '&hisDeptCode=' + hisDeptCode + '&hisDoctorCode=' + hisDoctorCode +
			"&t=" + (new Date().getTime());
		var layerIndex = layer.msg('人员信息验证中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if (layerIndex != null) layer.close(layerIndex);
			var success = data.success;
			if (success == undefined || success == null) success = data;
			if (success === true) {
				setCurUserInfo(data.value || {});
				//手工调用数据库升级服务
				onDBUpdate(function(data) {
					layer.msg('人员信息验证成功', {
						icon: 1,
						time: 500
					}, function() {
						if (callback) callback();
					});
				});
			} else {
				layer.msg('人员信息验证账号或密码错误！');
				var info = data.ErrorInfo || data.msg;
				$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
			}
		});
	};
	/**
	 * @description 帐号登录成功后,手工调用数据库升级服务
	 * @param {Object} callback
	 */
	function onDBUpdate(callback) {
		if (!webassistconfig.Common.LOGIN_AFTER_ISUPDATEDB) {
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
			var url = uxutil.path.ROOT + webassistconfig.Common.DBUPDATE_URL;
			uxutil.server.ajax({
				url: url
			}, function(data) {
				if (layerIndex != null) layer.close(layerIndex);
				cachedata.setCache("BT_SYS_DBUpdate", true);
				callback(data);
			});
		}
	};
	/**
	 * @description 封装模块传入参数信息
	 */
	function getParams() {
		var arr = [];
		arr.push("hisWardId=" + defaultParams.HisWardId);
		arr.push("wardId=" + defaultParams.WardId); //病区编码
		arr.push("hisDeptId=" + defaultParams.HisDeptId);
		arr.push("deptId=" + defaultParams.DeptId); //科室编码

		arr.push("userId=" + defaultParams.UserId); //当前用户编码
		arr.push("hisDoctorId=" + defaultParams.HisDoctorId);
		arr.push("doctorId=" + defaultParams.DoctorId); //医生编码
		arr.push("gradeId=" + defaultParams.GradeId); //医生等级编码
		arr.push("lowLimit=" + defaultParams.LowLimit); //医生等级审核范围
		arr.push("upperLimit=" + defaultParams.UpperLimit); //医生等级审核范围

		arr.push("admId=" + defaultParams.AdmId); //就诊号
		arr.push("patId=" + defaultParams.HisPatId); //病历号
		arr.push("patNo=" + defaultParams.PatNo);
		arr.push("cname=" + defaultParams.CName);
		arr.push("moduleCode=" + defaultParams.ModuleCode); //模块编码
		var newParams = encodeURI(arr.join("&")); //IE需要进行编码
		return newParams;
	};
	/**
	 * @description 将获取到的用户信息设置到缓存中
	 * @param {Object} data
	 */
	function setCurUserInfo(data) {
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

		//当前用户的医生等级
		defaultParams.GradeId = data.GradeId;
		defaultParams.GradeName = data.GradeName;

		//当前用户的医生审核范围
		defaultParams.LowLimit = data.LowLimit;
		//当前用户的医生审核范围
		defaultParams.UpperLimit = data.UpperLimit;
		var key = webassistconfig.cachekeys.SYSDOCTORINFO_KEY;
		var settings = {
			"key": key,
			"value": data || null
		};
		webassistconfig.setData(key, settings);
		//建议采用这种方式缓存
		cachedata.setCache(key, data);
	};
	/**
	 * @description 是否HIS调用设置
	 * @param {Object} b
	 */
	function setIshisCall(b) {
		var key = webassistconfig.cachekeys.ISHISCALL_KEY;
		var settings = {
			"key": key,
			"value": b
		};
		webassistconfig.setData(key, settings);
		//建议采用这种方式缓存
		cachedata.setCache(key, b);
	};
	/**
	 * @description HIS调用时,缓存依传入HIS参数信息获取到用户信息
	 * @param {Object} data
	 */
	function setHisParams(data) {
		var key = webassistconfig.cachekeys.HISPARAMS_KEY;
		var settings = {
			"key": key,
			"value": data || null
		};
		webassistconfig.setData(key, settings);
		//建议采用这种方式缓存
		cachedata.setCache(key, data);
	};
	
	initSysInfo();

});
