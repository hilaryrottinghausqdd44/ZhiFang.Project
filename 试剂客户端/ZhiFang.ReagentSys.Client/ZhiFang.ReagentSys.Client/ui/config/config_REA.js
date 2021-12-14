/**
 * LRMP参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};

JcallShell.System = JcallShell.System || {};
/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = 'LRMP系统';
/**系统编号*/
JcallShell.System.CODE = 'LRMP';
/**系统登录顶部图片*/
JcallShell.System.LoginTopImage = '/images/rea/LoginTop.png';
/**第三方库ADO项目名称*/
JcallShell.System.ADOName = 'rea2015ado';

/**系统登录后处理*/
JcallShell.System.onAfterLogin = function(callback) {
	JcallShell.REA.System.init(callback);
	JcallShell.REA.System.initRunParams();
	//用户列表UI配置信息
	//JcallShell.REA.BUserUIConfig.getUIConfigByHQL("", null);
	//弹出预警信息处理
	JShell.Action.delay(function() {
		JcallShell.REA.Alarm.onLoginAlarm();
	}, null, 1800);
	//JcallShell.REA.ReaGoods.getClassList("GoodsClass",true);
	//JcallShell.REA.ReaGoods.getClassList("GoodsClassType",true);
};

JcallShell.REA = JcallShell.REA || {};

JcallShell.REA.Goods = {
	EXCEL: '试剂信息模版V3.2.xls'
};
JcallShell.REA.User = {
	EXCEL: '用户信息模版V1.xls'
};
JcallShell.REA.TestItem = {
	EXCEL: '检验项目模版V1.xls'
};
JcallShell.REA.System = {
	/**中心机构ID*/
	CENORG_ID: null,
	/**中心机构编码*/
	CENORG_CODE: null,
	/**第三方的机构代码*/
	CENORG_THIRD_CODE: null,
	/**中心机构名称*/
	CENORG_NAME: null,
	/**初始化信息*/
	init: function(callback) {
		var me = this;
		var DEPTCODE = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTCODE);

		if(!DEPTCODE) {
			var msg = "【DEPTCODE机构编号为空】";
			//LOG输出
			JcallShell.Msg.log(JcallShell.Date.toString(new Date(), false, true) + msg);
			return;
		}

		var url = JcallShell.System.Path.ROOT + "/ReaSysManageService.svc/ST_UDTO_SearchCenOrgByHQL";
		url += "?isPlanish=true&fields=CenOrg_Id,CenOrg_OrgNo,CenOrg_CName,CenOrg_ShortCode";
		url += "&where=cenorg.OrgNo='" + DEPTCODE + "'";

		JcallShell.Server.get(url, function(data) {
			var msg = "";
			if(data.success) {
				if(data.value && data.value.count == 1) {
					var obj = data.value.list[0];
					me.CENORG_ID = obj.CenOrg_Id;
					me.CENORG_CODE = obj.CenOrg_OrgNo;
					me.CENORG_NAME = obj.CenOrg_CName;
					me.CENORG_THIRD_CODE = obj.CenOrg_ShortCode;
					msg = "【获取到中心机构信息成功】";
				} else {
					msg = "【获取到中心机构信息失败】";
					//JcallShell.Msg.error("没有获取到中心机构信息");
				}
			} else {
				msg = "【获取到中心机构信息失败】";
				//JcallShell.Msg.error(data.msg);
			}
			//LOG输出
			JcallShell.Msg.log(JcallShell.Date.toString(new Date(), false, true) + msg);
			if(Ext.typeOf(callback) == 'function') {
				var bo = msg ? false : true;
				callback(bo);
			}
		});
	},
	/**系统登录后初始化部分系统运行参数信息*/
	initRunParams: function() {
		//系统运行参数"启用用户UI配置"
		JShell.REA.RunParams.getRunParamsValue("EnableUserUIConfig", true, function(data) {
			if(data.value && data.value.ParaValue && data.value.ParaValue == 1) {
				JcallShell.REA.BUserUIConfig.getUIConfigByHQL("", null);
			}
		});
		//系统运行参数"数据库是否独立部署"
		JShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", true, function(data) {});
		//系统运行参数"列表默认分页记录数"
		JShell.REA.RunParams.getRunParamsValue("ReaUIDefaultPageSize", true, function(data) {});
		//系统运行参数"是否启用库存库房权限"
		JShell.REA.RunParams.getRunParamsValue("ReaBmsQtyDtlIsUseEmp", true, function(data) {});
		//系统运行参数"是否按移库人权限移库"
		JShell.REA.RunParams.getRunParamsValue("ReaBmsTransferDocIsUseEmpOut", true, function(data) {});
		//系统运行参数"是否按出库人权限出库"
		JShell.REA.RunParams.getRunParamsValue("ReaBmsOutDocUseIsEmpOut", true, function(data) {});
		//系统运行参数"启用库存报警"
		JShell.REA.RunParams.getRunParamsValue("IsInventoryAlarm", true, function(data) {});
		//系统运行参数"启用效期报警"
		JShell.REA.RunParams.getRunParamsValue("IsExpirationAlarm", true, function(data) {});
		//系统运行参数"启用注册证预警"
		JShell.REA.RunParams.getRunParamsValue("IsRegistAlarm", true, function(data) {});
		//系统运行参数"订单审批金额"
		JShell.REA.RunParams.getRunParamsValue("ReaBmsCenOrderDocApprovalTotalPrice", true, function(data) {});
		//系统运行参数"库存货品是否需要性能验证后才能使用出库"
		JShell.REA.RunParams.getRunParamsValue("ReaBmsOutIsNeedPerformanceTest", true, function(data) {});
	}
};
/**枚举*/
JcallShell.REA.Enum = {
	/**结构类型*/
	CenOrgType: {
		'E1': '厂商',
		'E2': '供应商',
		'E3': '实验室 '
	},
	/**供货总单_紧急标志*/
	BmsCenSaleDoc_UrgentFlag: {
		'E0': '正常',
		'E1': '紧急'
	},
	/**供货总单_单据状态*/
	BmsCenSaleDoc_Status: {
		'E0': '临时',
		'E2': '已审核',
		'E1': '已验收'
	},
	/**供货总单_数据上传标志*/
	BmsCenSaleDoc_IOFlag: {
		'E0': '未提取',
		'E1': '已提取',
		'E2': '部分提取'
	},
	/**供货总单_来源*/
	BmsCenSaleDoc_Source: {
		'E1': '平台(供应商)',
		'E2': '平台(实验室)',
		'E3': 'PC(供应商)',
		'E4': 'PC(实验室)',
		'E5': '手持(供应商)',
		'E6': '手持(实验室)'
	},
	
	/**订货总单_紧急标志*/
	BmsCenOrderDoc_SupplyStatus: {
		'E1': {
			value: '未供货',
			color: '#fff',
			bcolor: '#b2b5b8'
		},
		'E2': {
			value: '部分供货',
			color: '#fff',
			bcolor: '#aeff7b'
		},
		'E3': {
			value: '终止供货',
			color: '#fff',
			bcolor: '#d9534f'
		},
		'E4': {
			value: '全部供货',
			color: '#fff',
			bcolor: '#72d923'
		}
	},

	/**订货总单_紧急标志*/
	BmsCenOrderDoc_UrgentFlag: {
		'E0': {
			value: '正常',
			color: '#fff',
			bcolor: '#5cb85c'
		},
		'E1': {
			value: '紧急',
			color: '#fff',
			bcolor: '#d9534f'
		}
	},
	/**订货总单_单据状态*/
	BmsCenOrderDoc_Status: {
		'E0': {
			value: '临时',
			color: '#fff',
			bcolor: '#ccc'
		},
		'E1': {
			value: '已提交',
			color: '#fff',
			bcolor: '#5cb85c'
		},
		'E2': {
			value: '已确认',
			color: '#fff',
			bcolor: '#5bc0de'
		},
		'E3': {
			value: '已出货',
			color: '#fff',
			bcolor: '#f0ad4e'
		},
		'E4': {
			value: '已验收',
			color: '#fff',
			bcolor: '#777'
		},
		'E999': {
			value: '已删除',
			color: '#fff',
			bcolor: 'red'
		}
	},

	/**出入库_使用标志*/
	UseFlag: {
		'E1': '仪器使用',
		'E2': '报损',
		'E3': '回退供应商',
		'E4': '调账',
		'E5': '入库'
	},
	/**订货总单_数据标志*/
	ReaBmsCenOrderDoc_IOFlag: {
		'E0': '未提取',
		'E1': '已上传',
		'E2': '取消上传',
		'E3': '供应商确认',
		'E4': '取消确认'
	},
	/**供货总单_数据标志*/
	ReaBmsCenSaleDoc_IOFlag: {
		'E0': '未提取',
		'E1': '已提取',
		'E2': '部分提取',
		'E3': '已上传'
	},
	/**颜色*/
	Color: {
		'E0': '#FFCC00',
		'E1': '#FF99CC',
		'E2': '#99CC33',
		'E3': '#CC0033',
		'E4': '#663366',
		'E5': '#999966',
		'E6': '#663300',
		'E7': '#6699CC'
	},
	/**
	 * @param {String} name 枚举类型名称
	 * @param {Boolean} hasAll 是否带'全部'选项
	 * @param {Boolean} hasColor 是否带颜色属性
	 * @param {Boolean} hasNull 是否带'无'选项
	 */
	getList: function(name, hasAll, hasColor, hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if(!obj) return [];

		if(hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for(var i in obj) {
			if(!hasNull) {
				if(obj[i] == '无') continue;
			}
			var li = [i.slice(1), obj[i]];
			if(hasColor) {
				li.push('font-weight:bold;color:' + me.Color[i] + ';');
			}
			list.push(li);
		}

		return list;
	},
	/**
	 * @param {String} name 枚举类型名称
	 * @param {Boolean} hasAll 是否带'全部'选项
	 * @param {Boolean} hasColor 是否带颜色属性
	 * @param {Boolean} hasNull 是否带'无'选项
	 */
	getComboboxList: function(name, hasAll, hasColor, hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if(!obj) return [];

		if(hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for(var i in obj) {
			if(!hasNull) {
				if(obj[i] == '无') continue;
			}
			var li = [i.slice(1), obj[i].value];
			if(hasColor) {
				li.push('font-weight:bold;color:' + obj[i].bcolor + ';');
			}
			list.push(li);
		}

		return list;
	}
};
/**系统运行参数*/
JcallShell.REA.RunParams = {
	/**运行参数集合*/
	Lists: {
		/**是否启用库存报警*/
		IsInventoryAlarm: {
			Id: "C-RBQD-BQIW-0001",
			Value: null,
			IsLoad: false,
			CName: "是否启用库存报警"
		},
		/**是否启用效期报警*/
		IsExpirationAlarm: {
			Id: "C-RBQD-BQEW-0002",
			Value: null,
			IsLoad: false,
			CName: "是否启用效期报警"
		},
		/**货品效期将到期预警天数*/
		GoodsValidityWarnDays: {
			Id: "C-RBQD-GVWD-0003",
			Value: null,
			IsLoad: false,
			CName: "货品效期预警天数"
		},
		/**验收货品扫码*/
		AcceptanceScanCode: {
			Id: "C-RBDC-GASC-0004",
			Value: null,
			IsLoad: false,
			CName: "验收货品扫码"
		},
		/**入库货品扫码*/
		InScanCode: {
			Id: "C-RBID-GISC-0005",
			Value: null,
			IsLoad: false,
			CName: "入库货品扫码"
		},
		/**出库货品扫码*/
		OutScanCode: {
			Id: "C-RBOD-GOSC-0006",
			Value: null,
			IsLoad: false,
			CName: "出库货品扫码"
		},
		/**移库货品扫码*/
		TransferScanCode: {
			Id: "C-RBTD-GTSC-0007",
			Value: null,
			IsLoad: false,
			CName: "移库货品扫码"
		},
		/**访问BS平台的URL*/
		BSPlatformURL: {
			Id: "C-BSPL-PURL-0009",
			Value: null,
			IsLoad: false,
			CName: "访问BS平台的URL"
		},
		/**验收双确认方式*/
		SecAccepterAccount: {
			Id: "C-RBSC-SAAC-0010",
			Value: null,
			IsLoad: false,
			CName: "验收双确认方式"
		},
		/**
		 * 订单上传类型
		 * 1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统;
		 * */
		OrderUploadeType: {
			Id: "C-RBCO-UPLO-0011",
			Value: null,
			IsLoad: false,
			CName: "订单上传类型"
		},
		/**出库领用人是否为登录人*/
		OutboundIsLogin: {
			Id: "C-RBOD-OBIL-0012",
			Value: null,
			IsLoad: false,
			CName: "出库领用人是否为登录人"
		},
		/**盘库审核是否需要确认*/
		ReaBmsCheckDocIsCheck: {
			Id: "C-RBCD-ISCH-0013",
			Value: null,
			IsLoad: false,
			CName: "盘库审核是否需要确认"
		},
		/**使用出库审核是否需要确认*/
		ReaBmsOutDocUseIsCheck: {
			Id: "C-RBCD-ISCH-0014",
			Value: null,
			IsLoad: false,
			CName: "使用出库审核是否需要确认"
		},
		/**报损出库审核是否需要确认*/
		ReaBmsOutDocLossIsCheck: {
			Id: "C-RBCD-ISCH-0015",
			Value: null,
			IsLoad: false,
			CName: "报损出库审核是否需要确认"
		},
		/**退供应商出库审核是否需要确认*/
		ReaBmsOutDocRefundSIsCheck: {
			Id: "C-RBCD-ISCH-0016",
			Value: null,
			IsLoad: false,
			CName: "退供应商出库审核是否需要确认"
		},
		/**数据库是否独立部署*/
		ReaDataBaseIsDeploy: {
			Id: "C-DATA-ISDI-0017",
			Value: null,
			IsLoad: false,
			CName: "数据库是否独立部署"
		},
		/**订单审核通过同时直接订单上传*/
		OrderCheckIsUploaded: {
			Id: "C-RBCO-CHEC-0018",
			Value: null,
			IsLoad: false,
			CName: "订单审核通过同时直接订单上传"
		},
		/**供应商确认订单时是否需要强制校验货品编码*/
		OrderConfirmIsVerifyGoodsNo: {
			Id: "C-RBCO-COFM-0019",
			Value: null,
			IsLoad: false,
			CName: "供应商确认订单时是否需要强制校验货品编码"
		},
		/**业务接口URL配置*/
		InterfaceUrlConfig: {
			Id: "C-IURL-CONF-0020",
			Value: null,
			IsLoad: false,
			CName: "业务接口URL配置"
		},
		/**使用出库仪器是否必填*/
		ReaBmsOutDocUseIsEquip: {
			Id: "C-RBCD-ISCH-0021",
			Value: null,
			IsLoad: false,
			CName: "使用出库仪器是否必填"
		},
		/**是否按出库人权限出库*/
		ReaBmsOutDocUseIsEmpOut: {
			Id: "C-RBCD-ISCH-0023",
			Value: null,
			IsLoad: false,
			CName: "是否按出库人权限出库"
		},
		/**注册证将过期预警天数*/
		RegistWillexpireWarning: {
			Id: "C-RRWW-WAEN-0024",
			Value: null,
			IsLoad: false,
			CName: "注册证将过期预警天数"
		},
		/**启用注册证预警*/
		IsRegistAlarm: {
			Id: "C-RRWW-ISWA-0025",
			Value: null,
			IsLoad: false,
			CName: "启用注册证预警"
		},
		/**移库审核是否需要确认*/
		ReaBmsTransferDocIsCheck: {
			Id: "C-RTCD-ISCH-0026",
			Value: null,
			IsLoad: false,
			CName: "移库审核是否需要确认"
		},
		/**是否按移库人权限移库*/
		ReaBmsTransferDocIsUseEmpOut: {
			Id: "C-RBTD-ISEO-0027",
			Value: null,
			IsLoad: false,
			CName: "是否按移库人权限移库"
		},
		/**订单审批金额*/
		ReaBmsCenOrderDocApprovalTotalPrice: {
			Id: "C-RBOD-APPR-0028",
			Value: null,
			IsLoad: false,
			CName: "订单审批金额"
		},
		/**出库确认后是否调用退库接口*/
		ReaBmsOutDocIsReturnInterface: {
			Id: "C-RBOD-ISRI-0029",
			Value: null,
			IsLoad: false,
			CName: "出库确认后是否调用退库接口"
		},
		/**列表默认分页记录数*/
		ReaUIDefaultPageSize: {
			Id: "C-LRMP-UIPA-0030",
			Value: null,
			IsLoad: false,
			CName: "列表默认分页记录数"
		},
		/**是否启用库存库房权限*/
		ReaBmsQtyDtlIsUseEmp: {
			Id: "C-RBQT-ISUE-0031",
			Value: null,
			IsLoad: false,
			CName: "是否启用库存库房权限"
		},
		/**@description 库存货品是否需要性能验证后才能使用出库*/
		ReaBmsOutIsNeedPerformanceTest: {
			Id: "C-RBOD-ISPV-0032",
			Value: null,
			IsLoad: false,
			CName: "库存货品是否需要性能验证后才能使用出库"
		},
		/**@description 效期预警默认已过期天数*/
		ExpirationAlarmWillexpireDefaultDays: {
			Id: "C-EAWE-DAYS-0033",
			Value: null,
			IsLoad: false,
			CName: "效期预警默认已过期天数"
		},
		/**@description 注册证预警默认已过期天数*/
		RegistWarnExpiredDefaultDays: {
			Id: "C-REWE-DDAY-0034",
			Value: null,
			IsLoad: false,
			CName: "注册证预警默认已过期天数"
		},
		/**@description 启用用户UI配置*/
		EnableUserUIConfig: {
			Id: "C-EUSE-UICF-0035",
			Value: null,
			IsLoad: false,
			CName: "启用用户UI配置"
		},
		/**
		 * @description 是否需要支持直接出库
		 * @description 在使用出库(全部)的功能模块使用该参数,如果设置为"是",使用出库(全部)的功能模块的"新增出库"为显示;
		 * @description 如果参数值设置为"否",使用出库(全部)的功能模块的"新增出库"为不显示;
		 * */
		ISNeedSupportDirectOut: {
			Id: "C-RBOD-ISDO-0036",
			Value: null,
			IsLoad: false,
			CName: "是否需要支持直接出库"
		},
		/**
		 * @description 接口数据是否需要重新生成条码
		 * @description 1:是;2:否;
		 */
		InterfaceDataISNeedCreateBarCode: {
			Id: "C-WTID-NTRB-0037",
			Value: null,
			IsLoad: false,
			CName: "接口数据是否需要重新生成条码"
		},
		/**
		 * @description 移库或出库扫码是否允许从所有库房获取库存货品
		 * @description 1:是;2:否;
		 */
		TranOrOutBarCodeIsAllowOfALLStorage: {
			Id: "C-TOBC-ISAL-0038",
			Value: null,
			IsLoad: false,
			CName: "移库或出库扫码是否允许从所有库房获取库存货品"
		},
		/**
		 * @description 盘库时实盘数是否取库存数
		 * @description 1:是;2:否;
		 */
		InventoryIsTakenFromQty: {
			Id: "C-IVTY-ISTQ-0039",
			Value: null,
			IsLoad: false,
			CName: "盘库时实盘数是否取库存数"
		},
		/**
		 * @description 是否开启近效期
		 * @description 1:是;2:否;
		 */
		IsOpenNearEffectPeriod: {
			Id: "C-RBOU-ISON-0040",
			Value: null,
			IsLoad: false,
			CName: "是否开启近效期"
		},
		/**
		 * @description 是否强制近效期出库
		 * @description 1:是;2:否;
		 */
		IsOutOfStockInNeartermPeriod: {
			Id: "C-RBOU-ISNP-0041",
			Value: null,
			IsLoad: false,
			CName: "是否强制近效期出库"
		},
		/**
		 * @description 是否开启供应批次合并
		 * @description 1:前台不显示，后台默认合并;2:前台不显示，后台默认不合并;
		 * @description 3:前台显示且默认勾选(合并);4:前台显示且默认不勾选(不合并)
		 */
		IsOpenMergeInDocNo: {
			Id: "C-RBOU-IIBM-0042", 
			Value: null,
			IsLoad: false,
			CName: "是否开启供应批次合并"
		},
		/**
		 * @description 平均使用量计算月数
		 */
		CalcAvgUsed:{
			Id: "C-CGSQ-AUCM-0043",
			Value: null,
			IsLoad: false,
			CName: "平均使用量计算月数"
		},
		/**
		 * @description 采购系数
		 */
		CoePurchase:{
			Id: "C-CGSQ-PUCO-0044",
			Value: null,
			IsLoad: false,
			CName: "采购系数"
		}
	},
	/***
	 * 获取运行参数值
	 * @param {Object} paraKey 运行参数key
	 * @param {Object} isRefresh 是否重新获取运行参数值
	 * @param {Object} callback 回调
	 */
	getRunParamsValue: function(paraKey, isRefresh, callback) {
		var me = this;
		//运行参数编码为空
		var result = {
			success: true,
			value: {
				ParaValue: null
			},
			msg: ""
		};
		if(!paraKey) {
			result.success = false;
			if(callback) {
				return callback(result);
			} else {
				return;
			}
		}
		//运行参数不存在Lists
		if(!JcallShell.REA.RunParams.Lists[paraKey]) {
			result.success = false;
			if(callback) {
				return callback(result);
			} else {
				return;
			}
		}
		
		if(isRefresh) {
			JcallShell.REA.RunParams.Lists[paraKey].IsLoad = false;
		}
		var value = JcallShell.REA.RunParams.Lists[paraKey].Value;
		if(value != 0 && !value) { // null != 0--true;!null--true
			isRefresh = true;
		}
		//运行参数值已存在,且不用重新调用服务获取,直接返回
		if(value && isRefresh != true) {
			result.success = true;
			result.value.ParaValue = JcallShell.REA.RunParams.Lists[paraKey].Value;
			if(callback) {
				return callback(result);
			} else {
				return;
			}
		}

		if(!JcallShell.REA.RunParams.Lists[paraKey].IsLoad) {
			JcallShell.REA.RunParams.Lists[paraKey].IsLoad = true;
			var url = JShell.System.Path.ROOT + "/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=" +
				JcallShell.REA.RunParams.Lists[paraKey].Id + "&t=" + (new Date().getDate());
			JShell.Server.get(url, function(data) {
				if(data.success) {
					JcallShell.REA.RunParams.Lists[paraKey].IsLoad = true;
					var obj = data.value;
					if(obj.ParaValue) {
						JcallShell.REA.RunParams.Lists[paraKey].Value = obj.ParaValue;
					}
					if(callback) {
						callback(data);
					} else {
						return;
					}
				} else {
					JcallShell.REA.RunParams.Lists[paraKey].IsLoad = false;
					if(callback) {
						callback(data);
					} else {
						return;
					}
				}
			});
		} else {
			if(callback) {
				callback(result);
			} else {
				return;
			}
		}
		//JShell.Action.delay(function() {}, null, 10);
	}
};
/**系统业务状态初始化*/
JcallShell.REA.StatusList = JcallShell.REA.StatusList || {
	getBaseInfo: function() {
		return {
			List: [], //["", '全部', 'font-weight:bold;color:black;text-align:center;']
			Enum: {}, //{Id:Name}
			BGColor: {}, //{Id:BGColor}
			FColor: {} //{Id:FontColor}
		};
	}
};
/**系统业务状态集合*/
JcallShell.REA.StatusList = {
	/**业务状态集合*/
	Status: {
		/**机构货品操作类型*/
		ReaGoodsOperation: JShell.REA.StatusList.getBaseInfo(),
		/**供应商与货品关系操作记录状态*/
		ReaGoodsOrgLinkStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端申请总单状态*/
		ReaBmsReqDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端订单状态*/
		ReaBmsOrderDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**订单总单付款状态*/
		ReaBmsOrderDocPayStaus: JShell.REA.StatusList.getBaseInfo(),
		/**订货单数据标志*/
		ReaBmsOrderDocIOFlag: JShell.REA.StatusList.getBaseInfo(),
		/**订单接口标志*/
		ReaBmsOrderDocThirdFlag: JShell.REA.StatusList.getBaseInfo(),
		/**客户端供货单及明细状态*/
		ReaBmsCenSaleDocAndDtlStatus: JShell.REA.StatusList.getBaseInfo(),
		/**供货单数据来源*/
		ReaBmsCenSaleDocSource: JShell.REA.StatusList.getBaseInfo(),
		/**供货单数据标志*/
		ReaBmsCenSaleDocIOFlag: JShell.REA.StatusList.getBaseInfo(),
		/**客户端验货单验收双确认方式*/
		ConfirmSecAccepterType: JShell.REA.StatusList.getBaseInfo(),
		/**客户端验货单数据来源类型*/
		ReaBmsCenSaleDocConfirmSourceType: JShell.REA.StatusList.getBaseInfo(),
		/**客户端验货单状态*/
		ReaBmsCenSaleDocConfirmStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端验货单明细状态*/
		ReaBmsCenSaleDtlConfirmStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端入库类型*/
		ReaBmsInDocInType: JShell.REA.StatusList.getBaseInfo(),
		/**客户端入库总单状态*/
		ReaBmsInDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端货品的条码类型*/
		ReaGoodsBarCodeType: JShell.REA.StatusList.getBaseInfo(),
		/**货品条码操作类型*/
		ReaGoodsBarcodeOperType: JShell.REA.StatusList.getBaseInfo(),
		/**库存操作记录操作类型*/
		ReaBmsQtyDtlOperationOperType: JShell.REA.StatusList.getBaseInfo(),
		/**系统参数编码*/
		SYSParaNo: JShell.REA.StatusList.getBaseInfo(),
		/**客户端盘库单状态*/
		ReaBmsCheckDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端盘库单锁定标志*/
		ReaBmsCheckDocLock: JShell.REA.StatusList.getBaseInfo(),
		/**客户端盘库单盘库结果*/
		ReaBmsCheckResult: JShell.REA.StatusList.getBaseInfo(),
		/**客户端出库类型*/
		ReaBmsOutDocOutType: JShell.REA.StatusList.getBaseInfo(),
		/**月结类型*/
		ReaBmsQtyMonthBalanceDocType: JShell.REA.StatusList.getBaseInfo(),
		/**月结统计类型(月结库存货品合并方式)*/
		ReaBmsQtyMonthBalanceDocStatisticalType: JShell.REA.StatusList.getBaseInfo(),
		/**货品条码生成或货品条码扫码*/
		ReaGoodsBarcodeOperationSerialType: JShell.REA.StatusList.getBaseInfo(),
		/**模板信息类型*/
		BTemplateType: JShell.REA.StatusList.getBaseInfo(),
		/**报表管理信息类型*/
		BReportType: JShell.REA.StatusList.getBaseInfo(),
		/**报表管理信息状态*/
		BReportStatus: JShell.REA.StatusList.getBaseInfo(),
		/**接口类型*/
		ReaBusinessInterfaceType: JShell.REA.StatusList.getBaseInfo(),
		/**业务类型*/
		ReaBusinessType: JShell.REA.StatusList.getBaseInfo(),
		/**库存试剂合并选择项*/
		ReaBmsStatisticalType: JShell.REA.StatusList.getBaseInfo(),
		/**预警分类*/
		AlertType: JShell.REA.StatusList.getBaseInfo(),
		/**客户端移库总单状态*/
		ReaBmsTransferDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端出库总单状态*/
		ReaBmsOutDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**出库使用量统计类型*/
		ReaMonthUsageStatisticsDocType: JShell.REA.StatusList.getBaseInfo(),
		/**出库使用量统计周期类型*/
		ReaMonthUsageStatisticsDocRoundType: JShell.REA.StatusList.getBaseInfo(),
		/**入库对帐标志*/
		ReaBmsInDocReconciliationMark: JShell.REA.StatusList.getBaseInfo(),
		/**入库对帐锁定标志*/
		ReaBmsInDocLockMark: JShell.REA.StatusList.getBaseInfo(),
		/**库存量预警比较值类型*/
		QtyWarningComparisonValueType: JShell.REA.StatusList.getBaseInfo(),
		/**货品批次的验证状态*/
		ReaGoodsLotVerificationStatus: JShell.REA.StatusList.getBaseInfo(),
		/**库存标志*/
		ReaBmsQtyDtlMark: JShell.REA.StatusList.getBaseInfo(),
		/**出库明细报表合并选择项*/
		ReaBmsOutDtlStatisticalType: JShell.REA.StatusList.getBaseInfo(),
		/**需求调整：入库明细报表合并选择项*/
		ReaBmsInDtlStatisticalType: JShell.REA.StatusList.getBaseInfo(),
		/**ReaBmsInDocIOFlag*/
		ReaBmsInDocIOFlag: JShell.REA.StatusList.getBaseInfo(),
		/**出库数据接口标志*/
		ReaBmsOutDocIOFlag: JShell.REA.StatusList.getBaseInfo(),
		/**出库单第三方接口标志*/
		ReaBmsOutDocThirdFlag:JShell.REA.StatusList.getBaseInfo(),
		/**订单明细汇总*/
		ReaBmsCenOrderDtlStatisticalType:JShell.REA.StatusList.getBaseInfo()
	},
	getBaseInfo: function() {
		return {
			List: [], //["", '全部', 'font-weight:bold;color:black;text-align:center;']
			Enum: {}, //{Id:Name}
			BGColor: {}, //{Id:BGColor}
			FColor: {} //{Id:FontColor}
		};
	},
	/**获取申请总单状态参数*/
	getParams: function(classname) {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": classname,
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		};
		return params;
	},
	/***
	 * 获取REA每年的业务状态信息
	 * @param {Object} classname 业务状态类名称
	 * @param {Object} isRefresh 是否重新获取
	 * @param {Object} hasAll 是否添加全部选择项
	 * @param {Object} callback
	 */
	getStatusList: function(classname, isRefresh, hasAll, callback) {
		//var me = this;
		//运行参数编码为空
		var result = {
			success: true,
			value: JShell.REA.StatusList.getBaseInfo(),
			msg: ""
		};
		if(!classname) {
			result.success = false;
			if(callback) callback(result);
			return;
		}
		//classname不存在Lists
		if(!JShell.REA.StatusList.Status[classname]) {
			result.success = false;
			if(callback) callback(result);
			return;
		}
		//运行参数值已存在,且不用重新调用服务获取,直接返回
		var tempStatus = JShell.REA.StatusList.Status[classname];
		if(!tempStatus || !tempStatus.List || tempStatus.List.length <= 0) isRefresh = true;
		if(tempStatus.List && isRefresh != true) {
			result.success = true;
			result.value = tempStatus;
			if(callback) callback(result);
			return;
		}

		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode(JShell.REA.StatusList.getParams(classname));
		JShell.REA.StatusList.Status[classname].List = [];
		JShell.REA.StatusList.Status[classname].Enum = {};
		JShell.REA.StatusList.Status[classname].FColor = {};
		JShell.REA.StatusList.Status[classname].BGColor = {};
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success && data.value && data.value[0][classname].length > 0) {
				if(hasAll) {
					JShell.REA.StatusList.Status[classname].List.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']);
				}
				Ext.Array.each(data.value[0][classname], function(obj, index) {
					var style = ['font-weight:bold;text-align:center;'];
					if(obj.FontColor) {
						JShell.REA.StatusList.Status[classname].FColor[obj.Id] = obj.FontColor;
					}
					if(obj.BGColor) {
						style.push('color:' + obj.BGColor); //background-
						JShell.REA.StatusList.Status[classname].BGColor[obj.Id] = obj.BGColor;
					}
					JShell.REA.StatusList.Status[classname].Enum[obj.Id] = obj.Name;
					tempArr = [obj.Id, obj.Name, style.join(';')];
					JShell.REA.StatusList.Status[classname].List.push(tempArr);
				});
				result.success = true;
				result.value = JShell.REA.StatusList.Status[classname];
				if(callback) callback(result);
			}
		}, false);
	}
};
/**系统登录后的预警提示信息*/
JcallShell.REA.Alarm = {
	/**登录成功后,获取库存预警,效期预警,注册证预警提示信息*/
	onLoginAlarm: function() {
		//var me = this;
		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_GetReaGoodsWarningAlertInfo' + '?t=' + new Date()
			.getTime();
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var isOpenWin = false;
				var result = data.value;
				if(!result) return;

				//启用库存报警
				if(result.StoreAlarm && result.StoreAlarm.IsStoreAlarm == true) {
					if(result.StoreAlarm.HasStoreLower == true) {
						isOpenWin = true;
					}
					if(result.StoreAlarm.HasStoreUpper == true) {
						isOpenWin = true;
					}
				}
				if(isOpenWin == false) {
					//启用效期报警
					if(result.ExpirationAlarm && result.ExpirationAlarm.IsExpirationAlarm == true) {
						if(result.ExpirationAlarm.HasExpired == true) {
							isOpenWin = true;
						}
						if(result.ExpirationAlarm.HasWillexpire == true) {
							isOpenWin = true;
						}
					}
				}
				if(isOpenWin == false) {
					//启用注册证预警
					if(result.RegistAlarm && result.RegistAlarm.IsExpirationAlarm == true) {
						if(result.RegistAlarm.HasExpired == true) {
							isOpenWin = true;
						}
						if(result.RegistAlarm.HasWillexpire == true) {
							isOpenWin = true;
						}
					}
				}
				if(isOpenWin == true) {
					JcallShell.REA.Alarm.openAlarmWin(result);
				}
			}
		});
	},
	/**打开预警提示窗体*/
	openAlarmWin: function(result) {
		JShell.Win.open('Shell.class.rea.client.warningalertinfo.App', {
			draggable: false, //移动功能
			resizable: true, //可变大小功能
			title: "预警信息",
			width: "96%",
			height: "92%",
			StoreAlarm: result.StoreAlarm,
			ExpirationAlarm: result.ExpirationAlarm,
			RegistAlarm: result.RegistAlarm,
			listeners: {

			}
		}).show();
	}
};
/**机构货品的一级分类及二级分类信息*/
JcallShell.REA.ReaGoods = {
	List: {
		/**机构货品的一级分类信息*/
		GoodsClass: [],
		/**机构货品的二级分类信息*/
		GoodsClassType: [],
	},
	getClassList: function(classType, isRefresh, callback) {
		//var me = this;
		var result = {
			success: true,
			value: [],
			msg: ""
		};
		if(!classType) {
			result.success = false;
			if(callback) callback(result);
			return;
		}
		if(!JShell.REA.ReaGoods.List[classType]) {
			result.success = false;
			if(callback) callback(result);
			return;
		}
		var tempStatus = JShell.REA.ReaGoods.List[classType];
		if(!tempStatus || !tempStatus.List || tempStatus.List.length <= 0) isRefresh = true;
		if(tempStatus.List && isRefresh != true) {
			result.success = true;
			result.value = tempStatus;
			if(callback) callback(result);
			return;
		}
		JShell.REA.ReaGoods.List[classType] = [];
		var url = JShell.System.Path.getRootUrl(
			"/ReaManageService.svc/RS_UDTO_SearchGoodsClassListByClassTypeAndHQL?classType=" + classType);
		JcallShell.Server.get(url, function(data) {
			if(data.success && data.value && data.value.length > 0) {
				JShell.REA.ReaGoods.List[classType].push(data.value);
				result.success = true;
				result.value = JShell.REA.ReaGoods.List[classType];
				if(callback) callback(result);
			}
		}, false);
	}
};
/**当前登录用户的UI配置信息*/
JcallShell.REA.BUserUIConfig = {
	List: {},
	addListByKey: function(userUIKey, params) {
		if(!userUIKey || !params) return;
		
		var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
		var userUIKey1=userUIKey;
		if(empID&&empID.length>0&&userUIKey.indexOf(empID)<0)userUIKey1=empID+userUIKey;
		if(!JcallShell.REA.BUserUIConfig.List) JcallShell.REA.BUserUIConfig.List = {};
		JcallShell.REA.BUserUIConfig.List[userUIKey1] = params;
	},
	removeByKey: function(userUIKey) {
		if(!userUIKey) return;
		
		var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
		var userUIKey1=userUIKey;
		if(empID&&empID.length>0&&userUIKey.indexOf(empID)<0)userUIKey1=empID+userUIKey;
		if(!JcallShell.REA.BUserUIConfig.List) JcallShell.REA.BUserUIConfig.List = {};
		if(JcallShell.REA.BUserUIConfig.List[userUIKey1]) delete JcallShell.REA.BUserUIConfig.List[userUIKey1];
	},
	getUIConfigByKey: function(userUIKey, isRefresh, callback) {
		var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
		var userUIKey1=empID+userUIKey;
		var userUI = JcallShell.REA.BUserUIConfig.List[userUIKey1];
		if(!userUI) isRefresh = true;
		
		if(userUI && isRefresh != true) {
			if(callback) callback(userUI);
			return;
		}
		JcallShell.REA.BUserUIConfig.getUIConfigByHQL(userUIKey, function(userUI2) {
			if(callback) callback(userUI2);
		});
	},
	getUIConfigByHQL: function(userUIKey, callback) {
		var userUI = null;
		var arr = [];
		if(userUIKey && userUIKey != '') {
			arr.push("buseruiconfig.UserUIKey='" + userUIKey + "'");
		}
		var empID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
		if(empID) {
			arr.push("buseruiconfig.EmpID=" + empID);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ") and buseruiconfig.IsUse=1";
		var fields =
			"BUserUIConfig_Id,BUserUIConfig_UserUIKey,BUserUIConfig_UserUIName,BUserUIConfig_UITypeID,BUserUIConfig_EmpID,BUserUIConfig_Comment";
		var url = JShell.System.Path.getRootUrl(
			"/ReaSysManageService.svc/ST_UDTO_SearchBUserUIConfigByHQL?isPlanish=true&fields=" + fields + "&where=" + where);
		JcallShell.Server.get(url, function(data) {
			var list = null;
			if(data.success && data.value && data.value.list)
				list = data.value.list;
			if(list && list.length > 0) {
				var userUIKey1=userUIKey;
				if(empID&&empID.length>0&&userUIKey.indexOf(empID)<0)userUIKey1=empID+userUIKey;
				JcallShell.REA.BUserUIConfig.List[userUIKey1] = list[0];
				if(userUIKey1) userUI = JcallShell.REA.BUserUIConfig.List[userUIKey1];
			}
			if(callback) callback(userUI);
		}, false);
	}
};

if (!JcallShell.REA.cachedata) {
	JcallShell.REA.cachedata = {
		commonDataKey: "cachedata",
		/***
		 * 获取某一window的最顶级的父窗体对象
		 * @param {Object} curWin:当前window对象
		 */
		getTop: function(curWin) {
			var me = this;
			curWin = curWin || window;
			var win = curWin.top == curWin ? curWin : me.getTop(curWin.top);
			return win;
		},
		/***
		 * 设置父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey:CacheData的key
		 * @param {Object} data:需要缓存的数据信息
		 */
		setCache: function(dictKey, data) {
			var me = this;
			if (!dictKey) dictKey = me.commonDataKey;
			var win = me.getTop(window);
			if (!win.CacheData) win.CacheData = {};
			win.CacheData[dictKey] = data;
		},
		/***
		 * 获取父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey:CacheData的key
		 */
		getCache: function(dictKey) {
			var me = this;
			var data = "";
			if (!dictKey) dictKey = me.commonDataKey;
			var win = me.getTop();
			if (!win) return data;
			if (win.CacheData) {
				data = win.CacheData[dictKey];
			}
			return data;
		},
		/***
		 * 删除父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey
		 */
		delete: function(dictKey) {
			var me = this;
			var win = me.getTop();
			if (!win) return;
			if (win.CacheData) {
				if (dictKey) {
					delete win.CacheData[dictKey];
				} else {
					win.CacheData = {};
				}
			}
		}
	};
}
(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG) {
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();