/**
 * @description 新增智能采购明细
 * @author liuyj
 * @version 2020-12-15
 */
Ext.define('Shell.class.rea.client.apply.mind.DtCheckPanel', {
	extend: 'Shell.class.rea.client.apply.basic.DtCheckPanel',
	requires: [
		'Shell.ux.toolbar.Button'
	],
	
	title: '新增采购明细',
	header: true,
	width: 1028,
	height: 580,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	//border: false,
	bodyPadding: 1,
	/**当前的申请部门*/
	CurDeptId: null,
	/**申请单当前状态*/
	Status: null,
	/**已选的货品IDS*/
	GoodIdStr: null,
	/**当前登录者所属部门(包含子部门)下全部的供应商货品信息*/
	ReaGoodsCenOrgList: null,
	formtype: "add",
	layout: {
		type: 'border'
	},
	
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	
		me.ReaGoodsCheck.on({
			onBeforeSearch: function(grid) {
				var goodIdStr = "";
				//已选择货品列表
				me.ReqDtlCheck.store.each(function(record) {
					goodIdStr += record.get("ReaBmsReqDtl_GoodsID") + ",";
				});
				var defaultWhere1 = " readeptgoods.ReaGoods.Visible=1 and readeptgoods.DeptID=" + me.CurDeptId;
				if(goodIdStr) {
					goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
					defaultWhere1 += " and readeptgoods.ReaGoods.Id not in(" + goodIdStr + ")";
				}
				me.ReaGoodsCheck.externalWhere = defaultWhere1;
			},
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.onAddRecord(grid, [record], function(record) {});
			},
			accept: function(grid, records) {
				me.onAddRecord(grid, records, function(record) {});
			}
		});
		me.ReqDtlCheck.on({
			onDelAfter: function(grid) {
				me.ReaGoodsCheck.onSearch();
			},
			onCheck: function() {
				me.onCheckClick();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var defaultWhere1 = "";
		var defaultWhere2 = "";
		//待选择列表
		if(me.CurDeptId) defaultWhere1 = " readeptgoods.ReaGoods.Visible=1 and readeptgoods.DeptID=" + me.CurDeptId;
		if(me.GoodIdStr && defaultWhere1) defaultWhere1 += " and ";
		if(me.GoodIdStr) defaultWhere1 += "readeptgoods.ReaGoods.Id not in(" + me.GoodIdStr + ")";
	
		me.ReaGoodsCheck = Ext.create('Shell.class.rea.client.apply.basic.ReaGoodsCheck', {
			header: true,
			itemId: 'ReaGoodsCheck',
			region: 'west',
			width: 425,
			split: true,
			collapsible: false,
			collapsed: false,
			CurDeptId:me.CurDeptId,
			defaultWhere: defaultWhere1
		});
		//已选列表
		if(me.PK) defaultWhere2 = "reabmsreqdtl.ReaBmsReqDoc.Id=" + me.PK;
		me.ReqDtlCheck = Ext.create('Shell.class.rea.client.apply.mind.ReqDtlCheck', {
			header: true,
			itemId: 'ReqDtlCheck',
			region: 'center',
			collapsible: false,
			collapsed: false,
			Status: me.Status,
			formtype: me.formtype,
			CurDeptId: me.CurDeptId,
			ReaGoodsCenOrgList: me.ReaGoodsCenOrgList,
			defaultWhere: defaultWhere2,
			defaultLoad: false
		});
		var appInfos = [me.ReaGoodsCheck, me.ReqDtlCheck];
		return appInfos;
	},
	/***
	 * @description 添加选择的货品明细到已选列表
	 * @param {Object} grid
	 * @param {Object} records
	 * @param {Object} callback
	 */
	onAddRecord: function(grid, records, callback) {
		var me = this;
		if(!records || records.length < 1) return;
	
		var removeArr = [];
		for(var i in records) {
			var record = records[i];
			var goodId = record.get("ReaDeptGoods_ReaGoods_Id");
			var record2 = null;
	
			if(me.ReqDtlCheck.store.getCount() > 0) {
				record2 = me.ReqDtlCheck.store.findRecord('ReaBmsReqDtl_GoodsID', goodId);
			}
	
			if(record2) {
				//已存在已选列表里
				me.ReaGoodsCheck.store.remove(record);
				//removeArr.push(record);
				if(callback) callback(record, "");
			} else {
				//货品的默认供应商赋值处理
				var defaultOrg = null;
				if(!me.ReaGoodsCenOrgList || me.ReaGoodsCenOrgList.length == 0) me.ReaGoodsCenOrgList = me.ReqDtlCheck.ReaGoodsCenOrgList;
				if(!me.ReaGoodsCenOrgList || me.ReaGoodsCenOrgList.length == 0) {
					me.ReqDtlCheck.getReaGoodsCenOrgList(true);
					me.ReaGoodsCenOrgList = me.ReqDtlCheck.ReaGoodsCenOrgList;
				}
	
				if(me.ReaGoodsCenOrgList || me.ReaGoodsCenOrgList.length >= 0) {
					for(var i = 0; i < me.ReaGoodsCenOrgList.length; i++) {
						var item = me.ReaGoodsCenOrgList[i];
						if(goodId == item.GoodsId) {
							var dataOrgList = item.ReaCenOrgVOList;
							if(dataOrgList) defaultOrg = dataOrgList[0];
							break;
						}
					}
				}
				//货品名称=货品名称+包装规格
				var goodsCName = record.get("ReaDeptGoods_ReaGoods_CName");
				//var unitMemo = record.get("ReaDeptGoods_ReaGoods_UnitMemo");
				//if(unitMemo) goodsCName = goodsCName + "(" + unitMemo + ")";
				var monthlyUsage = record.get("ReaDeptGoods_ReaGoods_MonthlyUsage");
				if(!monthlyUsage) monthlyUsage = 0;
				var addRecord = {
					"ReaBmsReqDtl_Id": "-1",
					"ReaBmsReqDtl_ReqGoodsQty": 0,
					"ReaBmsReqDtl_GoodsQty": 0,
					"ReaBmsReqDtl_ExpectedStock": 0,
					"ReaBmsReqDtl_GoodsID": goodId,
					"ReaBmsReqDtl_GoodsCName": goodsCName,
					"ReaBmsReqDtl_ReaGoodsNo": record.get("ReaDeptGoods_ReaGoods_ReaGoodsNo"),
					"ReaBmsReqDtl_GoodsUnit": record.get("ReaDeptGoods_ReaGoods_UnitName"),
					"ReaBmsReqDtl_UnitMemo": record.get("ReaDeptGoods_ReaGoods_UnitMemo"),
					"ReaBmsReqDtl_ProdOrgName": record.get("ReaDeptGoods_ReaGoods_ProdOrgName"),
					"ReaBmsReqDtl_CurrentQty": record.get("ReaDeptGoods_CurrentQtyVO_GoodsQty"),
					"ReaBmsReqDtl_DispOrder":record.get("ReaDeptGoods_ReaGoods_DispOrder"),
					"GoodsOtherQty": record.get("GoodsOtherQty"),
					"SuitableType": record.get("ReaDeptGoods_ReaGoods_SuitableType"),
					"ReaBmsReqDtl_MonthlyUsage": monthlyUsage
	
				};
				
				//智能采购调取服务计算平均使用量和建议采购量
				var url = "/ReaManageService.svc/ST_UDTO_CalcAvgUsedAndSuggestPurchaseQty?goodsId=" + goodId;
				url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
				JShell.Server.get(url, function(data) {
					if(data.success) {
						//var list = (data.data || {}).list || [];
						var list = data.value;
						addRecord["ReaBmsReqDtl_AvgUsedQty"] = list.AvgUsedQty;
						addRecord["ReaBmsReqDtl_SuggestPurchaseQty"] = list.SuggestPurchaseQty;
						addRecord["ReaBmsReqDtl_ReqGoodsQty"] = list.SuggestPurchaseQty;
						addRecord["ReaBmsReqDtl_GoodsQty"] = list.SuggestPurchaseQty;
					} else {
						JShell.Msg.error(data.msg);
					}
				},false);
				
				
				
				
				if(defaultOrg) {
					addRecord["ReaBmsReqDtl_CompGoodsLinkID"] = defaultOrg.Id;
					addRecord["ReaBmsReqDtl_ReaCenOrg_Id"] = defaultOrg.CenOrgId;
					addRecord["ReaBmsReqDtl_OrgName"] = defaultOrg.CenOrgCName;
					addRecord["ReaBmsReqDtl_CenOrgGoodsNo"] = defaultOrg.CenOrgGoodsNo;
					var Price = defaultOrg.Price;
					if(!Price) Price = 0;
					Price = parseFloat(Price);
					var SumTotal = 0;
					addRecord['ReaBmsReqDtl_Price'] = Price;
					addRecord['ReaBmsReqDtl_SumTotal'] = SumTotal;
				}
				var prodID = record.get("ReaDeptGoods_ReaGoods_ProdID");
				if(prodID) {
					addRecord["ReaBmsReqDtl_ProdID"] = prodID;
				}
				
				me.ReqDtlCheck.store.add(addRecord);
				me.ReaGoodsCheck.store.remove(record);
				//removeArr.push(record);
				if(callback) callback(record, "");
			}
		}
	}
	
	
});