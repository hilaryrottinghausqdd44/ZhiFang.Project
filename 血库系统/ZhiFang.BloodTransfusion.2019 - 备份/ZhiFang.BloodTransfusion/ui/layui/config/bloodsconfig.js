/**
	@name：layui.bloodsconfig BS血库系统配置信息 
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

	var bloodsconfig = {
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
			//用血申请确认后是否自动完成审批
			ConfirmedIsAutoCompleted: false,
			//医嘱申请获取患者的多少天内LIS检验结果的配置
			GET_LISRESULT_DAYS: 7,
			//新增医嘱申请时,检验项目LIS结果为空时,设置的默认值
			LIS_DEFAULT_ITEMSRESULT: "检查中",
			//是否调用CS服务登录及验证用户信息
			ISHASCSLOGIN: false,
			//登录成功后,是否调用数据库升级服务
			LOGIN_AFTER_ISUPDATEDB: true,

			//第三方外部调用时的默认登录帐号
			DEFAULT_ACCOUNT: "19999",
			//第三方外部调用时的默认登录密码
			DEFAULT_PWD: "19999",
			//HIS调用时,依传入HIS医生ID,获取到的医生信息
			HISCALL_URL: "/BloodTransfusionManageService.svc/BT_SYS_GetSysCurDoctorInfoByHisCode",
			//按PUser的帐号及密码登录
			LOGINOFPUSER_URL: "/BloodTransfusionManageService.svc/BT_SYS_LoginOfPUser",
			//按HIS医生对照码获取PUser的帐号及密码登录
			LOGINOFPUSERBYHISCODE_URL: "/BloodTransfusionManageService.svc/BT_SYS_LoginOfPUserByHisCode",
			//PUser用户注销服务
			LOGOUTOFPUSER_URL: "/BloodTransfusionManageService.svc/BT_SYS_LogoutOfPUser",
			//数据库手工升级服务
			DBUPDATE_URL: "/BloodTransfusionManageService.svc/BT_SYS_DBUpdate",
			//用血申请PDF存放路径
			PDF_SAVE_URL: uxutil.path.ROOT + "/PDFReport/医嘱申请/",
			//PDFJS预览URL
			PDFJS_URL: uxutil.path.ROOT + "/ui/src/pdfjs/web/viewer.html"
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
			CS_BASE_URL: csbaseurl,

			//按患者病历号调用CS服务获取HIS病人信息
			CS_GETPATINFO_URL: "/ilabstar/_30_BI_BloodInterfaceService/BloodInterfaceService.BGetHisPatInfo",
			//按用血申请单号调用CS服务返回用血申请信息给HIS
			CS_TOHISDATA_URL: "/ilabstar/_30_BI_BloodInterfaceService/BloodInterfaceService.BreqToHis",
			//按用血申请单号调用CS服务作废HIS处理
			CS_TOHISOBSOLETE_URL: "/ilabstar/_30_BI_BloodInterfaceService/BloodInterfaceService.BreqCancel",

			//查看当前库存信息
			CS_GETBLOODQTY_URL: "/ilabstar/_30_bi_bloodinterfaceservice/BloodInterfaceService/getBloodinInfo",
			//按申请单号+类型获取配血记录或发血记录信息
			CS_GEBLOODBILLINFO_URL: "/ilabstar/_30_bi_bloodinterfaceservice/BloodInterfaceService/getBloodBillInfo",
			//按申请单号+类型+记录Id获取配血记录PDF或发血记录PDF信息
			CS_GEBLOODREPORTPDF_URL: "/ilabstar/_30_bi_bloodinterfaceservice/BloodInterfaceService/GetBloodReportImage"
		},
		//与HIS接口
		HisInterface: {
			//用血申请传入的患者参数非空字段
			NONEMPTYFIELD: "admId", //就诊号:admId或病历号:patNo
			//用血申请审核完成后是否返回给HIS
			ISTOHISDATA: true,
			//紧急用血是否在用血申请确认提交时上传HIS
			ISBUSETIMETYPEIDAUTOUPLOADADD: true,
			//用血申请作废时是否调用HIS作废接口
			ISTOHISOBSOLETE: true,

			/**
			 * @description 获取LIS检验结果时,是否同按门诊号及住院号获取:
			 * @description 门诊号:PatID；就诊号：AdmId; 住院号：PatNo (PatNo=00000123 or PatNo= 123)
			 */
			ISGETLISRESULTOFPATIDORPATNO: true,
			//紧急用血是否设置为不可操作或者只读(不进行控制,已停用,后续应该弃用)
			//ISBUSETIMETYPEIDREADONLY: false,
			//(患者ABO及患者Rh从LIS获取为空时)是否允许手工选择患者ABO及患者Rh
			ISALLOWPATABOANDRHOPT: false,

			//用血申请+,是否隐藏科室查询项
			ISHIDEPTNOOFSEARCH: true,
			//用血申请+,是否隐藏医生查询项
			ISHIDEDOCTORNOOFSEARCH: true,
			//用血申请+,是否隐藏就诊类型查询项
			ISHIDEBReqType: true,
			//用血申请+,是否隐藏申请类型查询项
			ISHIDEBloodUseType: true,
			//用血申请+,是否隐藏模糊查询项
			ISHIDELikeSearch: true,
			//用血申请登记时,是否隐藏医生录入项
			ISHIDEDOCTORNOOFADD: true,
			//用血申请登记时,是否隐藏入院日期录入项
			ISHIDETOHOSDETEOFADD: true

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
		//HIS调用时,依传入HIS患者病历号获取到的病人信息
		PatInfoMap: {
			addresstype: "BloodBReqForm_AddressType",
			ageall: "BloodBReqForm_AgeALL",
			anesth: "BloodBReqForm_Anesth",
			bpress: "BloodBReqForm_Bpress",
			breqtypeid: "BloodBReqForm_BReqTypeID",
			busetimetypeid: "BloodBReqForm_BUseTimeTypeID",
			beforuse: "BloodBReqForm_BeforUse",
			birth: "BloodBReqForm_Birth",
			birthday: "BloodBReqForm_Birthday",
			bodytpe: "BloodBReqForm_Bodytpe",
			patid: "BloodBReqForm_PatID",
			patno: "BloodBReqForm_PatNo",
			cname: "BloodBReqForm_CName",
			deptno: "BloodBReqForm_DeptNo",
			diag: "BloodBReqForm_Diag",
			gravida: "BloodBReqForm_Gravida",
			patheight: "BloodBReqForm_PatHeight",
			patweight: "BloodBReqForm_PatWeight",
			sex: "BloodBReqForm_Sex",
			wristbandno: "BloodBReqForm_WristBandNo",
			bed: "BloodBReqForm_Bed",
			breathe: "BloodBReqForm_Breathe",
			hisabocode: "BloodBReqForm_PatABO", //患者ABO "BloodBReqForm_HisABOCode",
			hisrhcode: "BloodBReqForm_PatRh", //患者Rh(D) "BloodBReqForm_HisrhCode",
			patindate: "BloodBReqForm_ToHosdate", //入院时间
			admid: "BloodBReqForm_AdmID", //HIS就诊号
			wardno: "BloodBReqForm_HisWardNo", //HIS病区代码
			pulse: "BloodBReqForm_Pulse", //脉搏
			isagree: "", //是否存在用血同意书:0:无;1:有;
			IsOrder: "", //医生是否有开相关医嘱:0:否;1:是;
			IsLabNoC: "", //医生已经有相关医嘱,但检验标本是否已采集:0:否;1:是;

			heartrate: "",
			patidentity: "",
			patinfodate: "",
			urine: "",
			visit_id: "",
			age: "",
			ageunit: "",
			costtype: "",
			donoraborh: "",
			transdate: "",
			transplant: ""
		},
		//血库系统的本地缓存key
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
			var url1 = bloodsconfig.getData(bloodsconfig.cachekeys.CSBASEURL_KEY);
			if (cachedata && !url1) url1 = cachedata.getCache(bloodsconfig.cachekeys.CSBASEURL_KEY);
			if (!url1) url1 = csbaseurl;
			return url1;
		},
		//获取按患者病历号调用CS服务获取HIS病人信息
		getHisPatInfoUrl: function() {
			return bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_GETPATINFO_URL;
		},
		//按用血申请单号调用CS服务返回用血申请信息给HIS
		getBreqToHisUrl: function() {
			return bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_TOHISDATA_URL;
		},
		//按用血申请单号调用CS服务作废HIS处理
		getBreqCancelUrl: function() {
			return bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_TOHISOBSOLETE_URL;
		},
		//查看当前库存信息
		getBloodInInfoUrl: function() {
			return bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_GETBLOODQTY_URL;
		},
		//按申请单号+类型获取配血记录或发血记录信息
		getBloodBillInfoUrl: function() {
			return bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_GEBLOODBILLINFO_URL;
		},
		//按申请单号+类型+记录Id获取配血记录PDF或发血记录PDF信息
		getBloodReportImageUrl: function() {
			return bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_GEBLOODREPORTPDF_URL;
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
			bloodsconfig.CSServer.CS_BASE_URL = url;
			bloodsconfig.setData(bloodsconfig.cachekeys.CSBASEURL_KEY, url);
		},
		//获取血库系统的当前操作人信息
		getCurOper: function() {
			var userInfo = {
				"empID": -1,
				"empName": ""
			};
			var isReadCookie = false;
			//判断是HIS调用还是血库系统登录
			var ishisCall = bloodsconfig.getData(bloodsconfig.cachekeys.ISHISCALL_KEY);
			//HIS系统调用,取医生信息作为当前操作人
			if (ishisCall == true) {
				var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);
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
	bloodsconfig.CSServer.CS_BASE_URL = bloodsconfig.getCsBaseUrl();
	//console.log(bloodsconfig.CSServer.CS_BASE_URL);

	//暴露接口
	exports('bloodsconfig', bloodsconfig);
});
