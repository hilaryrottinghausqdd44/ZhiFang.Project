layui.extend({
	uxutil: 'ux/util',
	uxdata: "ux/data",
	bloodsconfig: 'config/bloodsconfig',
	cachedata: '/views/modules/bloodtransfusion/cachedata'
}).use(['layer', 'uxutil', 'uxdata', 'bloodsconfig', 'cachedata'], function() {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var layer = layui.layer;
	var bloodsconfig = layui.bloodsconfig;
	var uxdata = layui.uxdata;
	var cachedata = layui.cachedata;

	/**默认传入参数*/
	var defaultParams = JSON.parse(JSON.stringify(bloodsconfig.HisParams));
	//医生站功能模块映射处理
	var moduleCodeStrategy = function() {
		//内部算法集合封装:type=2指BS血库模块为外部系统调用
		var strategy = {};
		/**
		 * 调用接口
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
	//初始化信息前先删除旧的缓存数据
	function clearData() {
		layui.data(bloodsconfig.cachekeys.ISHISCALL_KEY, null);
		layui.data(bloodsconfig.cachekeys.HISPARAMS_KEY, null);
		//layui.data(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY, null);
		layui.data(bloodsconfig.cachekeys.HISCALLPATINFO_KEY, null);
	};
	//初始化默认传入参数信息
	function initDefaultParams() {
		clearData();
		var info = "";
		//接收传入参数
		var params = uxutil.params.get();
		//功能模块编码
		if (params["moduleCode"]) defaultParams.ModuleCode = params["moduleCode"];
		//console.log(params["moduleCode"]);
		//是否获取6.6医生信息
		var isgetdoctor = false;
		if (params["isgetdoctor"]) isgetdoctor = params["isgetdoctor"];
		if (isgetdoctor == "1" || isgetdoctor == "true") {
			isgetdoctor = true;
		} else {
			isgetdoctor = false;
		}
		//帐号编码
		if (params["account"]) defaultParams.Account = params["account"];
		if (!defaultParams.Account) { //从cookie获取帐号信息
			defaultParams.Account = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME);
		}
		if (!defaultParams.Account) {
			info = info + "获取当前登录帐号信息为空!";
		}
		if (!defaultParams.ModuleCode || defaultParams.ModuleCode.length <= 0) info = info + "传入的功能模块编码为空!";
		if (info.length > 0) {
			$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
		} else {
			//数据库升级处理
			onDBUpdate(function() {
				//初始化运行参数
				if (JcallShell&&JcallShell.BLTF) {
					console.log(JcallShell.BLTF);
					JcallShell.BLTF.RunInfo.initAll();
				}else{
					console.log("未加载config/config_BLTF.js");
				}
				
				//初始化字典信息
				ininDictList(function(){
					
				});	
				//打开对应的业务模块
				locationOpen(isgetdoctor);
			});
		}
	};
	/**
	 * 初始化字典信息
	 */
	function ininDictList(callback){
		if(callback)callback();
	};
	/**
	 * 调用模块映射
	 */
	function locationOpen(isgetdoctor) {
		var href = moduleCodeStrategy(defaultParams.ModuleCode);
		if (href.length > 0) {
			if (isgetdoctor == true) {
				//6.6数据结构
				getSysDoctorInfo("", function(success) {
					if (success == true) {
						var params = getParams();
						href += (href.indexOf('?') == -1 ? '?' : '&') + "t=" + new Date().getTime() + "&" + params;
						location.href = uxutil.path.UI + href;
					} else {
						$("#info").removeClass("layui-hide").addClass("layui-show").html("获取帐号对应的医生信息失败!");
						return;
					}
				});
			} else {
				location.href = uxutil.path.UI + href;
			}
		} else {
			var info = "传入的参数(moduleCode)映射失败,获取不到访问模块信息!";
			$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
			return;
		}
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
			var layerIndex = layer.msg('加载处理中...', {
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
	/**
	 * 按登录帐号获取对应的医生信息
	 * @param {Object} account
	 * @param {Object} callback
	 */
	function getSysDoctorInfo(account, callback) {
		var hisDeptCode = "";
		if (!account) account = defaultParams.Account;
		if (!account) {
			var info = "传入的参数(account)为空,获取不到登录帐号信息!";
			$("#info").removeClass("layui-hide").addClass("layui-show").html(info);
			return;
		}
		//判断帐号对应的医生信息是否已存在
		var key = bloodsconfig.cachekeys.SYSDOCTORINFO_KEY;
		var curDoctor = bloodsconfig.getData(key);
		if (curDoctor && curDoctor["Account"] == account) {
			setSysDoctorInfo(curDoctor || {});
			setDefaultParams(curDoctor || {});
			if (callback) {
				return callback(true);
			} else {
				return callback(false);
			}
		}
		var url = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_SYS_GetSysCurDoctorInfoByAccount";
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
				setDefaultParams(data.value || {});
				if (callback) callback(success);
			} else {
				if (callback) callback(success);
			}
		});
	};
	//封装参数信息
	function getParams() {
		var arr = [];
		if (defaultParams.HisDeptId) arr.push("hisDeptId=" + defaultParams.HisDeptId);
		if (defaultParams.DeptId) arr.push("deptId=" + defaultParams.DeptId);
		if (defaultParams.HisDoctorId) arr.push("hisDoctorId=" + defaultParams.HisDoctorId);
		arr.push("doctorId=" + defaultParams.DoctorId);
		var newParams = encodeURI(arr.join("&")); //IE需要进行编码
		return newParams;
	};
	/**
	 * 按传入帐号获取到的医生信息,补全默认参数信息
	 * @param {Object} data
	 */
	function setDefaultParams(params) {
		//医生Id
		if (params["DoctorId"]) defaultParams.DoctorId = params["DoctorId"];
		//his医生Id
		if (params["HisDoctorId"]) defaultParams.HisDoctorId = params["HisDoctorId"];
		//科室Id
		if (params["DeptId"]) defaultParams.DeptId = params["DeptId"];
		//his科室Id
		if (params["HisDeptId"]) defaultParams.HisDeptId = params["HisDeptId"];
		//功能模块编码
		if (params["moduleCode"]) defaultParams.ModuleCode = params["moduleCode"];
	};
	/**
	 * 缓存获取到的医生信息
	 * @param {Object} data
	 */
	function setSysDoctorInfo(data) {
		var key = bloodsconfig.cachekeys.SYSDOCTORINFO_KEY;
		var settings = {
			"key": key,
			"value": data || null
		};
		bloodsconfig.setData(key, settings);
	};
	initDefaultParams();

});
