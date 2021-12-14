/**
 * @description 部门智能采购申请录入申请明细列表
 * @author liuyj
 * @version 2020-12-15
 */
Ext.define('Shell.class.rea.client.apply.mind.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ReqDtlGrid',

	title: '申请明细信息',
	width: 800,
	height: 500,
	
	//查询服务
	selectUrl:'/ReaManageService.svc/ST_UDTO_SearchReaBmsReqDtlByHQL?isPlanish=true&isCalcQty=true',
	//选择模板服务
	selectModelUrl:'/ReaManageService.svc/RS_UDTO_SearchGoodsByDeptIdAndTemplateIdByHQL?isPlanish=true',
	
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	defaultOrderBy: [{
		property: 'ReaBmsReqDtl_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'apply.mind.ReqDtlGrid',
	/**用户UI配置Name*/
	userUIName: "采购申请明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('onSaveAllDt');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsReqDtl_ReaGoodsNo',
			text: '货品编码',
			width: 85,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsID',
			text: '货品Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_CompGoodsLinkID',
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'ReaBmsReqDtl_DispOrder',
			text: '显示次序',
			width: 65,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsReqDtl_GoodsCName',
			text: '货品名称',
			width: 150,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ReaCenOrg_Id',
			text: '供应商Id',
			width: 135,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_MonthlyUsage',
			text: '理论月用量',
			width: 75,
			sortable: false,
			defaultRenderer: true
		}];
	
		columns.push({
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>删除</b>"';
					if(me.formtype === "show")
						return "";
					else
						return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.delRecords([rec]);
				}
			}]
		});
		columns.push(me.createReaCenOrgColumn());
		columns.push({
			dataIndex: 'ReaBmsReqDtl_ArrivalTime',
			text: '<b style="color:blue;">到货时间</b>',
			width: 85,
			sortable: false,
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d',
				listeners: {
					focus: function(com, e, eOpts) {
						me.comSetReadOnly(com, e);
					}
				}
			}
		});
		columns.push({
			dataIndex: 'ReaBmsReqDtl_AvgUsedQty',
			text: '<b style="color:blue;">平均使用量</b>',
			width: 70,
			sortable: false
		},{
			dataIndex: 'ReaBmsReqDtl_SuggestPurchaseQty',
			text: '<b style="color:blue;">建议采购量</b>',
			width: 70,
			sortable: false
		});
		columns.push(me.createReqGoodsQtyColumn());
		columns.push(me.createGoodsQtyColumn());
		columns.push(me.createCurrentQtyColumn(), {
			dataIndex: 'ReaBmsReqDtl_ExpectedStock',
			text: '预期库存量',
			hidden: true,
			width: 75,
			sortable: false,
			defaultRenderer: true
		});
		columns.push({
			dataIndex: 'ReaBmsReqDtl_Price',
			text: '供货商价格',
			hidden: true,
			width: 75,
			sortable: false,
			type: 'float',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsReqDtl_SumTotal',
			text: '合计金额',
			hidden: true,
			width: 75,
			sortable: false,
			type: 'float',
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		});
	
		columns.push({
			dataIndex: 'ReaBmsReqDtl_GoodsUnitID',
			text: '包装单位Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsUnit',
			text: '包装单位',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_UnitMemo',
			text: '规格',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ProdID',
			text: '厂家ID',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ProdOrgName',
			text: '品牌',
			width: 75,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_CenOrgGoodsNo',
			text: '供应商货品编码',
			hidden: true,
			width: 105,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'GoodsOtherQty',
			text: '货品对应同系列的库存数量',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_Memo',
			sortable: false,
			text: '<b style="color:blue;">备注</b>',
			width: 80,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		});
		return columns;
	},
	
	/**@description 显示新增货品明细*/
	showDtGridCheck: function() {
		var me = this;
		me.getReaGoodsCenOrgList(true);
		if(me.ReaGoodsCenOrgList == null || me.ReaGoodsCenOrgList.length == 0) {
			JShell.Msg.error('请在部门货品维护里维护好货品信息后再操作！');
			return;
		}
		var dtData = [];
		var goodIdStr = "";
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		me.store.each(function(record) {
			record.commit();
			dtData.push(record);
			goodIdStr += record.get("ReaBmsReqDtl_GoodsID") + ",";
		});
		goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			Status: me.Status,
			CurDeptId: me.CurDeptId,
			GoodIdStr: goodIdStr,
			ReaGoodsCenOrgList: me.ReaGoodsCenOrgList,
			listeners: {
				close: function(p) {
					me.onSearch();
				},
				save: function(p, saveData) {
					me.onCheckSaveDt(p, saveData);
				}
			}
		};
		if(me.PK) {
			config.formtype = 'edit';
			config.PK = me.PK;
		} else {
			config.formtype = 'add';
		}
		var win = JShell.Win.open('Shell.class.rea.client.apply.mind.DtCheckPanel', config);
		win.show();
		if(dtData.length > 0)
			win.loadDtData(dtData);
	},
	/**明细模板选择后,申请明细列表的货品信息处理*/
	onAddRecords: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
		if(record) cbo.setValue(record.get("ReaChooseGoodsTemplate_CName"));
		else cbo.setValue("");
	
		if(!record) return;
	
		var contextJson = record.get("ReaChooseGoodsTemplate_ContextJson");
		var templateId=record.get("ReaChooseGoodsTemplate_Id");
		if(contextJson) contextJson = JcallShell.JSON.decode(contextJson);
		if(!contextJson){
			contextJson = record.get("ReaChooseGoodsTemplate_ContextJson");
			contextJson=contextJson.replace(/\\'/g, '’').replace(/\\"/g, '”');;
			try{
				contextJson =JcallShell.JSON.decode(contextJson);
			}catch (e) {
				contextJson ="";
			}
		}
		if(!contextJson) return;
		
		var url = me.getModelLoadUrl(templateId);
		
		if(Ext.typeOf(contextJson)!='undefined'){
			  JShell.Server.get(url, function(data) {
				if(data.success) {
					var list = data.value.list;
					for(var i=0;i<contextJson.length;i++){
						for(var j=0;j<list.length;j++){
							if(contextJson[i]["ReaBmsReqDtl_GoodsID"] == list[j]["ReaDeptGoods_ReaGoods_Id"]){
								contextJson[i]["ReaBmsReqDtl_CurrentQty"]=list[j]["ReaDeptGoods_CurrentQtyVO_GoodsQty"];
								contextJson[i]["ReaBmsReqDtl_DispOrder"]=list[j]["ReaDeptGoods_ReaGoods_DispOrder"];
								contextJson[i]["ReaBmsReqDtl_GoodsCName"]=list[j]["ReaDeptGoods_ReaGoods_CName"];
								contextJson[i]["ReaBmsReqDtl_GoodsUnit"]=list[j]["ReaDeptGoods_ReaGoods_UnitName"];
								contextJson[i]["ReaBmsReqDtl_GoodsUnitID"]=list[j]["ReaDeptGoods_ReaGoods_GoodsUnitID"];
								contextJson[i]["ReaBmsReqDtl_MonthlyUsage"]=list[j]["ReaDeptGoods_ReaGoods_MonthlyUsage"];
								contextJson[i]["ReaBmsReqDtl_OrgName"]=list[j]["ReaDeptGoods_ReaGoods_ReaCompanyName"];
								contextJson[i]["ReaBmsReqDtl_Price"]=list[j]["ReaDeptGoods_ReaGoods_Price"];
								contextJson[i]["ReaBmsReqDtl_ProdID"]=list[j]["ReaDeptGoods_ReaGoods_ProdID"];
								contextJson[i]["ReaBmsReqDtl_ProdOrgName"]=list[j]["ReaDeptGoods_ReaGoods_ProdOrgName"];
								contextJson[i]["ReaBmsReqDtl_ReaGoodsNo"]=list[j]["ReaDeptGoods_ReaGoods_ReaGoodsNo"];
								contextJson[i]["ReaBmsReqDtl_UnitMemo"]=list[j]["ReaDeptGoods_ReaGoods_UnitMemo"];
								contextJson[i]["ReaBmsReqDtl_AvgUsedQty"]=list[j]["ReaDeptGoods_ReaGoods_AvgUsedQty"];
								contextJson[i]["ReaBmsReqDtl_SuggestPurchaseQty"]=list[j]["ReaDeptGoods_ReaGoods_SuggestPurchaseQty"];
								
							}
						}
					}
					Ext.Array.each(contextJson, function(data) {
						var goodId = data["ReaBmsReqDtl_GoodsID"];
						var indexOf = -1;
						if(goodId) indexOf = me.store.findExact("ReaBmsReqDtl_GoodsID", goodId);
						//模板的货品不存在明细列表里
						if(goodId && indexOf == -1) {
							data["ReaBmsReqDtl_Id"] = "-1";
							data["CurrentQty"] = "";
							data["GoodsOtherQty"] = "";
							me.store.add(data);
						}
					});
				}else{
					JShell.Msg.error(data.msg);
				}
			  });
		}
		
	},
	
	/**获取带查询参数的URL*/
	getModelLoadUrl: function(templateId) {
		var me = this,
			arr = [];
		
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectModelUrl;
		
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=ReaDeptGoods_ReaGoods_AvgUsedQty,ReaDeptGoods_ReaGoods_SuggestPurchaseQty,ReaDeptGoods_ReaGoods_Id,ReaDeptGoods_ReaGoods_Price,ReaDeptGoods_ReaGoods_GoodsQty,ReaDeptGoods_Id,ReaDeptGoods_ReaGoods_ReaGoodsNo,ReaDeptGoods_ReaGoods_GoodsUnitID,ReaDeptGoods_ReaGoods_CName,ReaDeptGoods_ReaGoods_EName,ReaDeptGoods_ReaGoods_DispOrder,ReaDeptGoods_ReaGoods_UnitName,ReaDeptGoods_ReaGoods_UnitMemo,ReaDeptGoods_CurrentQtyVO_GoodsQty,ReaDeptGoods_ReaGoods_ProdOrgName,ReaDeptGoods_ReaGoods_MonthlyUsage,ReaDeptGoods_ReaGoods_ReaCompanyName,ReaDeptGoods_ReaGoods_SuitableType,ReaDeptGoods_ReaGoods_ShortCode,ReaDeptGoods_ReaGoods_ProdID' ;
		
		if(me.CurDeptId){
			url+='&deptId='+JShell.String.encode(me.CurDeptId);
		}
		
		if(templateId){
			url+='&templateId='+JShell.String.encode(templateId);
		}
		url+='&isCalcQty=true';
		return url;
	}
	
});