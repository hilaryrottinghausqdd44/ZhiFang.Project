/**
 * @description 部门采购申请录入申请明细列表
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyDtGrid',

	title: '申请明细信息',
	width: 800,
	height: 500,
	
	//选择模板服务
	selectModelUrl:'/ReaManageService.svc/RS_UDTO_SearchGoodsByDeptIdAndTemplateIdByHQL?isPlanish=true',

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**新增明细或删除明细按钮的启用状态*/
	buttonsDisabled: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否启用同步库存按钮*/
	hasSyncQty: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**当前的申请部门*/
	CurDeptId: null,
	
	defaultOrderBy: [{
		property: 'ReaBmsReqDtl_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onSaveAllDt', 'onAddAfter', 'onDelAfter', 'onEditAfter');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [
		{
			dataIndex: 'ReaBmsReqDtl_GoodsSName',
			text: '简称',
			width: 85,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序字段
				me.store.sort({
					property: "ReaGoods_SName",
					direction: state
				});
			}
		},{
			dataIndex: 'ReaBmsReqDtl_GoodsEName',
			text: '英文名称',
			width: 85,
			hidden:true,
			defaultRenderer: true,
			doSort: function(state) {
				//自定义排序字段
				me.store.sort({
					property: "ReaGoods_EName",
					direction: state
				});
			}
		},{
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
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			iconCls: 'button-add',
			itemId: "btnAdd",
			disabled: true,
			text: '新增明细',
			tooltip: '新增货品明细',
			handler: function() {
				me.onAddDtClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-del',
			itemId: "btnDel",
			disabled: true,
			hidden: true,
			text: '删除明细',
			tooltip: '删除勾选中的明细行',
			handler: function() {
				me.onDeleteDtClick();
			}
		});
		items.push('-', {
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 180,
			name: 'cboGoodstemplate',
			itemId: 'cboGoodstemplate',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 385,
				height: 460,
				checkOne: true,
				TemplateType: "ReaReqDtl"
				//defaultWhere: " reachoosegoodstemplate.SName='ReaReqDtl'"
			},
			className: 'Shell.class.rea.client.goodstemplate.CheckGrid',
			listeners: {
				beforetriggerclick: function(p) {
					//if(!p.classConfig || !p.classConfig.DeptID) me.setGoodstemplateClassConfig();
					if(!p.classConfig || !p.classConfig.DeptID) {
						JShell.Msg.warning('获取部门信息为空,请选择部门后再操作!');
						return false;
					}
					me.setGoodstemplateClassConfig();
				},
				check: function(p, record) {
					p.close();
					me.onAddRecords(record);
				}
			}
		}, {
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnSaveTemplate",
			text: '保存模板',
			tooltip: '保存模板',
			handler: function() {
				me.onSaveTemplate();
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 160,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名',
			fields: ['reabmsreqdtl.GoodsCName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		if(me.hasSyncQty == true) {
			items.push('-', {
				xtype: 'button',
				iconCls: 'button-search',
				text: '同步库存',
				tooltip: '同步库存数',
				hidden: true,
				handler: function() {
					me.onCurrentQtyClick();
				}
			});
		}
		return items;
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.setButtonsDisabled(me.buttonsDisabled);
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;
		me.setBtnDisabled("btnAdd", disabled);
		me.setBtnDisabled("btnDel", disabled);
		me.setBtnDisabled("btnSave", disabled);
	},
	setBtnDisabled: function(itemId, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(itemId);
			if(btn) btn.setDisabled(disabled);
		}
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
		var win = JShell.Win.open('Shell.class.rea.client.apply.basic.DtCheckPanel', config);
		win.show();
		if(dtData.length > 0)
			win.loadDtData(dtData);
	},
	/**@description 新增明细选择后处理方法*/
	onCheckSaveDt: function(p, saveData) {
		var me = this;
		//如果是全新的新增
		switch(me.formtype) {
			case "add":
				me.store.removeAll();
				if(saveData)
					me.store.loadData(saveData);
				p.close();
				me.fireEvent('onAddAfter', me);
				break;
			default:
				if(saveData) me.onSaveOfUpdate(saveData, function(data) {
					if(data.success) {
						JShell.Msg.alert('新增货品明细保存数据成功', null, 1000);
						p.close();
						me.onSearch();
						me.fireEvent('onAddAfter', me);
					} else {
						JShell.Msg.error('新增货品明细保存数据失败！' + data.msg);
					}
				});
				break;
		}
	},
	/**@description 新增按钮点击处理方法*/
	onAddDtClick: function() {
		var me = this;
		if(me.CurDeptId) {
			me.showDtGridCheck();
		} else {
			JShell.Msg.alert("请选择申请部门后再操作!", null, 1000);
		}
	},
	/**@description 明细保存按钮点击处理方法*/
	onSaveDtClick: function() {
		var me = this;
		var isBreak = false;
		var msgInfo = "";
		me.store.each(function(record) {
			var goodsQty = record.get("ReaBmsReqDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			goodsQty = parseFloat(goodsQty);
			if(goodsQty <= 0) {
				isBreak = true;
				var goodName = record.get("ReaBmsReqDtl_GoodsCName");
				msgInfo = "当前货品名为" + goodName + ",其申请数量<=0!不能保存!";
				return false;
			}
		});
		if(isBreak == true) {
			JShell.Msg.error(msgInfo);
			return;
		}

		var result = me.getDtSaveParams();
		me.onSaveOfUpdate(result, function(data) {
			if(data.success) {
				JShell.Msg.alert('新增货品明细保存数据成功', null, 1000);
				me.fireEvent('onSaveAllDt', me, true);
			} else {
				JShell.Msg.error('新增货品明细保存数据失败！' + data.msg);
				me.fireEvent('onSaveAllDt', me, true);
			}
		});
	},
	/**订单选择改变或部门选择改变后更新模板选择的配置项*/
	setGoodstemplateClassConfig: function(isSearch) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
			if(cbo) {
				cbo.setValue("");
				cbo.changeClassConfig({
					"DeptID": me.CurDeptId
				});
				var picker=cbo.getPicker();
				if(picker){
					picker.DeptID=me.CurDeptId;
					if(isSearch==true)picker.onSearch();
				}
			}
		}
	},
	/**@description 模板保存*/
	onSaveTemplate: function() {
		var me = this;
		if(me.store.getCount() <= 0) {
			JShell.Msg.error('当前选择的货品明细为空!');
			return;
		}
		if(!me.CurDeptId) {
			JShell.Msg.error('获取部门信息为空!');
			return;
		}
		var tempArr = [];
		me.store.each(function(record) {
			var data = {};
			data = Ext.apply(data, record.data);
			data["ReaBmsReqDtl_Id"] = "-1";
			data["ReaBmsReqDtl_CurrentQty"] = "";
			data["CurrentQty"] = "";
			data["GoodsOtherQty"] = "";
			tempArr.push(data);
		});
		var config = {
			resizable: true,
			formtype: "add",
			DeptID: me.CurDeptId,
			DeptName: me.CurDeptName,
			TemplateType: "ReaReqDtl",
			ContextJson: JcallShell.JSON.encode(tempArr),
			listeners: {
				save: function(p, id) {
					var buttonsToolbar = me.getComponent("buttonsToolbar");
					var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
					if(cbo) cbo.getPicker().onSearch();
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodstemplate.ApplyForm', config);
		win.show();
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
		
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=ReaDeptGoods_ReaGoods_Id,ReaDeptGoods_ReaGoods_Price,ReaDeptGoods_ReaGoods_GoodsQty,ReaDeptGoods_Id,ReaDeptGoods_ReaGoods_ReaGoodsNo,ReaDeptGoods_ReaGoods_GoodsUnitID,ReaDeptGoods_ReaGoods_CName,ReaDeptGoods_ReaGoods_EName,ReaDeptGoods_ReaGoods_DispOrder,ReaDeptGoods_ReaGoods_UnitName,ReaDeptGoods_ReaGoods_UnitMemo,ReaDeptGoods_CurrentQtyVO_GoodsQty,ReaDeptGoods_ReaGoods_ProdOrgName,ReaDeptGoods_ReaGoods_MonthlyUsage,ReaDeptGoods_ReaGoods_ReaCompanyName,ReaDeptGoods_ReaGoods_SuitableType,ReaDeptGoods_ReaGoods_ShortCode,ReaDeptGoods_ReaGoods_ProdID' ;
		
		if(me.CurDeptId){
			url+='&deptId='+JShell.String.encode(me.CurDeptId);
		}
		
		if(templateId){
			url+='&templateId='+JShell.String.encode(templateId);
		}
		url+='&isCalcQty=false';
		return url;
	}
});