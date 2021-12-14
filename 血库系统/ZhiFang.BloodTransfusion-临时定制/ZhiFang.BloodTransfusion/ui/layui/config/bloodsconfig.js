/**
	@name：layui.bloodsconfig BS血库系统配置信息 
	@author：longfc
	@version 2019-07-4
 */
layui.extend({
	uxutil: 'ux/util'
}).define(['jquery', 'uxutil'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;

	//cs血库服务端的域名或服务访问的IP:192.168.0.73
	var csbaseurl = "http://localhost";
	/**版本号*/
	var version = "1.0.0.17";

	var bloodsconfig = {
		/**版本号*/
		version: version,
		config: {
			/**版本号*/
			version: version
		},
		//公共配置
		Common: {
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
			//按用血申请单号打印用血申请单
			CS_BREQ_PRINT_URL: csbaseurl + "/ilabstar/_30_BI_BloodInterfaceService/BloodInterfaceService.BreqPrint",
			//按患者病历号调用CS服务获取HIS病人信息
			CS_GETPATINFO_URL: csbaseurl + "/ilabstar/_30_BI_BloodInterfaceService/BloodInterfaceService.BGetHisPatInfo",
			//按用血申请单号调用CS服务返回用血申请信息给HIS
			CS_TOHISDATA_URL: csbaseurl + "/ilabstar/_30_BI_BloodInterfaceService/BloodInterfaceService.BreqToHis",
			//按用血申请单号调用CS服务作废HIS处理
			CS_TOHISOBSOLETE_URL: csbaseurl + "/ilabstar/_30_BI_BloodInterfaceService/BloodInterfaceService.BreqCancel",
			//查看当前库存信息
			CS_GETBLOODQTY_URL: csbaseurl + "/ilabstar/_30_bi_bloodinterfaceservice/BloodInterfaceService/getBloodinInfo",
			//按申请单号+类型获取配血记录或发血记录信息
			CS_GEBLOODBILLINFO_URL: csbaseurl +
				"/ilabstar/_30_bi_bloodinterfaceservice/BloodInterfaceService/getBloodBillInfo",
			//按申请单号+类型+记录Id获取配血记录PDF或发血记录PDF信息
			CS_GEBLOODREPORTPDF_URL: csbaseurl +
				"/ilabstar/_30_bi_bloodinterfaceservice/BloodInterfaceService/GetBloodReportImage"
		},
		//与HIS接口
		HisInterface: {
			//用血申请传入的患者参数非空字段
			NONEMPTYFIELD: "admId", //就诊号:admId或病历号:patNo
			//用血申请单在审核完成后是否返回给HIS
			ISTOHISDATA: true,
			//用血申请作废时是否调用HIS作废接口
			ISTOHISOBSOLETE: true,
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
			ISHIDETOHOSDETEOFADD: true,
			//在用血申请确认提交时,紧急用血是否自动上传数据到HIS
			ISBUSETIMETYPEIDAUTOUPLOADADD: true,
			//获取LIS检验结果时,是否同按门诊号及住院号获取:门诊号:PatID；就诊号：AdmId; 住院号：PatNo (PatNo=00000123 or PatNo= 123) 
			ISGETLISRESULTOFPATIDORPATNO: true,
			//紧急用血是否设置为不可操作或者只读(不进行控制,已停用,后续应该弃用)
			ISBUSETIMETYPEIDREADONLY: false,
			//(患者ABO及患者Rh从LIS获取为空时)是否允许手工选择患者ABO及患者Rh
			ISALLOWPATABOANDRHOPT: false
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
			//是否HIS调用的KEY
			ISHISCALL_KEY: "ishisCall",
			//缓存HIS调用时传入的参数信息的KEY
			HISPARAMS_KEY: "HisParams",
			//缓存"HIS调用时,依传入HIS医生ID,获取到的当前用户信息(SysCurUserInfo)"的KEY
			SYSDOCTORINFO_KEY: "SysCurUserInfo",
			//缓存"HIS调用时,依传入HIS患者病历号获取到的病人信息"的KEY
			HISCALLPATINFO_KEY: "hisCallPatInfo",
		},
		//设置血库系统的本地缓存数据
		setData: function(key, settings) {
			layui.data(key, settings);
		},
		//获取血库系统的本地缓存数据
		getData: function(key) {
			return layui.data(key)[key];
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

	//暴露接口
	exports('bloodsconfig', bloodsconfig);
});
