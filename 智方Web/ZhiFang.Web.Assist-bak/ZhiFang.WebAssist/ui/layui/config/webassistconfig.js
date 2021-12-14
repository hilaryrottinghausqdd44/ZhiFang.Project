/**
	@name：layui.webassistconfig BS血库系统配置信息 
	@author：longfc
	@version 2019-07-4
 */
layui.extend({
	uxutil: 'ux/util'
	//cachedata: '/views/modules/bloodtransfusion/cachedata'
}).define(['jquery', 'uxutil', 'cachedata'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var cachedata = layui.cachedata;

	/**
	 * @description CS血库服务端的域名或服务访问的IP:192.168.0.73;
	 * @description 2020-09-03调整为优先取系统运行参数“CS服务访问URL”的配置项;
	 * @description 如果系统运行参数“CS服务访问URL”没配置，才取这里的配置;
	 */
	var csbaseurl = "http://192.168.0.73";

	var webassistconfig = {
		//自定义提示信息
		AlertInfo: {
			//医生站提示信息
			Doctor: {
				//用血申请确认后自动完成审批的提示信息
				ConfirmedInfo: "请到钉钉完成输血单相关审核"
			},
			//护士站提示信息
			Nurse: {

			},
			//输血科提示信息
			BloodTrans: {

			}
		},
		//公共配置
		Common: {
			
			//是否调用CS服务登录及验证用户信息
			ISHASCSLOGIN: false,
			//登录成功后,是否调用数据库升级服务
			LOGIN_AFTER_ISUPDATEDB: true,

			//第三方外部调用时的默认登录帐号
			DEFAULT_ACCOUNT: "19999",
			//第三方外部调用时的默认登录密码
			DEFAULT_PWD: "19999",
			//HIS调用时,依传入HIS医生ID,获取到的医生信息
			HISCALL_URL: "/ServerWCF/WebAssistManageService.svc/BT_SYS_GetSysCurDoctorInfoByHisCode",
			//按PUser的帐号及密码登录
			LOGINOFPUSER_URL: "/ServerWCF/WebAssistManageService.svc/BT_SYS_LoginOfPUser",
			//按HIS医生对照码获取PUser的帐号及密码登录
			LOGINOFPUSERBYHISCODE_URL: "/ServerWCF/WebAssistManageService.svc/BT_SYS_LoginOfPUserByHisCode",
			//PUser用户注销服务
			LOGOUTOFPUSER_URL: "/ServerWCF/WebAssistManageService.svc/BT_SYS_LogoutOfPUser",
			//数据库手工升级服务
			DBUPDATE_URL: "/ServerWCF/WebAssistManageService.svc/BT_SYS_DBUpdate"
		},
		//CS服务交互
		CSServer: {
			//CS服务端口
			CS_PORT: "",
			//CS机构Id
			CS_LABID: "6",
			//CS服务访问域名称domain http://r.zhifang.com.cn/ilabstar
			CS_DOMAIN: '',
			//CS依传入的帐号及密码验证及返回用户帐号服务名称
			CS_LONIN: "/xservice/xlis.ConfirmUser66",
			//CS密钥
			CS_PMOPERTYPEKEY: "A3751787-C218-45A8-A79B-32ACAD2973EC",
			//CS血库服务端的域名或服务访问的IP--传回给后台服务使用
			CS_BASE_URL: csbaseurl
			//调用CS服务获取审批不通过原因
			//CS_UNPASSDESC_URL: "/ilabstar/_30_bi_bloodinterfaceservice/BloodInterfaceService/GetBreqUnPassdesc"
		},
		//与HIS接口
		HisInterface: {
			
		},
		//HIS调用时传入的参数信息
		HisParams: {
			"SysCode": "", //子系统编码
			"HisWardId": "", //His病区Id
			"hisDeptId": "", //His科室Id
			"HisDoctorId": "", //His医生Id
			"WardId": "", //病区Id
			"DeptId": "", //科室Id
			'DoctorId': '', //医生ID
			"AdmId": "", //His的就诊号
			"HisPatId": "", //医嘱申请患者Id				
			"PatNo": "", //医嘱申请患者住院号
			"CName": "" //患者姓名				
		},
		//当前操作系统的医生信息
		SysCurUserInfo: {
			"DeptId": "", //科室Id
			"DeptCName": "", //科室
			"HisDeptId": "", //科室HIS对照码
			'UserId': '', //用户ID
			'UserCName': '', //用户名称
			'DoctorId': '', //医生ID
			'DoctorCName': '', //医生姓名
			"HisDoctorId": '', //HIS医生对照码
			"GradeId": "", //医生所属等级Id
			"GradeName": "", //医生所属等级
			"LowLimit": "", //用血量审核范围下限值
			"UpperLimit": "" //用血量审核范围上限值			
		},
		//系统的本地缓存key
		cachekeys: {
			//缓存CS血库服务端的域名或服务访问的IP的KEY
			CSBASEURL_KEY: "CSServiceAccessURL",
			//是否HIS调用的KEY
			ISHISCALL_KEY: "ishisCall",
			//缓存HIS调用时传入的参数信息的KEY
			HISPARAMS_KEY: "HisParams",
			//缓存"HIS调用时,依传入HIS医生ID,获取到的当前用户信息(SysCurUserInfo)"的KEY
			SYSDOCTORINFO_KEY: "SysCurUserInfo",
			//缓存"HIS调用时,依传入HIS患者病历号获取到的病人信息"的KEY
			HISCALLPATINFO_KEY: "hisCallPatInfo"
		},

		//获取缓存的运行参数CS血库服务端的域名或服务访问的IP
		getCsBaseUrl: function() {
			//需要从服务器获取缓存好
			var url1 = webassistconfig.getData(webassistconfig.cachekeys.CSBASEURL_KEY);
			if (cachedata && !url1) url1 = cachedata.getCache(webassistconfig.cachekeys.CSBASEURL_KEY);
			if (!url1) url1 = csbaseurl;
			return url1;
		},
		//获取按患者病历号调用CS服务获取HIS病人信息
		getHisPatInfoUrl: function() {
			return webassistconfig.getCsBaseUrl() + webassistconfig.CSServer.CS_GETPATINFO_URL;
		},
		//按用血申请单号调用CS服务返回用血申请信息给HIS
		getBreqToHisUrl: function() {
			return webassistconfig.getCsBaseUrl() + webassistconfig.CSServer.CS_TOHISDATA_URL;
		},
		//按用血申请单号调用CS服务作废HIS处理
		getBreqCancelUrl: function() {
			return webassistconfig.getCsBaseUrl() + webassistconfig.CSServer.CS_TOHISOBSOLETE_URL;
		},
		//查看当前库存信息
		getBloodInInfoUrl: function() {
			return webassistconfig.getCsBaseUrl() + webassistconfig.CSServer.CS_GETBLOODQTY_URL;
		},
		//按申请单号+类型获取配血记录或发血记录信息
		getBloodBillInfoUrl: function() {
			return webassistconfig.getCsBaseUrl() + webassistconfig.CSServer.CS_GEBLOODBILLINFO_URL;
		},
		//按申请单号+类型+记录Id获取配血记录PDF或发血记录PDF信息
		getBloodReportImageUrl: function() {
			return webassistconfig.getCsBaseUrl() + webassistconfig.CSServer.CS_GEBLOODREPORTPDF_URL;
		},
		//设置血库系统的本地缓存数据
		setData: function(key, settings) {
			if (cachedata) cachedata.setCache(key, settings);
			layui.data(key, settings);
		},
		//获取血库系统的本地缓存数据
		getData: function(key) {
			var result1 = layui.data(key)[key];
			if (cachedata && !result1) result1 = cachedata.getCache(key);
			return result1;
		},
		setCsBaseUrl: function(url) {
			if (!url) return;
			webassistconfig.CSServer.CS_BASE_URL = url;
			webassistconfig.setData(webassistconfig.cachekeys.CSBASEURL_KEY, url);
		},
		//获取血库系统的当前操作人信息
		getCurOper: function() {
			var userInfo = {
				"empID": -1,
				"empName": ""
			};
			var isReadCookie = false;
			//判断是HIS调用还是血库系统登录
			var ishisCall = webassistconfig.getData(webassistconfig.cachekeys.ISHISCALL_KEY);
			//HIS系统调用,取医生信息作为当前操作人
			if (ishisCall == true) {
				var sysCurUserInfo = webassistconfig.getData(webassistconfig.cachekeys.SYSDOCTORINFO_KEY);
				if (sysCurUserInfo) {
					if (sysCurUserInfo.DoctorId) userInfo.empID = sysCurUserInfo.DoctorId;
					if (sysCurUserInfo.DoctorCName) userInfo.empName = sysCurUserInfo.DoctorCName;
				} else {
					isReadCookie = true;
				}
			} else {
				isReadCookie = true;
			}
			if (isReadCookie) {
				//取当前登录帐号信息
				var useId = uxutil.cookie.get(uxutil.cookie.map.USERID);
				var useName = uxutil.cookie.get(uxutil.cookie.map.USERNAME);
				if (useId) userInfo.empID = useId;
				if (useName) userInfo.empName = useName;
			}
			return userInfo;
		}
	};

	//重新赋值
	webassistconfig.CSServer.CS_BASE_URL = webassistconfig.getCsBaseUrl();
	
	//暴露接口
	exports('webassistconfig', webassistconfig);
});
