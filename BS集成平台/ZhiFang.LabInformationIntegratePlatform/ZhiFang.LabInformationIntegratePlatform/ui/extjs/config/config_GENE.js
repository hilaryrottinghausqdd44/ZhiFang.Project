/**
 * PKI参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};

JcallShell.System = JcallShell.System || {};
/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = '基因系统';
/**系统编号*/
JcallShell.System.CODE = 'GENE';
/**系统登录顶部图片*/
JcallShell.System.LoginTopImage = '/images/gene/LoginTop.png';
/**第三方库ADO项目名称*/
JcallShell.System.ADOName = 'rea2015ado';

/**系统登录后处理*/
JcallShell.System.onAfterLogin = function(callback) {
	//JcallShell.REA.System.init(callback);
};

JcallShell.REA = JcallShell.REA || {};

JcallShell.REA.Goods = {
	EXCEL: '试剂信息模版V3.2.xls'
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
			//console.log(me.CENORG_CODE);
			//LOG输出
			JcallShell.Msg.log(JcallShell.Date.toString(new Date(), false, true) + msg);
			if(Ext.typeOf(callback) == 'function') {
				var bo = msg ? false : true;
				callback(bo);
			}
		});
	}
};
/**系统业务状态初始化*/
JcallShell.REA.StatusList=JcallShell.REA.StatusList||{
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
		/**客户端申请总单状态*/
		ReaBmsReqDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**客户端订单状态*/
		ReaBmsOrderDocStatus: JShell.REA.StatusList.getBaseInfo(),
		/**订货单数据标志*/
		ReaBmsOrderDocIOFlag: JShell.REA.StatusList.getBaseInfo(),
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
		/**小组类型状态*/
        SectionType: JShell.REA.StatusList.getBaseInfo(),
        //基因
        GeneNRequestFormStatus: JShell.REA.StatusList.getBaseInfo(),
        //基因判定公式枚举
        JudgmentType: JShell.REA.StatusList.getBaseInfo(),
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
				"classnamespace": "ZhiFang.Entity.Gene"
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
		if(!tempStatus || !tempStatus.List||tempStatus.List.length<=0) isRefresh = true;
		if(tempStatus.List&&isRefresh != true) {
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
			if(data.success) {
				if(data.value) {
					if(data.value[0][classname].length > 0) {
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
					}
				}
			}
		}, false);
	}
};

JcallShell.GENE = JcallShell.GENE || {};
JcallShell.GENE.ClassDict = {
	//命名空间域名
	_classNameSpace:'ZhiFang.Entity.Gene',
	/** @public
	 * 初始化字典信息，支持单个字典，也支持多个字典
	 * @param {Object} className 类名
	 * @param {Object} callback 回调函数
	 * @example
	 * 	JcallShell.System.ClassDict.init(
	 * 		'PContractStatus',
	 * 		function(){
	 * 			//回调函数处理
	 * 		}
	 * 	);
	 * 	JcallShell.System.ClassDict.init(
	 * 		['PContractStatus','PTaskStatus'],
	 * 		function(){
	 * 			//回调函数处理
	 * 		});
	 */
	init:function(className,callback){
		var me = JcallShell.System.ClassDict;
		var type = Ext.typeOf(className);
		
		if(type == 'string'){
			className = [className];
		}
		
		var hasData = true;
			
		for(var i in className){
			className[i] = {classnamespace:this._classNameSpace,classname:className[i]};
			if(!me[className[i].classname]){
				hasData = false;
			}
		}
		
		if(hasData){
			if(Ext.typeOf(callback) == 'function'){
				callback();
			}
		}else{
			JcallShell.System.ClassDict.loadClassInfoList(className,callback);
		}
	},
	/** @public
	 * 根据字典内容ID获取字典内容
	 * @param {Object} className
	 * @param {Object} id
	 */
	getClassInfoById:function(className,id){
		return JcallShell.System.ClassDict.getClassInfoById(className,id);
	},
	/** @public
	 * 根据字典内容Name获取字典内容
	 * @param {Object} className
	 * @param {Object} name
	 */
	getClassInfoByName:function(className,name){
		return JcallShell.System.ClassDict.getClassInfoByName(className,name);
	}
};

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